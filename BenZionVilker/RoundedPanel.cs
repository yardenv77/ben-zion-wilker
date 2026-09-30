using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BenZionVilker
{
    // A Panel with rounded corners -- WinForms has no built-in support for this (unlike the
    // reference dashboards' card style), so the control's Region is clipped to a rounded-
    // rectangle path. Painting and child controls both respect the clip, so the parent's own
    // background shows through at the cut corners -- for the "card floating on the page"
    // effect to look right, the parent must be Theme.Ground, which every panel already is
    // via Theme.ApplyPanelBackground.
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 12;

        // Surface (#FFFFFF) and Ground (#F5F5F3) are close enough in tone that a card with no
        // stroke at all is nearly invisible against the page background -- this default gives
        // every card a visible edge regardless of exactly which two colors it sits between.
        public Color BorderColor { get; set; } = Theme.Border;
        public int BorderThickness { get; set; } = 1;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateRegion();
            base.OnPaint(e);

            if (BorderThickness <= 0 || Width <= 0 || Height <= 0) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Inset by the pen width so the stroke lands entirely inside the clipped Region --
            // drawing exactly on the region's own edge would clip away half the pen's width.
            Rectangle inset = new Rectangle(0, 0, Width - BorderThickness, Height - BorderThickness);
            using (GraphicsPath path = RoundedRectPath(inset, CornerRadius))
            using (Pen pen = new Pen(BorderColor, BorderThickness))
                e.Graphics.DrawPath(pen, path);
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using (GraphicsPath path = RoundedRectPath(new Rectangle(0, 0, Width, Height), CornerRadius))
                this.Region = new Region(path);
        }

        private static GraphicsPath RoundedRectPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
