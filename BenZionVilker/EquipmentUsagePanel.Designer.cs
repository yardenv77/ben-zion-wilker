namespace BenZionVilker
{
    partial class EquipmentUsagePanel
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
            this.dataGridView_usages = new System.Windows.Forms.DataGridView();
            this.label_equipmentUsageId = new System.Windows.Forms.Label();
            this.textBox_equipmentUsageId = new System.Windows.Forms.TextBox();
            this.label_equipment = new System.Windows.Forms.Label();
            this.comboBox_equipment = new System.Windows.Forms.ComboBox();
            this.label_dailyWorkLog = new System.Windows.Forms.Label();
            this.comboBox_dailyWorkLog = new System.Windows.Forms.ComboBox();
            this.label_hoursOperated = new System.Windows.Forms.Label();
            this.textBox_hoursOperated = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_usages)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(340, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(320, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול שימוש בציוד";
            //
            // dataGridView_usages
            //
            this.dataGridView_usages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_usages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_usages.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_usages.Name = "dataGridView_usages";
            this.dataGridView_usages.ReadOnly = true;
            this.dataGridView_usages.RowTemplate.Height = 24;
            this.dataGridView_usages.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_usages.TabIndex = 1;
            this.dataGridView_usages.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_usages_CellClick);
            //
            // label_equipmentUsageId
            //
            this.label_equipmentUsageId.AutoSize = true;
            this.label_equipmentUsageId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipmentUsageId.Location = new System.Drawing.Point(820, 260);
            this.label_equipmentUsageId.Name = "label_equipmentUsageId";
            this.label_equipmentUsageId.Size = new System.Drawing.Size(60, 17);
            this.label_equipmentUsageId.TabIndex = 2;
            this.label_equipmentUsageId.Text = "מזהה";
            //
            // textBox_equipmentUsageId
            //
            this.textBox_equipmentUsageId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_equipmentUsageId.Location = new System.Drawing.Point(550, 257);
            this.textBox_equipmentUsageId.Name = "textBox_equipmentUsageId";
            this.textBox_equipmentUsageId.ReadOnly = true;
            this.textBox_equipmentUsageId.Size = new System.Drawing.Size(250, 25);
            this.textBox_equipmentUsageId.TabIndex = 3;
            this.textBox_equipmentUsageId.TabStop = false;
            //
            // label_equipment
            //
            this.label_equipment.AutoSize = true;
            this.label_equipment.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipment.Location = new System.Drawing.Point(820, 295);
            this.label_equipment.Name = "label_equipment";
            this.label_equipment.Size = new System.Drawing.Size(48, 17);
            this.label_equipment.TabIndex = 4;
            this.label_equipment.Text = "ציוד";
            //
            // comboBox_equipment
            //
            this.comboBox_equipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_equipment.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_equipment.FormattingEnabled = true;
            this.comboBox_equipment.Location = new System.Drawing.Point(550, 292);
            this.comboBox_equipment.Name = "comboBox_equipment";
            this.comboBox_equipment.Size = new System.Drawing.Size(250, 25);
            this.comboBox_equipment.TabIndex = 5;
            //
            // label_dailyWorkLog
            //
            this.label_dailyWorkLog.AutoSize = true;
            this.label_dailyWorkLog.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dailyWorkLog.Location = new System.Drawing.Point(820, 330);
            this.label_dailyWorkLog.Name = "label_dailyWorkLog";
            this.label_dailyWorkLog.Size = new System.Drawing.Size(70, 17);
            this.label_dailyWorkLog.TabIndex = 6;
            this.label_dailyWorkLog.Text = "יומן עבודה";
            //
            // comboBox_dailyWorkLog
            //
            this.comboBox_dailyWorkLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_dailyWorkLog.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_dailyWorkLog.FormattingEnabled = true;
            this.comboBox_dailyWorkLog.Location = new System.Drawing.Point(550, 327);
            this.comboBox_dailyWorkLog.Name = "comboBox_dailyWorkLog";
            this.comboBox_dailyWorkLog.Size = new System.Drawing.Size(250, 25);
            this.comboBox_dailyWorkLog.TabIndex = 7;
            //
            // label_hoursOperated
            //
            this.label_hoursOperated.AutoSize = true;
            this.label_hoursOperated.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_hoursOperated.Location = new System.Drawing.Point(820, 365);
            this.label_hoursOperated.Name = "label_hoursOperated";
            this.label_hoursOperated.Size = new System.Drawing.Size(80, 17);
            this.label_hoursOperated.TabIndex = 8;
            this.label_hoursOperated.Text = "שעות הפעלה";
            //
            // textBox_hoursOperated
            //
            this.textBox_hoursOperated.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_hoursOperated.Location = new System.Drawing.Point(550, 362);
            this.textBox_hoursOperated.Name = "textBox_hoursOperated";
            this.textBox_hoursOperated.Size = new System.Drawing.Size(250, 25);
            this.textBox_hoursOperated.TabIndex = 9;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 10;
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
            this.button_update.TabIndex = 11;
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
            this.button_delete.TabIndex = 12;
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
            this.button_back.TabIndex = 13;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // EquipmentUsagePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_hoursOperated);
            this.Controls.Add(this.label_hoursOperated);
            this.Controls.Add(this.comboBox_dailyWorkLog);
            this.Controls.Add(this.label_dailyWorkLog);
            this.Controls.Add(this.comboBox_equipment);
            this.Controls.Add(this.label_equipment);
            this.Controls.Add(this.textBox_equipmentUsageId);
            this.Controls.Add(this.label_equipmentUsageId);
            this.Controls.Add(this.dataGridView_usages);
            this.Controls.Add(this.label_title);
            this.Name = "EquipmentUsagePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_usages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_usages;
        private System.Windows.Forms.Label label_equipmentUsageId;
        private System.Windows.Forms.TextBox textBox_equipmentUsageId;
        private System.Windows.Forms.Label label_equipment;
        private System.Windows.Forms.ComboBox comboBox_equipment;
        private System.Windows.Forms.Label label_dailyWorkLog;
        private System.Windows.Forms.ComboBox comboBox_dailyWorkLog;
        private System.Windows.Forms.Label label_hoursOperated;
        private System.Windows.Forms.TextBox textBox_hoursOperated;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
