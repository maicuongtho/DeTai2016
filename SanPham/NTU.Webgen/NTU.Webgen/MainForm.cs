using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace NTU.Webgen
{
    public partial class MainForm : Form
    {
        String webFolder;
        String cvXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\gvStandardTemplate\cv.xml";
        public MainForm()
        {
            InitializeComponent();
        }
      
   

        private void mnuWeb_HieuChinh_Click(object sender, EventArgs e)
        {
          //  HienTab("hieuchinh");
        }

        // Lý lịch khoa học Tab
        private void ribbonTabItem3_Click(object sender, EventArgs e)
        {

            //HienTab("lylich");

        }
      
        void HienTab(String tenTab)
        {
        //    for (int i = 0; i < superTabControlWebsite.Tabs.Count; i++) superTabControlWebsite.Tabs[i].Visible = false;
      //      superTabControlWebsite.Tabs[tenTab].Visible = true;
        }
        // Tải dữ liệu lên điều khiển
        private void mnuLL_btnTaiLaiXML_Click(object sender, EventArgs e)
        {

       
        }

        private void ll_btnLuuThongtinChung_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.Load(cvXMLFile);
            XmlNode newXMLNode = xmlDom.SelectSingleNode("root/ThongTinChung/HoVaTen");
            newXMLNode.InnerText = "ok nè";
            xmlDom.Save(cvXMLFile);
        }

        public XElement Get(string name, string xmlfile)
        {
            XElement xelement = XElement.Load(xmlfile);
            IEnumerable<XElement> settings = xelement.Elements();

            return settings.FirstOrDefault(x => x.Name == name);
        }

        private void mnuWeb_TaoMoi_Click(object sender, EventArgs e)
        {
            }
        
        // Mở lại website đã có trên máy cục bộ
        private void mnuWeb_MoLai_Click(object sender, EventArgs e)
        {
            DialogResult rs= openFileDialog1.ShowDialog();
            if (rs == System.Windows.Forms.DialogResult.OK) {
                String fileName = openFileDialog1.FileName;
                webFolder = fileName.Substring(0, fileName.Length - 11);
               // MessageBox.Show(webFolder);

                System.IO.StreamReader sr = new
                System.IO.StreamReader(fileName);
                String noidung = sr.ReadToEnd();
                MessageBox.Show("Đã nạp xong nội dung, mời Bạn chỉnh sửa","Thông báo");

            }
        }

        private void btnPub_TapChi_Click(object sender, EventArgs e)
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

        private void btnPub_BaoCao_Click(object sender, EventArgs e)
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

        private void btnPub_BaoCao0_Click(object sender, EventArgs e)
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

        private void btnPub_Sach_Click(object sender, EventArgs e)
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

        private void ribbonTabItemGioiThieu_Button_Click(object sender, EventArgs e)
        {
        }
    }
   
}
