namespace BenZionVilker
{
    partial class BudgetLinePanel
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
            this.dataGridView_budgetLines = new System.Windows.Forms.DataGridView();
            this.label_budgetLineId = new System.Windows.Forms.Label();
            this.textBox_budgetLineId = new System.Windows.Forms.TextBox();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.label_category = new System.Windows.Forms.Label();
            this.textBox_category = new System.Windows.Forms.TextBox();
            this.label_plannedAmount = new System.Windows.Forms.Label();
            this.textBox_plannedAmount = new System.Windows.Forms.TextBox();
            this.label_actualAmount = new System.Windows.Forms.Label();
            this.textBox_actualAmount = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_budgetLines)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(360, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(280, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול שורות תקציב";
            //
            // dataGridView_budgetLines
            //
            this.dataGridView_budgetLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_budgetLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_budgetLines.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_budgetLines.Name = "dataGridView_budgetLines";
            this.dataGridView_budgetLines.ReadOnly = true;
            this.dataGridView_budgetLines.RowTemplate.Height = 24;
            this.dataGridView_budgetLines.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_budgetLines.TabIndex = 1;
            this.dataGridView_budgetLines.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_budgetLines_CellClick);
            //
            // label_budgetLineId
            //
            this.label_budgetLineId.AutoSize = true;
            this.label_budgetLineId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_budgetLineId.Location = new System.Drawing.Point(820, 260);
            this.label_budgetLineId.Name = "label_budgetLineId";
            this.label_budgetLineId.Size = new System.Drawing.Size(90, 17);
            this.label_budgetLineId.TabIndex = 2;
            this.label_budgetLineId.Text = "מזהה שורה";
            //
            // textBox_budgetLineId
            //
            this.textBox_budgetLineId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_budgetLineId.Location = new System.Drawing.Point(550, 257);
            this.textBox_budgetLineId.Name = "textBox_budgetLineId";
            this.textBox_budgetLineId.ReadOnly = true;
            this.textBox_budgetLineId.Size = new System.Drawing.Size(250, 25);
            this.textBox_budgetLineId.TabIndex = 3;
            this.textBox_budgetLineId.TabStop = false;
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
            // label_category
            //
            this.label_category.AutoSize = true;
            this.label_category.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_category.Location = new System.Drawing.Point(820, 330);
            this.label_category.Name = "label_category";
            this.label_category.Size = new System.Drawing.Size(72, 17);
            this.label_category.TabIndex = 6;
            this.label_category.Text = "קטגוריה";
            //
            // textBox_category
            //
            this.textBox_category.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_category.Location = new System.Drawing.Point(550, 327);
            this.textBox_category.Name = "textBox_category";
            this.textBox_category.Size = new System.Drawing.Size(250, 25);
            this.textBox_category.TabIndex = 7;
            //
            // label_plannedAmount
            //
            this.label_plannedAmount.AutoSize = true;
            this.label_plannedAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_plannedAmount.Location = new System.Drawing.Point(820, 365);
            this.label_plannedAmount.Name = "label_plannedAmount";
            this.label_plannedAmount.Size = new System.Drawing.Size(90, 17);
            this.label_plannedAmount.TabIndex = 8;
            this.label_plannedAmount.Text = "סכום מתוכנן";
            //
            // textBox_plannedAmount
            //
            this.textBox_plannedAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_plannedAmount.Location = new System.Drawing.Point(550, 362);
            this.textBox_plannedAmount.Name = "textBox_plannedAmount";
            this.textBox_plannedAmount.Size = new System.Drawing.Size(250, 25);
            this.textBox_plannedAmount.TabIndex = 9;
            //
            // label_actualAmount
            //
            this.label_actualAmount.AutoSize = true;
            this.label_actualAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_actualAmount.Location = new System.Drawing.Point(820, 400);
            this.label_actualAmount.Name = "label_actualAmount";
            this.label_actualAmount.Size = new System.Drawing.Size(80, 17);
            this.label_actualAmount.TabIndex = 10;
            this.label_actualAmount.Text = "סכום בפועל";
            //
            // textBox_actualAmount
            //
            this.textBox_actualAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_actualAmount.Location = new System.Drawing.Point(550, 397);
            this.textBox_actualAmount.Name = "textBox_actualAmount";
            this.textBox_actualAmount.Size = new System.Drawing.Size(250, 25);
            this.textBox_actualAmount.TabIndex = 11;
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
            // BudgetLinePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_actualAmount);
            this.Controls.Add(this.label_actualAmount);
            this.Controls.Add(this.textBox_plannedAmount);
            this.Controls.Add(this.label_plannedAmount);
            this.Controls.Add(this.textBox_category);
            this.Controls.Add(this.label_category);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.textBox_budgetLineId);
            this.Controls.Add(this.label_budgetLineId);
            this.Controls.Add(this.dataGridView_budgetLines);
            this.Controls.Add(this.label_title);
            this.Name = "BudgetLinePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_budgetLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_budgetLines;
        private System.Windows.Forms.Label label_budgetLineId;
        private System.Windows.Forms.TextBox textBox_budgetLineId;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Label label_category;
        private System.Windows.Forms.TextBox textBox_category;
        private System.Windows.Forms.Label label_plannedAmount;
        private System.Windows.Forms.TextBox textBox_plannedAmount;
        private System.Windows.Forms.Label label_actualAmount;
        private System.Windows.Forms.TextBox textBox_actualAmount;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
