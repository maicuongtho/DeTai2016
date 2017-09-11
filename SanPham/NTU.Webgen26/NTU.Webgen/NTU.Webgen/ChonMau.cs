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
            CongCu.gotoSite(webPath + "/publications.html"); 
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
            folderBrowserDialog1.Description = "Chọn nơi lưu dự an web";
            DialogResult rs = folderBrowserDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                thumucChon = folderBrowserDialog1.SelectedPath;
                //MessageBox.Show(thumucChon);
                //                thumucChon = thumucGoc + "UserChoices\\Mau0";
                if (!File.Exists(thumucChon + "\\config.ntu"))
                {
                   // Directory.CreateDirectory(thumucChon);
                    String fileCauHinh = thumucChon+"\\config.ntu";
                    FileStream fs = File.Create(fileCauHinh);
                    
                    this.Enabled = false;
                    CongCu.DirectoryCopy(TempPath+"Mau0", thumucChon, true);
                    this.Enabled = true;
                    MessageBox.Show("Chọn mẫu xong, Mời hiệu chỉnh dữ liệu", "Thông báo");
                    
                    StreamWriter outputFile = new StreamWriter(fs);
                    outputFile.WriteLine(fileCauHinh);
                    outputFile.WriteLine(0); // mẫu 0
                    outputFile.Close();
                    fs.Close();
                    projectFolder = thumucChon;
                    chuongtrinhChinh.projectFolder = thumucChon;
                    MessageBox.Show(projectFolder);

                   ProjectExplorer pj = new ProjectExplorer(projectFolder);
                   SplitContainer p = (SplitContainer)chuongtrinhChinh.Controls["splitContainer1"];
                   p.Panel2.Controls.Add(pj);
    
                   chuongtrinhChinh.Update();
                   
                }
                else
                    MessageBox.Show("Bạn đã chọn mẫu này", "NTUWebgen Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnMau4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Chọn nơi lưu website";
            DialogResult rs = folderBrowserDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                thumucChon = folderBrowserDialog1.SelectedPath;
                //MessageBox.Show(thumucChon);
                //                thumucChon = thumucGoc + "UserChoices\\Mau0";
                if (!File.Exists(thumucChon + "\\config.ntu"))
                {
                    // Directory.CreateDirectory(thumucChon);
                    String fileCauHinh = thumucChon + "\\config.ntu";
                    FileStream fs = File.Create(fileCauHinh);

                    this.Enabled = false;
                    CongCu.DirectoryCopy(TempPath+"Mau4", thumucChon, true);
                    this.Enabled = true;
                    MessageBox.Show("Chọn mẫu xong, Mời hiệu chỉnh dữ liệu", "Thông báo");

                    StreamWriter outputFile = new StreamWriter(fs);
                    outputFile.WriteLine(fileCauHinh);
                    outputFile.WriteLine(4);  // mẫu 4
                    outputFile.Close();
                    fs.Close();
                    projectFolder = thumucChon;
                    MessageBox.Show(projectFolder);
                    chuongtrinhChinh.projectFolder = thumucChon;
                    ProjectExplorer pj = new ProjectExplorer(projectFolder);
                    SplitContainer p = (SplitContainer)chuongtrinhChinh.Controls["splitContainer1"];
                    p.Panel2.Controls.Add(pj);

                    chuongtrinhChinh.Update();

                }
                else
                    MessageBox.Show("Bạn đã chọn mẫu này", "Cảnh báo");

            }
        }

      

    }

}
