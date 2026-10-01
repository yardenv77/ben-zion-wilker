namespace BenZionVilker
{
    partial class ProjectProfitabilityReportPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label_title = new System.Windows.Forms.Label();
            this.label_dateFrom = new System.Windows.Forms.Label();
            this.dateTimePicker_from = new System.Windows.Forms.DateTimePicker();
            this.label_dateTo = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.button_generate = new System.Windows.Forms.Button();
            this.panel_kpiRevenue = new BenZionVilker.RoundedPanel();
            this.label_kpiRevenueValue = new System.Windows.Forms.Label();
            this.label_kpiRevenueCaption = new System.Windows.Forms.Label();
            this.panel_kpiCost = new BenZionVilker.RoundedPanel();
            this.label_kpiCostValue = new System.Windows.Forms.Label();
            this.label_kpiCostCaption = new System.Windows.Forms.Label();
            this.panel_kpiProfit = new BenZionVilker.RoundedPanel();
            this.label_kpiProfitValue = new System.Windows.Forms.Label();
            this.label_kpiProfitCaption = new System.Windows.Forms.Label();
            this.dataGridView_report = new System.Windows.Forms.DataGridView();
            this.panel_chart = new System.Windows.Forms.Panel();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_report)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(280, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(440, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "דוח רווחיות פרויקטים";
            //
            // label_dateFrom
            //
            this.label_dateFrom.AutoSize = true;
            this.label_dateFrom.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dateFrom.Location = new System.Drawing.Point(820, 73);
            this.label_dateFrom.Name = "label_dateFrom";
            this.label_dateFrom.Size = new System.Drawing.Size(70, 17);
            this.label_dateFrom.TabIndex = 1;
            this.label_dateFrom.Text = "מתאריך";
            //
            // dateTimePicker_from
            //
            this.dateTimePicker_from.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dateTimePicker_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_from.Location = new System.Drawing.Point(550, 70);
            this.dateTimePicker_from.Name = "dateTimePicker_from";
            this.dateTimePicker_from.Size = new System.Drawing.Size(250, 25);
            this.dateTimePicker_from.TabIndex = 2;
            //
            // label_dateTo
            //
            this.label_dateTo.AutoSize = true;
            this.label_dateTo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dateTo.Location = new System.Drawing.Point(820, 113);
            this.label_dateTo.Name = "label_dateTo";
            this.label_dateTo.Size = new System.Drawing.Size(70, 17);
            this.label_dateTo.TabIndex = 3;
            this.label_dateTo.Text = "עד תאריך";
            //
            // dateTimePicker_to
            //
            this.dateTimePicker_to.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dateTimePicker_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_to.Location = new System.Drawing.Point(550, 110);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(250, 25);
            this.dateTimePicker_to.TabIndex = 4;
            //
            // label_project
            //
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_project.Location = new System.Drawing.Point(820, 153);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(48, 17);
            this.label_project.TabIndex = 5;
            this.label_project.Text = "פרויקט";
            //
            // comboBox_project
            //
            this.comboBox_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(550, 150);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.Size = new System.Drawing.Size(250, 25);
            this.comboBox_project.TabIndex = 6;
            //
            // button_generate
            //
            this.button_generate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_generate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_generate.Location = new System.Drawing.Point(550, 190);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(250, 42);
            this.button_generate.TabIndex = 7;
            this.button_generate.Text = "הפק דוח";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            //
            // KPI summary cards (course step 10.2/10.4 reference: colored rounded tiles for
            // a headline number). Populated in button_generate_Click; start empty/"--" so an
            // ungenerated report doesn't show stale or zeroed figures.
            //
            // panel_kpiRevenue
            //
            this.panel_kpiRevenue.Location = new System.Drawing.Point(670, 250);
            this.panel_kpiRevenue.Name = "panel_kpiRevenue";
            this.panel_kpiRevenue.Size = new System.Drawing.Size(280, 90);
            this.panel_kpiRevenue.TabIndex = 8;
            this.panel_kpiRevenue.Controls.Add(this.label_kpiRevenueCaption);
            this.panel_kpiRevenue.Controls.Add(this.label_kpiRevenueValue);
            //
            // label_kpiRevenueValue
            //
            this.label_kpiRevenueValue.AutoSize = true;
            this.label_kpiRevenueValue.Location = new System.Drawing.Point(16, 14);
            this.label_kpiRevenueValue.Name = "label_kpiRevenueValue";
            this.label_kpiRevenueValue.Size = new System.Drawing.Size(30, 32);
            this.label_kpiRevenueValue.TabIndex = 0;
            this.label_kpiRevenueValue.Text = "--";
            //
            // label_kpiRevenueCaption
            //
            this.label_kpiRevenueCaption.AutoSize = true;
            this.label_kpiRevenueCaption.Location = new System.Drawing.Point(16, 58);
            this.label_kpiRevenueCaption.Name = "label_kpiRevenueCaption";
            this.label_kpiRevenueCaption.Size = new System.Drawing.Size(70, 15);
            this.label_kpiRevenueCaption.TabIndex = 1;
            this.label_kpiRevenueCaption.Text = "סה\"כ הכנסה";
            //
            // panel_kpiCost
            //
            this.panel_kpiCost.Location = new System.Drawing.Point(360, 250);
            this.panel_kpiCost.Name = "panel_kpiCost";
            this.panel_kpiCost.Size = new System.Drawing.Size(280, 90);
            this.panel_kpiCost.TabIndex = 9;
            this.panel_kpiCost.Controls.Add(this.label_kpiCostCaption);
            this.panel_kpiCost.Controls.Add(this.label_kpiCostValue);
            //
            // label_kpiCostValue
            //
            this.label_kpiCostValue.AutoSize = true;
            this.label_kpiCostValue.Location = new System.Drawing.Point(16, 14);
            this.label_kpiCostValue.Name = "label_kpiCostValue";
            this.label_kpiCostValue.Size = new System.Drawing.Size(30, 32);
            this.label_kpiCostValue.TabIndex = 0;
            this.label_kpiCostValue.Text = "--";
            //
            // label_kpiCostCaption
            //
            this.label_kpiCostCaption.AutoSize = true;
            this.label_kpiCostCaption.Location = new System.Drawing.Point(16, 58);
            this.label_kpiCostCaption.Name = "label_kpiCostCaption";
            this.label_kpiCostCaption.Size = new System.Drawing.Size(90, 15);
            this.label_kpiCostCaption.TabIndex = 1;
            this.label_kpiCostCaption.Text = "סה\"כ עלות בפועל";
            //
            // panel_kpiProfit
            //
            this.panel_kpiProfit.Location = new System.Drawing.Point(50, 250);
            this.panel_kpiProfit.Name = "panel_kpiProfit";
            this.panel_kpiProfit.Size = new System.Drawing.Size(280, 90);
            this.panel_kpiProfit.TabIndex = 10;
            this.panel_kpiProfit.Controls.Add(this.label_kpiProfitCaption);
            this.panel_kpiProfit.Controls.Add(this.label_kpiProfitValue);
            //
            // label_kpiProfitValue
            //
            this.label_kpiProfitValue.AutoSize = true;
            this.label_kpiProfitValue.Location = new System.Drawing.Point(16, 14);
            this.label_kpiProfitValue.Name = "label_kpiProfitValue";
            this.label_kpiProfitValue.Size = new System.Drawing.Size(30, 32);
            this.label_kpiProfitValue.TabIndex = 0;
            this.label_kpiProfitValue.Text = "--";
            //
            // label_kpiProfitCaption
            //
            this.label_kpiProfitCaption.AutoSize = true;
            this.label_kpiProfitCaption.Location = new System.Drawing.Point(16, 58);
            this.label_kpiProfitCaption.Name = "label_kpiProfitCaption";
            this.label_kpiProfitCaption.Size = new System.Drawing.Size(100, 15);
            this.label_kpiProfitCaption.TabIndex = 1;
            this.label_kpiProfitCaption.Text = "רווח נטו כולל";
            //
            // dataGridView_report
            //
            this.dataGridView_report.AllowUserToAddRows = false;
            this.dataGridView_report.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_report.Location = new System.Drawing.Point(50, 360);
            this.dataGridView_report.Name = "dataGridView_report";
            this.dataGridView_report.ReadOnly = true;
            this.dataGridView_report.RowTemplate.Height = 24;
            this.dataGridView_report.Size = new System.Drawing.Size(900, 330);
            this.dataGridView_report.TabIndex = 11;
            //
            // panel_chart -- UC-05 "interactive dashboard with tables and charts"
            // (docs/00e-use-cases.md, Part 2). Hand-drawn column chart (see
            // panel_chart_Paint in the .cs file) comparing revenue vs. actual cost per
            // project, one cluster per row from sp_report_project_profitability -- the
            // same "identify unprofitable projects early" comparison the grid's numbers
            // already carry, just visual. Drawn with plain GDI+ rather than a charting
            // library: the one available NuGet port for .NET 8
            // (System.Windows.Forms.DataVisualization) throws a FileNotFoundException for
            // System.Data.SqlClient at runtime -- leftover .NET Framework-era
            // DbProviderFactories probing code that doesn't resolve on .NET 8 -- so a
            // hand-drawn chart is the more reliable choice here, not a stopgap.
            //
            this.panel_chart.Location = new System.Drawing.Point(50, 710);
            this.panel_chart.Name = "panel_chart";
            this.panel_chart.Size = new System.Drawing.Size(900, 280);
            this.panel_chart.TabIndex = 12;
            //
            // button_back
            //
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(445, 1020);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 13;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // ProjectProfitabilityReportPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.panel_chart);
            this.Controls.Add(this.dataGridView_report);
            this.Controls.Add(this.panel_kpiProfit);
            this.Controls.Add(this.panel_kpiCost);
            this.Controls.Add(this.panel_kpiRevenue);
            this.Controls.Add(this.button_generate);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.dateTimePicker_to);
            this.Controls.Add(this.label_dateTo);
            this.Controls.Add(this.dateTimePicker_from);
            this.Controls.Add(this.label_dateFrom);
            this.Controls.Add(this.label_title);
            this.Name = "ProjectProfitabilityReportPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 1090);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_report)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_dateFrom;
        private System.Windows.Forms.DateTimePicker dateTimePicker_from;
        private System.Windows.Forms.Label label_dateTo;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Button button_generate;
        private BenZionVilker.RoundedPanel panel_kpiRevenue;
        private System.Windows.Forms.Label label_kpiRevenueValue;
        private System.Windows.Forms.Label label_kpiRevenueCaption;
        private BenZionVilker.RoundedPanel panel_kpiCost;
        private System.Windows.Forms.Label label_kpiCostValue;
        private System.Windows.Forms.Label label_kpiCostCaption;
        private BenZionVilker.RoundedPanel panel_kpiProfit;
        private System.Windows.Forms.Label label_kpiProfitValue;
        private System.Windows.Forms.Label label_kpiProfitCaption;
        private System.Windows.Forms.DataGridView dataGridView_report;
        private System.Windows.Forms.Panel panel_chart;
        private System.Windows.Forms.Button button_back;
    }
}
