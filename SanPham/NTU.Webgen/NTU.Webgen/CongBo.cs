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
    public partial class CongBo : UserControl
    {
        static String xmlCongBoFile = @"\data\congbo.xml";
        //static String xmlTapChi = @"\data\TapChi.xml";
        //static String xmlSach = @"\data\\Sach.xml";
        //static String xmlBaoCao1 = @"\data\BaoCaoHoiThao.xml";
        //static String xmlBaoCao0 = @"\data\BaoCaoHoiThao0.xml";

        static String htmlCongBoFile = @"\publications.html";
        String ProjectFolder;
        
        String fullXMLFile;
        String fullHTMLFile;
        public CongBo()
        {
            InitializeComponent();
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            fullXMLFile  = ProjectFolder + xmlCongBoFile;
            fullHTMLFile = ProjectFolder + htmlCongBoFile;
            LoadTapChi();
            LoadBaoCao();
            LoadBaoCao0();
            LoadSach();
            linkWebCourse.Text = fullHTMLFile;
          
        }
        public CongBo(String ProjectFolder) {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            fullXMLFile = ProjectFolder + xmlCongBoFile;
            fullHTMLFile = ProjectFolder + htmlCongBoFile;
            LoadTapChi();
            LoadBaoCao();
            LoadBaoCao0();
            LoadSach();
            linkWebCourse.Text = fullHTMLFile;
        }
        void LoadTapChi()
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;

            XmlNodeList Nodes_TC = root.SelectNodes("/root/DSBaiBao/BaiBao");
            XmlNode[] tc = Nodes_TC.Cast<XmlNode>().ToArray();

            //Sắp xếp
            //Array.Sort(tc, (n1, n2) =>
            //{
            //    int res = String.CompareOrdinal(n1.ChildNodes[2].InnerText, n2.ChildNodes[2].InnerText);
            //    if (res == 0)
            //    {
            //        return String.CompareOrdinal(n1.OuterXml, n2.OuterXml);
            //    }
            //    return res;
            //});
            // hết sx

            DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSTC);
            dt.Clear();
            foreach (XmlNode k in tc)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tacgia = k.ChildNodes[1].InnerText;
                String nam = k.ChildNodes[2].InnerText;
                String tieude = k.ChildNodes[3].InnerText;
                String tentapchi = k.ChildNodes[4].InnerText;
                String tap = k.ChildNodes[5].InnerText;
                String so = k.ChildNodes[6].InnerText;
                String trang = k.ChildNodes[7].InnerText;
                String ngonngu = k.ChildNodes[8].InnerText;
                String dinhkem = k.ChildNodes[9].InnerText;
                dt.LoadDataRow(new[] { STT, tacgia, nam, tieude, tentapchi, tap, so, trang, ngonngu, dinhkem }, LoadOption.Upsert);
            }
            try { dataGridViewX_DSTC.DataSource = dt; }
            catch { dataGridViewX_DSTC.DataSource = null; }

        }
        void LoadBaoCao() {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;

            XmlNodeList Nodes_bc = root.SelectNodes("/root/DSBaoCao1/BaoCao1");
            XmlNode[] bc = Nodes_bc.Cast<XmlNode>().ToArray();
            DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao);
            dt.Clear();
            foreach (XmlNode k in bc)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tacgia = k.ChildNodes[1].InnerText;
                String nam = k.ChildNodes[2].InnerText;
                String tenbaocao = k.ChildNodes[3].InnerText;
                String tenhoithao = k.ChildNodes[4].InnerText;
                String thoigian = k.ChildNodes[5].InnerText;
                String nhaxb = k.ChildNodes[6].InnerText;
                String noixb = k.ChildNodes[7].InnerText;
                String trang = k.ChildNodes[8].InnerText;
                String ngonngu = k.ChildNodes[9].InnerText;
                String kem = k.ChildNodes[10].InnerText;

                dt.LoadDataRow(new[]
                                      {
                                      STT,
                                      tacgia,
                                      nam,
                                      tenbaocao,
                                      tenhoithao,
                                      thoigian,
                                      nhaxb,
                                      noixb,
                                      trang,
                                      ngonngu,
                                      kem
                                      }, LoadOption.Upsert);
            }
            try { dataGridViewX_DSBaoCao.DataSource = dt; }
            catch { dataGridViewX_DSBaoCao.DataSource = null; }
        }
        void LoadBaoCao0()
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;

            XmlNodeList Nodes_bc = root.SelectNodes("/root/DSBaoCao0/BaoCao0");
            XmlNode[] bc = Nodes_bc.Cast<XmlNode>().ToArray();
            DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao0);
            dt.Clear();
            foreach (XmlNode k in bc)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tacgia = k.ChildNodes[1].InnerText;
                String nam = k.ChildNodes[2].InnerText;
                String tenbaocao = k.ChildNodes[3].InnerText;
                String tenhoithao = k.ChildNodes[4].InnerText;
                String thoigian = k.ChildNodes[5].InnerText;
                String ngonngu = k.ChildNodes[6].InnerText;
                String url = k.ChildNodes[7].InnerText;

                dt.LoadDataRow(new[]
                                      {
                                      STT,
                                      tacgia,
                                      nam,
                                      tenbaocao,
                                      tenhoithao,
                                      thoigian,
                                      ngonngu,
                                      url
                                      }, LoadOption.Upsert);
            }
            try { dataGridViewX_DSBaoCao0.DataSource = dt; }
            catch { dataGridViewX_DSBaoCao0.DataSource = null; }
        }
        void LoadSach()
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;

            XmlNodeList Nodes_s = root.SelectNodes("/root/DSSach/Sach");
            XmlNode[] s = Nodes_s.Cast<XmlNode>().ToArray();
            DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSSach);
            dt.Clear();
            foreach (XmlNode k in s)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tacgia = k.ChildNodes[1].InnerText;
                String namxb = k.ChildNodes[2].InnerText;
                String tensach = k.ChildNodes[3].InnerText;
                String nhaxb = k.ChildNodes[4].InnerText;
                String noixb = k.ChildNodes[5].InnerText;
                
                dt.LoadDataRow(new[] { STT, tacgia, namxb, tensach, nhaxb, noixb}, LoadOption.Upsert);
            }
            try { dataGridViewX_DSSach.DataSource = dt; }
            catch { dataGridViewX_DSSach.DataSource = null; }

        }
        #region Thao tac Tap chi
        
        private void dataGridViewX_DSTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["STT"].Value.ToString();
                txtTacGia.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TacGia"].Value.ToString();
                txtNam.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Nam"].Value.ToString();
                txtTenBaiBao.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TieuDeBaiBao"].Value.ToString();
                txtTenTapChi.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["TenTapChi"].Value.ToString();
                txtTap.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Tap"].Value.ToString();
                txtSo.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["So"].Value.ToString();
                txtTrang.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["Trang"].Value.ToString();
                lblHangChon_TapChi.Text = e.RowIndex.ToString();
                txtDinhKem.Text = dataGridViewX_DSTC.SelectedRows[0].Cells["DinhKem"].Value.ToString();

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
            catch
            {
                txtTacGia.Text = "";
                txtNam.Text = "";
                txtTenBaiBao.Text = "";
                txtTenTapChi.Text = "";
                txtTap.Text = "";
                txtSo.Text = "";
                txtTrang.Text = "";
                txtId.Text = "";
            }
        }

        private void linkTapChi_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkTapChi_Them.Text == "Thêm mới")
            {
                txtTacGia.Text = "";
                txtNam.Text = "";
                txtTenBaiBao.Text = "";
                txtTenTapChi.Text = "";
                txtTap.Text = "";
                txtSo.Text = "";
                txtTrang.Text = "";
                txtId.Text = "";
                txtTacGia.Enabled = true;
                txtNam.Enabled = true;
                txtTenBaiBao.Enabled = true;
                txtTenTapChi.Enabled = true;
                txtTap.Enabled = true;
                txtSo.Enabled = true;
                txtTrang.Enabled = true;
                txtId.Enabled = true;
                btnDinhKem.Enabled = true;
                radioButtonViet.Enabled = true;
                radioButton2.Enabled = true;

                linkTapChi_Them.BackColor = Color.Red;
                linkTapChi_Them.Text = "Xác nhận thêm";
                linkTapChi_Sua.Enabled = false;
                linkTapChi_Xoa.Enabled = false;
                linkTapChi_Huy.Text = "Hủy bỏ thêm";
                linkTapChi_Huy.Visible = true;
            }
            else 
            {
                // Thêm vào lưới
                if ((txtTacGia.Text.Length <= 3)||(txtTenBaiBao.Text.Length<=5))
                    MessageBox.Show("Vui lòng nhập Tác giả/ Tên bài báo", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String ngongu = "Tiếng Việt";
                    if (!radioButtonViet.Checked) ngongu = "Tiếng Anh";
                    String fileDinhDem = txtDinhKem.Text;//.Substring(txtDinhKem.Text.LastIndexOf("\\")+1);

                    DataTable dtTapChi = CongCu.GetContentAsDataTable(dataGridViewX_DSTC);
                    dtTapChi.LoadDataRow(new[]
                                      {
                                      (dtTapChi.Rows.Count+1).ToString(),
                                                                          txtTacGia.Text,
                                      txtNam.Text,
                                      txtTenBaiBao.Text,
                                      txtTenTapChi.Text,
                                      txtTap.Text,
                                      txtSo.Text,
                                      txtTrang.Text,  
                                      ngongu,
                                      fileDinhDem
                                      }, LoadOption.Upsert);
                    try { dataGridViewX_DSTC.DataSource = dtTapChi; dataGridViewX_DSTC.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSTC.DataSource = null; }

                }

                //--------------------
                txtTacGia.Text = "";
                txtNam.Text = "";
                txtTenBaiBao.Text = "";
                txtTenTapChi.Text = "";
                txtTap.Text = "";
                txtSo.Text = "";
                txtTrang.Text = "";
                txtId.Text = "";
                txtTacGia.Enabled = false;
                txtNam.Enabled = false;
                txtTenBaiBao.Enabled = false;
                txtTenTapChi.Enabled = false;
                txtTap.Enabled = false;
                txtSo.Enabled = false;
                txtTrang.Enabled = false;
                txtId.Enabled = false;
                btnDinhKem.Enabled = false;
                radioButtonViet.Enabled = false;
                radioButton2.Enabled = false;
                linkTapChi_Them.BackColor = Color.Transparent;
                linkTapChi_Them.Text ="Thêm mới";
                linkTapChi_Sua.Enabled = true;
                linkTapChi_Xoa.Enabled = true;
                linkTapChi_Huy.Text = "Hủy";
                linkTapChi_Huy.Visible = false;
            }

        }

        private void linkTapChi_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_TapChi.Text == "-1") MessageBox.Show("Vui lòng chọn báo cáo tạp chí cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtQTct = CongCu.GetContentAsDataTable(dataGridViewX_DSTC);
                int i = Int16.Parse(lblHangChon_TapChi.Text);
                DialogResult rs = MessageBox.Show("Bạn có muốn xóa: " + dtQTct.Rows[i]["TieuDeBaiBao"].ToString(), "NTUWebgen Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dtQTct.Rows.RemoveAt(i);
                    try { dataGridViewX_DSTC.DataSource = dtQTct; dataGridViewX_DSTC.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSTC.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_TapChi.Text = "-1";

                    txtTacGia.Text = "";
                    txtNam.Text = "";
                    txtTenBaiBao.Text = "";
                    txtTenTapChi.Text = "";
                    txtTap.Text = "";
                    txtSo.Text = "";
                    txtTrang.Text = "";
                    txtId.Text = "";
                }
            }
        }

        private void linkTapChi_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkTapChi_Sua.Text == "Sửa")
            {
                if (lblHangChon_TapChi.Text == "-1") MessageBox.Show("Vui lòng chọn tạp chí cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTacGia.Enabled = true;
                    txtNam.Enabled = true;
                    txtTenBaiBao.Enabled = true;
                    txtTenTapChi.Enabled = true;
                    txtTap.Enabled = true;
                    txtSo.Enabled = true;
                    txtTrang.Enabled = true;
                    txtId.Enabled = true;
                    btnDinhKem.Enabled = true;
                    radioButtonViet.Enabled = true;
                    radioButton2.Enabled = true;
                    linkTapChi_Sua.Text = "Xác nhận sửa";
                    linkTapChi_Sua.BackColor = Color.Red;

                    linkTapChi_Them.Enabled = false;
                    linkTapChi_Xoa.Enabled = false;
                    linkTapChi_Huy.Text = "Hủy bỏ sửa";
                    linkTapChi_Huy.Visible = true;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSTC);
                int i = Int16.Parse(lblHangChon_TapChi.Text);
                String ngongu = "Tiếng Việt";
                if (!radioButtonViet.Checked) ngongu = "Tiếng Anh";
                dtMoi.Rows[i]["NgonNgu"] = ngongu;
                dtMoi.Rows[i]["TacGia"] = txtTacGia.Text;
                dtMoi.Rows[i]["Nam"] = txtNam.Text;
                dtMoi.Rows[i]["TieuDeBaiBao"] = txtTenBaiBao.Text;
                dtMoi.Rows[i]["TenTapChi"] = txtTenTapChi.Text;
                dtMoi.Rows[i]["Tap"] = txtTap.Text;
                dtMoi.Rows[i]["So"] = txtSo.Text;
                dtMoi.Rows[i]["Trang"] = txtTrang.Text;
                dtMoi.Rows[i]["DinhKem"] = txtDinhKem.Text;


                try { dataGridViewX_DSTC.DataSource = dtMoi; dataGridViewX_DSTC.Refresh(); }
                catch (Exception ex) { dataGridViewX_DSTC.DataSource = null; }

                txtTacGia.Enabled = false;
                txtNam.Enabled = false;
                txtTenBaiBao.Enabled = false;
                txtTenTapChi.Enabled = false;
                txtTap.Enabled = false;
                txtSo.Enabled = false;
                txtTrang.Enabled = false;
                txtId.Enabled = false;
                btnDinhKem.Enabled = false;
                radioButtonViet.Enabled = false;
                radioButton2.Enabled = false;
                linkTapChi_Sua.Text = "Sửa";
                linkTapChi_Sua.BackColor = Color.Transparent;
                linkTapChi_Them.Enabled = true;
                linkTapChi_Xoa.Enabled = true;
                linkTapChi_Huy.Text = "Hủy";
                linkTapChi_Huy.Visible = false;
            }
        }
        private void btnDinhKem_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog1.ShowDialog();
            if (rs == DialogResult.OK) txtDinhKem.Text = openFileDialog1.FileName;
        }

        private void linkTapChi_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTacGia.Enabled = false;
            txtNam.Enabled = false;
            txtTenBaiBao.Enabled = false;
            txtTenTapChi.Enabled = false;
            txtTap.Enabled = false;
            txtSo.Enabled = false;
            txtTrang.Enabled = false;
            txtId.Enabled = false;

            lblHangChon_TapChi.Text = "-1";

            txtTacGia.Text = "";
            txtNam.Text = "";
            txtTenBaiBao.Text = "";
            txtTenTapChi.Text = "";
            txtTap.Text = "";
            txtSo.Text = "";
            txtTrang.Text = "";
            txtId.Text = "";

            linkTapChi_Them.Text = "Thêm mới";
            linkTapChi_Them.Enabled = true;
            linkTapChi_Them.BackColor = Color.Transparent;

            linkTapChi_Sua.Text = "Sửa";
            linkTapChi_Sua.Enabled = true;
            linkTapChi_Sua.BackColor = Color.Transparent;

            linkTapChi_Xoa.Enabled = true;
            linkTapChi_Huy.Visible = false;

        }
        #endregion

        
        #region Thao tác trên báo cáo hội thảo có ấn bản
        private void dataGridViewX_DSBaoCao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtBaoCao_TacGia.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TacGiabc"].Value.ToString();
                txtBaoCao_Nam.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["Nambc"].Value.ToString();
                txtBaoCao_TenBaoCao.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TenBaoCao"].Value.ToString();
                txtBC_TenHoiThao.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["TenHoiThao"].Value.ToString();
                txtBCThoiGianDiaDiem.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["ThoiGianDiaDiem"].Value.ToString();
                txtBCNhaXuatBan.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NhaXuatBan"].Value.ToString();
                txtBCNoiXuatBan.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NoiXuatBan"].Value.ToString();
                txtBC_Trang.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["Trangbc"].Value.ToString();

                txtBCId.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["STTbc"].Value.ToString();
                lblHangChon_BaoCao.Text = e.RowIndex.ToString();
                if (dataGridViewX_DSBaoCao.SelectedRows[0].Cells["NgonNgubc"].Value.ToString() == "Tiếng Việt")
                {
                    radBaoCaoViet.Checked = true;
                    radBaoCaoAnh.Checked = false;
                }
                else
                {
                    radBaoCaoViet.Checked = false;
                    radBaoCaoAnh.Checked = true;
                }

                txtBaoCao_DinhKem.Text = dataGridViewX_DSBaoCao.SelectedRows[0].Cells["DinhKembc"].Value.ToString();
            }
            catch
            {
                txtBaoCao_TacGia.Text = "";
                txtBaoCao_Nam.Text = "";
                txtBaoCao_TenBaoCao.Text = "";
                txtBC_TenHoiThao.Text = "";
                txtBCThoiGianDiaDiem.Text = "";
                txtBCNoiXuatBan.Text = "";
                txtBCNhaXuatBan.Text = "";
                txtBC_Trang.Text = "";
                txtBaoCao_DinhKem.Text = "";
            }
        }
       


        private void linkBC_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkBC_Them.Text == "Thêm mới")
            {
                txtBaoCao_TacGia.Text = "";
                txtBaoCao_Nam.Text = "";
                txtBaoCao_TenBaoCao.Text = "";
                txtBC_TenHoiThao.Text = "";
                txtBCThoiGianDiaDiem.Text = "";
                txtBCNoiXuatBan.Text = "";
                txtBCNhaXuatBan.Text = "";
                txtBC_Trang.Text = "";
                txtBaoCao_DinhKem.Text = "";

                txtBaoCao_TacGia.Enabled = true;
                txtBaoCao_Nam.Enabled = true;
                txtBaoCao_TenBaoCao.Enabled = true;
                txtBC_TenHoiThao.Enabled = true;
                txtBCThoiGianDiaDiem.Enabled = true;
                txtBCNoiXuatBan.Enabled = true;
                txtBCNhaXuatBan.Enabled = true;
                txtBC_Trang.Enabled = true;
                btnBaoCao_DinhKem.Enabled = true;
                radBaoCaoAnh.Enabled = true;
                radBaoCaoViet.Enabled = true;

                linkBC_Them.BackColor = Color.Red;
                linkBC_Them.Text = "Xác nhận thêm";
                linkBC_Sua.Enabled = false;
                linkBC_Xoa.Enabled = false;
                linkBC_Huy.Text = "Hủy bỏ thêm";
                linkBC_Huy.Visible = true;
            }
            else
            {
                // Thêm vào lưới
                if ((txtBaoCao_TacGia.Text.Length <= 3) || (txtBaoCao_TenBaoCao.Text.Length <= 5))
                    MessageBox.Show("Vui lòng nhập Tác giả/ Tên báo cáo", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String ngongu = "Tiếng Việt";
                    if (!radBaoCaoViet.Checked) ngongu = "Tiếng Anh";

                    String fileDinhDem = txtBaoCao_DinhKem.Text;//.Substring(txtBaoCao_DinhKem.Text.LastIndexOf("\\")+1);
                    DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao);
                    dt.LoadDataRow(new[]
                                      {
                                      (dt.Rows.Count+1).ToString(),
                                        txtBaoCao_TacGia.Text,
                                        txtBaoCao_Nam.Text,
                                        txtBaoCao_TenBaoCao.Text,
                                        txtBC_TenHoiThao.Text, 
                                        txtBCThoiGianDiaDiem.Text ,
                                        txtBCNhaXuatBan.Text,
                                        txtBCNoiXuatBan.Text,
                                        txtBC_Trang.Text,
                                        ngongu,
                                        fileDinhDem
                                      }, LoadOption.Upsert);
                    try { dataGridViewX_DSBaoCao.DataSource = dt; dataGridViewX_DSBaoCao.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSBaoCao.DataSource = null; }

                }

                //--------------------
                txtBaoCao_TacGia.Text = "";
                txtBaoCao_Nam.Text = "";
                txtBaoCao_TenBaoCao.Text = "";
                txtBC_TenHoiThao.Text = "";
                txtBCThoiGianDiaDiem.Text = "";
                txtBCNoiXuatBan.Text = "";
                txtBCNhaXuatBan.Text = "";
                txtBC_Trang.Text = "";
                txtBaoCao_DinhKem.Text = "";

                txtBaoCao_TacGia.Enabled = false;
                txtBaoCao_Nam.Enabled = false;
                txtBaoCao_TenBaoCao.Enabled = false;
                txtBC_TenHoiThao.Enabled = false;
                txtBCThoiGianDiaDiem.Enabled = false;
                txtBCNoiXuatBan.Enabled = false;
                txtBCNhaXuatBan.Enabled = false;
                txtBC_Trang.Enabled = false;
                btnBaoCao_DinhKem.Enabled = false;
                radBaoCaoAnh.Enabled = false;
                radBaoCaoViet.Enabled = false;
                linkBC_Them.BackColor = Color.Transparent;
                linkBC_Them.Text = "Thêm mới";
                linkBC_Sua.Enabled = true;
                linkBC_Xoa.Enabled = true;
                linkBC_Huy.Text = "Hủy";
                linkBC_Huy.Visible = false;
            }
        }

        private void linkBC_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_BaoCao.Text == "-1") MessageBox.Show("Vui lòng chọn báo cáo cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dt= CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao);
                int i = Int16.Parse(lblHangChon_BaoCao.Text);
                dt.Rows.RemoveAt(i);
                try { dataGridViewX_DSBaoCao.DataSource = dt; dataGridViewX_DSBaoCao.Refresh(); }
                catch (Exception ex) { dataGridViewX_DSBaoCao.DataSource = null; }
                MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblHangChon_BaoCao.Text = "-1";

                txtBaoCao_TacGia.Text = "";
                txtBaoCao_Nam.Text = "";
                txtBaoCao_TenBaoCao.Text = "";
                txtBC_TenHoiThao.Text = "";
                txtBCThoiGianDiaDiem.Text = "";
                txtBCNoiXuatBan.Text = "";
                txtBCNhaXuatBan.Text = "";
                txtBC_Trang.Text = "";

            }
        }

        private void linkBC_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkBC_Sua.Text == "Sửa")
            {
                if (lblHangChon_BaoCao.Text == "-1") MessageBox.Show("Vui lòng chọn báo cáo cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtBaoCao_TacGia.Enabled = true;
                    txtBaoCao_Nam.Enabled = true;
                    txtBaoCao_TenBaoCao.Enabled = true;
                    txtBC_TenHoiThao.Enabled = true;
                    txtBCThoiGianDiaDiem.Enabled = true;
                    txtBCNoiXuatBan.Enabled = true;
                    txtBCNhaXuatBan.Enabled = true;
                    txtBC_Trang.Enabled = true;
                    btnBaoCao_DinhKem.Enabled = true;
                    radBaoCaoAnh.Enabled = true;
                    radBaoCaoViet.Enabled = true;

                    linkBC_Sua.BackColor = Color.Red;
                    linkBC_Sua.Text = "Xác nhận sửa";
                    linkBC_Them.Enabled = false;
                    linkBC_Xoa.Enabled = false;
                    linkBC_Huy.Text = "Hủy bỏ sửa";
                    linkBC_Huy.Visible = true;

                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao);
                int i = Int16.Parse(lblHangChon_BaoCao.Text);
                String ngongu = "Tiếng Việt";
                if (!radBaoCaoViet.Checked) ngongu = "Tiếng Anh";
                dtMoi.Rows[i]["TacGiabc"] = txtBaoCao_TacGia.Text;
                dtMoi.Rows[i]["Nambc"] = txtBaoCao_Nam.Text;
                dtMoi.Rows[i]["TenBaoCao"] = txtBaoCao_TenBaoCao.Text;
                dtMoi.Rows[i]["TenHoiThao"] = txtBC_TenHoiThao.Text;
                dtMoi.Rows[i]["ThoiGianDiaDiem"] = txtBCThoiGianDiaDiem.Text;
                dtMoi.Rows[i]["NhaXuatBan"] = txtBCNhaXuatBan.Text;
                dtMoi.Rows[i]["NoiXuatBan"] = txtBCNoiXuatBan.Text;
                dtMoi.Rows[i]["Trangbc"] = txtBC_Trang.Text;
                dtMoi.Rows[i]["NgonNgubc"] = ngongu;
                dtMoi.Rows[i]["DinhKembc"] = txtBaoCao_DinhKem.Text;

                try { dataGridViewX_DSBaoCao.DataSource = dtMoi; dataGridViewX_DSBaoCao.Refresh(); }
                catch   { dataGridViewX_DSBaoCao.DataSource = null; }

                txtBaoCao_TacGia.Enabled = false;
                txtBaoCao_Nam.Enabled = false;
                txtBaoCao_TenBaoCao.Enabled = false;
                txtBC_TenHoiThao.Enabled = false;
                txtBCThoiGianDiaDiem.Enabled = false;
                txtBCNoiXuatBan.Enabled = false;
                txtBCNhaXuatBan.Enabled = false;
                txtBC_Trang.Enabled = false;
                btnBaoCao_DinhKem.Enabled = false;
                radBaoCaoAnh.Enabled = false;
                radBaoCaoViet.Enabled = false;
                

                linkBC_Sua.BackColor = Color.Transparent;
                linkBC_Sua.Text = "Sửa";
                linkBC_Them.Enabled = true;
                linkBC_Xoa.Enabled = true;
                linkBC_Huy.Text = "Hủy";
                linkBC_Huy.Visible = false;

            }
        }

        private void linkBC_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblHangChon_BaoCao.Text = "-1";

            txtBaoCao_TacGia.Text = "";
            txtBaoCao_Nam.Text = "";
            txtBaoCao_TenBaoCao.Text = "";
            txtBC_TenHoiThao.Text = "";
            txtBCThoiGianDiaDiem.Text = "";
            txtBCNoiXuatBan.Text = "";
            txtBCNhaXuatBan.Text = "";
            txtBC_Trang.Text = "";

            txtBaoCao_TacGia.Enabled = false;
            txtBaoCao_Nam.Enabled = false;
            txtBaoCao_TenBaoCao.Enabled = false;
            txtBC_TenHoiThao.Enabled = false;
            txtBCThoiGianDiaDiem.Enabled = false;
            txtBCNoiXuatBan.Enabled = false;
            txtBCNhaXuatBan.Enabled = false;
            txtBC_Trang.Enabled = false;
            btnBaoCao_DinhKem.Enabled = false;
            radBaoCaoAnh.Enabled = false;
            radBaoCaoViet.Enabled = false;


           
            if (linkBC_Them.Text == "Xác nhận thêm")
            {
                linkBC_Them.Text = "Thêm mới";
                linkBC_Them.BackColor = Color.Transparent;
            }

            if (linkBC_Sua.Text == "Xác nhận sửa")
            {
                linkBC_Sua.Text = "Sửa";
                linkBC_Sua.BackColor = Color.Transparent;
            }

            linkBC_Sua.Enabled = true;
            linkBC_Them.Enabled = true;
            linkBC_Xoa.Enabled = true;
            linkBC_Huy.Text = "Hủy";
            linkBC_Huy.Visible = false;

        }

        private void btnBaoCao_DinhKem_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog2.ShowDialog();
            if (rs == DialogResult.OK) txtBaoCao_DinhKem.Text = openFileDialog2.FileName;
        }
        #endregion
          #region Thao tác báo cáo không ấn phẩm
        private void dataGridViewX_DSBaoCao0_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt0BC_TacGia.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TacGiaObc"].Value.ToString();
                txt0BC_Nam.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["NamObc"].Value.ToString();
                txt0BC_TenBaoCao.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TenBaoCaoObc"].Value.ToString();
                txt0BC_TenHoiThao.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["TenHoiThaoObc"].Value.ToString();
                txt0BC_ThoiGian.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["ThoiGianDiaDiemObc"].Value.ToString();
                txt0BC_URL.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["URLObc"].Value.ToString();
                txt0BC_id.Text = dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["STTObc"].Value.ToString();
                lblHangChon_BaoCao0.Text = e.RowIndex.ToString();
                if (dataGridViewX_DSBaoCao0.SelectedRows[0].Cells["NgonNguObc"].Value.ToString() == "Tiếng Việt")
                {
                    radOBcViet.Checked = true;
                    radBaoCaoAnh.Checked = false;
                }
                else
                {
                    radOBcViet.Checked = false;
                    radOBcAnh.Checked = true;
                }

            }
            catch
            {
                txt0BC_TacGia.Text = "";
                txt0BC_Nam.Text = "";
                txt0BC_TenBaoCao.Text = "";
                txt0BC_TenHoiThao.Text = "";
                txt0BC_ThoiGian.Text = "";
                txt0BC_URL.Text = "";
                txt0BC_id.Text = "";
            }

        }
        private void linkObc_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkObc_Them.Text == "Thêm mới")
            {
                txt0BC_TacGia.Text = "";
                txt0BC_Nam.Text = "";
                txt0BC_TenBaoCao.Text = "";
                txt0BC_TenHoiThao.Text = "";
                txt0BC_ThoiGian.Text = "";
                txt0BC_URL.Text = "";
                txt0BC_id.Text = "";

                txt0BC_TacGia.Enabled = true;
                txt0BC_Nam.Enabled = true;
                txt0BC_TenBaoCao.Enabled = true;
                txt0BC_TenHoiThao.Enabled = true;
                txt0BC_ThoiGian.Enabled = true;
                txt0BC_URL.Enabled = true;
                txt0BC_id.Enabled = true;
                radOBcAnh.Enabled = true;
                radOBcViet.Enabled = true;

                linkObc_Them.BackColor = Color.Red;
                linkObc_Them.Text = "Xác nhận thêm";
                linkObc_Sua.Enabled = false;
                linkObc_Xoa.Enabled = false;
                
                linkObc_Huy.Text = "Hủy bỏ thêm";
                linkObc_Huy.Visible = true;
            }
            else
            {
                // Thêm vào lưới
                if ((txt0BC_TacGia.Text.Length <= 3) || (txt0BC_TenBaoCao.Text.Length <= 5))
                    MessageBox.Show("Vui lòng nhập Tác giả/ Tên báo cáo", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String ngongu = "Tiếng Việt";
                    if (!radOBcViet.Checked) ngongu = "Tiếng Anh";

                    DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao0);
                    dt.LoadDataRow(new[]
                                      {
                                        (dt.Rows.Count+1).ToString(),
                                        txt0BC_TacGia.Text,
                                        txt0BC_Nam.Text,
                                        txt0BC_TenBaoCao.Text,
                                        txt0BC_TenHoiThao.Text, 
                                        txt0BC_ThoiGian.Text ,
                                        ngongu,
                                        txt0BC_URL.Text
                                      }, LoadOption.Upsert);
                    try { dataGridViewX_DSBaoCao0.DataSource = dt; dataGridViewX_DSBaoCao0.Refresh(); }
                    catch  { dataGridViewX_DSBaoCao0.DataSource = null; }

                }

                //--------------------
                txt0BC_TacGia.Text = "";
                txt0BC_Nam.Text = "";
                txt0BC_TenBaoCao.Text = "";
                txt0BC_TenHoiThao.Text = "";
                txt0BC_ThoiGian.Text = "";
                txt0BC_URL.Text = "";
                txt0BC_id.Text = "";

                txt0BC_TacGia.Enabled = false;
                txt0BC_Nam.Enabled = false;
                txt0BC_TenBaoCao.Enabled = false;
                txt0BC_TenHoiThao.Enabled = false;
                txt0BC_ThoiGian.Enabled = false;
                txt0BC_URL.Enabled = false;
                txt0BC_id.Enabled = false;
                radOBcAnh.Enabled = false;
                radOBcViet.Enabled = false;

                linkObc_Them.BackColor = Color.Transparent;
                linkObc_Them.Text = "Thêm mới";
                linkObc_Sua.Enabled = true;
                linkObc_Xoa.Enabled = true;
                linkObc_Huy.Text = "Hủy";
                linkObc_Huy.Visible = false;
            }
        }

        private void linkObc_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_BaoCao0.Text == "-1") MessageBox.Show("Vui lòng chọn báo cáo cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                
                DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao0);
                int i = Int16.Parse(lblHangChon_BaoCao0.Text);
                DialogResult rs = MessageBox.Show("Bạn sẽ xóa báo cáo: "+dt.Rows[i]["TenBaoCaoObc"].ToString(), "NTUWebgen Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    dt.Rows.RemoveAt(i);
                    try { dataGridViewX_DSBaoCao0.DataSource = dt; dataGridViewX_DSBaoCao0.Refresh(); }
                    catch { dataGridViewX_DSBaoCao0.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_BaoCao0.Text = "-1";

                    txt0BC_TacGia.Text = "";
                    txt0BC_Nam.Text = "";
                    txt0BC_TenBaoCao.Text = "";
                    txt0BC_TenHoiThao.Text = "";
                    txt0BC_ThoiGian.Text = "";
                    txt0BC_URL.Text = "";
                    txt0BC_id.Text = "";
                }


            }
        }

        private void linkObc_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkObc_Sua.Text == "Sửa")
            {
                if (lblHangChon_BaoCao0.Text == "-1") MessageBox.Show("Vui lòng chọn báo cáo cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txt0BC_TacGia.Enabled = true;
                    txt0BC_Nam.Enabled = true;
                    txt0BC_TenBaoCao.Enabled = true;
                    txt0BC_TenHoiThao.Enabled = true;
                    txt0BC_ThoiGian.Enabled = true;
                    txt0BC_URL.Enabled = true;
                    txt0BC_id.Enabled = true;
                    radOBcAnh.Enabled = true;
                    radOBcViet.Enabled = true;
                    linkObc_Sua.BackColor = Color.Red;
                    linkObc_Sua.Text = "Xác nhận sửa";
                    linkObc_Huy.Text = "Hủy bỏ sửa";
                    linkObc_Huy.Visible = true;
                    linkObc_Xoa.Enabled = false;
                    linkObc_Them.Enabled = false;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao0);
                int i = Int16.Parse(lblHangChon_BaoCao0.Text);
                String ngongu = "Tiếng Việt";
                if (!radOBcViet.Checked) ngongu = "Tiếng Anh";
                dtMoi.Rows[i]["TacGiaObc"] = txt0BC_TacGia.Text;
                dtMoi.Rows[i]["NamObc"] = txt0BC_Nam.Text;
                dtMoi.Rows[i]["TenBaoCaoObc"] = txt0BC_TenBaoCao.Text;
                dtMoi.Rows[i]["TenHoiThaoObc"] = txt0BC_TenHoiThao.Text;
                dtMoi.Rows[i]["ThoiGianDiaDiemObc"] = txt0BC_ThoiGian.Text;
                dtMoi.Rows[i]["URLObc"] = txt0BC_URL.Text;
                dtMoi.Rows[i]["STTObc"] = txt0BC_id.Text;
                dtMoi.Rows[i]["NgonNguObc"] = ngongu;
                try { dataGridViewX_DSBaoCao0.DataSource = dtMoi; dataGridViewX_DSBaoCao0.Refresh(); }
                catch { dataGridViewX_DSBaoCao0.DataSource = null; }

                txt0BC_TacGia.Enabled = false;
                txt0BC_Nam.Enabled = false;
                txt0BC_TenBaoCao.Enabled = false;
                txt0BC_TenHoiThao.Enabled = false;
                txt0BC_ThoiGian.Enabled = false;
                txt0BC_URL.Enabled = false;
                txt0BC_id.Enabled = false;
                radOBcAnh.Enabled = false;
                radOBcViet.Enabled = false;
                linkObc_Sua.BackColor = Color.Transparent;
                linkObc_Sua.Text = "Sửa";
                linkObc_Huy.Text = "Hủy";
                linkObc_Huy.Visible = false;
                linkObc_Xoa.Enabled = true;
                linkObc_Them.Enabled = true;
            }
        }
        private void linkObc_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt0BC_TacGia.Text = "";
            txt0BC_Nam.Text = "";
            txt0BC_TenBaoCao.Text = "";
            txt0BC_TenHoiThao.Text = "";
            txt0BC_ThoiGian.Text = "";
            txt0BC_URL.Text = "";
            txt0BC_id.Text = "";
            lblHangChon_BaoCao0.Text = "-1";
            txt0BC_TacGia.Enabled = false;
            txt0BC_Nam.Enabled = false;
            txt0BC_TenBaoCao.Enabled = false;
            txt0BC_TenHoiThao.Enabled = false;
            txt0BC_ThoiGian.Enabled = false;
            txt0BC_URL.Enabled = false;
            txt0BC_id.Enabled = false;
            radOBcAnh.Enabled = false;
            radOBcViet.Enabled = false;

            linkObc_Xoa.Enabled = true;
            if (linkObc_Them.Text == "Xác nhận thêm")
            {
                linkObc_Them.Text = "Thêm mới";
                linkObc_Them.BackColor = Color.Transparent;
            }

            if (linkObc_Sua.Text == "Xác nhận sửa")
            {
                linkObc_Sua.Text = "Sửa";
                linkObc_Sua.BackColor = Color.Transparent;
            }
            linkObc_Them.Enabled = true;
            linkObc_Sua.Enabled = true;
            linkObc_Huy.Visible = false;

        }
         #endregion

        #region Thao tac tren sach
        private void dataGridViewX_DSSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtSach_TacGia.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["TacGiasach"].Value.ToString();
                txtSach_NamXB.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NamXBsach"].Value.ToString();
                txtSach_TenSach.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["TenSach"].Value.ToString();
                txtSach_NhaXB.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NhaXBsach"].Value.ToString();
                txtSach_NoiXB.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["NoiXBsach"].Value.ToString();
                txtSach_Id.Text = dataGridViewX_DSSach.SelectedRows[0].Cells["STTsach"].Value.ToString();
                lblHangChon_Sach.Text = e.RowIndex.ToString();
               }
            catch
            {
                txtSach_TacGia.Text = "";
                txtSach_NamXB.Text = "";
                txtSach_TenSach.Text ="";
                txtSach_NhaXB.Text = "";
                txtSach_NoiXB.Text = "";
                txtSach_Id.Text = "";
                lblHangChon_Sach.Text ="-1";
            }
        }


        private void linkSach_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSach_Them.Text == "Thêm mới")
            {
                txtSach_TacGia.Text = "";
                txtSach_NamXB.Text = "";
                txtSach_TenSach.Text = "";
                txtSach_NhaXB.Text = "";
                txtSach_NoiXB.Text = "";
                txtSach_Id.Text = "";
                lblHangChon_Sach.Text = "-1";

                txtSach_TacGia.Enabled = true;
                txtSach_NamXB.Enabled = true;
                txtSach_TenSach.Enabled = true;
                txtSach_NhaXB.Enabled = true;
                txtSach_NoiXB.Enabled = true;
                txtSach_Id.Enabled = true; 
              

                linkSach_Them.BackColor = Color.Red;
                linkSach_Them.Text = "Xác nhận thêm";
                linkSach_Sua.Enabled = false;
                linkSach_Xoa.Enabled = false;

                linkSach_Huy.Text = "Hủy bỏ thêm";
                linkSach_Huy.Visible = true;
            }
            else
            {
                // Thêm vào lưới
                if ((txtSach_TacGia.Text.Length <= 3) || (txtSach_TenSach.Text.Length <= 5))
                    MessageBox.Show("Vui lòng nhập Tác giả/ Tên sách hoặc bookchapter", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                  
                    DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSSach);
                    dt.LoadDataRow(new[]
                                      {
                                        (dt.Rows.Count+1).ToString(),
                                       txtSach_TacGia.Text,
                                        txtSach_NamXB.Text,
                                        txtSach_TenSach.Text,
                                        txtSach_NhaXB.Text, 
                                        txtSach_NoiXB.Text
                                      }, LoadOption.Upsert);
                    try { dataGridViewX_DSSach.DataSource = dt; dataGridViewX_DSSach.Refresh(); }
                    catch { dataGridViewX_DSSach.DataSource = null; }

                }

                //--------------------
                txtSach_TacGia.Text = "";
                txtSach_NamXB.Text = "";
                txtSach_TenSach.Text = "";
                txtSach_NhaXB.Text = "";
                txtSach_NoiXB.Text = "";
                txtSach_Id.Text = "";
                lblHangChon_Sach.Text = "-1";

                txtSach_TacGia.Enabled = false;
                txtSach_NamXB.Enabled = false;
                txtSach_TenSach.Enabled = false;
                txtSach_NhaXB.Enabled = false;
                txtSach_NoiXB.Enabled = false;
                txtSach_Id.Enabled = false;

                linkSach_Them.BackColor = Color.Transparent;
                linkSach_Them.Text = "Thêm mới";
                linkSach_Sua.Enabled = true;
                linkSach_Xoa.Enabled = true;
                linkSach_Huy.Text = "Hủy";
                linkSach_Huy.Visible = false;
            }
        }

        private void linkSach_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_Sach.Text == "-1") MessageBox.Show("Vui lòng chọn sách cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                DataTable dt = CongCu.GetContentAsDataTable(dataGridViewX_DSSach);
                int i = Int16.Parse(lblHangChon_Sach.Text);
                DialogResult rs = MessageBox.Show("Bạn sẽ xóa sách: " + dt.Rows[i]["TenSach"].ToString(), "NTUWebgen Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    dt.Rows.RemoveAt(i);
                    try { dataGridViewX_DSSach.DataSource = dt; dataGridViewX_DSSach.Refresh(); }
                    catch { dataGridViewX_DSSach.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_Sach.Text = "-1";
                    txtSach_TacGia.Text = "";
                    txtSach_NamXB.Text = "";
                    txtSach_TenSach.Text = "";
                    txtSach_NhaXB.Text = "";
                    txtSach_NoiXB.Text = "";
                    txtSach_Id.Text = "";
                  
                   
                }


            }
        }

        private void linkSach_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSach_Sua.Text == "Sửa")
            {
                if (lblHangChon_Sach.Text == "-1") MessageBox.Show("Vui lòng chọn sách cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtSach_TacGia.Enabled = true;
                    txtSach_NamXB.Enabled = true;
                    txtSach_TenSach.Enabled = true;
                    txtSach_NhaXB.Enabled = true;
                    txtSach_NoiXB.Enabled = true;
                    txtSach_Id.Enabled = true;

                    linkSach_Sua.BackColor = Color.Red;
                    linkSach_Sua.Text = "Xác nhận sửa";
                    linkSach_Huy.Text = "Hủy bỏ sửa";
                    linkSach_Huy.Visible = true;
                    linkSach_Xoa.Enabled = false;
                    linkSach_Them.Enabled = false;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSSach);
                int i = Int16.Parse(lblHangChon_Sach.Text);
                
                dtMoi.Rows[i]["TacGiasach"] = txtSach_TacGia.Text;
                dtMoi.Rows[i]["NamXBsach"] = txtSach_NamXB.Text;
                dtMoi.Rows[i]["TenSach"] = txtSach_TenSach.Text;
                dtMoi.Rows[i]["NhaXBsach"] = txtSach_NhaXB.Text;
                dtMoi.Rows[i]["NoiXBsach"] = txtSach_NoiXB.Text;

                try { dataGridViewX_DSSach.DataSource = dtMoi; dataGridViewX_DSSach.Refresh(); }
                catch { dataGridViewX_DSSach.DataSource = null; }

                txtSach_TacGia.Enabled = false;
                txtSach_NamXB.Enabled = false;
                txtSach_TenSach.Enabled = false;
                txtSach_NhaXB.Enabled = false;
                txtSach_NoiXB.Enabled = false;
                txtSach_Id.Enabled = false;

                linkSach_Sua.BackColor = Color.Transparent;
                linkSach_Sua.Text = "Sửa";
                linkSach_Huy.Text = "Hủy";
                linkSach_Huy.Visible = false;
                linkSach_Xoa.Enabled = true;
                linkSach_Them.Enabled = true;
            }
        }

        private void linkSach_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSach_TacGia.Text = "";
            txtSach_NamXB.Text = "";
            txtSach_TenSach.Text = "";
            txtSach_NhaXB.Text = "";
            txtSach_NoiXB.Text = "";
            txtSach_Id.Text = "";
            lblHangChon_Sach.Text = "-1";

            txtSach_TacGia.Enabled = false;
            txtSach_NamXB.Enabled = false;
            txtSach_TenSach.Enabled = false;
            txtSach_NhaXB.Enabled = false;
            txtSach_NoiXB.Enabled = false;
            txtSach_Id.Enabled = false;

           

            linkSach_Xoa.Enabled = true;
            
                linkSach_Them.Text = "Thêm mới";
                linkSach_Them.BackColor = Color.Transparent;
            

             
                linkSach_Sua.Text = "Sửa";
                linkSach_Sua.BackColor = Color.Transparent;
             
            linkSach_Them.Enabled = true;
            linkSach_Sua.Enabled = true;
            linkSach_Huy.Visible = false;
        }

        #endregion


        void SaveCongBo()
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;
            #region Tap chi
            XmlNode Nodes_BaiBao = root.SelectSingleNode("DSBaiBao");
            XmlElement xmlENodes_BaiBao = Nodes_BaiBao as XmlElement;
            Nodes_BaiBao.InnerText = "";
            DataTable dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSTC);
            String baibaodinhkem="";
            foreach (DataRow r in dtTarget.Rows)
            {
                XmlElement newNode = doc.CreateElement("BaiBao");

                //1. Upload or No
                String fileBaiBao = r["DinhKem"].ToString();
                baibaodinhkem = fileBaiBao;
                if (fileBaiBao.Contains("\\"))
                {
                    String filenameCopy = fileBaiBao.Substring(fileBaiBao.LastIndexOf("\\") + 1);
                    File.Copy(fileBaiBao, ProjectFolder + "\\data\\DinhKem\\" + filenameCopy, true);
                    baibaodinhkem = filenameCopy;
                   
                }

                newNode.InnerXml =
                        "<id>" + r["STT"] + "</id>" +
                        "<TacGia>" + r["TacGia"] + "</TacGia>" +
                        "<Ngay>" + r["Nam"] + "</Ngay>" +
                        "<TieuDeBaiBao>" + r["TieuDeBaiBao"] + "</TieuDeBaiBao>" +
                        "<TenTapChi>" + r["TenTapChi"] + "</TenTapChi>" +
                        "<Tap>" + r["Tap"] + "</Tap>" +
                        "<So>" + r["So"] + "</So>" +
                        "<Trang>" + r["Trang"] + "</Trang>" +
                        "<NgonNgu>" + r["NgonNgu"] + "</NgonNgu>" +
                        "<DinhKem>" + baibaodinhkem+ "</DinhKem>";
                xmlENodes_BaiBao.AppendChild(newNode);

            }
           
            #endregion



            #region Bao cao co an pham
            XmlNode Nodes_BaoCao1 = root.SelectSingleNode("DSBaoCao1");
            XmlElement xmlENodes_BaoCao1 = Nodes_BaoCao1 as XmlElement;
            Nodes_BaoCao1.InnerText = "";
            dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao);
            String baocaodinhkem = "";
            foreach (DataRow r in dtTarget.Rows)
            {
                //1. Upload or No
               
                String fileBaoCao = r["DinhKembc"].ToString();
                baocaodinhkem = fileBaoCao;
                if (fileBaoCao.Contains("\\"))
                {
                    String filenameCopy = fileBaoCao.Substring(fileBaoCao.LastIndexOf("\\") + 1);
                    File.Copy(fileBaoCao, ProjectFolder + "\\data\\DinhKem\\" + filenameCopy, true);
                    baocaodinhkem = filenameCopy;
                    
                }
                
                XmlElement newNode = doc.CreateElement("BaoCao1");
                newNode.InnerXml =
                        "<id>" + r["STTbc"] + "</id>" +
                        "<TacGia>" + r["TacGiabc"] + "</TacGia>" +
                        "<Nam>" + r["Nambc"] + "</Nam>" +
                        "<TenBaoCao>" + r["TenBaoCao"] + "</TenBaoCao>" +
                        "<TenHoiThao>" + r["TenHoiThao"] + "</TenHoiThao>" +
                        "<ThoiGianDiaDiem>" + r["ThoiGianDiaDiem"] + "</ThoiGianDiaDiem>" +
                        "<NhaXuatBan>" + r["NhaXuatBan"] + "</NhaXuatBan>" +
                        "<NoiXuatBan>" + r["NoiXuatBan"] + "</NoiXuatBan>" +
                        "<Trang>" + r["Trangbc"] + "</Trang>" +
                        "<NgonNgu>" + r["NgonNgubc"] + "</NgonNgu>" +
                        "<DinhKem>" + baocaodinhkem + "</DinhKem>";
                xmlENodes_BaoCao1.AppendChild(newNode);
            }
            #endregion

            #region Bao cao KHONG co an pham
            XmlNode Nodes_BaoCao0 = root.SelectSingleNode("DSBaoCao0");
            XmlElement xmlENodes_BaoCao0 = Nodes_BaoCao0 as XmlElement;
            Nodes_BaoCao0.InnerText = "";
            dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSBaoCao0);
            foreach (DataRow r in dtTarget.Rows)
            {
                XmlElement newNode = doc.CreateElement("BaoCao0");
                newNode.InnerXml =
                        "<id>" + r["STTObc"] + "</id>" +
                        "<TacGia>" + r["TacGiaObc"] + "</TacGia>" +
                        "<Nam>" + r["NamObc"] + "</Nam>" +
                        "<TenBaoCao>" + r["TenBaoCaoObc"] + "</TenBaoCao>" +
                        "<TenHoiThao>" + r["TenHoiThaoObc"] + "</TenHoiThao>" +
                        "<ThoiGianDiaDiem>" + r["ThoiGianDiaDiemObc"] + "</ThoiGianDiaDiem>" +
                        "<NgonNgu>" + r["NgonNguObc"] + "</NgonNgu>" +
                        "<URL>" + r["URLObc"] + "</URL>";

                xmlENodes_BaoCao0.AppendChild(newNode);
            }
            #endregion

            #region Sach
            XmlNode Nodes_Sach = root.SelectSingleNode("DSSach");
            XmlElement xmlENodes_Sach = Nodes_Sach as XmlElement;
            Nodes_Sach.InnerText = "";
            dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSSach);
            foreach (DataRow r in dtTarget.Rows)
            {
                XmlElement newNode = doc.CreateElement("Sach");
                newNode.InnerXml =
                        "<id>" + r["STTsach"] + "</id>" +
                        "<TacGia>" + r["TacGiasach"] + "</TacGia>" +
                        "<NamXB>" + r["NamXBsach"] + "</NamXB>" +
                        "<TenSach>" + r["TenSach"] + "</TenSach>" +
                        "<NhaXB>" + r["NhaXBsach"] + "</NhaXB>" +
                        "<NoiXB>" + r["NoiXBsach"] + "</NoiXB>";// +
                //"<DinhKem>" + r["STTsach"] + "</DinhKem>";

                xmlENodes_Sach.AppendChild(newNode);
            }
            #endregion
            doc.Save(fullXMLFile); LoadTapChi();
            LoadBaoCao(); LoadBaoCao0(); LoadSach();
            MessageBox.Show("Lưu thành công", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCongBo();
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = CongCu.XML2HTML_TapChi1(fullXMLFile, ProjectFolder).ToString();
            CongCu.ReplaceContent(fullHTMLFile, "TapChi", noiDungMoi);

              noiDungMoi = CongCu.XML2HTML_BaoCaoHoiThao1(fullXMLFile, ProjectFolder).ToString();
              CongCu.ReplaceContent(fullHTMLFile, "BaoCao1", noiDungMoi);

              noiDungMoi = CongCu.XML2HTML_BaoCaoHoiThao0(fullXMLFile).ToString();
              CongCu.ReplaceContent(fullHTMLFile, "BaoCao0", noiDungMoi);

              noiDungMoi = CongCu.XML2HTML_Sach(fullXMLFile).ToString();
              CongCu.ReplaceContent(fullHTMLFile, "Sach", noiDungMoi);
            // Dành cho mẫu 4
            //UserInfo u = CongCu.getUserInfo(ProjectFolder + "\\data\\index.xml");
            //CongCu.ReplaceContent(pubHTMLFile, "anhTrai", "<img src=\"" + u.HinhAnh + "\" width=100% height=186px>");
            //CongCu.ReplaceContent(pubHTMLFile, "tenTrai", "website của " + u.HoTen);
            //// Thêm tiêu đề
            //CongCu.ReplaceTite(pubHTMLFile, "NTU. " + u.HoTen + "-Báo cáo hội thảo");
              MessageBox.Show("Xuất xong. Trang của bạn là" + (char)10 + (char)13 + fullHTMLFile, "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkWebCourse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CongCu.gotoSite(fullHTMLFile);
        }





    }
}
