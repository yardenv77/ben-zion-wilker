namespace BenZionVilker
{
    partial class EquipmentAssignmentPanel
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
            this.dataGridView_assignments = new System.Windows.Forms.DataGridView();
            this.label_equipmentAssignmentId = new System.Windows.Forms.Label();
            this.textBox_equipmentAssignmentId = new System.Windows.Forms.TextBox();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.label_equipment = new System.Windows.Forms.Label();
            this.comboBox_equipment = new System.Windows.Forms.ComboBox();
            this.label_startDate = new System.Windows.Forms.Label();
            this.textBox_startDate = new System.Windows.Forms.TextBox();
            this.label_endDate = new System.Windows.Forms.Label();
            this.textBox_endDate = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_assignments)).BeginInit();
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
            this.label_title.Text = "שיבוץ ציוד לפרויקטים";
            //
            // dataGridView_assignments
            //
            this.dataGridView_assignments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_assignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_assignments.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_assignments.Name = "dataGridView_assignments";
            this.dataGridView_assignments.ReadOnly = true;
            this.dataGridView_assignments.RowTemplate.Height = 24;
            this.dataGridView_assignments.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_assignments.TabIndex = 1;
            this.dataGridView_assignments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_assignments_CellClick);
            //
            // label_equipmentAssignmentId
            //
            this.label_equipmentAssignmentId.AutoSize = true;
            this.label_equipmentAssignmentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipmentAssignmentId.Location = new System.Drawing.Point(820, 260);
            this.label_equipmentAssignmentId.Name = "label_equipmentAssignmentId";
            this.label_equipmentAssignmentId.Size = new System.Drawing.Size(60, 17);
            this.label_equipmentAssignmentId.TabIndex = 2;
            this.label_equipmentAssignmentId.Text = "מזהה";
            //
            // textBox_equipmentAssignmentId
            //
            this.textBox_equipmentAssignmentId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_equipmentAssignmentId.Location = new System.Drawing.Point(550, 257);
            this.textBox_equipmentAssignmentId.Name = "textBox_equipmentAssignmentId";
            this.textBox_equipmentAssignmentId.ReadOnly = true;
            this.textBox_equipmentAssignmentId.Size = new System.Drawing.Size(250, 25);
            this.textBox_equipmentAssignmentId.TabIndex = 3;
            this.textBox_equipmentAssignmentId.TabStop = false;
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
            // label_equipment
            //
            this.label_equipment.AutoSize = true;
            this.label_equipment.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_equipment.Location = new System.Drawing.Point(820, 330);
            this.label_equipment.Name = "label_equipment";
            this.label_equipment.Size = new System.Drawing.Size(48, 17);
            this.label_equipment.TabIndex = 6;
            this.label_equipment.Text = "ציוד";
            //
            // comboBox_equipment
            //
            this.comboBox_equipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_equipment.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_equipment.FormattingEnabled = true;
            this.comboBox_equipment.Location = new System.Drawing.Point(550, 327);
            this.comboBox_equipment.Name = "comboBox_equipment";
            this.comboBox_equipment.Size = new System.Drawing.Size(250, 25);
            this.comboBox_equipment.TabIndex = 7;
            //
            // label_startDate
            //
            this.label_startDate.AutoSize = true;
            this.label_startDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_startDate.Location = new System.Drawing.Point(820, 365);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(80, 17);
            this.label_startDate.TabIndex = 8;
            this.label_startDate.Text = "תאריך התחלה";
            //
            // textBox_startDate
            //
            this.textBox_startDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_startDate.Location = new System.Drawing.Point(550, 362);
            this.textBox_startDate.Name = "textBox_startDate";
            this.textBox_startDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_startDate.TabIndex = 9;
            //
            // label_endDate
            //
            this.label_endDate.AutoSize = true;
            this.label_endDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_endDate.Location = new System.Drawing.Point(820, 400);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(70, 17);
            this.label_endDate.TabIndex = 10;
            this.label_endDate.Text = "תאריך סיום";
            //
            // textBox_endDate
            //
            this.textBox_endDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_endDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_endDate.Name = "textBox_endDate";
            this.textBox_endDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_endDate.TabIndex = 11;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 12;
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
            this.button_update.TabIndex = 13;
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
            this.button_delete.TabIndex = 14;
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
            this.button_back.TabIndex = 15;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // EquipmentAssignmentPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_endDate);
            this.Controls.Add(this.label_endDate);
            this.Controls.Add(this.textBox_startDate);
            this.Controls.Add(this.label_startDate);
            this.Controls.Add(this.comboBox_equipment);
            this.Controls.Add(this.label_equipment);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.textBox_equipmentAssignmentId);
            this.Controls.Add(this.label_equipmentAssignmentId);
            this.Controls.Add(this.dataGridView_assignments);
            this.Controls.Add(this.label_title);
            this.Name = "EquipmentAssignmentPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_assignments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_assignments;
        private System.Windows.Forms.Label label_equipmentAssignmentId;
        private System.Windows.Forms.TextBox textBox_equipmentAssignmentId;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Label label_equipment;
        private System.Windows.Forms.ComboBox comboBox_equipment;
        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.TextBox textBox_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.TextBox textBox_endDate;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
