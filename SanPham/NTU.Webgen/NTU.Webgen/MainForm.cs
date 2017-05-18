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
            //XmlReader xmlReader = XmlReader.Create(cvXMLFile);
            //StringBuilder s = new StringBuilder();
            //while (xmlReader.Read())
            //{
            //    if (xmlReader.NodeType == XmlNodeType.Element) {
            //        switch (xmlReader.Name) {
            //            case "HoVaTen": ll_txtHoTen.Text = xmlReader.ReadInnerXml(); break;
            //            case "NamSinh": ll_txtNamSinh.Text = xmlReader.ReadInnerXml(); break;
            //            case "GioiTinh": ll_txtGioiTinh.Text = xmlReader.ReadInnerXml(); break;
            //            case "LoaiHocVi": ll_txtHocvi.Text = xmlReader.ReadInnerXml(); break;
            //            case "NamDatHocVi": ll_txtHocviNamDat.Text = xmlReader.ReadInnerXml(); break;
            //            case "HocHam": ll_txtHocHam.Text = xmlReader.ReadInnerXml(); break;
            //            case "ChucDanh": ll_txtChucDanh.Text = xmlReader.ReadInnerXml(); break;
            //            case "NN1": ll_txtNgoaiNgu.Text = xmlReader.ReadInnerXml(); break;
            //        }
            //    }
                  
            //}
            //xmlReader.Close();
            //MessageBox.Show("Tải thành công!");

       
        }

        private void ll_btnLuuThongtinChung_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.Load(cvXMLFile);
            XmlNode newXMLNode = xmlDom.SelectSingleNode("root/ThongTinChung/HoVaTen");
            newXMLNode.InnerText = "ok nè";
            xmlDom.Save(cvXMLFile);

            
            //XElement xelement = XElement.Load(cvXMLFile);
            //IEnumerable<XElement> settings = xelement.Elements();

            //settings.FirstOrDefault(x => x.Name == "HoVaTen").SetValue("treeee");

            //xelement.Save(cvXMLFile);


        }
        // note - just a string (name) passed in
        public XElement Get(string name, string xmlfile)
        {
            XElement xelement = XElement.Load(xmlfile);
            IEnumerable<XElement> settings = xelement.Elements();

            return settings.FirstOrDefault(x => x.Name == name);
        }

        private void mnuWeb_TaoMoi_Click(object sender, EventArgs e)
        {
            ChonMau ctrlChonMau = new ChonMau();
            CongCu.AddTab("tabChonMau", "Tạo mới website", superTabControlWindows, ctrlChonMau, true,30);

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
            if (superTabControlWindows.Tabs.Contains("tabGIoiThieu"))
            {
                var t = (SuperTabItem)superTabControlWindows.Tabs["tabGIoiThieu"];
                superTabControlWindows.SelectedTab = t;
            }
            else
            {
                GioiThieu sc = new GioiThieu();
                CongCu.AddTab("tabGIoiThieu", "Giới thiệu", superTabControlWindows, sc, true, 10);
            }
        }
    }
   
}
