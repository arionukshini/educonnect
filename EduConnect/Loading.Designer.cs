namespace EduConnect
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.loadinglogo = new System.Windows.Forms.PictureBox();
            this.loadingbar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.loadinglogo)).BeginInit();
            this.SuspendLayout();
            // 
            // loadinglogo
            // 
            this.loadinglogo.BackColor = System.Drawing.Color.Transparent;
            this.loadinglogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadinglogo.BackgroundImage")));
            this.loadinglogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loadinglogo.Location = new System.Drawing.Point(12, 84);
            this.loadinglogo.Name = "loadinglogo";
            this.loadinglogo.Size = new System.Drawing.Size(726, 230);
            this.loadinglogo.TabIndex = 0;
            this.loadinglogo.TabStop = false;
            // 
            // loadingbar
            // 
            this.loadingbar.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.loadingbar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingbar.Location = new System.Drawing.Point(-2, 379);
            this.loadingbar.Name = "loadingbar";
            this.loadingbar.Size = new System.Drawing.Size(755, 20);
            this.loadingbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.loadingbar.TabIndex = 1;
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 400);
            this.ControlBox = false;
            this.Controls.Add(this.loadingbar);
            this.Controls.Add(this.loadinglogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.loadinglogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox loadinglogo;
        private System.Windows.Forms.ProgressBar loadingbar;
    }
}