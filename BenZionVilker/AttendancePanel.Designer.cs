namespace BenZionVilker
{
    partial class AttendancePanel
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
            this.dataGridView_attendances = new System.Windows.Forms.DataGridView();
            this.label_attendanceId = new System.Windows.Forms.Label();
            this.textBox_attendanceId = new System.Windows.Forms.TextBox();
            this.label_employee = new System.Windows.Forms.Label();
            this.comboBox_employee = new System.Windows.Forms.ComboBox();
            this.label_dailyWorkLog = new System.Windows.Forms.Label();
            this.comboBox_dailyWorkLog = new System.Windows.Forms.ComboBox();
            this.label_startTime = new System.Windows.Forms.Label();
            this.textBox_startTime = new System.Windows.Forms.TextBox();
            this.label_endTime = new System.Windows.Forms.Label();
            this.textBox_endTime = new System.Windows.Forms.TextBox();
            this.label_taskDescription = new System.Windows.Forms.Label();
            this.textBox_taskDescription = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_attendances)).BeginInit();
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
            this.label_title.Text = "ניהול נוכחות עובדים";
            //
            // dataGridView_attendances
            //
            this.dataGridView_attendances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_attendances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_attendances.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_attendances.Name = "dataGridView_attendances";
            this.dataGridView_attendances.ReadOnly = true;
            this.dataGridView_attendances.RowTemplate.Height = 24;
            this.dataGridView_attendances.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_attendances.TabIndex = 1;
            this.dataGridView_attendances.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_attendances_CellClick);
            //
            // label_attendanceId
            //
            this.label_attendanceId.AutoSize = true;
            this.label_attendanceId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_attendanceId.Location = new System.Drawing.Point(820, 260);
            this.label_attendanceId.Name = "label_attendanceId";
            this.label_attendanceId.Size = new System.Drawing.Size(60, 17);
            this.label_attendanceId.TabIndex = 2;
            this.label_attendanceId.Text = "מזהה";
            //
            // textBox_attendanceId
            //
            this.textBox_attendanceId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_attendanceId.Location = new System.Drawing.Point(550, 257);
            this.textBox_attendanceId.Name = "textBox_attendanceId";
            this.textBox_attendanceId.ReadOnly = true;
            this.textBox_attendanceId.Size = new System.Drawing.Size(250, 25);
            this.textBox_attendanceId.TabIndex = 3;
            this.textBox_attendanceId.TabStop = false;
            //
            // label_employee
            //
            this.label_employee.AutoSize = true;
            this.label_employee.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_employee.Location = new System.Drawing.Point(820, 295);
            this.label_employee.Name = "label_employee";
            this.label_employee.Size = new System.Drawing.Size(48, 17);
            this.label_employee.TabIndex = 4;
            this.label_employee.Text = "עובד";
            //
            // comboBox_employee
            //
            this.comboBox_employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_employee.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_employee.FormattingEnabled = true;
            this.comboBox_employee.Location = new System.Drawing.Point(550, 292);
            this.comboBox_employee.Name = "comboBox_employee";
            this.comboBox_employee.Size = new System.Drawing.Size(250, 25);
            this.comboBox_employee.TabIndex = 5;
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
            // label_startTime
            //
            this.label_startTime.AutoSize = true;
            this.label_startTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_startTime.Location = new System.Drawing.Point(820, 365);
            this.label_startTime.Name = "label_startTime";
            this.label_startTime.Size = new System.Drawing.Size(80, 17);
            this.label_startTime.TabIndex = 8;
            this.label_startTime.Text = "שעת התחלה";
            //
            // textBox_startTime
            //
            this.textBox_startTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_startTime.Location = new System.Drawing.Point(550, 362);
            this.textBox_startTime.Name = "textBox_startTime";
            this.textBox_startTime.Size = new System.Drawing.Size(250, 25);
            this.textBox_startTime.TabIndex = 9;
            //
            // label_endTime
            //
            this.label_endTime.AutoSize = true;
            this.label_endTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_endTime.Location = new System.Drawing.Point(820, 400);
            this.label_endTime.Name = "label_endTime";
            this.label_endTime.Size = new System.Drawing.Size(80, 17);
            this.label_endTime.TabIndex = 10;
            this.label_endTime.Text = "שעת סיום";
            //
            // textBox_endTime
            //
            this.textBox_endTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_endTime.Location = new System.Drawing.Point(550, 397);
            this.textBox_endTime.Name = "textBox_endTime";
            this.textBox_endTime.Size = new System.Drawing.Size(250, 25);
            this.textBox_endTime.TabIndex = 11;
            //
            // label_taskDescription
            //
            this.label_taskDescription.AutoSize = true;
            this.label_taskDescription.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_taskDescription.Location = new System.Drawing.Point(820, 435);
            this.label_taskDescription.Name = "label_taskDescription";
            this.label_taskDescription.Size = new System.Drawing.Size(80, 17);
            this.label_taskDescription.TabIndex = 12;
            this.label_taskDescription.Text = "תיאור משימה";
            //
            // textBox_taskDescription
            //
            this.textBox_taskDescription.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_taskDescription.Location = new System.Drawing.Point(550, 432);
            this.textBox_taskDescription.Name = "textBox_taskDescription";
            this.textBox_taskDescription.Size = new System.Drawing.Size(250, 25);
            this.textBox_taskDescription.TabIndex = 13;
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
            // AttendancePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_taskDescription);
            this.Controls.Add(this.label_taskDescription);
            this.Controls.Add(this.textBox_endTime);
            this.Controls.Add(this.label_endTime);
            this.Controls.Add(this.textBox_startTime);
            this.Controls.Add(this.label_startTime);
            this.Controls.Add(this.comboBox_dailyWorkLog);
            this.Controls.Add(this.label_dailyWorkLog);
            this.Controls.Add(this.comboBox_employee);
            this.Controls.Add(this.label_employee);
            this.Controls.Add(this.textBox_attendanceId);
            this.Controls.Add(this.label_attendanceId);
            this.Controls.Add(this.dataGridView_attendances);
            this.Controls.Add(this.label_title);
            this.Name = "AttendancePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 685);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_attendances)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_attendances;
        private System.Windows.Forms.Label label_attendanceId;
        private System.Windows.Forms.TextBox textBox_attendanceId;
        private System.Windows.Forms.Label label_employee;
        private System.Windows.Forms.ComboBox comboBox_employee;
        private System.Windows.Forms.Label label_dailyWorkLog;
        private System.Windows.Forms.ComboBox comboBox_dailyWorkLog;
        private System.Windows.Forms.Label label_startTime;
        private System.Windows.Forms.TextBox textBox_startTime;
        private System.Windows.Forms.Label label_endTime;
        private System.Windows.Forms.TextBox textBox_endTime;
        private System.Windows.Forms.Label label_taskDescription;
        private System.Windows.Forms.TextBox textBox_taskDescription;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
