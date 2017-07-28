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
    public partial class LienKet : UserControl
    {
        static String xmlFile = @"\\data\\LienKet.xml";
        static String htmlFile = @"\weblink.html";
        String ProjectFolder;
        String fullXMLFile;
        String fullHTMLFile;
        bool isThemMoi, isSua;
        public LienKet()
        {
            InitializeComponent();
            btnSave.Enabled = false;
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            this.fullXMLFile = ProjectFolder + xmlFile;
            this.fullHTMLFile = (ProjectFolder + htmlFile);
            CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
        }

        public LienKet(String ProjectFolder)
        {
            InitializeComponent();
            btnSave.Enabled = false;
            this.ProjectFolder = ProjectFolder;
            this.fullXMLFile = ProjectFolder + xmlFile;
            CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
        }
        void XoaInput() {
            txtLK.Text = "";
            txtDC.Text = "";
        }

        void EnableTextBox(bool state)
        {
            txtDC.Enabled = state;
            txtLK.Enabled = state;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            txtDC.Enabled = true;
            txtLK.Enabled = true;
            btnSave.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn lên kết cần xóa", "Thông báo");
                return;
            }
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Tiến hành xóa
            //Select the cd node with the matching title
            XmlNode oldCd;
            XmlElement root = doc.DocumentElement;
            int id = Int16.Parse(txtId.Text);

            oldCd = root.SelectSingleNode("/DSLienKet/LienKet[id='" + id + "']");
            DialogResult rs = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                root.RemoveChild(oldCd);
                //save the output to a file
                doc.Save(fullXMLFile);
                CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
                XoaInput();
                EnableTextBox(false);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isSua = true;
            btnSave.Enabled = true;
            txtDC.Enabled = true;
            txtLK.Enabled = true;
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
            if (isThemMoi)
            {
                //create new instance of XmlDocument
                XmlDocument xmlDom = new XmlDocument();
                //load from file
                xmlDom.Load(fullXMLFile);

                //create node and add value
                XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "LienKet", null);

                //create id node
                XmlNode nodeid = xmlDom.CreateElement("id");
                //add value for it
                nodeid.InnerText = getMaxId(fullXMLFile).ToString();


                //node Lien ket
                XmlNode nodeLK = xmlDom.CreateElement("TieuDe");
                nodeLK.InnerText = txtLK.Text;

                //node Dia chi Lien ket
                XmlNode nodeDiaChi = xmlDom.CreateElement("DiaChi");
                nodeDiaChi.InnerText = System.Web.HttpUtility.UrlEncode(txtDC.Text);

                //add to parent node
                node.AppendChild(nodeid);
                node.AppendChild(nodeLK);
                node.AppendChild(nodeDiaChi);
                //add to elements collection
                xmlDom.DocumentElement.AppendChild(node);

                //save back
                xmlDom.Save(fullXMLFile);
                MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

                // Cập nhật lại lưới
                CongCu.XML2Grid(fullXMLFile, dataGridViewX_DSLienKet);
              //  EnalbeTextBox(false);
                isThemMoi = false;
                txtDC.Text = "";
                txtLK.Text = "";
                txtDC.Enabled = false;
                txtLK.Enabled = false;
                btnSua.Enabled = true;
                //XoaInput();
            } // het them
            if (isSua) { 
                XmlTextReader reader = new XmlTextReader(fullXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                int id = Int16.Parse(txtId.Text);
               
               
                oldCd = root.SelectSingleNode("/DSLienKet/LienKet[id='" + id + "']");

                XmlElement newCd = doc.CreateElement("LienKet");
                newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
                        "<TieuDe>" + txtLK.Text + "</TieuDe>" +
                        "<DiaChi>" + System.Web.HttpUtility.UrlEncode(txtDC.Text) + "</DiaChi>";
                       
                root.ReplaceChild(newCd, oldCd);

                //save the output to a file
                doc.Save(fullXMLFile);
                CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
                isSua = false;
                txtDC.Text = "";
                txtLK.Text = "";
                txtDC.Enabled = false;
                txtLK.Enabled =false;
            }

        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            XuatWeb.XuatLienKet(fullXMLFile, fullHTMLFile);
        }

        private void dataGridViewX_DSLienKet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridViewX_DSLienKet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLK.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells["cotMoTa"].Value.ToString();
            txtDC.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells["cotDiaChi"].Value.ToString();
            txtId.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells["STT"].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CongCu.gotoSite("https://scholar.google.com.vn/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CongCu.gotoSite("https://www.researchgate.net/home");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CongCu.gotoSite("https://www.academia.edu");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try { CongCu.gotoSite(txtDC.Text); }
            catch (Exception ex) { MessageBox.Show("Địa chỉ không đúng","NTUWebgen Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Exclamation); }
        }
    }
}
