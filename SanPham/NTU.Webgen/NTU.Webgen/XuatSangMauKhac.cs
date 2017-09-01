using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;

namespace NTU.Webgen
{
    public partial class XuatSangMauKhac : DevComponents.DotNetBar.OfficeForm
    {
        String TempPath; 
        String webPath;
        String thumucGoc;
        String thumucChon;
        public String projectFolder;
        ChuongTrinh chuongtrinhChinh;
        String ProjectFolder;
        int idMau;
        public XuatSangMauKhac()
        {
            InitializeComponent();
        }

        public XuatSangMauKhac(String prjFolder, int idMau)
        {
            InitializeComponent();
            thumucGoc = System.AppDomain.CurrentDomain.BaseDirectory;
            TempPath = thumucGoc+"Templates\\";
            ProjectFolder = prjFolder;
            this.idMau = idMau;
            switch (idMau) {
                case 0: groupPanel1.Enabled = false; groupPanel1.BackColor = Color.Azure; break;
                case 1: groupPanel2.Enabled = false; groupPanel2.BackColor = Color.Azure; break;
                case 2: groupPanel3.Enabled = false; groupPanel3.BackColor = Color.Azure; break;
                case 3: groupPanel4.Enabled = false; groupPanel4.BackColor = Color.Azure; break;
                case 4: groupPanel5.Enabled = false; groupPanel5.BackColor = Color.Azure; break;
                case 5: groupPanel6.Enabled = false; groupPanel6.BackColor = Color.Azure; break;
                case 6: groupPanel7.Enabled = false; groupPanel7.BackColor = Color.Azure; break;
                case 7: groupPanel8.Enabled = false; groupPanel8.BackColor = Color.Azure; break;
                case 8: groupPanel9.Enabled = false; groupPanel9.BackColor = Color.Azure; break;
                case 9: groupPanel10.Enabled = false; groupPanel10.BackColor = Color.Azure; break;
                case 10: groupPanel11.Enabled = false; groupPanel11.BackColor = Color.Azure; break; 
            }
        }

        void ChonMauKhac(int id)
        {
            folderBrowserDialog1.Description = "Chọn nơi lưu website";
            DialogResult rs = folderBrowserDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                String thumucChon = folderBrowserDialog1.SelectedPath;
                if (!File.Exists(thumucChon + "\\config.ntu"))
                {
                    // Directory.CreateDirectory(thumucChon);
                    String fileCauHinh = thumucChon + "\\config.ntu";
                    FileStream fs = File.Create(fileCauHinh);

                    this.Enabled = false;
                    CongCu.DirectoryCopy(TempPath + "Mau" + id.ToString(), thumucChon, true);
                    this.Enabled = true;
                    MessageBox.Show("Chọn mẫu xong, Mời hiệu chỉnh dữ liệu", "Thông báo");

                    StreamWriter outputFile = new StreamWriter(fs);
                    outputFile.WriteLine(fileCauHinh);
                    outputFile.WriteLine(id);  // mẫu 4
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