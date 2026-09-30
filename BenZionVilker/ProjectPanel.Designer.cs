namespace BenZionVilker
{
    partial class ProjectPanel
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
            this.dataGridView_projects = new System.Windows.Forms.DataGridView();
            this.label_projectId = new System.Windows.Forms.Label();
            this.textBox_projectId = new System.Windows.Forms.TextBox();
            this.label_tender = new System.Windows.Forms.Label();
            this.comboBox_tender = new System.Windows.Forms.ComboBox();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_address = new System.Windows.Forms.Label();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.label_plannedStartDate = new System.Windows.Forms.Label();
            this.textBox_plannedStartDate = new System.Windows.Forms.TextBox();
            this.label_plannedEndDate = new System.Windows.Forms.Label();
            this.textBox_plannedEndDate = new System.Windows.Forms.TextBox();
            this.label_actualStartDate = new System.Windows.Forms.Label();
            this.textBox_actualStartDate = new System.Windows.Forms.TextBox();
            this.label_actualEndDate = new System.Windows.Forms.Label();
            this.textBox_actualEndDate = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.label_projectManager = new System.Windows.Forms.Label();
            this.comboBox_projectManager = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_projects)).BeginInit();
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
            this.label_title.Text = "ניהול פרויקטים";
            //
            // dataGridView_projects
            //
            this.dataGridView_projects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_projects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_projects.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_projects.Name = "dataGridView_projects";
            this.dataGridView_projects.ReadOnly = true;
            this.dataGridView_projects.RowTemplate.Height = 24;
            this.dataGridView_projects.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_projects.TabIndex = 1;
            this.dataGridView_projects.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_projects_CellClick);
            //
            // label_projectId (col A)
            //
            this.label_projectId.AutoSize = true;
            this.label_projectId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_projectId.Location = new System.Drawing.Point(820, 260);
            this.label_projectId.Name = "label_projectId";
            this.label_projectId.Size = new System.Drawing.Size(80, 17);
            this.label_projectId.TabIndex = 2;
            this.label_projectId.Text = "מזהה פרויקט";
            //
            // textBox_projectId
            //
            this.textBox_projectId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_projectId.Location = new System.Drawing.Point(550, 257);
            this.textBox_projectId.Name = "textBox_projectId";
            this.textBox_projectId.ReadOnly = true;
            this.textBox_projectId.Size = new System.Drawing.Size(250, 25);
            this.textBox_projectId.TabIndex = 3;
            this.textBox_projectId.TabStop = false;
            //
            // label_tender (col A)
            //
            this.label_tender.AutoSize = true;
            this.label_tender.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tender.Location = new System.Drawing.Point(820, 295);
            this.label_tender.Name = "label_tender";
            this.label_tender.Size = new System.Drawing.Size(48, 17);
            this.label_tender.TabIndex = 4;
            this.label_tender.Text = "מכרז";
            //
            // comboBox_tender
            //
            this.comboBox_tender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tender.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_tender.FormattingEnabled = true;
            this.comboBox_tender.Location = new System.Drawing.Point(550, 292);
            this.comboBox_tender.Name = "comboBox_tender";
            this.comboBox_tender.Size = new System.Drawing.Size(250, 25);
            this.comboBox_tender.TabIndex = 5;
            //
            // label_name (col A)
            //
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_name.Location = new System.Drawing.Point(820, 330);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(76, 17);
            this.label_name.TabIndex = 6;
            this.label_name.Text = "שם פרויקט";
            //
            // textBox_name
            //
            this.textBox_name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_name.Location = new System.Drawing.Point(550, 327);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(250, 25);
            this.textBox_name.TabIndex = 7;
            //
            // label_address (col A)
            //
            this.label_address.AutoSize = true;
            this.label_address.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_address.Location = new System.Drawing.Point(820, 365);
            this.label_address.Name = "label_address";
            this.label_address.Size = new System.Drawing.Size(56, 17);
            this.label_address.TabIndex = 8;
            this.label_address.Text = "כתובת";
            //
            // textBox_address
            //
            this.textBox_address.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_address.Location = new System.Drawing.Point(550, 362);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(250, 25);
            this.textBox_address.TabIndex = 9;
            //
            // label_plannedStartDate (col A)
            //
            this.label_plannedStartDate.AutoSize = true;
            this.label_plannedStartDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_plannedStartDate.Location = new System.Drawing.Point(820, 400);
            this.label_plannedStartDate.Name = "label_plannedStartDate";
            this.label_plannedStartDate.Size = new System.Drawing.Size(120, 17);
            this.label_plannedStartDate.TabIndex = 10;
            this.label_plannedStartDate.Text = "תאריך התחלה מתוכנן";
            //
            // textBox_plannedStartDate
            //
            this.textBox_plannedStartDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_plannedStartDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_plannedStartDate.Name = "textBox_plannedStartDate";
            this.textBox_plannedStartDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_plannedStartDate.TabIndex = 11;
            //
            // label_plannedEndDate (col B)
            //
            this.label_plannedEndDate.AutoSize = true;
            this.label_plannedEndDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_plannedEndDate.Location = new System.Drawing.Point(430, 260);
            this.label_plannedEndDate.Name = "label_plannedEndDate";
            this.label_plannedEndDate.Size = new System.Drawing.Size(120, 17);
            this.label_plannedEndDate.TabIndex = 12;
            this.label_plannedEndDate.Text = "תאריך סיום מתוכנן";
            //
            // textBox_plannedEndDate
            //
            this.textBox_plannedEndDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_plannedEndDate.Location = new System.Drawing.Point(190, 257);
            this.textBox_plannedEndDate.Name = "textBox_plannedEndDate";
            this.textBox_plannedEndDate.Size = new System.Drawing.Size(230, 25);
            this.textBox_plannedEndDate.TabIndex = 13;
            //
            // label_actualStartDate (col B)
            //
            this.label_actualStartDate.AutoSize = true;
            this.label_actualStartDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_actualStartDate.Location = new System.Drawing.Point(430, 295);
            this.label_actualStartDate.Name = "label_actualStartDate";
            this.label_actualStartDate.Size = new System.Drawing.Size(110, 17);
            this.label_actualStartDate.TabIndex = 14;
            this.label_actualStartDate.Text = "תאריך התחלה בפועל";
            //
            // textBox_actualStartDate
            //
            this.textBox_actualStartDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_actualStartDate.Location = new System.Drawing.Point(190, 292);
            this.textBox_actualStartDate.Name = "textBox_actualStartDate";
            this.textBox_actualStartDate.Size = new System.Drawing.Size(230, 25);
            this.textBox_actualStartDate.TabIndex = 15;
            //
            // label_actualEndDate (col B)
            //
            this.label_actualEndDate.AutoSize = true;
            this.label_actualEndDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_actualEndDate.Location = new System.Drawing.Point(430, 330);
            this.label_actualEndDate.Name = "label_actualEndDate";
            this.label_actualEndDate.Size = new System.Drawing.Size(100, 17);
            this.label_actualEndDate.TabIndex = 16;
            this.label_actualEndDate.Text = "תאריך סיום בפועל";
            //
            // textBox_actualEndDate
            //
            this.textBox_actualEndDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_actualEndDate.Location = new System.Drawing.Point(190, 327);
            this.textBox_actualEndDate.Name = "textBox_actualEndDate";
            this.textBox_actualEndDate.Size = new System.Drawing.Size(230, 25);
            this.textBox_actualEndDate.TabIndex = 17;
            //
            // label_status (col B)
            //
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_status.Location = new System.Drawing.Point(430, 365);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(48, 17);
            this.label_status.TabIndex = 18;
            this.label_status.Text = "סטטוס";
            //
            // comboBox_status
            //
            this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Location = new System.Drawing.Point(190, 362);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(230, 25);
            this.comboBox_status.TabIndex = 19;
            //
            // label_projectManager (col B)
            //
            this.label_projectManager.AutoSize = true;
            this.label_projectManager.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_projectManager.Location = new System.Drawing.Point(430, 400);
            this.label_projectManager.Name = "label_projectManager";
            this.label_projectManager.Size = new System.Drawing.Size(90, 17);
            this.label_projectManager.TabIndex = 20;
            this.label_projectManager.Text = "מנהל פרויקט";
            //
            // comboBox_projectManager
            //
            this.comboBox_projectManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_projectManager.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_projectManager.FormattingEnabled = true;
            this.comboBox_projectManager.Location = new System.Drawing.Point(190, 397);
            this.comboBox_projectManager.Name = "comboBox_projectManager";
            this.comboBox_projectManager.Size = new System.Drawing.Size(230, 25);
            this.comboBox_projectManager.TabIndex = 21;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 22;
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
            this.button_update.TabIndex = 23;
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
            this.button_delete.TabIndex = 24;
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
            this.button_back.TabIndex = 25;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // ProjectPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_projectManager);
            this.Controls.Add(this.label_projectManager);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_actualEndDate);
            this.Controls.Add(this.label_actualEndDate);
            this.Controls.Add(this.textBox_actualStartDate);
            this.Controls.Add(this.label_actualStartDate);
            this.Controls.Add(this.textBox_plannedEndDate);
            this.Controls.Add(this.label_plannedEndDate);
            this.Controls.Add(this.textBox_plannedStartDate);
            this.Controls.Add(this.label_plannedStartDate);
            this.Controls.Add(this.textBox_address);
            this.Controls.Add(this.label_address);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.comboBox_tender);
            this.Controls.Add(this.label_tender);
            this.Controls.Add(this.textBox_projectId);
            this.Controls.Add(this.label_projectId);
            this.Controls.Add(this.dataGridView_projects);
            this.Controls.Add(this.label_title);
            this.Name = "ProjectPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_projects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_projects;
        private System.Windows.Forms.Label label_projectId;
        private System.Windows.Forms.TextBox textBox_projectId;
        private System.Windows.Forms.Label label_tender;
        private System.Windows.Forms.ComboBox comboBox_tender;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_address;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.Label label_plannedStartDate;
        private System.Windows.Forms.TextBox textBox_plannedStartDate;
        private System.Windows.Forms.Label label_plannedEndDate;
        private System.Windows.Forms.TextBox textBox_plannedEndDate;
        private System.Windows.Forms.Label label_actualStartDate;
        private System.Windows.Forms.TextBox textBox_actualStartDate;
        private System.Windows.Forms.Label label_actualEndDate;
        private System.Windows.Forms.TextBox textBox_actualEndDate;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Label label_projectManager;
        private System.Windows.Forms.ComboBox comboBox_projectManager;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
