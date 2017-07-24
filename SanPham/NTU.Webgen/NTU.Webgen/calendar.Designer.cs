namespace NTU.Webgen
{
    partial class calendar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.htmlEditor1 = new WinHtmlEditor.HtmlEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.balloonTipAnhDaiDien = new DevComponents.DotNetBar.BalloonTip();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtURLLich = new System.Windows.Forms.TextBox();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btnSua = new DevComponents.DotNetBar.ButtonX();
            this.btnXuatWeb = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nội dung";
            // 
            // htmlEditor1
            // 
            this.htmlEditor1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.htmlEditor1.BodyInnerHTML = null;
            this.htmlEditor1.BodyInnerText = null;
            this.htmlEditor1.EnterToBR = false;
            this.htmlEditor1.FontSize = WinHtmlEditor.FontSize.Three;
            this.htmlEditor1.Location = new System.Drawing.Point(71, 20);
            this.htmlEditor1.Name = "htmlEditor1";
            this.htmlEditor1.ShowStatusBar = false;
            this.htmlEditor1.ShowToolBar = true;
            this.htmlEditor1.ShowWb = true;
            this.htmlEditor1.Size = new System.Drawing.Size(838, 360);
            this.htmlEditor1.TabIndex = 1;
            this.htmlEditor1.WebBrowserShortcutsEnabled = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Địa chỉ lịch:";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(453, 57);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(139, 51);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "Chưa biết tạo lịch, mời xem hướng dẫn";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.pictureBox1);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1001, 580);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NTU.Webgen.Properties.Resources.google_calendar_2;
            this.pictureBox1.Location = new System.Drawing.Point(660, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(307, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.htmlEditor1);
            this.groupBox2.Location = new System.Drawing.Point(44, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(915, 386);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lịch công tác ( tự biên tập)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHelp);
            this.groupBox1.Controls.Add(this.txtURLLich);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(44, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 116);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lịch Google Calendar";
            // 
            // txtURLLich
            // 
            this.txtURLLich.Location = new System.Drawing.Point(116, 31);
            this.txtURLLich.Name = "txtURLLich";
            this.txtURLLich.Size = new System.Drawing.Size(476, 20);
            this.txtURLLich.TabIndex = 3;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btnSua);
            this.panelEx3.Controls.Add(this.btnXuatWeb);
            this.panelEx3.Controls.Add(this.btnSave);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1001, 35);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 18;
            // 
            // btnSua
            // 
            this.btnSua.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSua.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSua.Image = global::NTU.Webgen.Properties.Resources.Edit_16x16;
            this.btnSua.Location = new System.Drawing.Point(44, 7);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(101, 24);
            this.btnSua.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2000;
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa dữ liệu";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXuatWeb
            // 
            this.btnXuatWeb.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXuatWeb.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXuatWeb.Image = global::NTU.Webgen.Properties.Resources._1491336195_domain_template;
            this.btnXuatWeb.Location = new System.Drawing.Point(258, 7);
            this.btnXuatWeb.Name = "btnXuatWeb";
            this.btnXuatWeb.Size = new System.Drawing.Size(159, 24);
            this.btnXuatWeb.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2000;
            this.btnXuatWeb.TabIndex = 15;
            this.btnXuatWeb.Text = "Xuất sang trang web";
            this.btnXuatWeb.Click += new System.EventHandler(this.btnXuatWeb_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Image = global::NTU.Webgen.Properties.Resources.Save_16x16;
            this.btnSave.Location = new System.Drawing.Point(151, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 24);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2000;
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "calendar";
            this.Size = new System.Drawing.Size(1001, 580);
            this.Load += new System.EventHandler(this.calendar_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private WinHtmlEditor.HtmlEditor htmlEditor1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.BalloonTip balloonTipAnhDaiDien;
        private DevComponents.DotNetBar.ButtonX btnSua;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnXuatWeb;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtURLLich;
    }
}
