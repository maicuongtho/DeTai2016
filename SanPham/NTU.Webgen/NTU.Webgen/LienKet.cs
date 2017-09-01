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
        static String xmlFile = @"\data\LienKet.xml";
        static String htmlFile = @"\weblink.html";
        String ProjectFolder;
        String fullXMLFile;
        String fullHTMLFile;
        
        public LienKet()
        {
            InitializeComponent();
            
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            this.fullXMLFile = ProjectFolder + xmlFile;
            this.fullHTMLFile = (ProjectFolder + htmlFile);
           // CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
             LoadLienKet();
             linkLabel1.Text = fullHTMLFile;
        }

        public LienKet(String ProjectFolder)
        {
            InitializeComponent();
             
            this.ProjectFolder = ProjectFolder;
            this.fullXMLFile = ProjectFolder + xmlFile;
             this.fullHTMLFile = (ProjectFolder + htmlFile);
            //CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
             LoadLienKet();
             linkLabel1.Text = fullHTMLFile;
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
          //  isThemMoi = true;
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
            //if (isThemMoi)
            //{
            //    //create new instance of XmlDocument
            //    XmlDocument xmlDom = new XmlDocument();
            //    //load from file
            //    xmlDom.Load(fullXMLFile);

            //    //create node and add value
            //    XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "LienKet", null);

            //    //create id node
            //    XmlNode nodeid = xmlDom.CreateElement("id");
            //    //add value for it
            //    nodeid.InnerText = getMaxId(fullXMLFile).ToString();


            //    //node Lien ket
            //    XmlNode nodeLK = xmlDom.CreateElement("TieuDe");
            //    nodeLK.InnerText = txtLK.Text;

            //    //node Dia chi Lien ket
            //    XmlNode nodeDiaChi = xmlDom.CreateElement("DiaChi");
            //    nodeDiaChi.InnerText = System.Web.HttpUtility.UrlEncode(txtDC.Text);

            //    //add to parent node
            //    node.AppendChild(nodeid);
            //    node.AppendChild(nodeLK);
            //    node.AppendChild(nodeDiaChi);
            //    //add to elements collection
            //    xmlDom.DocumentElement.AppendChild(node);

            //    //save back
            //    xmlDom.Save(fullXMLFile);
            //    MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

            //    // Cập nhật lại lưới
            //    CongCu.XML2Grid(fullXMLFile, dataGridViewX_DSLienKet);
            //  //  EnalbeTextBox(false);
            //    isThemMoi = false;
            //    txtDC.Text = "";
            //    txtLK.Text = "";
            //    txtDC.Enabled = false;
            //    txtLK.Enabled = false;
            //    btnSua.Enabled = true;
            //    //XoaInput();
            //} // het them
            //if (isSua) { 
            //    XmlTextReader reader = new XmlTextReader(fullXMLFile);
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(reader);
            //    reader.Close();

            //    //Select the cd node with the matching title
            //    XmlNode oldCd;
            //    XmlElement root = doc.DocumentElement;
            //    int id = Int16.Parse(txtId.Text);
               
               
            //    oldCd = root.SelectSingleNode("/DSLienKet/LienKet[id='" + id + "']");

            //    XmlElement newCd = doc.CreateElement("LienKet");
            //    newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
            //            "<TieuDe>" + txtLK.Text + "</TieuDe>" +
            //            "<DiaChi>" + System.Web.HttpUtility.UrlEncode(txtDC.Text) + "</DiaChi>";
                       
            //    root.ReplaceChild(newCd, oldCd);

            //    //save the output to a file
            //    doc.Save(fullXMLFile);
            //    CongCu.XML2_LienKetGrid(fullXMLFile, dataGridViewX_DSLienKet);
            //    isSua = false;
            //    txtDC.Text = "";
            //    txtLK.Text = "";
            //    txtDC.Enabled = false;
            //    txtLK.Enabled =false;
            //}
            SaveLienKet();
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
            try
            {
                txtLK.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells[1].Value.ToString();
                txtDC.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells[2].Value.ToString();
                txtId.Text = dataGridViewX_DSLienKet.SelectedRows[0].Cells[0].Value.ToString();
                lblHangChon_LK.Text = e.RowIndex.ToString();
            }
            catch {

                txtLK.Text = "";
                txtDC.Text = "";
                txtId.Text = "";
                lblHangChon_LK.Text = "-1";
            }
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

        private void linkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkThem.Text == "Thêm mới")
            {
                txtLK.Enabled = true;
                txtLK.Text = "";

                txtDC.Text = "";
                txtDC.Enabled = true;
                
                linkThem.BackColor = Color.Red;
                linkThem.Text = "Xác nhận thêm";
                linkXoa.Enabled = false;
                linkSua.Enabled = false;
                linkHuy.Text = "Hủy bỏ thêm";
                linkHuy.Visible = true;
            }
            else
            {

                if ((txtLK.Text.Length <= 3) || (txtDC.Text.Length <= 3))
                    MessageBox.Show("Vui lòng nhập tên, địa chỉ liên kết đến", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtLK = CongCu.GetContentAsDataTable(dataGridViewX_DSLienKet);
                    dtLK.LoadDataRow(new[]
                                      {
                                      (dtLK.Rows.Count+1).ToString(),
                                      txtLK.Text,
                                      txtDC.Text                                     
                                       
                                      }, LoadOption.Upsert);
                    try { dataGridViewX_DSLienKet.DataSource = dtLK; dataGridViewX_DSLienKet.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSLienKet.DataSource = null; }
                    int i = 0;

                }
                txtLK.Enabled = false;
                txtLK.Text = "";

                txtDC.Text = "";
                txtDC.Enabled = false;

                linkThem.BackColor = Color.Transparent;
                linkThem.Text = "Thêm mới";
                linkXoa.Enabled = true;
                linkSua.Enabled = true;
                linkHuy.Text = "Hủy bỏ thêm";
                linkHuy.Visible = false;

            }
        }

        private void linkXoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_LK.Text == "-1") MessageBox.Show("Vui lòng chọn liên kết cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtQTct = CongCu.GetContentAsDataTable(dataGridViewX_DSLienKet);
                int i = Int16.Parse(lblHangChon_LK.Text);
                DialogResult rs = MessageBox.Show("Bạn có chắc xóa bản ghi này", "NTUWebgen Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dtQTct.Rows.RemoveAt(i);
                    try { dataGridViewX_DSLienKet.DataSource = dtQTct; dataGridViewX_DSLienKet.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSLienKet.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_LK.Text = "-1";
                    txtLK.Text = "";
                    txtDC.Text = "";
                }
            }
        }

        private void linkSua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSua.Text == "Sửa")
            {
                if (lblHangChon_LK.Text == "-1") MessageBox.Show("Vui lòng chọn liên kết cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtDC.Enabled = true;
                    txtLK.Enabled = true;
                    
                    linkSua.Text = "Xác nhận sửa";
                    linkSua.BackColor = Color.Red;
                    linkThem.Enabled = false;
                    linkXoa.Enabled = false;
                    linkHuy.Text = "Hủy bỏ sửa";
                    linkHuy.Visible = true;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSLienKet);
                int i = Int16.Parse(lblHangChon_LK.Text);
               
                dtMoi.Rows[i]["CotMoTa"] = txtLK.Text;
                dtMoi.Rows[i]["cotDiaChi"] = txtDC.Text;
             

                try { dataGridViewX_DSLienKet.DataSource = dtMoi; dataGridViewX_DSLienKet.Refresh(); }
                catch (Exception ex) { dataGridViewX_DSLienKet.DataSource = null; }

                txtDC.Enabled = false;
                txtLK.Enabled = false;

                linkSua.Text = "Sửa";
                linkSua.BackColor = Color.Transparent;
                linkThem.Enabled = true;
                linkXoa.Enabled = true;
             
                linkHuy.Visible = false;
            }
        }

        private void linkHuy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkThem.Enabled = true; linkThem.BackColor = Color.Transparent; linkThem.Text = "Thêm mới";
            linkXoa.Enabled = true;
            linkSua.Enabled = true; linkSua.BackColor = Color.Transparent; linkSua.Text = "Sửa";
            linkHuy.Visible = false;
            txtDC.Text = "";
            txtLK.Text = "";
        }

        void LoadLienKet()
            { // Đọc file XML
                XmlTextReader reader = new XmlTextReader(fullXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
             XmlElement root = doc.DocumentElement;
             XmlNodeList Nodes_LK = root.SelectNodes("/root/DSLienKet/LienKet");
             XmlNode[] lk = Nodes_LK.Cast<XmlNode>().ToArray();

             DataTable dtLK = CongCu.GetContentAsDataTable(dataGridViewX_DSLienKet);
             foreach (XmlNode k in lk)
             {
                 String STT = k.ChildNodes[0].InnerText;
                 String dc = k.ChildNodes[1].InnerText;
                 String tenlk = System.Web.HttpUtility.UrlDecode(k.ChildNodes[2].InnerText);
                 
                 dtLK.LoadDataRow(new[]
                                      {
                                      STT,
                                      dc,
                                      tenlk
                                      }, LoadOption.Upsert);
             }
             try { dataGridViewX_DSLienKet.DataSource = dtLK; }
             catch { dataGridViewX_DSLienKet.DataSource = null; }
          
        }

        void SaveLienKet()
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNode Nodes_LK= root.SelectSingleNode("DSLienKet");
            XmlElement xmlENodes_LK = Nodes_LK as XmlElement;
            Nodes_LK.InnerText = "";
            DataTable dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSLienKet);
            foreach (DataRow r in dtTarget.Rows)
                {
                    XmlElement newNode = doc.CreateElement("LienKet");
                    newNode.InnerXml =
                          "<id>" + r[0] + "</id>" +
                          "<TieuDe>" + r[1] + "</TieuDe>" +
                          "<DiaChi>" +System.Web.HttpUtility.UrlEncode(r[2].ToString()) + "</DiaChi>";
                    xmlENodes_LK.AppendChild(newNode);
                }
              
            doc.Save(fullXMLFile);
            MessageBox.Show("Lưu thành công", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CongCu.gotoSite(fullHTMLFile);
        }
    }
}
