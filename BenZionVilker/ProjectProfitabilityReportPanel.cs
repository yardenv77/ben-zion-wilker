using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    /// <summary>
    /// UC-05 (דוח רווחיות ותזרים מזומנים) -- מסך קריאה בלבד (שלב 8.3): מסננים למעלה,
    /// DataGridView למטה, בלי כפתורי שמירה/עדכון/מחיקה. קורא ישירות ל-
    /// sp_report_project_profitability -- אין מחלקת Entity לדוחות, הם לא ישות בתחום.
    /// היקף ראשוני בלבד (שלב 8.1): סיכום רווחיות לפי פרויקט; פילוח חודשי של תזרים
    /// המזומנים (UC-05 MSS שלב 7) נדחה לשלב הבא, לפי בחירת "הדוח הפשוט ביותר" של הצוות.
    /// </summary>
    public partial class ProjectProfitabilityReportPanel : UserControl
    {
        private List<(string label, double revenue, double cost)> chartData = new List<(string label, double revenue, double cost)>();

        public ProjectProfitabilityReportPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);

            // KPI cards' own Labels are children of a RoundedPanel, not direct children of
            // this panel, so ApplyStandardPanelTheme's loop never reaches them -- styled
            // explicitly here instead. Revenue/cost are neutral (info/warning); profit's
            // color is set again in button_generate_Click once its sign is known.
            Theme.ApplyKpiCard(panel_kpiRevenue, label_kpiRevenueValue, label_kpiRevenueCaption, Theme.InfoBg, Theme.InfoText);
            Theme.ApplyKpiCard(panel_kpiCost, label_kpiCostValue, label_kpiCostCaption, Theme.WarningBg, Theme.WarningText);
            Theme.ApplyKpiCard(panel_kpiProfit, label_kpiProfitValue, label_kpiProfitCaption, Theme.InfoBg, Theme.InfoText);
            Theme.WrapInCard(dataGridView_report);

            panel_chart.BackColor = Theme.Surface;
            panel_chart.Paint += panel_chart_Paint;
            Theme.WrapInCard(panel_chart);

            comboBox_project.Items.Add("-- כל הפרויקטים --");
            foreach (Project p in Program.Projects)
                comboBox_project.Items.Add(p.getProjectId() + " - " + p.getName());
            comboBox_project.SelectedIndex = 0;

            dateTimePicker_from.Value = DateTime.Today.AddYears(-1);
            dateTimePicker_to.Value = DateTime.Today.AddYears(1);
        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            if (dateTimePicker_from.Value.Date > dateTimePicker_to.Value.Date)
            {
                MessageBox.Show("תאריך ההתחלה חייב להיות לפני תאריך הסיום", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_report_project_profitability @date_from, @date_to, @project_id";
            cmd.Parameters.AddWithValue("@date_from", dateTimePicker_from.Value.Date);
            cmd.Parameters.AddWithValue("@date_to", dateTimePicker_to.Value.Date);

            if (comboBox_project.SelectedIndex <= 0)
                cmd.Parameters.AddWithValue("@project_id", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@project_id", resolveSelectedProjectId());

            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr;
            try
            {
                rdr = SC.execute_query(cmd);
            }
            catch (Exception ex)
            {
                // execute_query no longer swallows failures (see SQL_CON.cs) -- this is the
                // one caller of it outside app startup, so it's caught locally: the user can
                // just click "הפק דוח" again, no need for Main()'s retry-or-exit prompt.
                MessageBox.Show("שגיאה בהפקת הדוח: " + ex.Message, "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DataTable dt = new DataTable();
            for (int i = 0; i < rdr.FieldCount; i++)
                dt.Columns.Add(rdr.GetName(i), typeof(object));

            while (rdr.Read())
            {
                object[] values = new object[rdr.FieldCount];
                rdr.GetValues(values);
                dt.Rows.Add(values);
            }
            rdr.Close();

            dataGridView_report.DataSource = dt;
            foreach (string moneyColumn in new[] { "הכנסה", "עלות_בפועל", "רווח_נטו", "אחוז_רווחיות" })
            {
                if (dataGridView_report.Columns.Contains(moneyColumn))
                    dataGridView_report.Columns[moneyColumn].DefaultCellStyle.Format = "N2";
            }

            chartData.Clear();

            if (dt.Rows.Count == 0)
            {
                label_kpiRevenueValue.Text = "--";
                label_kpiCostValue.Text = "--";
                label_kpiProfitValue.Text = "--";
                Theme.ApplyKpiCard(panel_kpiProfit, label_kpiProfitValue, label_kpiProfitCaption, Theme.InfoBg, Theme.InfoText);
                panel_chart.Invalidate();
                MessageBox.Show("אין נתונים פיננסיים לטווח ולפרויקט שנבחרו", "הודעה", MessageBoxButtons.OK);
                return;
            }

            decimal totalRevenue = 0, totalCost = 0, totalProfit = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalRevenue += Convert.ToDecimal(row["הכנסה"]);
                totalCost += Convert.ToDecimal(row["עלות_בפועל"]);
                totalProfit += Convert.ToDecimal(row["רווח_נטו"]);

                chartData.Add((row["שם_פרויקט"].ToString(), Convert.ToDouble(row["הכנסה"]), Convert.ToDouble(row["עלות_בפועל"])));
            }
            panel_chart.Invalidate();

            label_kpiRevenueValue.Text = totalRevenue.ToString("N0") + " ₪";
            label_kpiCostValue.Text = totalCost.ToString("N0") + " ₪";
            label_kpiProfitValue.Text = totalProfit.ToString("N0") + " ₪";

            // Profit's card color carries meaning (loss vs. gain), reusing the same
            // success/danger pair the status badges use elsewhere in the app.
            Color profitBg = totalProfit >= 0 ? Theme.SuccessBg : Theme.DangerBg;
            Color profitText = totalProfit >= 0 ? Theme.SuccessText : Theme.DangerText;
            Theme.ApplyKpiCard(panel_kpiProfit, label_kpiProfitValue, label_kpiProfitCaption, profitBg, profitText);
        }

        // Hand-drawn grouped column chart -- revenue vs. actual cost per project. See the
        // Designer.cs comment on panel_chart for why this isn't a charting-library control.
        private void panel_chart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle bounds = panel_chart.ClientRectangle;

            if (chartData.Count == 0)
            {
                TextRenderer.DrawText(g, "אין נתונים להצגה -- הפיקו דוח", Theme.BodyFont, bounds, Theme.TextSecondary,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            // Legend -- labels are right-aligned within a fixed-width rectangle ending just
            // before the swatch, so "עלות בפועל" (longer than "הכנסה") can't run into its box.
            int swatchX = bounds.Width - 150;
            Rectangle revenueLabelRect = new Rectangle(bounds.Width - 270, 10, 110, 18);
            Rectangle costLabelRect = new Rectangle(bounds.Width - 270, 32, 110, 18);
            using (SolidBrush revenueBrush = new SolidBrush(Theme.BrandPrimary))
            using (SolidBrush costBrush = new SolidBrush(Theme.WarningText))
            {
                TextRenderer.DrawText(g, "הכנסה", Theme.CaptionFont, revenueLabelRect, Theme.TextPrimary,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                g.FillRectangle(revenueBrush, swatchX, 12, 14, 14);
                TextRenderer.DrawText(g, "עלות בפועל", Theme.CaptionFont, costLabelRect, Theme.TextPrimary,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                g.FillRectangle(costBrush, swatchX, 34, 14, 14);
            }

            const int topMargin = 50, bottomMargin = 80, sideMargin = 20;
            int plotHeight = bounds.Height - topMargin - bottomMargin;
            int plotWidth = bounds.Width - sideMargin * 2;
            int baselineY = topMargin + plotHeight;

            double maxValue = 1;
            foreach (var row in chartData)
                maxValue = Math.Max(maxValue, Math.Max(row.revenue, row.cost));

            using (Pen borderPen = new Pen(Theme.Border))
                g.DrawLine(borderPen, sideMargin, baselineY, bounds.Width - sideMargin, baselineY);

            int groupWidth = plotWidth / chartData.Count;
            int barWidth = Math.Min(40, groupWidth / 3);
            const int gap = 6;

            using (SolidBrush revenueBrush = new SolidBrush(Theme.BrandPrimary))
            using (SolidBrush costBrush = new SolidBrush(Theme.WarningText))
            {
                for (int i = 0; i < chartData.Count; i++)
                {
                    var row = chartData[i];
                    int groupCenterX = sideMargin + groupWidth * i + groupWidth / 2;

                    int revenueHeight = (int)(row.revenue / maxValue * plotHeight);
                    int costHeight = (int)(row.cost / maxValue * plotHeight);

                    int revenueX = groupCenterX - barWidth - gap / 2;
                    int costX = groupCenterX + gap / 2;

                    if (revenueHeight > 0) g.FillRectangle(revenueBrush, revenueX, baselineY - revenueHeight, barWidth, revenueHeight);
                    if (costHeight > 0) g.FillRectangle(costBrush, costX, baselineY - costHeight, barWidth, costHeight);

                    Rectangle labelRect = new Rectangle(groupCenterX - groupWidth / 2, baselineY + 6, groupWidth, bottomMargin - 10);
                    TextRenderer.DrawText(g, row.label, Theme.CaptionFont, labelRect, Theme.TextSecondary,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak);
                }
            }
        }

        private int resolveSelectedProjectId()
        {
            return int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}
