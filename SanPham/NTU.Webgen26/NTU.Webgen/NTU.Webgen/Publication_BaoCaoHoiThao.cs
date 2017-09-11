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
using System.IO;

namespace NTU.Webgen
{
    public partial class Publication_BaoCaoHoiThao : UserControl
    {
        bool isThemmoi, isSua, isXoa;
        String baocaoXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\BaoCaoHoiThao.xml";
        String pubHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\publications.html";
        String pubHTMLFolder = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/";
        String ProjectFolder;
        public Publication_BaoCaoHoiThao()
        {
            InitializeComponent();
            EnableInput(false);
            isThemmoi = false; isSua=false; isXoa=false;
            CongCu.XML2Grid(baocaoXMLFile, dataGridViewX_DSBaoCao);
        }

        public Publication_BaoCaoHoiThao(String ProjectFolder)
        {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.baocaoXMLFile = ProjectFolder + "\\data\\BaoCaoHoiThao.xml";
            this.pubHTMLFile = ProjectFolder + "\\publications.html";
            this.pubHTMLFolder = ProjectFolder.Replace("\\", "/");
            EnableInput(false);
            isThemmoi = false; isSua = false; isXoa = false;
            CongCu.XML2Grid(baocaoXMLFile, dataGridViewX_DSBaoCao);
        }

        private void txtSo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaInput();
            EnableInput(true);
            isThemmoi = true;
            btnSave.Enabled = true;
        }
        void EnableInput(bool t) {
            txtTacGia.Enabled = t;
            txtNam.Enabled = t;
            txtTenBaiBao.Enabled = t;
            txtTenHoiThao.Enabled = t;
            txtThoiGianDiaDiem.Enabled = t;
            txtNhaXuatBan.Enabled = t;
            txtNoiXuatBan.Enabled= t;
            txtTrang.Enabled = t;
            radioButton2.Enabled = t;
            radioButtonViet.Enabled = t;
        }
        void XoaInput() {
            txtTacGia.Text = "";
            txtNam.Text = "";
            txtTenBaiBao.Text = "";
            txtTenHoiThao.Text = "";
            txtThoiGianDiaDiem.Text = "";
            txtNhaXuatBan.Text = "";
            txtNoiXuatBan.Text = "";
            txtTrang.Text = "";
            txtId.Text = "";
            txtTacGia.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Thêm mới
            if (isThemmoi)
            {
                //create new instance of XmlDocument
                XmlDocument xmlDom = new XmlDocument();
                //load from file
                xmlDom.Load(baocaoXMLFile);

                //create node and add value
                XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "BaoCao1", null);

                //create id node
                XmlNode nodeid = xmlDom.CreateElement("id");
                //add value for it
                nodeid.InnerText = getMaxId(baocaoXMLFile).ToString();


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

                XmlNode nodeNhaXB = xmlDom.CreateElement("NhaXuatBan");
                nodeNhaXB.InnerText = txtNhaXuatBan.Text;

                XmlNode nodeNoiXB = xmlDom.CreateElement("NoiXuatBan");
                nodeNoiXB.InnerText = txtNoiXuatBan.Text;


                XmlNode nodeTrang = xmlDom.CreateElement("Trang");
                nodeTrang.InnerText = txtTrang.Text;
                
                XmlNode nodeNgonNgu = xmlDom.CreateElement("NgonNgu");
                if (radioButtonViet.Checked) nodeNgonNgu.InnerText = "Tiếng Việt";
                else nodeNgonNgu.InnerText = "Tiếng Anh";

                XmlNode nodeDinhKem = xmlDom.CreateElement("DinhKem");
                // Luu file dinh kem
                String fileNguon = lblDinhKem.Text;

                if (fileNguon != "")
                {
                    int i = fileNguon.LastIndexOf("\\");
                    String tenFile = fileNguon.Substring(i+1);
                    String FileDinhKem = ProjectFolder + "\\data\\DinhKem\\" + tenFile;
                    File.Copy(fileNguon, FileDinhKem);
                    nodeDinhKem.InnerText = FileDinhKem;
                }

                //add to parent node
                node.AppendChild(nodeid);
                node.AppendChild(nodeTacGia);
                node.AppendChild(nodeNam);
                node.AppendChild(nodeTenBaoCao);
                node.AppendChild(nodeNgonNgu);
                node.AppendChild(nodeTenHoiThao);
                node.AppendChild(nodeThoiGianDiaDiem);
                node.AppendChild(nodeNhaXB);
                node.AppendChild(nodeNoiXB);
                node.AppendChild(nodeTrang);
                node.AppendChild(nodeDinhKem);

                //add to elements collection
                xmlDom.DocumentElement.AppendChild(node);

                //save back
                xmlDom.Save(baocaoXMLFile);
                MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

                // Cập nhật lại lưới
                CongCu.XML2Grid(baocaoXMLFile, dataGridViewX_DSBaoCao);
                EnableInput(false);
                isThemmoi = false;
                XoaInput();
            }
            #endregion Thêm mới

            #region Sửa
            if (isSua)
            {
                XmlTextReader reader = new XmlTextReader(baocaoXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                int id = Int16.Parse(txtId.Text);
                String strNgonNgu = "Tiếng Việt";
                if (!radioButtonViet.Checked) strNgonNgu = "Tiếng Anh";
                oldCd = root.SelectSingleNode("/DSBaoCao1/BaoCao1[id='" + id + "']");

                String fileDinhKemGoc = oldCd.ChildNodes[10].InnerText;
                //MessageBox.Show(fileDinhKemGoc);
                String fileDinhKem = lblDinhKem.Text;
                if (fileDinhKem != fileDinhKemGoc)
                {
                    int i = fileDinhKem.LastIndexOf("\\");
                    String tenFile = fileDinhKem.Substring(i+1);
                    fileDinhKemGoc = ProjectFolder + "\\data\\DinhKem\\" + tenFile;
                    try { File.Copy(fileDinhKem, fileDinhKemGoc); }
                    catch (Exception exx)
                    {
                        int oo = 0; ;
                    }
                }



                XmlElement newCd = doc.CreateElement("BaoCao1");
                newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
                        "<TacGia>" + txtTacGia.Text + "</TacGia>" +
                        "<Nam>" + txtNam.Text + "</Nam>" +
                        "<TenBaoCao>" + txtTenBaiBao.Text + "</TenBaoCao>" +
                        "<NgonNgu>" + strNgonNgu + "</NgonNgu>" +
                        "<TenHoiThao>" + txtTenHoiThao.Text + "</TenHoiThao>" +
                        "<ThoiGianDiaDiem>" + txtThoiGianDiaDiem.Text + "</ThoiGianDiaDiem>" +
                        "<NhaXuatBan>" + txtNhaXuatBan.Text + "</NhaXuatBan>" +
                        "<NoiXuatBan>" + txtNoiXuatBan.Text + "</NoiXuatBan>" +
                        "<Trang>" + txtTrang.Text + "</Trang>"+
                        "<DinhKem>" + fileDinhKemGoc + "</DinhKem>"; ;

                root.ReplaceChild(newCd, oldCd);

                //save the output to a file
                doc.Save(baocaoXMLFile);
                CongCu.XML2Grid(baocaoXMLFile, dataGridViewX_DSBaoCao);
                isSua = false;
                EnableInput(false);
            
            }
            #endregion Sửa
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

        private void dataGridViewX_DSBaoCao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTacGia.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TacGia"].Value.ToString();
            txtNam.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["Nam"].Value.ToString();
            txtTenBaiBao.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TenBaoCao"].Value.ToString();
            txtTenHoiThao.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TenHoiThao"].Value.ToString();
            txtThoiGianDiaDiem.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["ThoiGianDiaDiem"].Value.ToString();
            txtNhaXuatBan.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NhaXuatBan"].Value.ToString();
            txtNoiXuatBan.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NoiXuatBan"].Value.ToString();
            txtId.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["STT"].Value.ToString();
            if (dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NgonNgu"].Value.ToString() == "Tiếng Việt")
            {
                radioButtonViet.Checked = true;
                radioButton2.Checked = false;
            }
            else {
                radioButtonViet.Checked = false;
                radioButton2.Checked = true;
            }

            lblDinhKem.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["DinhKem"].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Báo cáo cần xóa", "Thông báo");
                return;
            }
            XmlTextReader reader = new XmlTextReader(baocaoXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the cd node with the matching title
            XmlNode oldCd;
            XmlElement root = doc.DocumentElement;
            int id = Int16.Parse(txtId.Text);

            oldCd = root.SelectSingleNode("/DSBaoCao1/BaoCao1[id='" + id + "']");
            DialogResult rs = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                root.RemoveChild(oldCd);
                //save the output to a file
                doc.Save(baocaoXMLFile);
                CongCu.XML2Grid(baocaoXMLFile, dataGridViewX_DSBaoCao);
                XoaInput();
                EnableInput(false);
            }
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = CongCu.XML2HTML_BaoCaoHoiThao(baocaoXMLFile, "BaoCao1").ToString();
            CongCu.ReplaceContent(pubHTMLFile, "BaoCao1", noiDungMoi);

            // Dành cho mẫu 4
            UserInfo u = CongCu.getUserInfo(ProjectFolder + "\\data\\index.xml");
            CongCu.ReplaceContent(pubHTMLFile, "anhTrai", "<img src=\"" + u.HinhAnh + "\" width=100% height=186px>");
            CongCu.ReplaceContent(pubHTMLFile, "tenTrai", "website của " + u.HoTen);
            // Thêm tiêu đề
            CongCu.ReplaceTite(pubHTMLFile, "NTU. " + u.HoTen + "-Báo cáo hội thảo");

            MessageBox.Show("Đã xuất thành công sang trang web: \n" + pubHTMLFile,"Thông báo");
        }

        private void btnDinhKem_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog1.ShowDialog();
            if (rs == DialogResult.OK) lblDinhKem.Text = openFileDialog1.FileName;
        }

       
    }
}
