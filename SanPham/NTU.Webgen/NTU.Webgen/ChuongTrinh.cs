using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace NTU.Webgen
{
    public partial class ChuongTrinh : DevComponents.DotNetBar.OfficeForm
    {
        
        public ChuongTrinh()
        {
            InitializeComponent();
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
            if (superTabControlWindows.Tabs.Contains("tabGIoiThieu"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabGIoiThieu"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                Home sc = new Home();
                CongCu.AddTab("tabGIoiThieu", "Giới thiệu", superTabControlWindows, sc, true, 10);
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
                Publication_TapChi tc = new Publication_TapChi();
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
                Publication_BaoCaoHoiThao bc = new Publication_BaoCaoHoiThao();
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
                Publication_BaoCaoHoiThao0 bc0 = new Publication_BaoCaoHoiThao0();
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
                Publication_Sach sc = new Publication_Sach();
                CongCu.AddTab("tabSach", "Sách", superTabControlWindows, sc, false, 10);
            }
        }
    }
}