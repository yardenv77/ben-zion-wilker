namespace BenZionVilker
{
    partial class SupplierPaymentPanel
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
            this.dataGridView_supplierPayments = new System.Windows.Forms.DataGridView();
            this.label_supplierPaymentId = new System.Windows.Forms.Label();
            this.textBox_supplierPaymentId = new System.Windows.Forms.TextBox();
            this.label_invoiceNumber = new System.Windows.Forms.Label();
            this.textBox_invoiceNumber = new System.Windows.Forms.TextBox();
            this.label_businessPartner = new System.Windows.Forms.Label();
            this.comboBox_businessPartner = new System.Windows.Forms.ComboBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.textBox_amount = new System.Windows.Forms.TextBox();
            this.label_dueDate = new System.Windows.Forms.Label();
            this.textBox_dueDate = new System.Windows.Forms.TextBox();
            this.label_paidDate = new System.Windows.Forms.Label();
            this.textBox_paidDate = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_supplierPayments)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(330, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(340, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול תשלומים לספקים";
            //
            // dataGridView_supplierPayments
            //
            this.dataGridView_supplierPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_supplierPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_supplierPayments.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_supplierPayments.Name = "dataGridView_supplierPayments";
            this.dataGridView_supplierPayments.ReadOnly = true;
            this.dataGridView_supplierPayments.RowTemplate.Height = 24;
            this.dataGridView_supplierPayments.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_supplierPayments.TabIndex = 1;
            this.dataGridView_supplierPayments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_supplierPayments_CellClick);
            //
            // label_supplierPaymentId
            //
            this.label_supplierPaymentId.AutoSize = true;
            this.label_supplierPaymentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_supplierPaymentId.Location = new System.Drawing.Point(820, 260);
            this.label_supplierPaymentId.Name = "label_supplierPaymentId";
            this.label_supplierPaymentId.Size = new System.Drawing.Size(90, 17);
            this.label_supplierPaymentId.TabIndex = 2;
            this.label_supplierPaymentId.Text = "מזהה תשלום";
            //
            // textBox_supplierPaymentId
            //
            this.textBox_supplierPaymentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_supplierPaymentId.Location = new System.Drawing.Point(550, 257);
            this.textBox_supplierPaymentId.Name = "textBox_supplierPaymentId";
            this.textBox_supplierPaymentId.ReadOnly = true;
            this.textBox_supplierPaymentId.Size = new System.Drawing.Size(250, 25);
            this.textBox_supplierPaymentId.TabIndex = 3;
            this.textBox_supplierPaymentId.TabStop = false;
            //
            // label_invoiceNumber
            //
            this.label_invoiceNumber.AutoSize = true;
            this.label_invoiceNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_invoiceNumber.Location = new System.Drawing.Point(820, 295);
            this.label_invoiceNumber.Name = "label_invoiceNumber";
            this.label_invoiceNumber.Size = new System.Drawing.Size(100, 17);
            this.label_invoiceNumber.TabIndex = 4;
            this.label_invoiceNumber.Text = "מספר חשבונית";
            //
            // textBox_invoiceNumber
            //
            this.textBox_invoiceNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_invoiceNumber.Location = new System.Drawing.Point(550, 292);
            this.textBox_invoiceNumber.Name = "textBox_invoiceNumber";
            this.textBox_invoiceNumber.Size = new System.Drawing.Size(250, 25);
            this.textBox_invoiceNumber.TabIndex = 5;
            //
            // label_businessPartner
            //
            this.label_businessPartner.AutoSize = true;
            this.label_businessPartner.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_businessPartner.Location = new System.Drawing.Point(820, 330);
            this.label_businessPartner.Name = "label_businessPartner";
            this.label_businessPartner.Size = new System.Drawing.Size(100, 17);
            this.label_businessPartner.TabIndex = 6;
            this.label_businessPartner.Text = "ספק / קבלן משנה";
            //
            // comboBox_businessPartner
            //
            this.comboBox_businessPartner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_businessPartner.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_businessPartner.FormattingEnabled = true;
            this.comboBox_businessPartner.Location = new System.Drawing.Point(550, 327);
            this.comboBox_businessPartner.Name = "comboBox_businessPartner";
            this.comboBox_businessPartner.Size = new System.Drawing.Size(250, 25);
            this.comboBox_businessPartner.TabIndex = 7;
            //
            // label_amount
            //
            this.label_amount.AutoSize = true;
            this.label_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_amount.Location = new System.Drawing.Point(820, 365);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(48, 17);
            this.label_amount.TabIndex = 8;
            this.label_amount.Text = "סכום";
            //
            // textBox_amount
            //
            this.textBox_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_amount.Location = new System.Drawing.Point(550, 362);
            this.textBox_amount.Name = "textBox_amount";
            this.textBox_amount.Size = new System.Drawing.Size(250, 25);
            this.textBox_amount.TabIndex = 9;
            //
            // label_dueDate
            //
            this.label_dueDate.AutoSize = true;
            this.label_dueDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dueDate.Location = new System.Drawing.Point(820, 400);
            this.label_dueDate.Name = "label_dueDate";
            this.label_dueDate.Size = new System.Drawing.Size(80, 17);
            this.label_dueDate.TabIndex = 10;
            this.label_dueDate.Text = "תאריך פירעון";
            //
            // textBox_dueDate
            //
            this.textBox_dueDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_dueDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_dueDate.Name = "textBox_dueDate";
            this.textBox_dueDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_dueDate.TabIndex = 11;
            //
            // label_paidDate
            //
            this.label_paidDate.AutoSize = true;
            this.label_paidDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_paidDate.Location = new System.Drawing.Point(820, 435);
            this.label_paidDate.Name = "label_paidDate";
            this.label_paidDate.Size = new System.Drawing.Size(80, 17);
            this.label_paidDate.TabIndex = 12;
            this.label_paidDate.Text = "תאריך תשלום";
            //
            // textBox_paidDate
            //
            this.textBox_paidDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_paidDate.Location = new System.Drawing.Point(550, 432);
            this.textBox_paidDate.Name = "textBox_paidDate";
            this.textBox_paidDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_paidDate.TabIndex = 13;
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
            // SupplierPaymentPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_paidDate);
            this.Controls.Add(this.label_paidDate);
            this.Controls.Add(this.textBox_dueDate);
            this.Controls.Add(this.label_dueDate);
            this.Controls.Add(this.textBox_amount);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.comboBox_businessPartner);
            this.Controls.Add(this.label_businessPartner);
            this.Controls.Add(this.textBox_invoiceNumber);
            this.Controls.Add(this.label_invoiceNumber);
            this.Controls.Add(this.textBox_supplierPaymentId);
            this.Controls.Add(this.label_supplierPaymentId);
            this.Controls.Add(this.dataGridView_supplierPayments);
            this.Controls.Add(this.label_title);
            this.Name = "SupplierPaymentPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 685);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_supplierPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_supplierPayments;
        private System.Windows.Forms.Label label_supplierPaymentId;
        private System.Windows.Forms.TextBox textBox_supplierPaymentId;
        private System.Windows.Forms.Label label_invoiceNumber;
        private System.Windows.Forms.TextBox textBox_invoiceNumber;
        private System.Windows.Forms.Label label_businessPartner;
        private System.Windows.Forms.ComboBox comboBox_businessPartner;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.TextBox textBox_amount;
        private System.Windows.Forms.Label label_dueDate;
        private System.Windows.Forms.TextBox textBox_dueDate;
        private System.Windows.Forms.Label label_paidDate;
        private System.Windows.Forms.TextBox textBox_paidDate;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
