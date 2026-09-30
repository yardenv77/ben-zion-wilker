namespace BenZionVilker
{
    partial class PaymentRequestPanel
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
            this.dataGridView_paymentRequests = new System.Windows.Forms.DataGridView();
            this.label_paymentRequestId = new System.Windows.Forms.Label();
            this.textBox_paymentRequestId = new System.Windows.Forms.TextBox();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.textBox_amount = new System.Windows.Forms.TextBox();
            this.label_submissionDate = new System.Windows.Forms.Label();
            this.textBox_submissionDate = new System.Windows.Forms.TextBox();
            this.label_approvalDate = new System.Windows.Forms.Label();
            this.textBox_approvalDate = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.label_missingDocuments = new System.Windows.Forms.Label();
            this.textBox_missingDocuments = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_paymentRequests)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(310, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(380, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול בקשות תשלום מלקוח";
            //
            // dataGridView_paymentRequests
            //
            this.dataGridView_paymentRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_paymentRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_paymentRequests.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_paymentRequests.Name = "dataGridView_paymentRequests";
            this.dataGridView_paymentRequests.ReadOnly = true;
            this.dataGridView_paymentRequests.RowTemplate.Height = 24;
            this.dataGridView_paymentRequests.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_paymentRequests.TabIndex = 1;
            this.dataGridView_paymentRequests.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_paymentRequests_CellClick);
            //
            // label_paymentRequestId
            //
            this.label_paymentRequestId.AutoSize = true;
            this.label_paymentRequestId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_paymentRequestId.Location = new System.Drawing.Point(820, 260);
            this.label_paymentRequestId.Name = "label_paymentRequestId";
            this.label_paymentRequestId.Size = new System.Drawing.Size(90, 17);
            this.label_paymentRequestId.TabIndex = 2;
            this.label_paymentRequestId.Text = "מזהה בקשה";
            //
            // textBox_paymentRequestId
            //
            this.textBox_paymentRequestId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_paymentRequestId.Location = new System.Drawing.Point(550, 257);
            this.textBox_paymentRequestId.Name = "textBox_paymentRequestId";
            this.textBox_paymentRequestId.ReadOnly = true;
            this.textBox_paymentRequestId.Size = new System.Drawing.Size(250, 25);
            this.textBox_paymentRequestId.TabIndex = 3;
            this.textBox_paymentRequestId.TabStop = false;
            //
            // label_project
            //
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_project.Location = new System.Drawing.Point(820, 295);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(56, 17);
            this.label_project.TabIndex = 4;
            this.label_project.Text = "פרויקט";
            //
            // comboBox_project
            //
            this.comboBox_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(550, 292);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.Size = new System.Drawing.Size(250, 25);
            this.comboBox_project.TabIndex = 5;
            //
            // label_amount
            //
            this.label_amount.AutoSize = true;
            this.label_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_amount.Location = new System.Drawing.Point(820, 330);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(48, 17);
            this.label_amount.TabIndex = 6;
            this.label_amount.Text = "סכום";
            //
            // textBox_amount
            //
            this.textBox_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_amount.Location = new System.Drawing.Point(550, 327);
            this.textBox_amount.Name = "textBox_amount";
            this.textBox_amount.Size = new System.Drawing.Size(250, 25);
            this.textBox_amount.TabIndex = 7;
            //
            // label_submissionDate
            //
            this.label_submissionDate.AutoSize = true;
            this.label_submissionDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_submissionDate.Location = new System.Drawing.Point(820, 365);
            this.label_submissionDate.Name = "label_submissionDate";
            this.label_submissionDate.Size = new System.Drawing.Size(64, 17);
            this.label_submissionDate.TabIndex = 8;
            this.label_submissionDate.Text = "תאריך הגשה";
            //
            // textBox_submissionDate
            //
            this.textBox_submissionDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_submissionDate.Location = new System.Drawing.Point(550, 362);
            this.textBox_submissionDate.Name = "textBox_submissionDate";
            this.textBox_submissionDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_submissionDate.TabIndex = 9;
            //
            // label_approvalDate
            //
            this.label_approvalDate.AutoSize = true;
            this.label_approvalDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_approvalDate.Location = new System.Drawing.Point(820, 400);
            this.label_approvalDate.Name = "label_approvalDate";
            this.label_approvalDate.Size = new System.Drawing.Size(64, 17);
            this.label_approvalDate.TabIndex = 10;
            this.label_approvalDate.Text = "תאריך אישור";
            //
            // textBox_approvalDate
            //
            this.textBox_approvalDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_approvalDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_approvalDate.Name = "textBox_approvalDate";
            this.textBox_approvalDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_approvalDate.TabIndex = 11;
            //
            // label_status
            //
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_status.Location = new System.Drawing.Point(820, 435);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(48, 17);
            this.label_status.TabIndex = 12;
            this.label_status.Text = "סטטוס";
            //
            // comboBox_status
            //
            this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Location = new System.Drawing.Point(550, 432);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(250, 25);
            this.comboBox_status.TabIndex = 13;
            //
            // label_missingDocuments
            //
            this.label_missingDocuments.AutoSize = true;
            this.label_missingDocuments.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_missingDocuments.Location = new System.Drawing.Point(820, 470);
            this.label_missingDocuments.Name = "label_missingDocuments";
            this.label_missingDocuments.Size = new System.Drawing.Size(100, 17);
            this.label_missingDocuments.TabIndex = 14;
            this.label_missingDocuments.Text = "מסמכים חסרים";
            //
            // textBox_missingDocuments
            //
            this.textBox_missingDocuments.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_missingDocuments.Location = new System.Drawing.Point(550, 467);
            this.textBox_missingDocuments.Name = "textBox_missingDocuments";
            this.textBox_missingDocuments.Size = new System.Drawing.Size(250, 25);
            this.textBox_missingDocuments.TabIndex = 15;
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
            // PaymentRequestPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_missingDocuments);
            this.Controls.Add(this.label_missingDocuments);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_approvalDate);
            this.Controls.Add(this.label_approvalDate);
            this.Controls.Add(this.textBox_submissionDate);
            this.Controls.Add(this.label_submissionDate);
            this.Controls.Add(this.textBox_amount);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.textBox_paymentRequestId);
            this.Controls.Add(this.label_paymentRequestId);
            this.Controls.Add(this.dataGridView_paymentRequests);
            this.Controls.Add(this.label_title);
            this.Name = "PaymentRequestPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_paymentRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_paymentRequests;
        private System.Windows.Forms.Label label_paymentRequestId;
        private System.Windows.Forms.TextBox textBox_paymentRequestId;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.TextBox textBox_amount;
        private System.Windows.Forms.Label label_submissionDate;
        private System.Windows.Forms.TextBox textBox_submissionDate;
        private System.Windows.Forms.Label label_approvalDate;
        private System.Windows.Forms.TextBox textBox_approvalDate;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Label label_missingDocuments;
        private System.Windows.Forms.TextBox textBox_missingDocuments;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
