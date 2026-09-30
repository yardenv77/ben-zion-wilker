namespace BenZionVilker
{
    partial class ClientPanel
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
            this.dataGridView_clients = new System.Windows.Forms.DataGridView();
            this.label_clientId = new System.Windows.Forms.Label();
            this.textBox_clientId = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_contactPerson = new System.Windows.Forms.Label();
            this.textBox_contactPerson = new System.Windows.Forms.TextBox();
            this.label_phone = new System.Windows.Forms.Label();
            this.textBox_phone = new System.Windows.Forms.TextBox();
            this.label_email = new System.Windows.Forms.Label();
            this.textBox_email = new System.Windows.Forms.TextBox();
            this.label_sector = new System.Windows.Forms.Label();
            this.textBox_sector = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_clients)).BeginInit();
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
            this.label_title.Text = "ניהול לקוחות";
            //
            // dataGridView_clients
            //
            this.dataGridView_clients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_clients.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_clients.Name = "dataGridView_clients";
            this.dataGridView_clients.ReadOnly = true;
            this.dataGridView_clients.RowTemplate.Height = 24;
            this.dataGridView_clients.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_clients.TabIndex = 1;
            this.dataGridView_clients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_clients_CellClick);
            //
            // label_clientId
            //
            this.label_clientId.AutoSize = true;
            this.label_clientId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_clientId.Location = new System.Drawing.Point(820, 260);
            this.label_clientId.Name = "label_clientId";
            this.label_clientId.Size = new System.Drawing.Size(80, 17);
            this.label_clientId.TabIndex = 2;
            this.label_clientId.Text = "מזהה לקוח";
            //
            // textBox_clientId
            //
            this.textBox_clientId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_clientId.Location = new System.Drawing.Point(550, 257);
            this.textBox_clientId.Name = "textBox_clientId";
            this.textBox_clientId.ReadOnly = true;
            this.textBox_clientId.Size = new System.Drawing.Size(250, 25);
            this.textBox_clientId.TabIndex = 3;
            this.textBox_clientId.TabStop = false;
            //
            // label_name
            //
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_name.Location = new System.Drawing.Point(820, 295);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(70, 17);
            this.label_name.TabIndex = 4;
            this.label_name.Text = "שם לקוח";
            //
            // textBox_name
            //
            this.textBox_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_name.Location = new System.Drawing.Point(550, 292);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(250, 25);
            this.textBox_name.TabIndex = 5;
            //
            // label_contactPerson
            //
            this.label_contactPerson.AutoSize = true;
            this.label_contactPerson.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_contactPerson.Location = new System.Drawing.Point(820, 330);
            this.label_contactPerson.Name = "label_contactPerson";
            this.label_contactPerson.Size = new System.Drawing.Size(60, 17);
            this.label_contactPerson.TabIndex = 6;
            this.label_contactPerson.Text = "איש קשר";
            //
            // textBox_contactPerson
            //
            this.textBox_contactPerson.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_contactPerson.Location = new System.Drawing.Point(550, 327);
            this.textBox_contactPerson.Name = "textBox_contactPerson";
            this.textBox_contactPerson.Size = new System.Drawing.Size(250, 25);
            this.textBox_contactPerson.TabIndex = 7;
            //
            // label_phone
            //
            this.label_phone.AutoSize = true;
            this.label_phone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_phone.Location = new System.Drawing.Point(820, 365);
            this.label_phone.Name = "label_phone";
            this.label_phone.Size = new System.Drawing.Size(48, 17);
            this.label_phone.TabIndex = 8;
            this.label_phone.Text = "טלפון";
            //
            // textBox_phone
            //
            this.textBox_phone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_phone.Location = new System.Drawing.Point(550, 362);
            this.textBox_phone.Name = "textBox_phone";
            this.textBox_phone.Size = new System.Drawing.Size(250, 25);
            this.textBox_phone.TabIndex = 9;
            //
            // label_email
            //
            this.label_email.AutoSize = true;
            this.label_email.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_email.Location = new System.Drawing.Point(820, 400);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(48, 17);
            this.label_email.TabIndex = 10;
            this.label_email.Text = "דוא\"ל";
            //
            // textBox_email
            //
            this.textBox_email.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_email.Location = new System.Drawing.Point(550, 397);
            this.textBox_email.Name = "textBox_email";
            this.textBox_email.Size = new System.Drawing.Size(250, 25);
            this.textBox_email.TabIndex = 11;
            //
            // label_sector
            //
            this.label_sector.AutoSize = true;
            this.label_sector.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_sector.Location = new System.Drawing.Point(820, 435);
            this.label_sector.Name = "label_sector";
            this.label_sector.Size = new System.Drawing.Size(76, 17);
            this.label_sector.TabIndex = 12;
            this.label_sector.Text = "תחום עיסוק";
            //
            // textBox_sector
            //
            this.textBox_sector.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_sector.Location = new System.Drawing.Point(550, 432);
            this.textBox_sector.Name = "textBox_sector";
            this.textBox_sector.Size = new System.Drawing.Size(250, 25);
            this.textBox_sector.TabIndex = 13;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 14;
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
            this.button_update.TabIndex = 15;
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
            this.button_delete.TabIndex = 16;
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
            this.button_back.TabIndex = 17;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // ClientPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_sector);
            this.Controls.Add(this.label_sector);
            this.Controls.Add(this.textBox_email);
            this.Controls.Add(this.label_email);
            this.Controls.Add(this.textBox_phone);
            this.Controls.Add(this.label_phone);
            this.Controls.Add(this.textBox_contactPerson);
            this.Controls.Add(this.label_contactPerson);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.textBox_clientId);
            this.Controls.Add(this.label_clientId);
            this.Controls.Add(this.dataGridView_clients);
            this.Controls.Add(this.label_title);
            this.Name = "ClientPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_clients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_clients;
        private System.Windows.Forms.Label label_clientId;
        private System.Windows.Forms.TextBox textBox_clientId;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_contactPerson;
        private System.Windows.Forms.TextBox textBox_contactPerson;
        private System.Windows.Forms.Label label_phone;
        private System.Windows.Forms.TextBox textBox_phone;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.TextBox textBox_email;
        private System.Windows.Forms.Label label_sector;
        private System.Windows.Forms.TextBox textBox_sector;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
