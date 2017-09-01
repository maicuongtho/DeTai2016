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
    public partial class ChuongTrinh : DevComponents.DotNetBar.OfficeForm
    {
        public String projectFolder = "";
        public int idMau;
        public ChuongTrinh()
        {
            InitializeComponent();
            setMainTabVisible(false);
        }

        
        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            if (splitContainer1.Orientation == Orientation.Vertical)
                Cursor.Current = Cursors.NoMoveVert;
            else
                Cursor.Current = Cursors.NoMoveHoriz; 
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabGioiThieu"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabGioiThieu"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                Home sc = new Home(@projectFolder,idMau);
                CongCu.AddTab("tabGioiThieu", "Giới thiệu", superTabControlWindows, sc, true, 10);
            }
        }

        private void mnuCongBo_BaiBao_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabTapChi"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabTapChi"];
                superTabControlWindows.SelectedTab = t;
            }


            else
            {
                Publication_TapChi tc = new Publication_TapChi(@projectFolder);
                CongCu.AddTab("tabTapChi", "Tạp chí", superTabControlWindows, tc, false, 10);
            }
        }

        private void mnuCongBo_Baocao1_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabBaoCao"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabBaoCao"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                Publication_BaoCaoHoiThao bc = new Publication_BaoCaoHoiThao(@projectFolder);
                CongCu.AddTab("tabBaoCao", "Báo cáo hội thảo", superTabControlWindows, bc, false, 10);

            }
        }

        private void mnuCongBo_Baocao0_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabBaoCao0"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabBaoCao0"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                Publication_BaoCaoHoiThao0 bc0 = new Publication_BaoCaoHoiThao0(@projectFolder);
                CongCu.AddTab("tabBaoCao0", "Báo cáo hội thảo (không ấn phẩm)", superTabControlWindows, bc0, false, 10);
            }
        }

        private void mnuCongBo_Sach_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabSach"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabSach"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                Publication_Sach sc = new Publication_Sach(@projectFolder);
                CongCu.AddTab("tabSach", "Sách", superTabControlWindows, sc, false, 10);
            }
        }

        private void mnuLich_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabLich"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabLich"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                calendar sc = new calendar(@projectFolder);
                CongCu.AddTab("tabLich", "Lịch", superTabControlWindows, sc, false, 10);
            }
        }

        private void mnuGiangDay_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabGiangDay"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabGiangDay"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                CacMonHoc sc = new CacMonHoc(@projectFolder,this);
                CongCu.AddTab("tabGiangDay", "Các học phần Giảng dạy", superTabControlWindows, sc, false, 10);
            }
        }

        public void setMainTabVisible(bool value)
        {
            menuStrip1.Items["mnuGioiThieu"].Visible = value;
            menuStrip1.Items["mnuLyLich"].Visible = value;
            menuStrip1.Items["mnuCongBo"].Visible = value;
            menuStrip1.Items["mnuGiangDay"].Visible = value;
            menuStrip1.Items["mnuLich"].Visible = value;
            menuStrip1.Items["mnuLienKet"].Visible = value;
            menuStrip1.Items["mnuXuatMauKhac"].Visible = value;
        }

        // Tạo mới
        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult rs= MessageBox.Show("Mời chọn một mẫu dưới đây","NTUWebgen", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.OK)
            {
                closeAllTab();
                setMainTabVisible(false);
                if (superTabControlWindows.Tabs.Contains("tabChonMau"))
                {
                    var t = (SuperTabItem)superTabControlWindows.Tabs["tabChonMau"];
                    superTabControlWindows.SelectedTab = t;
                }
                else
                {
                    ChonMau sc = new ChonMau(this);
                    CongCu.AddTab("tabChonMau", "Chọn mẫu", superTabControlWindows, sc, false, 10);
                    // Cập nhật thư mục làm việc, bật project explorer
                //    setMainTabVisible(true);
                }
            }
        }
        void closeAllTab()
        { 
            for (int i=0; i<superTabControlWindows.Tabs.Count; i++)
                     superTabControlWindows.Tabs.Remove(i);

        }

        //Mở
        private void mỞToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           DialogResult rs=  openFileDialog1.ShowDialog();
           //Đọc file cấu hình để nạp thư mục tương ứng
           if (rs == DialogResult.OK)
           {
               closeAllTab();
               setMainTabVisible(false);
               String fileCauHinh = openFileDialog1.FileName;
               FileStream fs =File.Open(fileCauHinh,FileMode.Open);
               StreamReader rd = new StreamReader(fs);
               String thumucChon = rd.ReadLine();
               idMau = Int16.Parse(rd.ReadLine());
               fs.Close();
               rd.Close();
              // projectFolder = thumucChon.Substring(0, thumucChon.Length - 11);
               projectFolder = thumucChon.Substring(0, thumucChon.LastIndexOf("\\"));
               
              // ProjectExplorer pj = new ProjectExplorer(projectFolder);
               MessageBox.Show(projectFolder);
              // splitContainer1.Panel2.Controls.Clear();
              // splitContainer1.Panel2.Controls.Add(pj);
               this.Update();
               setMainTabVisible(true);
           }

        }

        private void mnuLyLich_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabLyLich"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabLyLich"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                LyLichKH sc = new LyLichKH(@projectFolder);
                CongCu.AddTab("tabLyLich", "Lý lịch khoa học", superTabControlWindows, sc, false, 10);
            }
        }

        private void mnuLienKet_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("taLienKet"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["taLienKet"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                LienKet sc = new LienKet(@projectFolder);
                CongCu.AddTab("taLienKet", "Liên kết học thuật", superTabControlWindows, sc, false, 10);
            }
        }

        private void mnuCongBo_Click(object sender, EventArgs e)
        {
            if (superTabControlWindows.Tabs.Contains("tabCongBo"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabCongBo"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                CongBo sc = new CongBo(@projectFolder);
                CongCu.AddTab("tabCongBo", "Công bố khoa học", superTabControlWindows, sc, false, 10);
            }
        }

        private void toolStripButton_Open_Click(object sender, EventArgs e)
        {
            mỞToolStripMenuItem1_Click(sender, e);
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuXuatMauKhac_Click(object sender, EventArgs e)
        {
            XuatSangMauKhac m = new XuatSangMauKhac(projectFolder,idMau);
            m.ShowDialog();

        }

        private void mnuUpDownLoad_Click(object sender, EventArgs e)
        {
            new Login().ShowDialog();
        }
    }
}