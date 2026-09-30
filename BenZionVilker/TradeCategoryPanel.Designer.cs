namespace BenZionVilker
{
    partial class TradeCategoryPanel
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
            this.dataGridView_tradeCategories = new System.Windows.Forms.DataGridView();
            this.label_tradeCategoryId = new System.Windows.Forms.Label();
            this.textBox_tradeCategoryId = new System.Windows.Forms.TextBox();
            this.label_categoryName = new System.Windows.Forms.Label();
            this.textBox_categoryName = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tradeCategories)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(350, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(300, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול תחומי עיסוק";
            //
            // dataGridView_tradeCategories
            //
            this.dataGridView_tradeCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_tradeCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_tradeCategories.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_tradeCategories.Name = "dataGridView_tradeCategories";
            this.dataGridView_tradeCategories.ReadOnly = true;
            this.dataGridView_tradeCategories.RowTemplate.Height = 24;
            this.dataGridView_tradeCategories.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_tradeCategories.TabIndex = 1;
            this.dataGridView_tradeCategories.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_tradeCategories_CellClick);
            //
            // label_tradeCategoryId
            //
            this.label_tradeCategoryId.AutoSize = true;
            this.label_tradeCategoryId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tradeCategoryId.Location = new System.Drawing.Point(820, 260);
            this.label_tradeCategoryId.Name = "label_tradeCategoryId";
            this.label_tradeCategoryId.Size = new System.Drawing.Size(80, 17);
            this.label_tradeCategoryId.TabIndex = 2;
            this.label_tradeCategoryId.Text = "מזהה תחום";
            //
            // textBox_tradeCategoryId
            //
            this.textBox_tradeCategoryId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_tradeCategoryId.Location = new System.Drawing.Point(550, 257);
            this.textBox_tradeCategoryId.Name = "textBox_tradeCategoryId";
            this.textBox_tradeCategoryId.ReadOnly = true;
            this.textBox_tradeCategoryId.Size = new System.Drawing.Size(250, 25);
            this.textBox_tradeCategoryId.TabIndex = 3;
            this.textBox_tradeCategoryId.TabStop = false;
            //
            // label_categoryName
            //
            this.label_categoryName.AutoSize = true;
            this.label_categoryName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_categoryName.Location = new System.Drawing.Point(820, 295);
            this.label_categoryName.Name = "label_categoryName";
            this.label_categoryName.Size = new System.Drawing.Size(70, 17);
            this.label_categoryName.TabIndex = 4;
            this.label_categoryName.Text = "שם תחום";
            //
            // textBox_categoryName
            //
            this.textBox_categoryName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_categoryName.Location = new System.Drawing.Point(550, 292);
            this.textBox_categoryName.Name = "textBox_categoryName";
            this.textBox_categoryName.Size = new System.Drawing.Size(250, 25);
            this.textBox_categoryName.TabIndex = 5;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 6;
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
            this.button_update.TabIndex = 7;
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
            this.button_delete.TabIndex = 8;
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
            this.button_back.TabIndex = 9;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // TradeCategoryPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_categoryName);
            this.Controls.Add(this.label_categoryName);
            this.Controls.Add(this.textBox_tradeCategoryId);
            this.Controls.Add(this.label_tradeCategoryId);
            this.Controls.Add(this.dataGridView_tradeCategories);
            this.Controls.Add(this.label_title);
            this.Name = "TradeCategoryPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tradeCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_tradeCategories;
        private System.Windows.Forms.Label label_tradeCategoryId;
        private System.Windows.Forms.TextBox textBox_tradeCategoryId;
        private System.Windows.Forms.Label label_categoryName;
        private System.Windows.Forms.TextBox textBox_categoryName;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}
