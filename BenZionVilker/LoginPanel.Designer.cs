namespace BenZionVilker
{
    partial class LoginPanel
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
            this.label_subtitle = new System.Windows.Forms.Label();
            this.label_employeeId = new System.Windows.Forms.Label();
            this.textBox_employeeId = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(400, 130);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(200, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "כניסה למערכת";
            //
            // label_subtitle
            //
            this.label_subtitle.AutoSize = true;
            this.label_subtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label_subtitle.Location = new System.Drawing.Point(330, 185);
            this.label_subtitle.Name = "label_subtitle";
            this.label_subtitle.Size = new System.Drawing.Size(340, 20);
            this.label_subtitle.TabIndex = 1;
            this.label_subtitle.Text = "בן ציון וילקר (1987) בע\"מ — ניהול פרויקטים";
            //
            // label_employeeId
            //
            this.label_employeeId.AutoSize = true;
            this.label_employeeId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_employeeId.Location = new System.Drawing.Point(600, 290);
            this.label_employeeId.Name = "label_employeeId";
            this.label_employeeId.Size = new System.Drawing.Size(70, 17);
            this.label_employeeId.TabIndex = 2;
            this.label_employeeId.Text = "מספר עובד";
            //
            // textBox_employeeId
            //
            this.textBox_employeeId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_employeeId.Location = new System.Drawing.Point(350, 287);
            this.textBox_employeeId.Name = "textBox_employeeId";
            this.textBox_employeeId.Size = new System.Drawing.Size(250, 25);
            this.textBox_employeeId.TabIndex = 3;
            //
            // label_password
            //
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_password.Location = new System.Drawing.Point(600, 340);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(52, 17);
            this.label_password.TabIndex = 4;
            this.label_password.Text = "סיסמה";
            //
            // textBox_password
            //
            this.textBox_password.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_password.Location = new System.Drawing.Point(350, 337);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(250, 25);
            this.textBox_password.TabIndex = 5;
            this.textBox_password.UseSystemPasswordChar = true;
            //
            // button_login
            //
            this.button_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_login.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_login.Location = new System.Drawing.Point(400, 400);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(200, 42);
            this.button_login.TabIndex = 6;
            this.button_login.Text = "כניסה";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            //
            // LoginPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_employeeId);
            this.Controls.Add(this.label_employeeId);
            this.Controls.Add(this.label_subtitle);
            this.Controls.Add(this.label_title);
            this.Name = "LoginPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_subtitle;
        private System.Windows.Forms.Label label_employeeId;
        private System.Windows.Forms.TextBox textBox_employeeId;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_login;
    }
}
