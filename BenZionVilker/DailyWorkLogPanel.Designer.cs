namespace BenZionVilker
{
    partial class DailyWorkLogPanel
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
            this.dataGridView_logs = new System.Windows.Forms.DataGridView();
            this.label_dailyWorkLogId = new System.Windows.Forms.Label();
            this.textBox_dailyWorkLogId = new System.Windows.Forms.TextBox();
            this.label_subcontractor = new System.Windows.Forms.Label();
            this.comboBox_subcontractor = new System.Windows.Forms.ComboBox();
            this.label_submittedBy = new System.Windows.Forms.Label();
            this.comboBox_submittedBy = new System.Windows.Forms.ComboBox();
            this.label_logDate = new System.Windows.Forms.Label();
            this.textBox_logDate = new System.Windows.Forms.TextBox();
            this.label_plannedQuantity = new System.Windows.Forms.Label();
            this.textBox_plannedQuantity = new System.Windows.Forms.TextBox();
            this.label_completedQuantity = new System.Windows.Forms.Label();
            this.textBox_completedQuantity = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logs)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(340, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(320, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "יומן עבודה יומי";
            //
            // dataGridView_logs
            //
            this.dataGridView_logs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_logs.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_logs.Name = "dataGridView_logs";
            this.dataGridView_logs.ReadOnly = true;
            this.dataGridView_logs.RowTemplate.Height = 24;
            this.dataGridView_logs.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_logs.TabIndex = 1;
            this.dataGridView_logs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_logs_CellClick);
            //
            // label_dailyWorkLogId
            //
            this.label_dailyWorkLogId.AutoSize = true;
            this.label_dailyWorkLogId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dailyWorkLogId.Location = new System.Drawing.Point(820, 260);
            this.label_dailyWorkLogId.Name = "label_dailyWorkLogId";
            this.label_dailyWorkLogId.Size = new System.Drawing.Size(70, 17);
            this.label_dailyWorkLogId.TabIndex = 2;
            this.label_dailyWorkLogId.Text = "מזהה יומן";
            //
            // textBox_dailyWorkLogId
            //
            this.textBox_dailyWorkLogId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_dailyWorkLogId.Location = new System.Drawing.Point(550, 257);
            this.textBox_dailyWorkLogId.Name = "textBox_dailyWorkLogId";
            this.textBox_dailyWorkLogId.ReadOnly = true;
            this.textBox_dailyWorkLogId.Size = new System.Drawing.Size(250, 25);
            this.textBox_dailyWorkLogId.TabIndex = 3;
            this.textBox_dailyWorkLogId.TabStop = false;
            //
            // label_subcontractor
            //
            this.label_subcontractor.AutoSize = true;
            this.label_subcontractor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_subcontractor.Location = new System.Drawing.Point(820, 295);
            this.label_subcontractor.Name = "label_subcontractor";
            this.label_subcontractor.Size = new System.Drawing.Size(72, 17);
            this.label_subcontractor.TabIndex = 4;
            this.label_subcontractor.Text = "קבלן משנה";
            //
            // comboBox_subcontractor
            //
            this.comboBox_subcontractor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_subcontractor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_subcontractor.FormattingEnabled = true;
            this.comboBox_subcontractor.Location = new System.Drawing.Point(550, 292);
            this.comboBox_subcontractor.Name = "comboBox_subcontractor";
            this.comboBox_subcontractor.Size = new System.Drawing.Size(250, 25);
            this.comboBox_subcontractor.TabIndex = 5;
            //
            // label_submittedBy
            //
            this.label_submittedBy.AutoSize = true;
            this.label_submittedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_submittedBy.Location = new System.Drawing.Point(820, 330);
            this.label_submittedBy.Name = "label_submittedBy";
            this.label_submittedBy.Size = new System.Drawing.Size(80, 17);
            this.label_submittedBy.TabIndex = 6;
            this.label_submittedBy.Text = "דווח על ידי";
            //
            // comboBox_submittedBy
            //
            this.comboBox_submittedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_submittedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_submittedBy.FormattingEnabled = true;
            this.comboBox_submittedBy.Location = new System.Drawing.Point(550, 327);
            this.comboBox_submittedBy.Name = "comboBox_submittedBy";
            this.comboBox_submittedBy.Size = new System.Drawing.Size(250, 25);
            this.comboBox_submittedBy.TabIndex = 7;
            //
            // label_logDate
            //
            this.label_logDate.AutoSize = true;
            this.label_logDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_logDate.Location = new System.Drawing.Point(820, 365);
            this.label_logDate.Name = "label_logDate";
            this.label_logDate.Size = new System.Drawing.Size(70, 17);
            this.label_logDate.TabIndex = 8;
            this.label_logDate.Text = "תאריך יומן";
            //
            // textBox_logDate
            //
            this.textBox_logDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_logDate.Location = new System.Drawing.Point(550, 362);
            this.textBox_logDate.Name = "textBox_logDate";
            this.textBox_logDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_logDate.TabIndex = 9;
            //
            // label_plannedQuantity
            //
            this.label_plannedQuantity.AutoSize = true;
            this.label_plannedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_plannedQuantity.Location = new System.Drawing.Point(820, 400);
            this.label_plannedQuantity.Name = "label_plannedQuantity";
            this.label_plannedQuantity.Size = new System.Drawing.Size(90, 17);
            this.label_plannedQuantity.TabIndex = 10;
            this.label_plannedQuantity.Text = "כמות מתוכננת";
            //
            // textBox_plannedQuantity
            //
            this.textBox_plannedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_plannedQuantity.Location = new System.Drawing.Point(550, 397);
            this.textBox_plannedQuantity.Name = "textBox_plannedQuantity";
            this.textBox_plannedQuantity.Size = new System.Drawing.Size(250, 25);
            this.textBox_plannedQuantity.TabIndex = 11;
            //
            // label_completedQuantity
            //
            this.label_completedQuantity.AutoSize = true;
            this.label_completedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_completedQuantity.Location = new System.Drawing.Point(820, 435);
            this.label_completedQuantity.Name = "label_completedQuantity";
            this.label_completedQuantity.Size = new System.Drawing.Size(80, 17);
            this.label_completedQuantity.TabIndex = 12;
            this.label_completedQuantity.Text = "כמות שבוצעה";
            //
            // textBox_completedQuantity
            //
            this.textBox_completedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_completedQuantity.Location = new System.Drawing.Point(550, 432);
            this.textBox_completedQuantity.Name = "textBox_completedQuantity";
            this.textBox_completedQuantity.Size = new System.Drawing.Size(250, 25);
            this.textBox_completedQuantity.TabIndex = 13;
            //
            // label_status
            //
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_status.Location = new System.Drawing.Point(820, 470);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(48, 17);
            this.label_status.TabIndex = 14;
            this.label_status.Text = "סטטוס";
            //
            // comboBox_status
            //
            this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Location = new System.Drawing.Point(550, 467);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(250, 25);
            this.comboBox_status.TabIndex = 15;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 16;
            this.button_save.Text = "שמירה";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            //
            // button_update
            //
            this.button_update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_update.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_update.Location = new System.Drawing.Point(610, 560);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(110, 42);
            this.button_update.TabIndex = 17;
            this.button_update.Text = "עדכון";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            //
            // button_delete
            //
            this.button_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_delete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_delete.Location = new System.Drawing.Point(470, 560);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 42);
            this.button_delete.TabIndex = 18;
            this.button_delete.Text = "מחיקה";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            //
            // button_back
            //
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(330, 560);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 19;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // DailyWorkLogPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_completedQuantity);
            this.Controls.Add(this.label_completedQuantity);
            this.Controls.Add(this.textBox_plannedQuantity);
            this.Controls.Add(this.label_plannedQuantity);
            this.Controls.Add(this.textBox_logDate);
            this.Controls.Add(this.label_logDate);
            this.Controls.Add(this.comboBox_submittedBy);
            this.Controls.Add(this.label_submittedBy);
            this.Controls.Add(this.comboBox_subcontractor);
            this.Controls.Add(this.label_subcontractor);
            this.Controls.Add(this.textBox_dailyWorkLogId);
            this.Controls.Add(this.label_dailyWorkLogId);
            this.Controls.Add(this.dataGridView_logs);
            this.Controls.Add(this.label_title);
            this.Name = "DailyWorkLogPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_logs;
        private System.Windows.Forms.Label label_dailyWorkLogId;
        private System.Windows.Forms.TextBox textBox_dailyWorkLogId;
        private System.Windows.Forms.Label label_subcontractor;
        private System.Windows.Forms.ComboBox comboBox_subcontractor;
        private System.Windows.Forms.Label label_submittedBy;
        private System.Windows.Forms.ComboBox comboBox_submittedBy;
        private System.Windows.Forms.Label label_logDate;
        private System.Windows.Forms.TextBox textBox_logDate;
        private System.Windows.Forms.Label label_plannedQuantity;
        private System.Windows.Forms.TextBox textBox_plannedQuantity;
        private System.Windows.Forms.Label label_completedQuantity;
        private System.Windows.Forms.TextBox textBox_completedQuantity;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
