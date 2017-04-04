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
        String cvXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\gvStandardTemplate\cv.xml";
        public MainForm()
        {
            InitializeComponent();
        }
      
        private void mnuWeb_ChonMau_Click(object sender, EventArgs e)
        {
            HienTab("chonmau");
        }

        private void mnuWeb_HieuChinh_Click(object sender, EventArgs e)
        {
            HienTab("hieuchinh");
        }

        // Lý lịch khoa học Tab
        private void ribbonTabItem3_Click(object sender, EventArgs e)
        {

            HienTab("lylich");

        }

        void HienTab(String tenTab)
        {
            for (int i = 0; i < superTabControlWebsite.Tabs.Count; i++) superTabControlWebsite.Tabs[i].Visible = false;
            superTabControlWebsite.Tabs[tenTab].Visible = true;
        }
        // Tải dữ liệu lên điều khiển
        private void mnuLL_btnTaiLaiXML_Click(object sender, EventArgs e)
        {
            XmlReader xmlReader = XmlReader.Create(cvXMLFile);
            StringBuilder s = new StringBuilder();
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element) {
                    switch (xmlReader.Name) {
                        case "HoVaTen": ll_txtHoTen.Text = xmlReader.ReadInnerXml(); break;
                        case "NamSinh": ll_txtNamSinh.Text = xmlReader.ReadInnerXml(); break;
                        case "GioiTinh": ll_txtGioiTinh.Text = xmlReader.ReadInnerXml(); break;
                        case "LoaiHocVi": ll_txtHocvi.Text = xmlReader.ReadInnerXml(); break;
                        case "NamDatHocVi": ll_txtHocviNamDat.Text = xmlReader.ReadInnerXml(); break;
                        case "HocHam": ll_txtHocHam.Text = xmlReader.ReadInnerXml(); break;
                        case "ChucDanh": ll_txtChucDanh.Text = xmlReader.ReadInnerXml(); break;
                        case "NN1": ll_txtNgoaiNgu.Text = xmlReader.ReadInnerXml(); break;
                    }
                }
                  
            }
            xmlReader.Close();
            MessageBox.Show("Tải thành công!");

       
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
    }
   
}
