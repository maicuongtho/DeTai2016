namespace NTU.Webgen
{
    partial class UploadDownload
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
            this.btnUp = new DevComponents.DotNetBar.ButtonX();
            this.btnDown = new DevComponents.DotNetBar.ButtonX();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUp.Location = new System.Drawing.Point(52, 42);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(180, 38);
            this.btnUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Upload";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDown.Location = new System.Drawing.Point(289, 42);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(180, 38);
            this.btnDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDown.TabIndex = 0;
            this.btnDown.Text = "Download";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(55, 100);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 44);
            this.progressBar1.TabIndex = 1;
            // 
            // UploadDownload
            // 
            this.ClientSize = new System.Drawing.Size(552, 151);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.DoubleBuffered = true;
            this.Name = "UploadDownload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UploadDownload";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnUp;
        private DevComponents.DotNetBar.ButtonX btnDown;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}