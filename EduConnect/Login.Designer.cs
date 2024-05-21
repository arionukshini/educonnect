namespace EduConnect
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loginemail = new System.Windows.Forms.Label();
            this.loginpassword = new System.Windows.Forms.Label();
            this.loginemailBox = new System.Windows.Forms.TextBox();
            this.loginpasswordBox = new System.Windows.Forms.TextBox();
            this.loginsignupBtn = new System.Windows.Forms.Button();
            this.loginBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(215, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 112);
            this.label1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(234, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(389, 184);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // loginemail
            // 
            this.loginemail.AutoSize = true;
            this.loginemail.BackColor = System.Drawing.Color.Transparent;
            this.loginemail.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginemail.ForeColor = System.Drawing.Color.White;
            this.loginemail.Location = new System.Drawing.Point(226, 235);
            this.loginemail.Name = "loginemail";
            this.loginemail.Size = new System.Drawing.Size(105, 47);
            this.loginemail.TabIndex = 2;
            this.loginemail.Text = "Email:";
            // 
            // loginpassword
            // 
            this.loginpassword.AutoSize = true;
            this.loginpassword.BackColor = System.Drawing.Color.Transparent;
            this.loginpassword.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginpassword.ForeColor = System.Drawing.Color.White;
            this.loginpassword.Location = new System.Drawing.Point(226, 308);
            this.loginpassword.Name = "loginpassword";
            this.loginpassword.Size = new System.Drawing.Size(156, 47);
            this.loginpassword.TabIndex = 3;
            this.loginpassword.Text = "Password:";
            // 
            // loginemailBox
            // 
            this.loginemailBox.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginemailBox.Location = new System.Drawing.Point(433, 249);
            this.loginemailBox.Name = "loginemailBox";
            this.loginemailBox.Size = new System.Drawing.Size(190, 30);
            this.loginemailBox.TabIndex = 4;
            // 
            // loginpasswordBox
            // 
            this.loginpasswordBox.BackColor = System.Drawing.SystemColors.Window;
            this.loginpasswordBox.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginpasswordBox.Location = new System.Drawing.Point(433, 322);
            this.loginpasswordBox.Name = "loginpasswordBox";
            this.loginpasswordBox.Size = new System.Drawing.Size(190, 30);
            this.loginpasswordBox.TabIndex = 5;
            this.loginpasswordBox.UseSystemPasswordChar = true;
            this.loginpasswordBox.MouseLeave += new System.EventHandler(this.loginpasswordBox_MouseLeave);
            this.loginpasswordBox.MouseHover += new System.EventHandler(this.loginpasswordBox_MouseHover);
            // 
            // loginsignupBtn
            // 
            this.loginsignupBtn.BackColor = System.Drawing.Color.White;
            this.loginsignupBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginsignupBtn.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginsignupBtn.Location = new System.Drawing.Point(697, 12);
            this.loginsignupBtn.Name = "loginsignupBtn";
            this.loginsignupBtn.Size = new System.Drawing.Size(125, 35);
            this.loginsignupBtn.TabIndex = 6;
            this.loginsignupBtn.Text = "Sign Up";
            this.loginsignupBtn.UseVisualStyleBackColor = false;
            this.loginsignupBtn.Click += new System.EventHandler(this.loginsignupBtn_Click);
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.White;
            this.loginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBtn.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.Location = new System.Drawing.Point(357, 387);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(125, 35);
            this.loginBtn.TabIndex = 7;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = false;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.loginsignupBtn);
            this.Controls.Add(this.loginpasswordBox);
            this.Controls.Add(this.loginemailBox);
            this.Controls.Add(this.loginpassword);
            this.Controls.Add(this.loginemail);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label loginemail;
        private System.Windows.Forms.Label loginpassword;
        private System.Windows.Forms.TextBox loginemailBox;
        private System.Windows.Forms.TextBox loginpasswordBox;
        private System.Windows.Forms.Button loginsignupBtn;
        private System.Windows.Forms.Button loginBtn;
    }
}