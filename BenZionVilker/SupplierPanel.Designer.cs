namespace BenZionVilker
{
    partial class SupplierPanel
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
            this.dataGridView_suppliers = new System.Windows.Forms.DataGridView();
            this.label_businessPartnerId = new System.Windows.Forms.Label();
            this.textBox_businessPartnerId = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_companyRegistrationNo = new System.Windows.Forms.Label();
            this.textBox_companyRegistrationNo = new System.Windows.Forms.TextBox();
            this.label_contactPerson = new System.Windows.Forms.Label();
            this.textBox_contactPerson = new System.Windows.Forms.TextBox();
            this.label_phone = new System.Windows.Forms.Label();
            this.textBox_phone = new System.Windows.Forms.TextBox();
            this.label_email = new System.Windows.Forms.Label();
            this.textBox_email = new System.Windows.Forms.TextBox();
            this.label_rating = new System.Windows.Forms.Label();
            this.textBox_rating = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_suppliers)).BeginInit();
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
            this.label_title.Text = "ניהול ספקים";
            //
            // dataGridView_suppliers
            //
            this.dataGridView_suppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_suppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_suppliers.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_suppliers.Name = "dataGridView_suppliers";
            this.dataGridView_suppliers.ReadOnly = true;
            this.dataGridView_suppliers.RowTemplate.Height = 24;
            this.dataGridView_suppliers.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_suppliers.TabIndex = 1;
            this.dataGridView_suppliers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_suppliers_CellClick);
            //
            // label_businessPartnerId
            //
            this.label_businessPartnerId.AutoSize = true;
            this.label_businessPartnerId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_businessPartnerId.Location = new System.Drawing.Point(820, 260);
            this.label_businessPartnerId.Name = "label_businessPartnerId";
            this.label_businessPartnerId.Size = new System.Drawing.Size(70, 17);
            this.label_businessPartnerId.TabIndex = 2;
            this.label_businessPartnerId.Text = "מזהה ספק";
            //
            // textBox_businessPartnerId
            //
            this.textBox_businessPartnerId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_businessPartnerId.Location = new System.Drawing.Point(550, 257);
            this.textBox_businessPartnerId.Name = "textBox_businessPartnerId";
            this.textBox_businessPartnerId.ReadOnly = true;
            this.textBox_businessPartnerId.Size = new System.Drawing.Size(250, 25);
            this.textBox_businessPartnerId.TabIndex = 3;
            this.textBox_businessPartnerId.TabStop = false;
            //
            // label_name
            //
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_name.Location = new System.Drawing.Point(820, 295);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(60, 17);
            this.label_name.TabIndex = 4;
            this.label_name.Text = "שם ספק";
            //
            // textBox_name
            //
            this.textBox_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_name.Location = new System.Drawing.Point(550, 292);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(250, 25);
            this.textBox_name.TabIndex = 5;
            //
            // label_companyRegistrationNo
            //
            this.label_companyRegistrationNo.AutoSize = true;
            this.label_companyRegistrationNo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_companyRegistrationNo.Location = new System.Drawing.Point(820, 330);
            this.label_companyRegistrationNo.Name = "label_companyRegistrationNo";
            this.label_companyRegistrationNo.Size = new System.Drawing.Size(64, 17);
            this.label_companyRegistrationNo.TabIndex = 6;
            this.label_companyRegistrationNo.Text = "מספר ח.פ.";
            //
            // textBox_companyRegistrationNo
            //
            this.textBox_companyRegistrationNo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_companyRegistrationNo.Location = new System.Drawing.Point(550, 327);
            this.textBox_companyRegistrationNo.Name = "textBox_companyRegistrationNo";
            this.textBox_companyRegistrationNo.Size = new System.Drawing.Size(250, 25);
            this.textBox_companyRegistrationNo.TabIndex = 7;
            //
            // label_contactPerson
            //
            this.label_contactPerson.AutoSize = true;
            this.label_contactPerson.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_contactPerson.Location = new System.Drawing.Point(820, 365);
            this.label_contactPerson.Name = "label_contactPerson";
            this.label_contactPerson.Size = new System.Drawing.Size(60, 17);
            this.label_contactPerson.TabIndex = 8;
            this.label_contactPerson.Text = "איש קשר";
            //
            // textBox_contactPerson
            //
            this.textBox_contactPerson.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_contactPerson.Location = new System.Drawing.Point(550, 362);
            this.textBox_contactPerson.Name = "textBox_contactPerson";
            this.textBox_contactPerson.Size = new System.Drawing.Size(250, 25);
            this.textBox_contactPerson.TabIndex = 9;
            //
            // label_phone
            //
            this.label_phone.AutoSize = true;
            this.label_phone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_phone.Location = new System.Drawing.Point(820, 400);
            this.label_phone.Name = "label_phone";
            this.label_phone.Size = new System.Drawing.Size(48, 17);
            this.label_phone.TabIndex = 10;
            this.label_phone.Text = "טלפון";
            //
            // textBox_phone
            //
            this.textBox_phone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_phone.Location = new System.Drawing.Point(550, 397);
            this.textBox_phone.Name = "textBox_phone";
            this.textBox_phone.Size = new System.Drawing.Size(250, 25);
            this.textBox_phone.TabIndex = 11;
            //
            // label_email
            //
            this.label_email.AutoSize = true;
            this.label_email.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_email.Location = new System.Drawing.Point(820, 435);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(48, 17);
            this.label_email.TabIndex = 12;
            this.label_email.Text = "דוא\"ל";
            //
            // textBox_email
            //
            this.textBox_email.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_email.Location = new System.Drawing.Point(550, 432);
            this.textBox_email.Name = "textBox_email";
            this.textBox_email.Size = new System.Drawing.Size(250, 25);
            this.textBox_email.TabIndex = 13;
            //
            // label_rating
            //
            this.label_rating.AutoSize = true;
            this.label_rating.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_rating.Location = new System.Drawing.Point(820, 470);
            this.label_rating.Name = "label_rating";
            this.label_rating.Size = new System.Drawing.Size(48, 17);
            this.label_rating.TabIndex = 14;
            this.label_rating.Text = "דירוג";
            //
            // textBox_rating
            //
            this.textBox_rating.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_rating.Location = new System.Drawing.Point(550, 467);
            this.textBox_rating.Name = "textBox_rating";
            this.textBox_rating.Size = new System.Drawing.Size(250, 25);
            this.textBox_rating.TabIndex = 15;
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
            this.button_save.Location = new System.Drawing.Point(750, 560);
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
            this.button_update.Location = new System.Drawing.Point(610, 560);
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
            this.button_delete.Location = new System.Drawing.Point(470, 560);
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
            this.button_back.Location = new System.Drawing.Point(330, 560);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 21;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // SupplierPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_rating);
            this.Controls.Add(this.label_rating);
            this.Controls.Add(this.textBox_email);
            this.Controls.Add(this.label_email);
            this.Controls.Add(this.textBox_phone);
            this.Controls.Add(this.label_phone);
            this.Controls.Add(this.textBox_contactPerson);
            this.Controls.Add(this.label_contactPerson);
            this.Controls.Add(this.textBox_companyRegistrationNo);
            this.Controls.Add(this.label_companyRegistrationNo);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.textBox_businessPartnerId);
            this.Controls.Add(this.label_businessPartnerId);
            this.Controls.Add(this.dataGridView_suppliers);
            this.Controls.Add(this.label_title);
            this.Name = "SupplierPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_suppliers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_suppliers;
        private System.Windows.Forms.Label label_businessPartnerId;
        private System.Windows.Forms.TextBox textBox_businessPartnerId;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_companyRegistrationNo;
        private System.Windows.Forms.TextBox textBox_companyRegistrationNo;
        private System.Windows.Forms.Label label_contactPerson;
        private System.Windows.Forms.TextBox textBox_contactPerson;
        private System.Windows.Forms.Label label_phone;
        private System.Windows.Forms.TextBox textBox_phone;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.TextBox textBox_email;
        private System.Windows.Forms.Label label_rating;
        private System.Windows.Forms.TextBox textBox_rating;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
