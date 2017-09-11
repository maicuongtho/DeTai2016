using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NTU.Webgen
{
    public partial class Publication_Sach : UserControl
    {
        bool isThemmoi, isSua, isXoa;
        String sachXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\Sach.xml";
        String pubHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\publications.html";
        String pubHTMLFolder = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/";
        String ProjectFolder;
        public Publication_Sach()
        {
            InitializeComponent();


            EnalbeTextBox(false);
            CongCu.XML2Grid(sachXMLFile, dataGridViewX_DSSach);
        }

        public Publication_Sach(String ProjectFolder)
        {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.sachXMLFile = ProjectFolder + "\\data\\Sach.xml";
            this.pubHTMLFile = ProjectFolder + "\\publications.html";
            this.pubHTMLFolder = ProjectFolder.Replace("\\", "/");
            EnalbeTextBox(false);
            CongCu.XML2Grid(sachXMLFile, dataGridViewX_DSSach);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaInput();
            EnalbeTextBox(true);
            isThemmoi = true;
            btnSave.Enabled = true;
        }


        void EnalbeTextBox(bool b) {
            txtNam.Enabled = b;
            txtNoiXB.Enabled = b;
            txtNhaXB.Enabled = b;
            txtTacGia.Enabled = b;
            txtTenSach.Enabled = b;
        }
        void XoaInput()
        {
            txtTacGia.Text = "";
            txtNam.Text = "";
            txtTenSach.Text = "";
            txtNhaXB.Text = "";
            txtNoiXB.Text = "";
            txtId.Text = "";
            txtTacGia.Focus();
        }
        int getMaxId(String xmlFile)
        {
            //create new instance of XmlDocument
            XmlDocument xmlDom = new XmlDocument();
            //load from file
            xmlDom.Load(xmlFile);
            XmlNodeList elemList = xmlDom.GetElementsByTagName("id");
            int MaxID = 0;
            for (int i = 0; i < elemList.Count; i++)
            {
                int id = Int16.Parse(elemList[i].InnerXml);
                if (MaxID < id) MaxID = id;
            }
            return MaxID + 1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isThemmoi)
            {

                //create new instance of XmlDocument
                XmlDocument xmlDom = new XmlDocument();
                //load from file
                xmlDom.Load(sachXMLFile);

                //create node and add value
                XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "Sach", null);

                //create id node
                XmlNode nodeid = xmlDom.CreateElement("id");
                //add value for it
                nodeid.InnerText = getMaxId(sachXMLFile).ToString();


                //node TacGia
                XmlNode nodeTacGia = xmlDom.CreateElement("TacGia");
                nodeTacGia.InnerText = txtTacGia.Text;


                XmlNode nodeNam = xmlDom.CreateElement("NamXB");
                nodeNam.InnerText = txtNam.Text;

                XmlNode nodeTenSach = xmlDom.CreateElement("TenSach");
                nodeTenSach.InnerText = txtTenSach.Text;

                XmlNode nodeNhaXB = xmlDom.CreateElement("NhaXB");
                nodeNhaXB.InnerText = txtNhaXB.Text;

                XmlNode nodeNoiXB = xmlDom.CreateElement("NoiXB");
                nodeNoiXB.InnerText = txtNoiXB.Text;

                //add to parent node
                node.AppendChild(nodeid);
                node.AppendChild(nodeTacGia);
                node.AppendChild(nodeNam);
                node.AppendChild(nodeTenSach);
                node.AppendChild(nodeNhaXB);
                node.AppendChild(nodeNoiXB);
               

                //add to elements collection
                xmlDom.DocumentElement.AppendChild(node);

                //save back
                xmlDom.Save(sachXMLFile);
                MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

                // Cập nhật lại lưới
                CongCu.XML2Grid(sachXMLFile, dataGridViewX_DSSach);
                EnalbeTextBox(false);
                isThemmoi = false;
                XoaInput();
            }
            if (isSua)
            {
                XmlTextReader reader = new XmlTextReader(sachXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                int id = Int16.Parse(txtId.Text);
                
                oldCd = root.SelectSingleNode("/DSSach/Sach[id='" + id + "']");

                XmlElement newCd = doc.CreateElement("Sach");
                newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
                        "<TacGia>" + txtTacGia.Text + "</TacGia>" +
                        "<NamXB>" + txtNam.Text + "</NamXB>" +
                        "<TenSach>" + txtTenSach.Text + "</TenSach>" +
                        "<NhaXB>" + txtNhaXB.Text + "</NhaXB>" +
                        "<NoiXB>" + txtNoiXB.Text + "</NoiXB>";
                    

                root.ReplaceChild(newCd, oldCd);

                //save the output to a file
                doc.Save(sachXMLFile);
                CongCu.XML2Grid(sachXMLFile, dataGridViewX_DSSach);
                isSua = false;
                EnalbeTextBox(false);

            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Sách cần sửa", "Thông báo");
                return;
            }
            isSua = true;
            EnalbeTextBox(true); 
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Sách cần xóa", "Thông báo");
                return;
            }
            XmlTextReader reader = new XmlTextReader(sachXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the cd node with the matching title
            XmlNode oldCd;
            XmlElement root = doc.DocumentElement;
            int id = Int16.Parse(txtId.Text);

            oldCd = root.SelectSingleNode("/DSSach/Sach[id='" + id + "']");
            DialogResult rs = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                root.RemoveChild(oldCd);
                //save the output to a file
                doc.Save(sachXMLFile);
                CongCu.XML2Grid(sachXMLFile, dataGridViewX_DSSach);
                XoaInput();
                EnalbeTextBox(false);
            }
        }

        private void dataGridViewX_DSSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTacGia.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["TacGia"].Value.ToString();
            txtNam.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NamXB"].Value.ToString();
            txtTenSach.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["TenSach"].Value.ToString();
            txtNhaXB.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NhaXB"].Value.ToString();
            txtNoiXB.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NoiXB"].Value.ToString();
            txtId.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["STT"].Value.ToString();
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = CongCu.XML2HTML_Sach(sachXMLFile, "Sach").ToString();

            CongCu.ReplaceContent(pubHTMLFile, "Sach", noiDungMoi);

            // Dành cho mẫu 4
            UserInfo u = CongCu.getUserInfo(ProjectFolder + "\\data\\index.xml");
            CongCu.ReplaceContent(pubHTMLFile, "anhTrai", "<img src=\"" + u.HinhAnh + "\" width=100% height=186px>");
            CongCu.ReplaceContent(pubHTMLFile, "tenTrai", "website của " + u.HoTen);
            // Thêm tiêu đề
            CongCu.ReplaceTite(pubHTMLFile, "NTU. " + u.HoTen + "-Báo cáo hội thảo");
            MessageBox.Show("Đã xuất thành công sang trang web: \n" + pubHTMLFile, "Thông báo");
            
        }
        
    }
}
