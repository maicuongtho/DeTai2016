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
    public partial class Publication_BaoCaoHoiThao0 : UserControl
    {
        bool isThemmoi, isSua, isXoa;
        String baocao0XMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\BaoCaoHoiThao0.xml";
        String pubHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\publications.html";
        String pubHTMLFolder = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/";
        String ProjectFolder;
        public Publication_BaoCaoHoiThao0()
        {
            InitializeComponent();
            EnableInput(false);
            isThemmoi = false; isSua = false; isXoa = false;
            CongCu.XML2Grid(baocao0XMLFile, dataGridViewX_DSBaoCao0);
        }


        public Publication_BaoCaoHoiThao0(String ProjectFolder)
        {
            InitializeComponent();
            EnableInput(false);
            this.ProjectFolder = ProjectFolder;
            this.baocao0XMLFile = ProjectFolder + "\\data\\BaoCaoHoiThao0.xml";
            this.pubHTMLFile = ProjectFolder + "\\publications.html";
            this.pubHTMLFolder = ProjectFolder.Replace("\\", "/");
            isThemmoi = false; isSua = false; isXoa = false;
            CongCu.XML2Grid(baocao0XMLFile, dataGridViewX_DSBaoCao0);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaInput();
            EnableInput(true);
            isThemmoi = true;
            btnSave.Enabled = true;
        }
        void EnableInput(bool t)
        {
            txtTacGia.Enabled = t;
            txtNam.Enabled = t;
            txtTenBaiBao.Enabled = t;
            txtTenHoiThao.Enabled = t;
            txtThoiGianDiaDiem.Enabled = t;
            txtURL.Enabled = t;
            radioButton2.Enabled = t;
            radioButtonViet.Enabled = t;
        }
        void XoaInput()
        {
            txtTacGia.Text = "";
            txtNam.Text = "";
            txtTenBaiBao.Text = "";
            txtTenHoiThao.Text = "";
            txtThoiGianDiaDiem.Text = "";
            txtURL.Text = "";
            txtId.Text = "";
            txtTacGia.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Báo cáo cần xóa", "Thông báo");
                return;
            }
            XmlTextReader reader = new XmlTextReader(baocao0XMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the cd node with the matching title
            XmlNode oldCd;
            XmlElement root = doc.DocumentElement;
            int id = Int16.Parse(txtId.Text);

            oldCd = root.SelectSingleNode("/DSBaoCao0/BaoCao0[id='" + id + "']");
            DialogResult rs = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                root.RemoveChild(oldCd);
                //save the output to a file
                doc.Save(baocao0XMLFile);
                CongCu.XML2Grid(baocao0XMLFile, dataGridViewX_DSBaoCao0);
                XoaInput();
                EnableInput(false);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Báo cáo cần sửa", "Thông báo");
                return;
            }
            isSua = true;
            EnableInput(true);   
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

            #region Thêm mới
            if (isThemmoi)
            {
                //create new instance of XmlDocument
                XmlDocument xmlDom = new XmlDocument();
                //load from file
                xmlDom.Load(baocao0XMLFile);

                //create node and add value
                XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "BaoCao0", null);

                //create id node
                XmlNode nodeid = xmlDom.CreateElement("id");
                //add value for it
                nodeid.InnerText = getMaxId(baocao0XMLFile).ToString();


                //node TacGia
                XmlNode nodeTacGia = xmlDom.CreateElement("TacGia");
                nodeTacGia.InnerText = txtTacGia.Text;


                XmlNode nodeNam = xmlDom.CreateElement("Nam");
                nodeNam.InnerText = txtNam.Text;

                XmlNode nodeTenBaoCao = xmlDom.CreateElement("TenBaoCao");
                nodeTenBaoCao.InnerText = txtTenBaiBao.Text;

                XmlNode nodeTenHoiThao = xmlDom.CreateElement("TenHoiThao");
                nodeTenHoiThao.InnerText = txtTenHoiThao.Text;

                XmlNode nodeThoiGianDiaDiem = xmlDom.CreateElement("ThoiGianDiaDiem");
                nodeThoiGianDiaDiem.InnerText = txtThoiGianDiaDiem.Text;

                XmlNode nodeURL = xmlDom.CreateElement("URL");
                nodeURL.InnerText = txtURL.Text;

                XmlNode nodeNgonNgu = xmlDom.CreateElement("NgonNgu");
                if (radioButtonViet.Checked) nodeNgonNgu.InnerText = "Tiếng Việt";
                else nodeNgonNgu.InnerText = "Tiếng Anh";



                //add to parent node
                node.AppendChild(nodeid);
                node.AppendChild(nodeTacGia);
                node.AppendChild(nodeNam);
                node.AppendChild(nodeTenBaoCao);
                node.AppendChild(nodeNgonNgu);
                node.AppendChild(nodeTenHoiThao);
                node.AppendChild(nodeThoiGianDiaDiem);
                node.AppendChild(nodeURL);


                //add to elements collection
                xmlDom.DocumentElement.AppendChild(node);

                //save back
                xmlDom.Save(baocao0XMLFile);
                MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

                // Cập nhật lại lưới
                CongCu.XML2Grid(baocao0XMLFile, dataGridViewX_DSBaoCao0);
                EnableInput(false);
                isThemmoi = false;
                XoaInput();
            }
            #endregion Thêm mới

            #region Sửa
            if (isSua)
            {
                XmlTextReader reader = new XmlTextReader(baocao0XMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                int id = Int16.Parse(txtId.Text);
                String strNgonNgu = "Tiếng Việt";
                if (!radioButtonViet.Checked) strNgonNgu = "Tiếng Anh";
                oldCd = root.SelectSingleNode("/DSBaoCao0/BaoCao0[id='" + id + "']");

                XmlElement newCd = doc.CreateElement("BaoCao0");
                newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
                        "<TacGia>" + txtTacGia.Text + "</TacGia>" +
                        "<Nam>" + txtNam.Text + "</Nam>" +
                        "<TenBaoCao>" + txtTenBaiBao.Text + "</TenBaoCao>" +
                        "<NgonNgu>" + strNgonNgu + "</NgonNgu>" +
                        "<TenHoiThao>" + txtTenHoiThao.Text + "</TenHoiThao>" +
                        "<ThoiGianDiaDiem>" + txtThoiGianDiaDiem.Text + "</ThoiGianDiaDiem>" +
                        "<URL>" + txtURL.Text + "</URL>";

                root.ReplaceChild(newCd, oldCd);

                //save the output to a file
                doc.Save(baocao0XMLFile);
                CongCu.XML2Grid(baocao0XMLFile, dataGridViewX_DSBaoCao0);
                isSua = false;
                EnableInput(false);

            }
            #endregion Sửa
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = CongCu.XML2HTML_BaoCaoHoiThao0(baocao0XMLFile, "BaoCao0").ToString();
            CongCu.ReplaceContent(pubHTMLFile, "BaoCao0", noiDungMoi);

            // Dành cho mẫu 4
            UserInfo u = CongCu.getUserInfo(ProjectFolder + "\\data\\index.xml");
            CongCu.ReplaceContent(pubHTMLFile, "anhTrai", "<img src=\"" + u.HinhAnh + "\" width=100% height=186px>");
            CongCu.ReplaceContent(pubHTMLFile, "tenTrai", "website của " + u.HoTen);
            // Thêm tiêu đề
            CongCu.ReplaceTite(pubHTMLFile, "NTU. " + u.HoTen + "-Báo cáo hội thảo");


            MessageBox.Show("Đã xuất thành công sang trang web: \n" + pubHTMLFile, "Thông báo");
        }

       
        private void dataGridViewX_DSBaoCao0_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTacGia.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TacGia"].Value.ToString();
            txtNam.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["Nam"].Value.ToString();
            txtTenBaiBao.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TenBaoCao"].Value.ToString();
            txtTenHoiThao.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TenHoiThao"].Value.ToString();
            txtThoiGianDiaDiem.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["ThoiGianDiaDiem"].Value.ToString();
            txtURL.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["URL"].Value.ToString();
            txtId.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["STT"].Value.ToString();
            if (dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["NgonNgu"].Value.ToString() == "Tiếng Việt")
            {
                radioButtonViet.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButtonViet.Checked = false;
                radioButton2.Checked = true;
            }
        }

    }
}
