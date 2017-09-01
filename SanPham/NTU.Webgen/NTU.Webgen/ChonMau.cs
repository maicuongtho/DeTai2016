using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using DevComponents.DotNetBar;


namespace NTU.Webgen
{
    public partial class ChonMau : UserControl
    {
      
        String TempPath; 
        String webPath;
        String thumucGoc;
        String thumucChon;
        public String projectFolder;
        ChuongTrinh chuongtrinhChinh;

        public ChonMau(ChuongTrinh c)
        {
            InitializeComponent();
            thumucGoc = System.AppDomain.CurrentDomain.BaseDirectory;
            TempPath = thumucGoc+"Templates\\";
            webPath = TempPath.Replace("\\", "/");
            chuongtrinhChinh = c;
            
        }

        private void linkLabel0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(0);
        }

        void ReSizeGroup() {

            int ws = SystemInformation.VirtualScreen.Width;
            int hs = SystemInformation.VirtualScreen.Height;
            int h = hs / 3 - 50;
            groupPanel1.Height = h;
            groupPanel2.Height = h; 
            groupPanel3.Height = h;
            groupPanel4.Height = h;
            groupPanel5.Height = h;
            groupPanel6.Height = h;
            groupPanel7.Height = h;
            groupPanel8.Height = h;
            groupPanel9.Height = h;
            groupPanel10.Height = h;
        }

        private void b0Chon_Click(object sender, EventArgs e)
        {
            ChepMau(0);
        }

        
        void ChepMau(int id)
        {
            folderBrowserDialog1.Description = "Chọn nơi lưu website";
            DialogResult rs = folderBrowserDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                thumucChon = folderBrowserDialog1.SelectedPath;
                if (!File.Exists(thumucChon + "\\config.ntu"))
                {
                    // Directory.CreateDirectory(thumucChon);
                    String fileCauHinh = thumucChon + "\\config.ntu";
                    FileStream fs = File.Create(fileCauHinh);

                    this.Enabled = false;
                    CongCu.DirectoryCopy(TempPath + "Mau"+id.ToString(), thumucChon, true);
                    this.Enabled = true;
                    MessageBox.Show("Chọn mẫu xong, Mời hiệu chỉnh dữ liệu", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    StreamWriter outputFile = new StreamWriter(fs);
                    outputFile.WriteLine(fileCauHinh);
                    outputFile.WriteLine(id);  // mẫu 4
                    outputFile.Close();
                    fs.Close();
                    projectFolder = thumucChon;
                    MessageBox.Show(projectFolder);
                    chuongtrinhChinh.projectFolder = thumucChon;
                   // ProjectExplorer pj = new ProjectExplorer(projectFolder);
                   // SplitContainer p = (SplitContainer)chuongtrinhChinh.Controls["splitContainer1"];
                   // p.Panel2.Controls.Add(pj);
                    chuongtrinhChinh.setMainTabVisible(true);

                    this.Visible = false;
                    chuongtrinhChinh.superTabControlWindows.Tabs.Remove("tabChonMau");
                    chuongtrinhChinh.Update();

                }
                else
                    MessageBox.Show("Bạn đã chọn mẫu này", "NTUWebgen",MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        void XemMau(int id) {
           // String thumucGoc1 = System.AppDomain.CurrentDomain.BaseDirectory;
           // TempPath = thumucGoc + "Templates\\";
            CongCu.gotoSite(TempPath+"Mau"+id.ToString()+"\\index.html");
        }
        private void btnChon1_Click(object sender, EventArgs e)
        {
            ChepMau(1);
        }

        private void btnChon2_Click(object sender, EventArgs e)
        {
            ChepMau(2);
        }

        private void btnMau3_Click(object sender, EventArgs e)
        {
            ChepMau(3);
        }

        private void btnChon4_Click(object sender, EventArgs e)
        {
            ChepMau(4);
        }

        private void btnChon5_Click(object sender, EventArgs e)
        {
            ChepMau(5);
        }

        private void btnChon6_Click(object sender, EventArgs e)
        {
            ChepMau(6);
        }

        private void btnChon7_Click(object sender, EventArgs e)
        {
            ChepMau(7);
        }

        private void btnChon8_Click(object sender, EventArgs e)
        {
            ChepMau(8);
        }

        private void btnChon9_Click(object sender, EventArgs e)
        {
            ChepMau(9);
        }

        private void btnChon10_Click(object sender, EventArgs e)
        {
            ChepMau(10);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(1);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(2);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(3);
        }

        private void linkLabel40_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(4);

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(5);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(6);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(7);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(8);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(9);
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            XemMau(10);
        }

      

    }

}
