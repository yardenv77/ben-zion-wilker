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
    /// DataGridView למטה, בלי כפתורי שמירה/עדכון/מחיקה. אין מחלקת Entity לדוחות, הם לא ישות בתחום.
    /// שני חלקים, אותם מסננים:
    ///   1. רווחיות לפי פרויקט -- sp_report_project_profitability (UC-05 MSS שלבים 5-7)
    ///   2. תזרים מזומנים חודשי -- sp_report_monthly_cash_flow (UC-05 MSS שלב 7, "monthly cash flow trends")
    /// </summary>
    public partial class ProjectProfitabilityReportPanel : UserControl
    {
        private List<(string label, double revenue, double cost)> chartData = new List<(string label, double revenue, double cost)>();
        private List<(string month, double cashIn, double cashOut, double cumulative)> cashFlowData = new List<(string month, double cashIn, double cashOut, double cumulative)>();

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

            // A section header, not a field label: ApplyStandardPanelTheme sized it like one,
            // so restore AutoSize and right-align it with the grid's right edge below it.
            Theme.ApplySectionLabel(label_cashFlowTitle);
            label_cashFlowTitle.AutoSize = true;
            label_cashFlowTitle.Left = dataGridView_cashFlow.Right - label_cashFlowTitle.Width;
            // Five short columns: spread them across the full width, matching the chart below
            dataGridView_cashFlow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Theme.WrapInCard(dataGridView_cashFlow);
            panel_cashFlowChart.BackColor = Theme.Surface;
            panel_cashFlowChart.Paint += panel_cashFlowChart_Paint;
            Theme.WrapInCard(panel_cashFlowChart);

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

            DataTable dt = runReport("sp_report_project_profitability");
            if (dt == null) return;
            DataTable cashFlow = runReport("sp_report_monthly_cash_flow");
            if (cashFlow == null) return;
            showCashFlow(cashFlow);

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

        // Runs one of the two report procedures with the screen's filters. Returns null
        // (after telling the user) if the query fails, so they can just click "הפק דוח" again.
        private DataTable runReport(string procedureName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE " + procedureName + " @date_from, @date_to, @project_id";
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
                // execute_query doesn't swallow failures (see SQL_CON.cs); caught locally here
                // rather than through Main()'s retry-or-exit prompt.
                MessageBox.Show("שגיאה בהפקת הדוח: " + ex.Message, "שגיאה", MessageBoxButtons.OK);
                return null;
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
            return dt;
        }

        private void showCashFlow(DataTable cashFlow)
        {
            dataGridView_cashFlow.DataSource = cashFlow;
            foreach (string moneyColumn in new[] { "הכנסות", "הוצאות", "תזרים_נטו", "יתרה_מצטברת" })
            {
                if (dataGridView_cashFlow.Columns.Contains(moneyColumn))
                    dataGridView_cashFlow.Columns[moneyColumn].DefaultCellStyle.Format = "N2";
            }
            foreach (DataGridViewColumn col in dataGridView_cashFlow.Columns)
                col.HeaderText = col.Name.Replace('_', ' ');

            cashFlowData.Clear();
            foreach (DataRow row in cashFlow.Rows)
                cashFlowData.Add((row["חודש"].ToString(), Convert.ToDouble(row["הכנסות"]),
                    Convert.ToDouble(row["הוצאות"]), Convert.ToDouble(row["יתרה_מצטברת"])));
            panel_cashFlowChart.Invalidate();
        }

        // Hand-drawn monthly chart: cash in / cash out columns per month around a zero line,
        // and the cumulative balance as a line on the same scale (it can go below zero).
        private void panel_cashFlowChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle bounds = panel_cashFlowChart.ClientRectangle;

            if (cashFlowData.Count == 0)
            {
                TextRenderer.DrawText(g, "אין תנועות כספיות לטווח ולפרויקט שנבחרו", Theme.BodyFont, bounds, Theme.TextSecondary,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            Color balanceColor = Theme.CategoryFinance;
            int swatchX = bounds.Width - 150;
            (string text, Color color)[] legend = { ("הכנסות", Theme.BrandPrimary), ("הוצאות", Theme.WarningText), ("יתרה מצטברת", balanceColor) };
            for (int i = 0; i < legend.Length; i++)
            {
                TextRenderer.DrawText(g, legend[i].text, Theme.CaptionFont, new Rectangle(bounds.Width - 270, 8 + i * 20, 110, 18),
                    Theme.TextPrimary, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                using (SolidBrush b = new SolidBrush(legend[i].color))
                    g.FillRectangle(b, swatchX, 10 + i * 20, 14, 14);
            }

            const int topMargin = 72, bottomMargin = 34, sideMargin = 20;
            int plotHeight = bounds.Height - topMargin - bottomMargin;
            int plotWidth = bounds.Width - sideMargin * 2;

            double maxValue = 1, minValue = 0;
            foreach (var m in cashFlowData)
            {
                maxValue = Math.Max(maxValue, Math.Max(m.cashIn, Math.Max(m.cashOut, m.cumulative)));
                minValue = Math.Min(minValue, m.cumulative);
            }
            Func<double, int> yOf = v => topMargin + (int)((maxValue - v) / (maxValue - minValue) * plotHeight);
            int zeroY = yOf(0);

            using (Pen zeroPen = new Pen(Theme.Border))
                g.DrawLine(zeroPen, sideMargin, zeroY, bounds.Width - sideMargin, zeroY);

            int groupWidth = plotWidth / cashFlowData.Count;
            int barWidth = Math.Max(4, Math.Min(28, groupWidth / 3));
            const int gap = 4;
            Point[] balancePoints = new Point[cashFlowData.Count];

            using (SolidBrush inBrush = new SolidBrush(Theme.BrandPrimary))
            using (SolidBrush outBrush = new SolidBrush(Theme.WarningText))
            {
                for (int i = 0; i < cashFlowData.Count; i++)
                {
                    var m = cashFlowData[i];
                    int centerX = sideMargin + groupWidth * i + groupWidth / 2;

                    int inTop = yOf(m.cashIn), outTop = yOf(m.cashOut);
                    if (zeroY - inTop > 0) g.FillRectangle(inBrush, centerX - barWidth - gap / 2, inTop, barWidth, zeroY - inTop);
                    if (zeroY - outTop > 0) g.FillRectangle(outBrush, centerX + gap / 2, outTop, barWidth, zeroY - outTop);

                    balancePoints[i] = new Point(centerX, yOf(m.cumulative));
                    TextRenderer.DrawText(g, m.month, Theme.CaptionFont,
                        new Rectangle(centerX - groupWidth / 2, bounds.Height - bottomMargin + 8, groupWidth, 20),
                        Theme.TextSecondary, TextFormatFlags.HorizontalCenter);
                }
            }

            using (Pen balancePen = new Pen(balanceColor, 2.5f))
            using (SolidBrush dotBrush = new SolidBrush(balanceColor))
            {
                if (balancePoints.Length > 1) g.DrawLines(balancePen, balancePoints);
                foreach (Point p in balancePoints)
                    g.FillEllipse(dotBrush, p.X - 4, p.Y - 4, 8, 8);
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
