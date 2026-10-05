using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;

namespace BenZionVilker
{
    /// <summary>
    /// Writes images as a PDF file, one A4 portrait page per image -- used by UC-05.Extend
    /// "Export Report to PDF" (ProjectProfitabilityReportPanel). The report is exported as
    /// a picture of the screen rather than as PDF text: PDF libraries for .NET lay Hebrew out
    /// left-to-right (no bidi support), so a picture is the only way the export reads exactly
    /// like the screen. Hand-written PDF (catalog, pages, one JPEG image + one draw command
    /// per page) -- no external library needed.
    /// </summary>
    public static class PdfExport
    {
        private const float PageWidth = 595f, PageHeight = 842f, Margin = 24f; // A4 in points

        public static void SaveImagesAsPdf(List<Bitmap> pages, string path)
        {
            // Object numbers: 1 catalog, 2 page tree, then per page i: page, image, content stream
            int objectCount = 2 + pages.Count * 3;
            long[] offsets = new long[objectCount + 1];

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                void write(string s) { byte[] b = Encoding.ASCII.GetBytes(s); fs.Write(b, 0, b.Length); }
                void startObject(int n) { offsets[n] = fs.Position; write(n + " 0 obj\n"); }

                write("%PDF-1.4\n");
                fs.Write(new byte[] { (byte)'%', 0xE2, 0xE3, 0xCF, 0xD3, (byte)'\n' }, 0, 6); // marks the file as binary

                startObject(1);
                write("<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");

                StringBuilder kids = new StringBuilder();
                for (int i = 0; i < pages.Count; i++)
                    kids.Append(3 + i * 3).Append(" 0 R ");
                startObject(2);
                write("<< /Type /Pages /Kids [" + kids.ToString().Trim() + "] /Count " + pages.Count + " >>\nendobj\n");

                for (int i = 0; i < pages.Count; i++)
                {
                    int pageObj = 3 + i * 3, imageObj = pageObj + 1, contentObj = pageObj + 2;
                    Bitmap image = pages[i];
                    byte[] jpeg = toJpeg(image);

                    // Scale to fit inside the margins, keep the aspect ratio, centre horizontally, top-align
                    float scale = Math.Min((PageWidth - 2 * Margin) / image.Width, (PageHeight - 2 * Margin) / image.Height);
                    float w = image.Width * scale, h = image.Height * scale;
                    float x = (PageWidth - w) / 2, y = PageHeight - Margin - h;

                    startObject(pageObj);
                    write("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /XObject << /Im0 "
                        + imageObj + " 0 R >> >> /Contents " + contentObj + " 0 R >>\nendobj\n");

                    startObject(imageObj);
                    write("<< /Type /XObject /Subtype /Image /Width " + image.Width + " /Height " + image.Height
                        + " /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length " + jpeg.Length + " >>\nstream\n");
                    fs.Write(jpeg, 0, jpeg.Length);
                    write("\nendstream\nendobj\n");

                    string draw = string.Format(CultureInfo.InvariantCulture, "q {0:F2} 0 0 {1:F2} {2:F2} {3:F2} cm /Im0 Do Q", w, h, x, y);
                    startObject(contentObj);
                    write("<< /Length " + draw.Length + " >>\nstream\n" + draw + "\nendstream\nendobj\n");
                }

                long xref = fs.Position;
                write("xref\n0 " + (objectCount + 1) + "\n0000000000 65535 f \n");
                for (int n = 1; n <= objectCount; n++)
                    write(offsets[n].ToString("D10") + " 00000 n \n");
                write("trailer\n<< /Size " + (objectCount + 1) + " /Root 1 0 R >>\nstartxref\n" + xref + "\n%%EOF\n");
            }
        }

        private static byte[] toJpeg(Bitmap image)
        {
            ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(), c => c.FormatID == ImageFormat.Jpeg.Guid);
            using (EncoderParameters quality = new EncoderParameters(1))
            using (MemoryStream ms = new MemoryStream())
            {
                quality.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 92L);
                image.Save(ms, jpegCodec, quality);
                return ms.ToArray();
            }
        }
    }
}
