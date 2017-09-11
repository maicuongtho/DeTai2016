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
       // public String projectFolder;
        ChuongTrinh chuongtrinhChinh;
        String ProjectFolder;
        int idMau;
        public XuatSangMauKhac()
        {
            InitializeComponent();
        }

        public XuatSangMauKhac(String prjFolder, int idMau, ChuongTrinh c)
        {
            InitializeComponent();
            thumucGoc = System.AppDomain.CurrentDomain.BaseDirectory;
            TempPath = thumucGoc+"Templates\\";
            ProjectFolder = prjFolder;
            chuongtrinhChinh = c;
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

        void ChonMauKhac(int IdMauMoi)
        {

            //DialogResult chon = MessageBox.Show("Bạn muốn ghi đè lên thư mục cũ không ?", "NTUWebgen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (chon == DialogResult.Yes) // GHi đè
            //{
            //    // Cập nhật lại file config
            //    String fileConf = ProjectFolder + "\\config.ntu";
            //    File.Delete(fileConf);
            //    FileStream fs = File.OpenWrite(fileConf);
            //    StreamWriter outputFile = new StreamWriter(fs);
            //    outputFile.WriteLine(fileConf);
            //    outputFile.WriteLine(IdMauMoi);   
            //    outputFile.Close();
            //    fs.Close();
            //    //Directory.Delete(ProjectFolder + "\\assets", true); // Xóa assserr

            //   // Directory.Delete(ProjectFolder + "\\courses", true); // Xóa courses
            //    // xóa các file cấp 1
            //    DirectoryInfo dir = new DirectoryInfo(ProjectFolder);
            //    FileInfo[] files = dir.GetFiles();
            //    for (int i = 0; i < files.Length; i++) File.Delete(files[i].FullName);

            //    // chỉ cần chép các mục khác , trừ phần data
            //    CongCu.DirectoryCopy(TempPath + "Mau" + IdMauMoi.ToString(), ProjectFolder, true, "data");
            //    MessageBox.Show("Chuyển sang mẫu mới xong", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}// không ghi đề
            //else
            //{ 
            
            //}


            folderBrowserDialog1.Description = "Chọn nơi lưu website mới";
            DialogResult rs = folderBrowserDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                String thumucChon = folderBrowserDialog1.SelectedPath;
                if (!File.Exists(thumucChon + "\\config.ntu"))
                {
                    // Directory.CreateDirectory(thumucChon);
                    String fileCauHinh = thumucChon + "\\config.ntu";
                    FileStream fs = File.Create(fileCauHinh);
                    StreamWriter outputFile = new StreamWriter(fs);
                    outputFile.WriteLine(fileCauHinh);
                    outputFile.WriteLine(IdMauMoi);  // mẫu 4
                    outputFile.Close();
                    fs.Close();

                    this.Enabled = false;
                    String thuMucDuocCop= TempPath + "Mau" + IdMauMoi.ToString();
                    CongCu.DirectoryCopy(thuMucDuocCop, thumucChon, true);
                    this.Enabled = true;

                 
                    //Copy tai nguyen trong thu muc cousre, tru file index
                    String thumucKhoaHocGoc = ProjectFolder + "\\courses";
                    String thumucKhoaHocMoi = thumucChon + "\\courses";
                    CongCu.DirectoryCopy(thumucKhoaHocGoc,thumucKhoaHocMoi , true);
                    // Copy lai file temlate khoas hoc
                    String fileMauKhoaHocGoc = thumucKhoaHocGoc+ "\\courseTemplate-empty.html";
                    String fileMauKhoaHocMoi = thumucKhoaHocMoi + "\\courseTemplate-empty.html";

                    File.Copy(fileMauKhoaHocGoc,fileMauKhoaHocMoi,true);


                    String[] dirC = Directory.GetDirectories(thumucKhoaHocGoc);
                    for (int i = 0; i < dirC.Length; i++)
                    {
                        String indexFile = thuMucDuocCop + "\\courses\\courseTemplate-empty.html";
                        String indexMoi = dirC[i] + "\\index.html";
                        File.Copy(indexFile, indexMoi, true);
                    }
                  //  XuatWeb.XuatIndex(
                    // Goi ham xuat lai cac trang web
                    MessageBox.Show("Chuyển sang mẫu mới xong", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chuongtrinhChinh.projectFolder = thumucChon;
                    chuongtrinhChinh.Update();

                    // ĐÓng sạch Tab
                    chuongtrinhChinh.superTabControlWindows.Tabs.Clear();
                    for (int i = 0; i < chuongtrinhChinh.superTabControlWindows.Tabs.Count; i++)
                    {
                        chuongtrinhChinh.superTabControlWindows.Tabs.Remove(i);
                    
                    }
                        // 

                }
                else
                    MessageBox.Show("Bạn đã chọn mẫu này", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }


        void XemMau(int id)
        {
            // String thumucGoc1 = System.AppDomain.CurrentDomain.BaseDirectory;
            // TempPath = thumucGoc + "Templates\\";
            CongCu.gotoSite(TempPath + "Mau" + id.ToString() + "\\index.html");
        }
        private void linkMau0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(0);
        }

        private void btnChon0_Click(object sender, EventArgs e)
        {
            ChonMauKhac(0);
        }

        private void linkMau1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(1);
        }

        private void btnChon1_Click(object sender, EventArgs e)
        {
            ChonMauKhac(1);
        }

        private void linkMau2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(2);
        }

        private void btnChon2_Click(object sender, EventArgs e)
        {
            ChonMauKhac(2);
        }

        private void linkMau3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(3);
        }

        private void btnChon3_Click(object sender, EventArgs e)
        {
            ChonMauKhac(3);
        }

        private void linkMau4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(4);
        }

        private void btnChon4_Click(object sender, EventArgs e)
        {
            ChonMauKhac(4);
        }

        private void linkMau5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(5);
        }

        private void btnChon5_Click(object sender, EventArgs e)
        {
            ChonMauKhac(5);
        }

        private void linkMau6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(6);
        }

        private void btnChon6_Click(object sender, EventArgs e)
        {
            ChonMauKhac(6);
        }

        private void linkMau7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(7);
        }

        private void btnChon7_Click(object sender, EventArgs e)
        {
            ChonMauKhac(7);
        }

        private void linkMau8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(8);
        }

        private void btnChon8_Click(object sender, EventArgs e)
        {
            ChonMauKhac(8);
        }

        private void linkMau9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(9);
        }

        private void btnChon9_Click(object sender, EventArgs e)
        {
            ChonMauKhac(9);
        }

        private void linkMau10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            XemMau(10);
        }

        private void btnChon10_Click(object sender, EventArgs e)
        {
            ChonMauKhac(10);
        }
    }
}