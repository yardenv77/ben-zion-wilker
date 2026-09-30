namespace BenZionVilker
{
    partial class EquipmentPanel
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
            this.dataGridView_equipments = new System.Windows.Forms.DataGridView();
            this.label_equipmentId = new System.Windows.Forms.Label();
            this.textBox_equipmentId = new System.Windows.Forms.TextBox();
            this.label_licenseNumber = new System.Windows.Forms.Label();
            this.textBox_licenseNumber = new System.Windows.Forms.TextBox();
            this.label_equipmentType = new System.Windows.Forms.Label();
            this.textBox_equipmentType = new System.Windows.Forms.TextBox();
            this.label_description = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.label_dailyCost = new System.Windows.Forms.Label();
            this.textBox_dailyCost = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_equipments)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(400, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(200, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול ציוד";
            //
            // dataGridView_equipments
            //
            this.dataGridView_equipments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_equipments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_equipments.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_equipments.Name = "dataGridView_equipments";
            this.dataGridView_equipments.ReadOnly = true;
            this.dataGridView_equipments.RowTemplate.Height = 24;
            this.dataGridView_equipments.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_equipments.TabIndex = 1;
            this.dataGridView_equipments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_equipments_CellClick);
            //
            // label_equipmentId
            //
            this.label_equipmentId.AutoSize = true;
            this.label_equipmentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipmentId.Location = new System.Drawing.Point(820, 260);
            this.label_equipmentId.Name = "label_equipmentId";
            this.label_equipmentId.Size = new System.Drawing.Size(70, 17);
            this.label_equipmentId.TabIndex = 2;
            this.label_equipmentId.Text = "מזהה ציוד";
            //
            // textBox_equipmentId
            //
            this.textBox_equipmentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_equipmentId.Location = new System.Drawing.Point(550, 257);
            this.textBox_equipmentId.Name = "textBox_equipmentId";
            this.textBox_equipmentId.ReadOnly = true;
            this.textBox_equipmentId.Size = new System.Drawing.Size(250, 25);
            this.textBox_equipmentId.TabIndex = 3;
            this.textBox_equipmentId.TabStop = false;
            //
            // label_licenseNumber
            //
            this.label_licenseNumber.AutoSize = true;
            this.label_licenseNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_licenseNumber.Location = new System.Drawing.Point(820, 295);
            this.label_licenseNumber.Name = "label_licenseNumber";
            this.label_licenseNumber.Size = new System.Drawing.Size(88, 17);
            this.label_licenseNumber.TabIndex = 4;
            this.label_licenseNumber.Text = "מספר רישוי";
            //
            // textBox_licenseNumber
            //
            this.textBox_licenseNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_licenseNumber.Location = new System.Drawing.Point(550, 292);
            this.textBox_licenseNumber.Name = "textBox_licenseNumber";
            this.textBox_licenseNumber.Size = new System.Drawing.Size(250, 25);
            this.textBox_licenseNumber.TabIndex = 5;
            //
            // label_equipmentType
            //
            this.label_equipmentType.AutoSize = true;
            this.label_equipmentType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipmentType.Location = new System.Drawing.Point(820, 330);
            this.label_equipmentType.Name = "label_equipmentType";
            this.label_equipmentType.Size = new System.Drawing.Size(64, 17);
            this.label_equipmentType.TabIndex = 6;
            this.label_equipmentType.Text = "סוג ציוד";
            //
            // textBox_equipmentType
            //
            this.textBox_equipmentType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_equipmentType.Location = new System.Drawing.Point(550, 327);
            this.textBox_equipmentType.Name = "textBox_equipmentType";
            this.textBox_equipmentType.Size = new System.Drawing.Size(250, 25);
            this.textBox_equipmentType.TabIndex = 7;
            //
            // label_description
            //
            this.label_description.AutoSize = true;
            this.label_description.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_description.Location = new System.Drawing.Point(820, 365);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(48, 17);
            this.label_description.TabIndex = 8;
            this.label_description.Text = "תיאור";
            //
            // textBox_description
            //
            this.textBox_description.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_description.Location = new System.Drawing.Point(550, 362);
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(250, 25);
            this.textBox_description.TabIndex = 9;
            //
            // label_dailyCost
            //
            this.label_dailyCost.AutoSize = true;
            this.label_dailyCost.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dailyCost.Location = new System.Drawing.Point(820, 400);
            this.label_dailyCost.Name = "label_dailyCost";
            this.label_dailyCost.Size = new System.Drawing.Size(76, 17);
            this.label_dailyCost.TabIndex = 10;
            this.label_dailyCost.Text = "עלות יומית";
            //
            // textBox_dailyCost
            //
            this.textBox_dailyCost.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_dailyCost.Location = new System.Drawing.Point(550, 397);
            this.textBox_dailyCost.Name = "textBox_dailyCost";
            this.textBox_dailyCost.Size = new System.Drawing.Size(250, 25);
            this.textBox_dailyCost.TabIndex = 11;
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
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 595);
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
            this.button_update.Location = new System.Drawing.Point(610, 595);
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
            this.button_delete.Location = new System.Drawing.Point(470, 595);
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
            this.button_back.Location = new System.Drawing.Point(330, 595);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 17;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // EquipmentPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_dailyCost);
            this.Controls.Add(this.label_dailyCost);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.textBox_equipmentType);
            this.Controls.Add(this.label_equipmentType);
            this.Controls.Add(this.textBox_licenseNumber);
            this.Controls.Add(this.label_licenseNumber);
            this.Controls.Add(this.textBox_equipmentId);
            this.Controls.Add(this.label_equipmentId);
            this.Controls.Add(this.dataGridView_equipments);
            this.Controls.Add(this.label_title);
            this.Name = "EquipmentPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 685);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_equipments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_equipments;
        private System.Windows.Forms.Label label_equipmentId;
        private System.Windows.Forms.TextBox textBox_equipmentId;
        private System.Windows.Forms.Label label_licenseNumber;
        private System.Windows.Forms.TextBox textBox_licenseNumber;
        private System.Windows.Forms.Label label_equipmentType;
        private System.Windows.Forms.TextBox textBox_equipmentType;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.Label label_dailyCost;
        private System.Windows.Forms.TextBox textBox_dailyCost;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
