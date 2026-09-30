namespace BenZionVilker
{
    partial class TenderPanel
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
            this.dataGridView_tenders = new System.Windows.Forms.DataGridView();
            this.label_tenderId = new System.Windows.Forms.Label();
            this.textBox_tenderId = new System.Windows.Forms.TextBox();
            this.label_tenderNumber = new System.Windows.Forms.Label();
            this.textBox_tenderNumber = new System.Windows.Forms.TextBox();
            this.label_client = new System.Windows.Forms.Label();
            this.comboBox_client = new System.Windows.Forms.ComboBox();
            this.label_titleField = new System.Windows.Forms.Label();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.label_estimatedValue = new System.Windows.Forms.Label();
            this.textBox_estimatedValue = new System.Windows.Forms.TextBox();
            this.label_submissionDeadline = new System.Windows.Forms.Label();
            this.textBox_submissionDeadline = new System.Windows.Forms.TextBox();
            this.label_publishedDate = new System.Windows.Forms.Label();
            this.textBox_publishedDate = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tenders)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(380, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(240, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול מכרזים";
            //
            // dataGridView_tenders
            //
            this.dataGridView_tenders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_tenders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_tenders.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_tenders.Name = "dataGridView_tenders";
            this.dataGridView_tenders.ReadOnly = true;
            this.dataGridView_tenders.RowTemplate.Height = 24;
            this.dataGridView_tenders.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_tenders.TabIndex = 1;
            this.dataGridView_tenders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_tenders_CellClick);
            //
            // label_tenderId
            //
            this.label_tenderId.AutoSize = true;
            this.label_tenderId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tenderId.Location = new System.Drawing.Point(820, 260);
            this.label_tenderId.Name = "label_tenderId";
            this.label_tenderId.Size = new System.Drawing.Size(80, 17);
            this.label_tenderId.TabIndex = 2;
            this.label_tenderId.Text = "מזהה מכרז";
            //
            // textBox_tenderId
            //
            this.textBox_tenderId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_tenderId.Location = new System.Drawing.Point(550, 257);
            this.textBox_tenderId.Name = "textBox_tenderId";
            this.textBox_tenderId.ReadOnly = true;
            this.textBox_tenderId.Size = new System.Drawing.Size(250, 25);
            this.textBox_tenderId.TabIndex = 3;
            this.textBox_tenderId.TabStop = false;
            //
            // label_tenderNumber
            //
            this.label_tenderNumber.AutoSize = true;
            this.label_tenderNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tenderNumber.Location = new System.Drawing.Point(820, 295);
            this.label_tenderNumber.Name = "label_tenderNumber";
            this.label_tenderNumber.Size = new System.Drawing.Size(90, 17);
            this.label_tenderNumber.TabIndex = 4;
            this.label_tenderNumber.Text = "מספר מכרז";
            //
            // textBox_tenderNumber
            //
            this.textBox_tenderNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_tenderNumber.Location = new System.Drawing.Point(550, 292);
            this.textBox_tenderNumber.Name = "textBox_tenderNumber";
            this.textBox_tenderNumber.Size = new System.Drawing.Size(250, 25);
            this.textBox_tenderNumber.TabIndex = 5;
            //
            // label_client
            //
            this.label_client.AutoSize = true;
            this.label_client.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_client.Location = new System.Drawing.Point(820, 330);
            this.label_client.Name = "label_client";
            this.label_client.Size = new System.Drawing.Size(48, 17);
            this.label_client.TabIndex = 6;
            this.label_client.Text = "לקוח";
            //
            // comboBox_client
            //
            this.comboBox_client.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_client.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_client.FormattingEnabled = true;
            this.comboBox_client.Location = new System.Drawing.Point(550, 327);
            this.comboBox_client.Name = "comboBox_client";
            this.comboBox_client.Size = new System.Drawing.Size(250, 25);
            this.comboBox_client.TabIndex = 7;
            //
            // label_titleField
            //
            this.label_titleField.AutoSize = true;
            this.label_titleField.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_titleField.Location = new System.Drawing.Point(820, 365);
            this.label_titleField.Name = "label_titleField";
            this.label_titleField.Size = new System.Drawing.Size(64, 17);
            this.label_titleField.TabIndex = 8;
            this.label_titleField.Text = "כותרת";
            //
            // textBox_title
            //
            this.textBox_title.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_title.Location = new System.Drawing.Point(550, 362);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(250, 25);
            this.textBox_title.TabIndex = 9;
            //
            // label_estimatedValue
            //
            this.label_estimatedValue.AutoSize = true;
            this.label_estimatedValue.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_estimatedValue.Location = new System.Drawing.Point(820, 400);
            this.label_estimatedValue.Name = "label_estimatedValue";
            this.label_estimatedValue.Size = new System.Drawing.Size(80, 17);
            this.label_estimatedValue.TabIndex = 10;
            this.label_estimatedValue.Text = "שווי משוער";
            //
            // textBox_estimatedValue
            //
            this.textBox_estimatedValue.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_estimatedValue.Location = new System.Drawing.Point(550, 397);
            this.textBox_estimatedValue.Name = "textBox_estimatedValue";
            this.textBox_estimatedValue.Size = new System.Drawing.Size(250, 25);
            this.textBox_estimatedValue.TabIndex = 11;
            //
            // label_submissionDeadline
            //
            this.label_submissionDeadline.AutoSize = true;
            this.label_submissionDeadline.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_submissionDeadline.Location = new System.Drawing.Point(820, 435);
            this.label_submissionDeadline.Name = "label_submissionDeadline";
            this.label_submissionDeadline.Size = new System.Drawing.Size(90, 17);
            this.label_submissionDeadline.TabIndex = 12;
            this.label_submissionDeadline.Text = "מועד הגשה";
            //
            // textBox_submissionDeadline
            //
            this.textBox_submissionDeadline.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_submissionDeadline.Location = new System.Drawing.Point(550, 432);
            this.textBox_submissionDeadline.Name = "textBox_submissionDeadline";
            this.textBox_submissionDeadline.Size = new System.Drawing.Size(250, 25);
            this.textBox_submissionDeadline.TabIndex = 13;
            //
            // label_publishedDate
            //
            this.label_publishedDate.AutoSize = true;
            this.label_publishedDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_publishedDate.Location = new System.Drawing.Point(820, 470);
            this.label_publishedDate.Name = "label_publishedDate";
            this.label_publishedDate.Size = new System.Drawing.Size(90, 17);
            this.label_publishedDate.TabIndex = 14;
            this.label_publishedDate.Text = "תאריך פרסום";
            //
            // textBox_publishedDate
            //
            this.textBox_publishedDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_publishedDate.Location = new System.Drawing.Point(550, 467);
            this.textBox_publishedDate.Name = "textBox_publishedDate";
            this.textBox_publishedDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_publishedDate.TabIndex = 15;
            //
            // label_status
            //
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_status.Location = new System.Drawing.Point(820, 505);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(48, 17);
            this.label_status.TabIndex = 16;
            this.label_status.Text = "סטטוס";
            //
            // comboBox_status
            //
            this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Location = new System.Drawing.Point(550, 502);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(250, 25);
            this.comboBox_status.TabIndex = 17;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 595);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 18;
            this.button_save.Text = "שמירה";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            //
            // button_update
            //
            this.button_update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_update.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_update.Location = new System.Drawing.Point(610, 595);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(110, 42);
            this.button_update.TabIndex = 19;
            this.button_update.Text = "עדכון";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            //
            // button_delete
            //
            this.button_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_delete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_delete.Location = new System.Drawing.Point(470, 595);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 42);
            this.button_delete.TabIndex = 20;
            this.button_delete.Text = "מחיקה";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            //
            // button_back
            //
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(330, 595);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 21;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // TenderPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_publishedDate);
            this.Controls.Add(this.label_publishedDate);
            this.Controls.Add(this.textBox_submissionDeadline);
            this.Controls.Add(this.label_submissionDeadline);
            this.Controls.Add(this.textBox_estimatedValue);
            this.Controls.Add(this.label_estimatedValue);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.label_titleField);
            this.Controls.Add(this.comboBox_client);
            this.Controls.Add(this.label_client);
            this.Controls.Add(this.textBox_tenderNumber);
            this.Controls.Add(this.label_tenderNumber);
            this.Controls.Add(this.textBox_tenderId);
            this.Controls.Add(this.label_tenderId);
            this.Controls.Add(this.dataGridView_tenders);
            this.Controls.Add(this.label_title);
            this.Name = "TenderPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 685);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tenders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_tenders;
        private System.Windows.Forms.Label label_tenderId;
        private System.Windows.Forms.TextBox textBox_tenderId;
        private System.Windows.Forms.Label label_tenderNumber;
        private System.Windows.Forms.TextBox textBox_tenderNumber;
        private System.Windows.Forms.Label label_client;
        private System.Windows.Forms.ComboBox comboBox_client;
        private System.Windows.Forms.Label label_titleField;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.Label label_estimatedValue;
        private System.Windows.Forms.TextBox textBox_estimatedValue;
        private System.Windows.Forms.Label label_submissionDeadline;
        private System.Windows.Forms.TextBox textBox_submissionDeadline;
        private System.Windows.Forms.Label label_publishedDate;
        private System.Windows.Forms.TextBox textBox_publishedDate;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
