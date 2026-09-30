namespace BenZionVilker
{
    partial class EmployeePanel
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
            this.dataGridView_employees = new System.Windows.Forms.DataGridView();
            this.label_employeeId = new System.Windows.Forms.Label();
            this.textBox_employeeId = new System.Windows.Forms.TextBox();
            this.label_firstName = new System.Windows.Forms.Label();
            this.textBox_firstName = new System.Windows.Forms.TextBox();
            this.label_lastName = new System.Windows.Forms.Label();
            this.textBox_lastName = new System.Windows.Forms.TextBox();
            this.label_nationalId = new System.Windows.Forms.Label();
            this.textBox_nationalId = new System.Windows.Forms.TextBox();
            this.label_role = new System.Windows.Forms.Label();
            this.comboBox_role = new System.Windows.Forms.ComboBox();
            this.label_dailyRate = new System.Windows.Forms.Label();
            this.textBox_dailyRate = new System.Windows.Forms.TextBox();
            this.label_certificationNo = new System.Windows.Forms.Label();
            this.textBox_certificationNo = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_employees)).BeginInit();
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
            this.label_title.Text = "ניהול עובדים";
            //
            // dataGridView_employees
            //
            this.dataGridView_employees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_employees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_employees.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_employees.Name = "dataGridView_employees";
            this.dataGridView_employees.ReadOnly = true;
            this.dataGridView_employees.RowTemplate.Height = 24;
            this.dataGridView_employees.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_employees.TabIndex = 1;
            this.dataGridView_employees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_employees_CellClick);
            //
            // label_employeeId
            //
            this.label_employeeId.AutoSize = true;
            this.label_employeeId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_employeeId.Location = new System.Drawing.Point(820, 260);
            this.label_employeeId.Name = "label_employeeId";
            this.label_employeeId.Size = new System.Drawing.Size(80, 17);
            this.label_employeeId.TabIndex = 2;
            this.label_employeeId.Text = "מזהה עובד";
            //
            // textBox_employeeId
            //
            this.textBox_employeeId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_employeeId.Location = new System.Drawing.Point(550, 257);
            this.textBox_employeeId.Name = "textBox_employeeId";
            this.textBox_employeeId.ReadOnly = true;
            this.textBox_employeeId.Size = new System.Drawing.Size(250, 25);
            this.textBox_employeeId.TabIndex = 3;
            this.textBox_employeeId.TabStop = false;
            //
            // label_firstName
            //
            this.label_firstName.AutoSize = true;
            this.label_firstName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_firstName.Location = new System.Drawing.Point(820, 295);
            this.label_firstName.Name = "label_firstName";
            this.label_firstName.Size = new System.Drawing.Size(60, 17);
            this.label_firstName.TabIndex = 4;
            this.label_firstName.Text = "שם פרטי";
            //
            // textBox_firstName
            //
            this.textBox_firstName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_firstName.Location = new System.Drawing.Point(550, 292);
            this.textBox_firstName.Name = "textBox_firstName";
            this.textBox_firstName.Size = new System.Drawing.Size(250, 25);
            this.textBox_firstName.TabIndex = 5;
            //
            // label_lastName
            //
            this.label_lastName.AutoSize = true;
            this.label_lastName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_lastName.Location = new System.Drawing.Point(820, 330);
            this.label_lastName.Name = "label_lastName";
            this.label_lastName.Size = new System.Drawing.Size(76, 17);
            this.label_lastName.TabIndex = 6;
            this.label_lastName.Text = "שם משפחה";
            //
            // textBox_lastName
            //
            this.textBox_lastName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_lastName.Location = new System.Drawing.Point(550, 327);
            this.textBox_lastName.Name = "textBox_lastName";
            this.textBox_lastName.Size = new System.Drawing.Size(250, 25);
            this.textBox_lastName.TabIndex = 7;
            //
            // label_nationalId
            //
            this.label_nationalId.AutoSize = true;
            this.label_nationalId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_nationalId.Location = new System.Drawing.Point(820, 365);
            this.label_nationalId.Name = "label_nationalId";
            this.label_nationalId.Size = new System.Drawing.Size(84, 17);
            this.label_nationalId.TabIndex = 8;
            this.label_nationalId.Text = "תעודת זהות";
            //
            // textBox_nationalId
            //
            this.textBox_nationalId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_nationalId.Location = new System.Drawing.Point(550, 362);
            this.textBox_nationalId.Name = "textBox_nationalId";
            this.textBox_nationalId.Size = new System.Drawing.Size(250, 25);
            this.textBox_nationalId.TabIndex = 9;
            //
            // label_role
            //
            this.label_role.AutoSize = true;
            this.label_role.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_role.Location = new System.Drawing.Point(820, 400);
            this.label_role.Name = "label_role";
            this.label_role.Size = new System.Drawing.Size(48, 17);
            this.label_role.TabIndex = 10;
            this.label_role.Text = "תפקיד";
            //
            // comboBox_role
            //
            this.comboBox_role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_role.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_role.FormattingEnabled = true;
            this.comboBox_role.Location = new System.Drawing.Point(550, 397);
            this.comboBox_role.Name = "comboBox_role";
            this.comboBox_role.Size = new System.Drawing.Size(250, 25);
            this.comboBox_role.TabIndex = 11;
            //
            // label_dailyRate
            //
            this.label_dailyRate.AutoSize = true;
            this.label_dailyRate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dailyRate.Location = new System.Drawing.Point(820, 435);
            this.label_dailyRate.Name = "label_dailyRate";
            this.label_dailyRate.Size = new System.Drawing.Size(76, 17);
            this.label_dailyRate.TabIndex = 12;
            this.label_dailyRate.Text = "תעריף יומי";
            //
            // textBox_dailyRate
            //
            this.textBox_dailyRate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_dailyRate.Location = new System.Drawing.Point(550, 432);
            this.textBox_dailyRate.Name = "textBox_dailyRate";
            this.textBox_dailyRate.Size = new System.Drawing.Size(250, 25);
            this.textBox_dailyRate.TabIndex = 13;
            //
            // label_certificationNo
            //
            this.label_certificationNo.AutoSize = true;
            this.label_certificationNo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_certificationNo.Location = new System.Drawing.Point(820, 470);
            this.label_certificationNo.Name = "label_certificationNo";
            this.label_certificationNo.Size = new System.Drawing.Size(92, 17);
            this.label_certificationNo.TabIndex = 14;
            this.label_certificationNo.Text = "מספר הסמכה";
            //
            // textBox_certificationNo
            //
            this.textBox_certificationNo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_certificationNo.Location = new System.Drawing.Point(550, 467);
            this.textBox_certificationNo.Name = "textBox_certificationNo";
            this.textBox_certificationNo.Size = new System.Drawing.Size(250, 25);
            this.textBox_certificationNo.TabIndex = 15;
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
            // EmployeePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_certificationNo);
            this.Controls.Add(this.label_certificationNo);
            this.Controls.Add(this.textBox_dailyRate);
            this.Controls.Add(this.label_dailyRate);
            this.Controls.Add(this.comboBox_role);
            this.Controls.Add(this.label_role);
            this.Controls.Add(this.textBox_nationalId);
            this.Controls.Add(this.label_nationalId);
            this.Controls.Add(this.textBox_lastName);
            this.Controls.Add(this.label_lastName);
            this.Controls.Add(this.textBox_firstName);
            this.Controls.Add(this.label_firstName);
            this.Controls.Add(this.textBox_employeeId);
            this.Controls.Add(this.label_employeeId);
            this.Controls.Add(this.dataGridView_employees);
            this.Controls.Add(this.label_title);
            this.Name = "EmployeePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_employees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_employees;
        private System.Windows.Forms.Label label_employeeId;
        private System.Windows.Forms.TextBox textBox_employeeId;
        private System.Windows.Forms.Label label_firstName;
        private System.Windows.Forms.TextBox textBox_firstName;
        private System.Windows.Forms.Label label_lastName;
        private System.Windows.Forms.TextBox textBox_lastName;
        private System.Windows.Forms.Label label_nationalId;
        private System.Windows.Forms.TextBox textBox_nationalId;
        private System.Windows.Forms.Label label_role;
        private System.Windows.Forms.ComboBox comboBox_role;
        private System.Windows.Forms.Label label_dailyRate;
        private System.Windows.Forms.TextBox textBox_dailyRate;
        private System.Windows.Forms.Label label_certificationNo;
        private System.Windows.Forms.TextBox textBox_certificationNo;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
