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
using System.Xml.Linq;

namespace NTU.Webgen
{
       
    public partial class Publication_TapChi : UserControl
    {
        bool isThemmoi, isSua, isXoa;
        String pubXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\TapChi.xml";
        String pubHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\publications.html";
        String pubHTMLFolder = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/";
        public Publication_TapChi()
        {
            InitializeComponent();
            EnalbeTextBox(false);
            CongCu.XML2Grid(pubXMLFile,dataGridViewX_DSTC);
        }

       // Thêm mới dữ liệu vào Grid
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaInput();
            EnalbeTextBox(true);
            isThemmoi = true;
            btnSave.Enabled = true;
         }

       
        void XoaInput()
        {
            txtTacGia.Text = "";
            txtNam.Text = "";
            txtTenBaiBao.Text = "";
            txtTenTapChi.Text = "";
            txtTap.Text = "";
            txtSo.Text = "";
            txtTrang.Text = "";
            txtId.Text = "";
            txtTacGia.Focus();
        }
        private void dataGridViewX_DSTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTacGia.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TacGia"].Value.ToString();
            txtNam.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Nam"].Value.ToString();
            txtTenBaiBao.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TieuDeBaiBao"].Value.ToString();
            txtTenTapChi.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TenTapChi"].Value.ToString();
            txtTap.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Tap"].Value.ToString();
            txtSo.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["So"].Value.ToString();
            txtTrang.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Trang"].Value.ToString();

            txtId.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["STT"].Value.ToString();
            if (dataGridViewX_DSTC.SelectedRows[0].Cells["NgonNgu"].Value.ToString() == "Tiếng Việt")
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

        // Lưu dữ liệu vào XML, cập nhật lưới
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isThemmoi)
            {

                //create new instance of XmlDocument
                XmlDocument xmlDom = new XmlDocument();
                //load from file
                xmlDom.Load(pubXMLFile);

                //create node and add value
                XmlNode node = xmlDom.CreateNode(XmlNodeType.Element, "BaiBao", null);

                //create id node
                XmlNode nodeid = xmlDom.CreateElement("id");
                //add value for it
                nodeid.InnerText = getMaxId(pubXMLFile).ToString();


                //node TacGia
                XmlNode nodeTacGia = xmlDom.CreateElement("TacGia");
                nodeTacGia.InnerText = txtTacGia.Text;


                XmlNode nodeNam = xmlDom.CreateElement("Ngay");
                nodeNam.InnerText = txtNam.Text;

                XmlNode nodeTieuDeBaiBao = xmlDom.CreateElement("TieuDeBaiBao");
                nodeTieuDeBaiBao.InnerText = txtTenBaiBao.Text;

                XmlNode nodeTenTapChi = xmlDom.CreateElement("TenTapChi");
                nodeTenTapChi.InnerText = txtTenTapChi.Text;

                XmlNode nodeTap = xmlDom.CreateElement("Tap");
                nodeTap.InnerText = txtTap.Text;

                XmlNode nodeSo = xmlDom.CreateElement("So");
                nodeSo.InnerText = txtSo.Text;

                XmlNode nodeTrang = xmlDom.CreateElement("Trang");
                nodeTrang.InnerText = txtTrang.Text;

                XmlNode nodeNgonNgu = xmlDom.CreateElement("NgonNgu");
                if (radioButtonViet.Checked) nodeNgonNgu.InnerText = "Tiếng Việt";
                else nodeNgonNgu.InnerText = "Tiếng Anh";

                //add to parent node
                node.AppendChild(nodeid);
                node.AppendChild(nodeTacGia);
                node.AppendChild(nodeNam);
                node.AppendChild(nodeTieuDeBaiBao);
                node.AppendChild(nodeNgonNgu);
                node.AppendChild(nodeTenTapChi);
                node.AppendChild(nodeTap);
                node.AppendChild(nodeSo);
                node.AppendChild(nodeTrang);


                //add to elements collection
                xmlDom.DocumentElement.AppendChild(node);

                //save back
                xmlDom.Save(pubXMLFile);
                MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK);

                // Cập nhật lại lưới
                CongCu.XML2Grid(pubXMLFile, dataGridViewX_DSTC);
                EnalbeTextBox(false);
                isThemmoi = false;
                XoaInput();
            }
            if (isSua)
            {
                XmlTextReader reader = new XmlTextReader(pubXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                int id = Int16.Parse(txtId.Text);
                String strNgonNgu = "Tiếng Việt";
                if (!radioButtonViet.Checked) strNgonNgu = "Tiếng Anh";
                oldCd = root.SelectSingleNode("/DSBaiBao/BaiBao[id='" + id + "']");

                XmlElement newCd = doc.CreateElement("BaiBao");
                newCd.InnerXml = "<id>" + this.txtId.Text + "</id>" +
                        "<TacGia>" + txtTacGia.Text + "</TacGia>" +
                        "<Ngay>" + txtNam.Text + "</Ngay>" +
                        "<TieuDeBaiBao>" + txtTenBaiBao.Text + "</TieuDeBaiBao>" +
                        "<NgonNgu>" + strNgonNgu + "</NgonNgu>" +
                        "<TenTapChi>" + txtTenTapChi.Text + "</TenTapChi>" +
                        "<Tap>" + txtTap.Text + "</Tap>" +
                        "<So>" + txtSo.Text + "</So>" +
                        "<Trang>" + txtTrang.Text + "</Trang>";

                root.ReplaceChild(newCd, oldCd);

                //save the output to a file
                doc.Save(pubXMLFile);
                CongCu.XML2Grid(pubXMLFile, dataGridViewX_DSTC);
                isSua = false;
                EnalbeTextBox(false);
                
            }


        }
        int getMaxId(String xmlFile) {
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
            return MaxID+1;
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = CongCu.XML2HTML_TapChi(pubXMLFile, "BaiBao").ToString();

            CongCu.ReplaceContent(pubHTMLFile, "TapChi", noiDungMoi);
            MessageBox.Show("Đã xuất thành công sang trang web: \n" + pubHTMLFile, "Thông báo");
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Báo cáo cần sửa", "Thông báo");
                return;
            }
            isSua = true;
            EnalbeTextBox(true);    
        }

        public void EnalbeTextBox(bool b) {
            txtTacGia.Enabled = b;
            txtNam.Enabled = b;
            txtTenBaiBao.Enabled = b;
            txtTenTapChi.Enabled = b;
            txtTap.Enabled = b;
            txtSo.Enabled = b;
            txtTrang.Enabled = b;
            radioButton2.Enabled = b;
            radioButtonViet.Enabled = b;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            String s = System.AppDomain.CurrentDomain.BaseDirectory;
            MessageBox.Show(s);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Báo cáo cần xóa", "Thông báo");
                return;
            }
            XmlTextReader reader = new XmlTextReader(pubXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the cd node with the matching title
            XmlNode oldCd;
            XmlElement root = doc.DocumentElement;
            int id = Int16.Parse(txtId.Text);

            oldCd = root.SelectSingleNode("/DSBaiBao/BaiBao[id='" + id + "']");
            DialogResult rs = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                root.RemoveChild(oldCd);
                //save the output to a file
                doc.Save(pubXMLFile);
                CongCu.XML2Grid(pubXMLFile, dataGridViewX_DSTC);
                XoaInput();
                EnalbeTextBox(false);
            }
        }      
    }
}
