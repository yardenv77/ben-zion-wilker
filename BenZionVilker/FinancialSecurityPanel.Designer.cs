namespace BenZionVilker
{
    partial class FinancialSecurityPanel
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
            this.dataGridView_securities = new System.Windows.Forms.DataGridView();
            this.label_type = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label_financialSecurityId = new System.Windows.Forms.Label();
            this.textBox_financialSecurityId = new System.Windows.Forms.TextBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.textBox_amount = new System.Windows.Forms.TextBox();
            this.label_issueDate = new System.Windows.Forms.Label();
            this.textBox_issueDate = new System.Windows.Forms.TextBox();
            this.label_expiryDate = new System.Windows.Forms.Label();
            this.textBox_expiryDate = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.label_bankName = new System.Windows.Forms.Label();
            this.textBox_bankName = new System.Windows.Forms.TextBox();
            this.label_guaranteeNumber = new System.Windows.Forms.Label();
            this.textBox_guaranteeNumber = new System.Windows.Forms.TextBox();
            this.label_insurerName = new System.Windows.Forms.Label();
            this.textBox_insurerName = new System.Windows.Forms.TextBox();
            this.label_policyNumber = new System.Windows.Forms.Label();
            this.textBox_policyNumber = new System.Windows.Forms.TextBox();
            this.label_coverageType = new System.Windows.Forms.Label();
            this.textBox_coverageType = new System.Windows.Forms.TextBox();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_securities)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(300, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(400, 36);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול ערבויות וביטוחים";
            //
            // dataGridView_securities
            //
            this.dataGridView_securities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_securities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_securities.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_securities.Name = "dataGridView_securities";
            this.dataGridView_securities.ReadOnly = true;
            this.dataGridView_securities.RowTemplate.Height = 24;
            this.dataGridView_securities.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_securities.TabIndex = 1;
            this.dataGridView_securities.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_securities_CellClick);
            //
            // label_type (col A)
            //
            this.label_type.AutoSize = true;
            this.label_type.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_type.Location = new System.Drawing.Point(820, 260);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(40, 17);
            this.label_type.TabIndex = 2;
            this.label_type.Text = "סוג";
            //
            // comboBox_type
            //
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Location = new System.Drawing.Point(550, 257);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(250, 25);
            this.comboBox_type.TabIndex = 3;
            //
            // label_financialSecurityId (col A)
            //
            this.label_financialSecurityId.AutoSize = true;
            this.label_financialSecurityId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_financialSecurityId.Location = new System.Drawing.Point(820, 295);
            this.label_financialSecurityId.Name = "label_financialSecurityId";
            this.label_financialSecurityId.Size = new System.Drawing.Size(60, 17);
            this.label_financialSecurityId.TabIndex = 4;
            this.label_financialSecurityId.Text = "מזהה";
            //
            // textBox_financialSecurityId
            //
            this.textBox_financialSecurityId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_financialSecurityId.Location = new System.Drawing.Point(550, 292);
            this.textBox_financialSecurityId.Name = "textBox_financialSecurityId";
            this.textBox_financialSecurityId.ReadOnly = true;
            this.textBox_financialSecurityId.Size = new System.Drawing.Size(250, 25);
            this.textBox_financialSecurityId.TabIndex = 5;
            this.textBox_financialSecurityId.TabStop = false;
            //
            // label_amount (col A)
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
            // label_issueDate (col A)
            //
            this.label_issueDate.AutoSize = true;
            this.label_issueDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_issueDate.Location = new System.Drawing.Point(820, 365);
            this.label_issueDate.Name = "label_issueDate";
            this.label_issueDate.Size = new System.Drawing.Size(80, 17);
            this.label_issueDate.TabIndex = 8;
            this.label_issueDate.Text = "תאריך הנפקה";
            //
            // textBox_issueDate
            //
            this.textBox_issueDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_issueDate.Location = new System.Drawing.Point(550, 362);
            this.textBox_issueDate.Name = "textBox_issueDate";
            this.textBox_issueDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_issueDate.TabIndex = 9;
            //
            // label_expiryDate (col A)
            //
            this.label_expiryDate.AutoSize = true;
            this.label_expiryDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_expiryDate.Location = new System.Drawing.Point(820, 400);
            this.label_expiryDate.Name = "label_expiryDate";
            this.label_expiryDate.Size = new System.Drawing.Size(80, 17);
            this.label_expiryDate.TabIndex = 10;
            this.label_expiryDate.Text = "תאריך תפוגה";
            //
            // textBox_expiryDate
            //
            this.textBox_expiryDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_expiryDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_expiryDate.Name = "textBox_expiryDate";
            this.textBox_expiryDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_expiryDate.TabIndex = 11;
            //
            // label_status (col A)
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
            // label_bankName (col B)
            //
            this.label_bankName.AutoSize = true;
            this.label_bankName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_bankName.Location = new System.Drawing.Point(430, 260);
            this.label_bankName.Name = "label_bankName";
            this.label_bankName.Size = new System.Drawing.Size(64, 17);
            this.label_bankName.TabIndex = 14;
            this.label_bankName.Text = "שם בנק";
            //
            // textBox_bankName
            //
            this.textBox_bankName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_bankName.Location = new System.Drawing.Point(190, 257);
            this.textBox_bankName.Name = "textBox_bankName";
            this.textBox_bankName.Size = new System.Drawing.Size(230, 25);
            this.textBox_bankName.TabIndex = 15;
            //
            // label_guaranteeNumber (col B)
            //
            this.label_guaranteeNumber.AutoSize = true;
            this.label_guaranteeNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_guaranteeNumber.Location = new System.Drawing.Point(430, 295);
            this.label_guaranteeNumber.Name = "label_guaranteeNumber";
            this.label_guaranteeNumber.Size = new System.Drawing.Size(80, 17);
            this.label_guaranteeNumber.TabIndex = 16;
            this.label_guaranteeNumber.Text = "מספר ערבות";
            //
            // textBox_guaranteeNumber
            //
            this.textBox_guaranteeNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_guaranteeNumber.Location = new System.Drawing.Point(190, 292);
            this.textBox_guaranteeNumber.Name = "textBox_guaranteeNumber";
            this.textBox_guaranteeNumber.Size = new System.Drawing.Size(230, 25);
            this.textBox_guaranteeNumber.TabIndex = 17;
            //
            // label_insurerName (col B)
            //
            this.label_insurerName.AutoSize = true;
            this.label_insurerName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_insurerName.Location = new System.Drawing.Point(430, 330);
            this.label_insurerName.Name = "label_insurerName";
            this.label_insurerName.Size = new System.Drawing.Size(48, 17);
            this.label_insurerName.TabIndex = 18;
            this.label_insurerName.Text = "מבטח";
            //
            // textBox_insurerName
            //
            this.textBox_insurerName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_insurerName.Location = new System.Drawing.Point(190, 327);
            this.textBox_insurerName.Name = "textBox_insurerName";
            this.textBox_insurerName.Size = new System.Drawing.Size(230, 25);
            this.textBox_insurerName.TabIndex = 19;
            //
            // label_policyNumber (col B)
            //
            this.label_policyNumber.AutoSize = true;
            this.label_policyNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_policyNumber.Location = new System.Drawing.Point(430, 365);
            this.label_policyNumber.Name = "label_policyNumber";
            this.label_policyNumber.Size = new System.Drawing.Size(80, 17);
            this.label_policyNumber.TabIndex = 20;
            this.label_policyNumber.Text = "מספר פוליסה";
            //
            // textBox_policyNumber
            //
            this.textBox_policyNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_policyNumber.Location = new System.Drawing.Point(190, 362);
            this.textBox_policyNumber.Name = "textBox_policyNumber";
            this.textBox_policyNumber.Size = new System.Drawing.Size(230, 25);
            this.textBox_policyNumber.TabIndex = 21;
            //
            // label_coverageType (col B)
            //
            this.label_coverageType.AutoSize = true;
            this.label_coverageType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_coverageType.Location = new System.Drawing.Point(430, 400);
            this.label_coverageType.Name = "label_coverageType";
            this.label_coverageType.Size = new System.Drawing.Size(64, 17);
            this.label_coverageType.TabIndex = 22;
            this.label_coverageType.Text = "סוג כיסוי";
            //
            // textBox_coverageType
            //
            this.textBox_coverageType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_coverageType.Location = new System.Drawing.Point(190, 397);
            this.textBox_coverageType.Name = "textBox_coverageType";
            this.textBox_coverageType.Size = new System.Drawing.Size(230, 25);
            this.textBox_coverageType.TabIndex = 23;
            //
            // label_project (col B)
            //
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_project.Location = new System.Drawing.Point(430, 435);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(60, 17);
            this.label_project.TabIndex = 24;
            this.label_project.Text = "פרויקט";
            //
            // comboBox_project
            //
            this.comboBox_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(190, 432);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.Size = new System.Drawing.Size(230, 25);
            this.comboBox_project.TabIndex = 25;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 24;
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
            this.button_update.TabIndex = 25;
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
            this.button_delete.TabIndex = 26;
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
            this.button_back.TabIndex = 27;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // FinancialSecurityPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.textBox_coverageType);
            this.Controls.Add(this.label_coverageType);
            this.Controls.Add(this.textBox_policyNumber);
            this.Controls.Add(this.label_policyNumber);
            this.Controls.Add(this.textBox_insurerName);
            this.Controls.Add(this.label_insurerName);
            this.Controls.Add(this.textBox_guaranteeNumber);
            this.Controls.Add(this.label_guaranteeNumber);
            this.Controls.Add(this.textBox_bankName);
            this.Controls.Add(this.label_bankName);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_expiryDate);
            this.Controls.Add(this.label_expiryDate);
            this.Controls.Add(this.textBox_issueDate);
            this.Controls.Add(this.label_issueDate);
            this.Controls.Add(this.textBox_amount);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.textBox_financialSecurityId);
            this.Controls.Add(this.label_financialSecurityId);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.dataGridView_securities);
            this.Controls.Add(this.label_title);
            this.Name = "FinancialSecurityPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_securities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_securities;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label_financialSecurityId;
        private System.Windows.Forms.TextBox textBox_financialSecurityId;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.TextBox textBox_amount;
        private System.Windows.Forms.Label label_issueDate;
        private System.Windows.Forms.TextBox textBox_issueDate;
        private System.Windows.Forms.Label label_expiryDate;
        private System.Windows.Forms.TextBox textBox_expiryDate;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Label label_bankName;
        private System.Windows.Forms.TextBox textBox_bankName;
        private System.Windows.Forms.Label label_guaranteeNumber;
        private System.Windows.Forms.TextBox textBox_guaranteeNumber;
        private System.Windows.Forms.Label label_insurerName;
        private System.Windows.Forms.TextBox textBox_insurerName;
        private System.Windows.Forms.Label label_policyNumber;
        private System.Windows.Forms.TextBox textBox_policyNumber;
        private System.Windows.Forms.Label label_coverageType;
        private System.Windows.Forms.TextBox textBox_coverageType;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
