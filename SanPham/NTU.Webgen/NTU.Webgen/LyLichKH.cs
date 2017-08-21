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
    public partial class LyLichKH : UserControl
    {
        static String xmlLyLichFile= @"\\data\\cv.xml";
        static String htmlLyLichFile=@"\cv.html";
        String ProjectFolder;
        String fullXMLFile;
        String fullHTMLFile;
        public LyLichKH()
        {
            InitializeComponent();
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            fullXMLFile = ProjectFolder + xmlLyLichFile;
            fullHTMLFile = ProjectFolder + htmlLyLichFile;
            LoadLyLich(fullXMLFile);

        }
        public LyLichKH(String ProjectFolder)
        {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            fullXMLFile = ProjectFolder + xmlLyLichFile;
            fullHTMLFile = ProjectFolder + htmlLyLichFile;
            LoadLyLich(fullXMLFile);
        }
        private void LoadLyLich(String xmlLyLichFile) {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(xmlLyLichFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;
           // XmlNode Nodes_T = root.SelectSingleNode("/root/DSHocPhan/HocPhan[id='" + idHocPhan + "']");
            //XmlElement bg = root. 
            #region Tự thuật
            XmlNode Nodes_TuThuat = root.SelectSingleNode("/root/TuThuat");
            String hoten = Nodes_TuThuat.ChildNodes[0].InnerText;
            String gioiTinh = Nodes_TuThuat.ChildNodes[1].InnerText;
            String ngaysinh = Nodes_TuThuat.ChildNodes[2].InnerText;
            String noisinh = Nodes_TuThuat.ChildNodes[3].InnerText;
            String quequan = Nodes_TuThuat.ChildNodes[4].InnerText;
            String dantoc = Nodes_TuThuat.ChildNodes[5].InnerText;

            txtHoTen.Text = hoten;
            if (gioiTinh == "Nam") radNam.Checked = true; else radNu.Checked = true;
            txtNgaySinh.Text = ngaysinh;
            txtNoiSinh.Text = noisinh;
            txtQueQuan.Text = quequan;
            
            XmlNode hv = Nodes_TuThuat.SelectSingleNode("HocViCaoNhat");
            String tenhocvi = hv.ChildNodes[0].InnerText;
            String namnhanhocvi = hv.ChildNodes[1].InnerText;
            String nuocnhanhocvi = hv.ChildNodes[2].InnerText;
            txtHocVi.Text = tenhocvi;
            txtNamNhanHV.Text = namnhanhocvi;
            txtNuocNhanHV.Text = nuocnhanhocvi;
            XmlNode chucdanh = Nodes_TuThuat.SelectSingleNode("ChucDanhKhoaHoc");
            String tenchucdanh = chucdanh.ChildNodes[0].InnerText;
            String nambonhiem = chucdanh.ChildNodes[1].InnerText;

            txtChucDanhKhoaHoc.Text = tenchucdanh;
            txtNamBoNhiem.Text = nambonhiem;
            
            XmlNode dvct = Nodes_TuThuat.SelectSingleNode("DonViCongTac");
            String tendonvi = dvct.ChildNodes[0].InnerText;
            String chucvu = dvct.ChildNodes[1].InnerText;
            txtDonViCongTac.Text = tendonvi;
            txtChucVu.Text = chucvu;
            XmlNode ll = Nodes_TuThuat.SelectSingleNode("LienLac");
            String choohiennay = ll.ChildNodes[0].InnerText;
            String diachill = ll.ChildNodes[1].InnerText;
            String dtCQ = ll.ChildNodes[2].InnerText;
            String dtNR = ll.ChildNodes[3].InnerText;
            String dtDD = ll.ChildNodes[4].InnerText;
            String fax = ll.ChildNodes[5].InnerText;
            String mail = ll.ChildNodes[6].InnerText;
            txtChoO.Text = choohiennay;
            txtDiaChiLL.Text = diachill;
            txtDienThoaiCQ.Text = dtCQ;
            txtDienThoaiNR.Text = dtNR;
            txtDienThoaiDD.Text = dtDD;
            txtFAX.Text = fax;
            txtEmail.Text = mail;
            
            #endregion
             #region Qua trinh dao tao
             XmlNode Nodes_DaoTao = root.SelectSingleNode("/root/QuaTrinhDaoTao");
             XmlNode daihoc1 = Nodes_DaoTao.SelectSingleNode("DaiHoc1");
             txtDH_HeDaoTao.Text = daihoc1.ChildNodes[0].InnerText;
             txtDH_NoiDT.Text = daihoc1.ChildNodes[1].InnerText;
             txtDH_NuocDT.Text = daihoc1.ChildNodes[2].InnerText;
             txtDH_ChuyenNganh.Text = daihoc1.ChildNodes[3].InnerText;
             txtDH_NamTotNghiep.Text = daihoc1.ChildNodes[4].InnerText;

             XmlNode daihoc2 = Nodes_DaoTao.SelectSingleNode("DaiHoc2");
             txtDH2_ChuyenNganh.Text = daihoc2.ChildNodes[0].InnerText;
             txtDH2_NamTotNghiep.Text = daihoc2.ChildNodes[1].InnerText;

             XmlNode thacsi = Nodes_DaoTao.SelectSingleNode("ThacSi");
             txtThS_NoiDT.Text = thacsi.ChildNodes[0].InnerText;
             txtThS_NuocDT.Text = thacsi.ChildNodes[1].InnerText;
             txtThS_ChuyenNganh.Text = thacsi.ChildNodes[2].InnerText;
             txtThS_NamCapBang.Text = thacsi.ChildNodes[3].InnerText;

             XmlNode tiensi = Nodes_DaoTao.SelectSingleNode("TienSi");
             txtTS_NoiDaoTao.Text = tiensi.ChildNodes[0].InnerText;
             txtTS_NuocDT.Text = tiensi.ChildNodes[1].InnerText;
             txtTS_ChuyenNganh.Text = tiensi.ChildNodes[2].InnerText;
             txtTS_ThoiGianDT.Text = tiensi.ChildNodes[3].InnerText;
             txtTS_NamCapBang.Text = tiensi.ChildNodes[4].InnerText;
             txtTS_LuanAn.Text = tiensi.ChildNodes[5].InnerText;

             XmlNode ngoai1 = Nodes_DaoTao.SelectSingleNode("NgoaiNgu1");
             txtNN1.Text = ngoai1.ChildNodes[0].InnerText;
             txtNN1_TrinhDo.Text = ngoai1.ChildNodes[1].InnerText;

             XmlNode ngoai2 = Nodes_DaoTao.SelectSingleNode("NgoaiNgu2");
             txtNN2.Text = ngoai2.ChildNodes[0].InnerText;
             txtNN2_TrinhDo.Text = ngoai2.ChildNodes[1].InnerText;

             #endregion
             #region Qua Trinh cong tac
             XmlNodeList Nodes_CongTac = root.SelectNodes("/root/DSQuaTrinhCongTac/QuaTrinhCongTac");
             XmlNode[] qtCongTac = Nodes_CongTac.Cast<XmlNode>().ToArray();

             DataTable dtCongTac = CongCu.GetContentAsDataTable(grdQuaTrinhCongTac);

             foreach (XmlNode k in qtCongTac)
             {
                 String STT = k.ChildNodes[0].InnerText;
                 String tungay = k.ChildNodes[1].InnerText;
                 String denngay = k.ChildNodes[2].InnerText;
                 String donvicongtac = k.ChildNodes[3].InnerText;
                 String congviecdamnhiem = k.ChildNodes[4].InnerText;

                 dtCongTac.LoadDataRow(new[]
                                      {
                                      STT,
                                      tungay,
                                      denngay,
                                      donvicongtac,
                                      congviecdamnhiem
                                      }, LoadOption.Upsert);
             }
             try { grdQuaTrinhCongTac.DataSource = dtCongTac; }
             catch { grdQuaTrinhCongTac.DataSource = null; }
             #endregion

             #region Đề tài nghiên cứu khoa học
             XmlNodeList Nodes_DeTai = root.SelectNodes("/root/NghienCuuKhoaHoc/DSDeTaiKH/DeTaiKH");
             XmlNode[] detai = Nodes_DeTai.Cast<XmlNode>().ToArray();

             DataTable dtDeTai = CongCu.GetContentAsDataTable(grdDeTaiKhoaHoc);
             foreach (XmlNode k in detai)
             {
                 String STT = k.ChildNodes[0].InnerText;
                 String tendetai = k.ChildNodes[1].InnerText;
                 String nambatdau = k.ChildNodes[2].InnerText;
                 String namhoanthanh = k.ChildNodes[3].InnerText;
                 String detaicap = k.ChildNodes[4].InnerText;
                 String trachnhiem = k.ChildNodes[5].InnerText;

                 dtDeTai.LoadDataRow(new[]
                                      {
                                      STT,
                                      tendetai,
                                      nambatdau,
                                      namhoanthanh,
                                      detaicap, trachnhiem
                                      }, LoadOption.Upsert);
             }
             try { grdDeTaiKhoaHoc.DataSource = dtDeTai; }
             catch { grdDeTaiKhoaHoc.DataSource = null; }
             #endregion
            #region Công trình khoa học công bố
             XmlNodeList Nodes_CongBo = root.SelectNodes("/root/NghienCuuKhoaHoc/DSCongTrinhKhoaHoc/CongBo");
             XmlNode[] congbo = Nodes_CongBo.Cast<XmlNode>().ToArray();

             DataTable dtCongBo = CongCu.GetContentAsDataTable(grdCongBoKhoaHoc);
             foreach (XmlNode k in congbo)
             {
                 String STT = k.ChildNodes[0].InnerText;
                 String tencongbo = k.ChildNodes[1].InnerText;
                 String namcongbo = k.ChildNodes[2].InnerText;
                 String noicongbo = k.ChildNodes[3].InnerText;
                 String loaicongbo = k.ChildNodes[4].InnerText;
                 dtCongBo.LoadDataRow(new[]
                                      {
                                      STT,
                                      tencongbo,
                                      noicongbo,
                                      namcongbo,
                                      loaicongbo
                                      }, LoadOption.Upsert);
             }
             try { grdCongBoKhoaHoc.DataSource = dtCongBo; }
             catch { grdCongBoKhoaHoc.DataSource = null; }
            #endregion
        }

        private void SaveLyLich(String xmlLyLichFile)
        {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(xmlLyLichFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;
            #region Luu tu thuat
                XmlNode old_TuThuat = root.SelectSingleNode("TuThuat");
                XmlElement new_TuThuat = doc.CreateElement("TuThuat");
                String gioitinh = "Nữ";
                if (radNam.Checked) gioitinh = "Nam";
                new_TuThuat.InnerXml =
                    "<HoTen>"+txtHoTen.Text +"</HoTen>" +
                    "<GioiTinh>" + gioitinh + "</GioiTinh>" +
                    "<NgaySinh>" + txtNgaySinh.Text+"</NgaySinh>" +
                    "<NoiSinh>" + txtNoiSinh.Text+"</NoiSinh>" +
                    "<QueQuan>" +txtQueQuan.Text +"</QueQuan>" +
                    "<DanToc> Kinh </DanToc>" +
                    "<HocViCaoNhat>" +
                        "<TenHocViCaoNhat>" + txtHocVi.Text+"</TenHocViCaoNhat>" +
                        "<NamNhanHV>" +txtNamNhanHV.Text +"</NamNhanHV>" +
                        "<NuocNhanHV>" + txtNuocNhanHV.Text+"</NuocNhanHV>" +
                    "</HocViCaoNhat>" +
                    "<ChucDanhKhoaHoc>" +
                        "<TenChucDanhCaoNhat>" + txtChucDanhKhoaHoc.Text+"</TenChucDanhCaoNhat>" +
                        "<NamBoNhiem>" + txtNamBoNhiem.Text+"</NamBoNhiem>" +
                    "</ChucDanhKhoaHoc>" +
                    "<DonViCongTac>" +
                       " <TenDonVi>" + txtDonViCongTac.Text +"</TenDonVi>" +
                        "<ChucVu>" +txtChucVu.Text +"</ChucVu>" +
                    "</DonViCongTac>" +
                    "<LienLac>" +
                        "<NoiOHienNay>" +txtChoO.Text +"</NoiOHienNay>" +
                        "<DiaChi>" +txtDiaChiLL.Text +"</DiaChi>" +
                        "<DTCoQuan>" + txtDienThoaiCQ.Text +"</DTCoQuan>" +
                        "<DTNhaRieng>" + txtDienThoaiNR.Text+"</DTNhaRieng>" +
                        "<DTDiDong>" +txtDienThoaiDD.Text +"</DTDiDong>" +
                        "<fax>" + txtFAX.Text+"</fax>" +
                        "<Email>" + txtEmail.Text+"</Email>" +
                    "</LienLac>";
                root.ReplaceChild(new_TuThuat, old_TuThuat);
              
                

            #endregion

            #region Lưu quá trình đào tạo
                XmlNode Nodes_QuaTrinhDaoTao = root.SelectSingleNode("QuaTrinhDaoTao");
                XmlElement xmlENodes_DaoTao = Nodes_QuaTrinhDaoTao as XmlElement;
                
                XmlNode old_Daihoc1 = Nodes_QuaTrinhDaoTao.SelectSingleNode("DaiHoc1");
                XmlElement dh1 = doc.CreateElement("DaiHoc1");
                dh1.InnerXml =
                      "<HeDT>" + txtDH_HeDaoTao.Text+"</HeDT>" +
                      "<NoiDaoTao>" + txtDH_NoiDT.Text + "</NoiDaoTao>" +
                      "<NuocDaoTao>" +txtDH_NuocDT.Text +"</NuocDaoTao>" +
                      "<ChuyenNganh>" +txtDH_ChuyenNganh.Text +"</ChuyenNganh>" +
                      "<NamTotNghiep>" + txtDH_NamTotNghiep.Text +"</NamTotNghiep>";
                xmlENodes_DaoTao.ReplaceChild(dh1, old_Daihoc1);


                XmlNode old_Daihoc2 = Nodes_QuaTrinhDaoTao.SelectSingleNode("DaiHoc2");
                XmlElement dh2 = doc.CreateElement("DaiHoc2");
                dh2.InnerXml =
                       "<ChuyenNganh>" + txtDH2_ChuyenNganh.Text + "</ChuyenNganh>" +
                      "<NamTotNghiep>" + txtDH2_NamTotNghiep.Text + "</NamTotNghiep>";
                xmlENodes_DaoTao.ReplaceChild(dh2, old_Daihoc2);

                XmlNode old_ThacSi = Nodes_QuaTrinhDaoTao.SelectSingleNode("ThacSi");
                XmlElement ths = doc.CreateElement("ThacSi");
                ths.InnerXml =
                      "<NoiDaoTao>"+txtThS_NoiDT.Text+"</NoiDaoTao>" +
                      "<NuocDaoTao>"+txtThS_NuocDT.Text+"</NuocDaoTao>" +
                      "<ChuyenNganh>"+txtThS_ChuyenNganh.Text+"</ChuyenNganh>" +
                      "<NamCapBang>"+txtThS_NamCapBang.Text+"</NamCapBang>";
                xmlENodes_DaoTao.ReplaceChild(ths, old_ThacSi);


                XmlNode old_TSi = Nodes_QuaTrinhDaoTao.SelectSingleNode("TienSi");
                XmlElement ts = doc.CreateElement("TienSi");
                ts.InnerXml =
                      "<NoiDaoTao>"+txtTS_NoiDaoTao.Text +"</NoiDaoTao>" +
                      "<NuocDaoTao>" + txtTS_NuocDT.Text + "</NuocDaoTao>" +
                      "<ChuyenNganh>" + txtTS_ChuyenNganh.Text + "</ChuyenNganh>" +
                      "<ThoiGianDaoTao>" + txtTS_ThoiGianDT.Text + "</ThoiGianDaoTao>" +
                      "<NamCapBang>" + txtTS_NamCapBang.Text + "</NamCapBang>" +
                      "<TenLuanAn>" + txtTS_LuanAn.Text + "</TenLuanAn>";
                xmlENodes_DaoTao.ReplaceChild(ts, old_TSi);

                XmlNode old_NN1 = Nodes_QuaTrinhDaoTao.SelectSingleNode("NgoaiNgu1");
                XmlElement nn1 = doc.CreateElement("NgoaiNgu1");
                nn1.InnerXml =
                        "<id>1</id>" +
                      "<TenNN>"+txtNN1.Text+"</TenNN>" +
                      "<TrinhDo>" + txtNN1_TrinhDo.Text + "</TrinhDo>";
                xmlENodes_DaoTao.ReplaceChild(nn1, old_NN1);

                XmlNode old_NN2 = Nodes_QuaTrinhDaoTao.SelectSingleNode("NgoaiNgu2");
                XmlElement nn2 = doc.CreateElement("NgoaiNgu2");
                nn2.InnerXml =
                        "<id>2</id>" +
                      "<TenNN>" + txtNN2.Text + "</TenNN>" +
                      "<TrinhDo>" + txtNN2_TrinhDo.Text + "</TrinhDo>";
                xmlENodes_DaoTao.ReplaceChild(nn2, old_NN2);

               
            #endregion
            #region Lưu quá trình công tác
                XmlNode Nodes_QuaTrinhCongTac = root.SelectSingleNode("DSQuaTrinhCongTac");
                XmlElement xmlENodes_CongTac = Nodes_QuaTrinhCongTac as XmlElement;
                Nodes_QuaTrinhCongTac.InnerText = "";
                DataTable dtTarget = CongCu.GetContentAsDataTable(grdQuaTrinhCongTac);
                foreach (DataRow r in dtTarget.Rows)
                {
                    XmlElement newNode = doc.CreateElement("QuaTrinhCongTac");
                    newNode.InnerXml =
                          "<id>" + r[0] + "</id>" +
                          "<TuNgay>" + r[1] + "</TuNgay>" +
                          "<DenNgay>" + r[2] + "</DenNgay>" +
                          "<DonViCongTac>" + r[3] + "</DonViCongTac>" +
                          "<CongViecDamNhiem>" + r[4] + "</CongViecDamNhiem>";
                    xmlENodes_CongTac.AppendChild(newNode);
                }
            #endregion

                #region Lưu đề tài nghiên cứu khoa học
                XmlNode Nodes_DeTai = root.SelectSingleNode("NghienCuuKhoaHoc/DSDeTaiKH");
                XmlElement xmlENodes_DeTai = Nodes_DeTai as XmlElement;
                Nodes_DeTai.InnerText = "";
                dtTarget = CongCu.GetContentAsDataTable(grdDeTaiKhoaHoc);
                foreach (DataRow r in dtTarget.Rows)
                {
                    XmlElement newNode = doc.CreateElement("DeTaiKH");
                    newNode.InnerXml =
                          "<id>" + r[0] + "</id>" +
                          "<TenDeTai>" + r[1] + "</TenDeTai>" +
                          "<NamBatDau>" + r[2] + "</NamBatDau>" +
                          "<NamHoanThanh>" + r[3] + "</NamHoanThanh>" +
                          "<DeTaiCap>" + r[4] + "</DeTaiCap>" +
                          "<TrachNhiem>" + r[5] + "</TrachNhiem>";
                    xmlENodes_DeTai.AppendChild(newNode);
                }
                #endregion

                #region Lưu công bố khoa học
                XmlNode Nodes_CongBo= root.SelectSingleNode("NghienCuuKhoaHoc/DSCongTrinhKhoaHoc");
                XmlElement xmlENodes_CongBo = Nodes_CongBo as XmlElement;
                Nodes_CongBo.InnerText = "";
                dtTarget = CongCu.GetContentAsDataTable(grdCongBoKhoaHoc);
                foreach (DataRow r in dtTarget.Rows)
                {
                    XmlElement newNode = doc.CreateElement("CongBo");
                    newNode.InnerXml =
                          "<id>" + r[0] + "</id>" +
                          "<TenCongTrinh>" + r[1] + "</TenCongTrinh>" +
                          "<NamCongBo>" + r[3] + "</NamCongBo>" +
                          "<NoiCongBo>" + r[2] + "</NoiCongBo>" +
                          "<LoaiCongBo>" + r[4] + "</LoaiCongBo>";
                    xmlENodes_CongBo.AppendChild(newNode);
                }
                #endregion
            doc.Save(fullXMLFile);
            MessageBox.Show("Lưu thành công", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #region Thao Tác quá trình công tác
        private void grdQuaTrinhCongTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblID.Text = grdQuaTrinhCongTac.SelectedRows[0].Cells["STTqtct"].Value.ToString();
                txtTuNgay.Text = grdQuaTrinhCongTac.SelectedRows[0].Cells["TuNgay"].Value.ToString();
                txtDenNgay.Text = grdQuaTrinhCongTac.SelectedRows[0].Cells["DenNgay"].Value.ToString();
                txtQT_donvicongtac.Text = grdQuaTrinhCongTac.SelectedRows[0].Cells["DonViCongTac"].Value.ToString();
                txtCongViecDamNhiem.Text = grdQuaTrinhCongTac.SelectedRows[0].Cells["CongViecDamNhiem"].Value.ToString();
                lblHangChon_QTCT.Text = e.RowIndex.ToString();
            }
            catch
            {
                txtTuNgay.Text = "";
                txtDenNgay.Text = "";
                txtQT_donvicongtac.Text = "";
                txtCongViecDamNhiem.Text = "";
                lblHangChon_QTCT.Text = "-1";
            }

        }

        private void linkQTCT_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkQTCT_Them.Text == "Thêm mới")
            {
                txtTuNgay.Enabled = true;
                txtTuNgay.Text = "";
                txtDenNgay.Enabled = true;
                txtDenNgay.Text = "";
                txtQT_donvicongtac.Enabled = true;
                txtQT_donvicongtac.Text = "";

                txtCongViecDamNhiem.Enabled = true;
                txtCongViecDamNhiem.Text = "";
                linkQTCT_Them.BackColor = Color.Red;
                linkQTCT_Them.Text = "Xác nhận thêm";
                linkQTCT_Xoa.Enabled = false;
                linkQTCT_Sua.Enabled = false;
                linkQTCT_Huy.Text = "Hủy bỏ thêm";
                linkQTCT_Huy.Visible = true;
            }
            else
            {

                if ((txtTuNgay.Text.Length <= 3))
                    MessageBox.Show("Vui lòng nhập thời điểm công tác", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtQTCT = CongCu.GetContentAsDataTable(grdQuaTrinhCongTac);
                    dtQTCT.LoadDataRow(new[]
                                      {
                                      (dtQTCT.Rows.Count+1).ToString(),
                                      txtTuNgay.Text,
                                      txtDenNgay.Text,
                                      txtQT_donvicongtac.Text,
                                      txtCongViecDamNhiem.Text
                                       
                                      }, LoadOption.Upsert);
                    try { grdQuaTrinhCongTac.DataSource = dtQTCT; grdQuaTrinhCongTac.Refresh(); }
                    catch (Exception ex) { grdQuaTrinhCongTac.DataSource = null; }

                }
                txtTuNgay.Enabled = false;
                txtTuNgay.Text = "";
                txtDenNgay.Enabled = false;
                txtDenNgay.Text = "";
                txtQT_donvicongtac.Enabled = false;
                txtQT_donvicongtac.Text = "";

                txtCongViecDamNhiem.Enabled = false;
                txtCongViecDamNhiem.Text = "";
                linkQTCT_Them.BackColor = Color.Transparent;
                linkQTCT_Them.Text = "Thêm mới";
                linkQTCT_Xoa.Enabled = true;
                linkQTCT_Sua.Enabled = true;
             
                linkQTCT_Huy.Visible = false;

            }
        }

        private void linkQTCT_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_QTCT.Text == "-1") MessageBox.Show("Vui lòng chọn loại cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtQTct= CongCu.GetContentAsDataTable(grdQuaTrinhCongTac);
                int i = Int16.Parse(lblHangChon_QTCT.Text);
                DialogResult rs = MessageBox.Show("Bạn có chắc xóa bản ghi này", "NTUWebgen Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dtQTct.Rows.RemoveAt(i);
                    try { grdQuaTrinhCongTac.DataSource = dtQTct; grdQuaTrinhCongTac.Refresh(); }
                    catch (Exception ex) { grdQuaTrinhCongTac.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_QTCT.Text = "-1";
                    txtTuNgay.Text = "";
                    txtDenNgay.Text = "";
                    txtQT_donvicongtac.Text = "";
                    txtCongViecDamNhiem.Text = "";
                }
            }
        }

        private void linkQTCT_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkQTCT_Sua.Text == "Sửa")
            {
                if (lblHangChon_QTCT.Text == "-1") MessageBox.Show("Vui lòng chọn loại cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTuNgay.Enabled = true;
                    txtDenNgay.Enabled = true;
                    txtQT_donvicongtac.Enabled = true;
                    txtCongViecDamNhiem.Enabled = true;
                    linkQTCT_Sua.Text = "Xác nhận sửa";
                    linkQTCT_Sua.BackColor = Color.Red;
                    linkQTCT_Them.Enabled = false;
                    linkQTCT_Xoa.Enabled = false;
                    linkQTCT_Huy.Text = "Hủy bỏ sửa";
                    linkQTCT_Huy.Visible = true;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(grdQuaTrinhCongTac);
                int i = Int16.Parse(lblHangChon_QTCT.Text);
                dtMoi.Rows[i]["TuNgay"] = txtTuNgay.Text;
                dtMoi.Rows[i]["DenNgay"] = txtDenNgay.Text;
                dtMoi.Rows[i]["DonViCongTac"] = txtQT_donvicongtac.Text;
                dtMoi.Rows[i]["CongViecDamNhiem"] = txtCongViecDamNhiem.Text;

                try { grdQuaTrinhCongTac.DataSource = dtMoi; grdQuaTrinhCongTac.Refresh(); }
                catch (Exception ex) { grdQuaTrinhCongTac.DataSource = null; }

                txtTuNgay.Enabled = false;
                txtDenNgay.Enabled = false;
                txtQT_donvicongtac.Enabled = false;
                txtCongViecDamNhiem.Enabled = false;
                linkQTCT_Sua.Text = "Sửa";
                linkQTCT_Sua.BackColor = Color.Transparent;
                linkQTCT_Them.Enabled = true;
                linkQTCT_Xoa.Enabled = true;
                
                linkQTCT_Huy.Visible = false;
            }
        }

      #endregion Thao Tác quá trình công tác

          
    #region Thao tác đề tài khoa học
        private void grdDeTaiKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("ok");
            try
            {
                lblIDDeTai.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["STTdetai"].Value.ToString();
                txtTenDeTaiNCKH.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["TenDeTai"].Value.ToString();
                txtNamBatDau.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["NamBatDau"].Value.ToString();
                txtNamHoanThanh.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["NamKetThuc"].Value.ToString();
                txtDeTaiCap.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["DeTaiCap"].Value.ToString();
                txtTrachNhiem.Text = grdDeTaiKhoaHoc.SelectedRows[0].Cells["TrachNhiem"].Value.ToString();
                lblHangChon_DeTai.Text = e.RowIndex.ToString();
            }
            catch
            {
                txtTenDeTaiNCKH.Text = "";
                txtNamBatDau.Text = "";
                txtNamHoanThanh.Text = "";
                txtDeTaiCap.Text = "";
                txtTrachNhiem.Text = "";
                lblHangChon_DeTai.Text = "-1";
            }
        }
        private void linkDeTai_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkDeTai_Them.Text == "Thêm mới")
            {
                txtTenDeTaiNCKH.Text = "";
                txtNamBatDau.Text = "";
                txtNamHoanThanh.Text = "";
                txtDeTaiCap.Text = "";
                txtTrachNhiem.Text = "";
                txtTenDeTaiNCKH.Enabled = true;
                txtNamBatDau.Enabled = true;
                txtNamHoanThanh.Enabled = true;
                txtDeTaiCap.Enabled = true;
                txtTrachNhiem.Enabled = true;

                linkDeTai_Xoa.Enabled = false;
                linkDeTai_Sua.Enabled = false;
                linkDeTai_Huy.Text = "Hủy bỏ thêm";
                linkDeTai_Huy.Visible = true;
                linkDeTai_Them.BackColor = Color.Red;
                linkDeTai_Them.Text = "Xác nhận thêm";
            }
            else
            {

                if ((txtTenDeTaiNCKH.Text.Length <= 3))
                    MessageBox.Show("Vui lòng nhập tên đề tài ", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtDeTai = CongCu.GetContentAsDataTable(grdDeTaiKhoaHoc);
                    dtDeTai.LoadDataRow(new[]
                                      {
                                      (dtDeTai.Rows.Count+1).ToString(),
                                      txtTenDeTaiNCKH.Text,
                                      txtNamBatDau.Text,
                                      txtNamHoanThanh.Text,
                                      txtDeTaiCap.Text,
                                      txtTrachNhiem.Text
                                       
                                      }, LoadOption.Upsert);
                    try { grdDeTaiKhoaHoc.DataSource = dtDeTai; grdDeTaiKhoaHoc.Refresh(); }
                    catch (Exception ex) { grdDeTaiKhoaHoc.DataSource = null; }

                }

                txtTenDeTaiNCKH.Text = "";
                txtNamBatDau.Text = "";
                txtNamHoanThanh.Text = "";
                txtDeTaiCap.Text = "";
                txtTrachNhiem.Text = "";
                txtTenDeTaiNCKH.Enabled = false;
                txtNamBatDau.Enabled = false;
                txtNamHoanThanh.Enabled = false;
                txtDeTaiCap.Enabled = false;
                txtTrachNhiem.Enabled = false;
                linkDeTai_Xoa.Enabled = true;
                linkDeTai_Sua.Enabled = true;
                linkDeTai_Them.BackColor = Color.Transparent;
                linkDeTai_Them.Text = "Thêm mới";
               
                linkDeTai_Huy.Visible = false;

            }
        }

        private void linkDeTai_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_DeTai.Text == "-1") MessageBox.Show("Vui lòng chọn đề tài cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtDeTai = CongCu.GetContentAsDataTable(grdDeTaiKhoaHoc);
                int i = Int16.Parse(lblHangChon_DeTai.Text);
                DialogResult rs = MessageBox.Show("Xác nhận xóa đề tài: " + dtDeTai.Rows[i][1], "NTUWebgen Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dtDeTai.Rows.RemoveAt(i);
                    try { grdDeTaiKhoaHoc.DataSource = dtDeTai; grdDeTaiKhoaHoc.Refresh(); }
                    catch (Exception ex) { grdDeTaiKhoaHoc.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_DeTai.Text = "-1";
                    txtTenDeTaiNCKH.Text = "";
                    txtNamBatDau.Text = "";
                    txtNamHoanThanh.Text = "";
                    txtDeTaiCap.Text = "";
                    txtTrachNhiem.Text = "";
                }

            }
        }

        private void linkDeTai_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkDeTai_Sua.Text == "Sửa")
            {
                if (lblHangChon_DeTai.Text == "-1") MessageBox.Show("Vui lòng chọn đề tài cần sửa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTenDeTaiNCKH.Enabled = true;
                    txtNamBatDau.Enabled = true;
                    txtNamHoanThanh.Enabled = true;
                    txtDeTaiCap.Enabled = true;
                    txtTrachNhiem.Enabled = true;
                    linkDeTai_Them.Enabled = false;
                    linkDeTai_Xoa.Enabled = false;
                    linkDeTai_Sua.BackColor = Color.Red;
                    linkDeTai_Sua.Text = "Cập nhật sửa";
                    linkDeTai_Huy.Text = "Hủy bỏ sửa";
                    linkDeTai_Huy.Visible = true;

                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(grdDeTaiKhoaHoc);
                int i = Int16.Parse(lblHangChon_DeTai.Text);
                dtMoi.Rows[i]["TenDeTai"] = txtTenDeTaiNCKH.Text;
                dtMoi.Rows[i]["NamBatDau"] = txtNamBatDau.Text;
                dtMoi.Rows[i]["NamKetThuc"] = txtNamHoanThanh.Text;
                dtMoi.Rows[i]["DeTaiCap"] = txtDeTaiCap.Text;
                dtMoi.Rows[i]["TrachNhiem"] = txtTrachNhiem.Text;

                try { grdDeTaiKhoaHoc.DataSource = dtMoi; grdDeTaiKhoaHoc.Refresh(); }
                catch (Exception ex) { grdDeTaiKhoaHoc.DataSource = null; }

                txtTenDeTaiNCKH.Enabled = false;
                txtNamBatDau.Enabled = false;
                txtNamHoanThanh.Enabled = false;
                txtDeTaiCap.Enabled = false;
                txtTrachNhiem.Enabled = false;
                linkDeTai_Them.Enabled = true;
                linkDeTai_Xoa.Enabled = true;
                linkDeTai_Huy.Visible = false;
                linkDeTai_Sua.BackColor = Color.Transparent;
                linkDeTai_Sua.Text = "Sửa";
            }
        }
    #endregion
    #region Thao tác coogn trinh khoa hoc cong bo

    private void grdCongBoKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblIDCongBo.Text = grdCongBoKhoaHoc.SelectedRows[0].Cells["STTCongBo"].Value.ToString();
                txtTenCongTrinh.Text = grdCongBoKhoaHoc.SelectedRows[0].Cells["TenCongBo"].Value.ToString();
                txtNamCongBo.Text = grdCongBoKhoaHoc.SelectedRows[0].Cells["NamCongBo"].Value.ToString();
                txtLoaiCongBo.Text = grdCongBoKhoaHoc.SelectedRows[0].Cells["LoaiCongBo"].Value.ToString();
                txtNoiCongBo.Text = grdCongBoKhoaHoc.SelectedRows[0].Cells["NoiCongBo"].Value.ToString();
                lblHangChon_CongBo.Text = e.RowIndex.ToString();
            }
            catch
            {
                txtTenCongTrinh.Text = "";
                txtNamCongBo.Text = "";
                txtLoaiCongBo.Text = "";
                txtNoiCongBo.Text = "";
               
                lblHangChon_CongBo.Text = "-1";
            }
        }
     private void linkCongBo_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkCongBo_Them.Text == "Thêm mới")
            {
                txtTenCongTrinh.Text = "";
                txtNamCongBo.Text = "";
                txtLoaiCongBo.Text = "";
                txtNoiCongBo.Text = "";

                txtTenCongTrinh.Enabled=true;
                txtNamCongBo.Enabled = true;
                txtLoaiCongBo.Enabled = true;
                txtNoiCongBo.Enabled = true;
                linkCongBo_Sua.Enabled = false;
                linkCongBo_Xoa.Enabled = false;
                linkCongBo_Them.BackColor = Color.Red;
                linkCongBo_Them.Text = "Xác nhận thêm";
                linkCongBo_Huy.Text = "Hủy bỏ thêm";
                linkCongBo_Huy.Visible = true;
            }
            else
            {

                if ((txtTenCongTrinh.Text.Length <= 3))
                    MessageBox.Show("Vui lòng nhập tên công trình khoa học ", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtCongBo = CongCu.GetContentAsDataTable(grdCongBoKhoaHoc);
                    dtCongBo.LoadDataRow(new[]
                                      {
                                      (dtCongBo.Rows.Count+1).ToString(),
                                      txtTenCongTrinh.Text,
                                         txtNoiCongBo.Text,
                                      txtNamCongBo.Text,
                                      txtLoaiCongBo.Text
                                   
                                    
                                       
                                      }, LoadOption.Upsert);
                    try { grdCongBoKhoaHoc.DataSource = dtCongBo; grdCongBoKhoaHoc.Refresh(); }
                    catch (Exception ex) { grdCongBoKhoaHoc.DataSource = null; }

                }

                txtTenCongTrinh.Text = "";
                txtNamCongBo.Text = "";
                txtLoaiCongBo.Text = "";
                txtNoiCongBo.Text = "";

                txtTenCongTrinh.Enabled = false;
                txtNamCongBo.Enabled = false;
                txtLoaiCongBo.Enabled = false;
                txtNoiCongBo.Enabled = false;

                linkCongBo_Sua.Enabled = true;
                linkCongBo_Xoa.Enabled = true;
                linkCongBo_Them.BackColor = Color.Transparent;
                linkCongBo_Huy.Visible = false;
                linkCongBo_Them.Text = "Thêm mới";

            }
        }

        private void linkCongBo_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_CongBo.Text == "-1") MessageBox.Show("Vui lòng chọn công bố cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dt = CongCu.GetContentAsDataTable(grdCongBoKhoaHoc);
                int i = Int16.Parse(lblHangChon_CongBo.Text);
                DialogResult rs = MessageBox.Show("Xác nhận xóa công bố: " + dt.Rows[i][1], "NTUWebgen Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dt.Rows.RemoveAt(i);
                    try { grdCongBoKhoaHoc.DataSource = dt; grdCongBoKhoaHoc.Refresh(); }
                    catch (Exception ex) { grdCongBoKhoaHoc.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_CongBo.Text = "-1";
                    txtTenCongTrinh.Text = "";
                    txtNamCongBo.Text = "";
                    txtLoaiCongBo.Text = "";
                    txtNoiCongBo.Text = "";
                }
            }
        }

        private void linkCongBo_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkCongBo_Sua.Text == "Sửa")
            {
                if (lblHangChon_CongBo.Text == "-1") MessageBox.Show("Vui lòng chọn công bố cần sửa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTenCongTrinh.Enabled = true;
                    txtNamCongBo.Enabled = true;
                    txtLoaiCongBo.Enabled = true;
                    txtNoiCongBo.Enabled = true;
                    linkCongBo_Sua.Text = "Cập nhật sửa";
                    linkCongBo_Sua.BackColor = Color.Red;
                    linkCongBo_Them.Enabled = false;
                    linkCongBo_Xoa.Enabled = false;
                    linkCongBo_Huy.Text = "Hủy bỏ sửa";
                    linkCongBo_Huy.Visible = true;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(grdCongBoKhoaHoc);
                int i = Int16.Parse(lblHangChon_CongBo.Text);
                dtMoi.Rows[i]["TenCongBo"] = txtTenCongTrinh.Text;
                dtMoi.Rows[i]["NoiCongBo"] = txtNoiCongBo.Text;
                dtMoi.Rows[i]["NamCongBo"] = txtNamCongBo.Text;
                dtMoi.Rows[i]["LoaiCongBo"] = txtLoaiCongBo.Text;

                try { grdCongBoKhoaHoc.DataSource = dtMoi; grdCongBoKhoaHoc.Refresh(); }
                catch (Exception ex) { grdCongBoKhoaHoc.DataSource = null; }

                txtTenCongTrinh.Enabled = false;
                txtNamCongBo.Enabled = false;
                txtLoaiCongBo.Enabled = false;
                txtNoiCongBo.Enabled = false;
                linkCongBo_Sua.BackColor = Color.Transparent;
                linkCongBo_Them.Enabled = true;
                linkCongBo_Xoa.Enabled = true;
                linkCongBo_Huy.Visible = false;
                linkCongBo_Sua.Text = "Sửa";
            }
        }
    #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveLyLich(fullXMLFile);
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            XuatWeb.XuatCV(fullXMLFile, fullHTMLFile);
            MessageBox.Show("Đã xuất xong, mời xem kết quả", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CongCu.gotoSite(fullHTMLFile);

        }

        private void linkDeTai_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTenDeTaiNCKH.Text = "";
            txtNamBatDau.Text = "";
            txtNamHoanThanh.Text = "";
            txtDeTaiCap.Text = "";
            txtTrachNhiem.Text = "";
            txtTenDeTaiNCKH.Enabled = false;
            txtNamBatDau.Enabled = false;
            txtNamHoanThanh.Enabled = false;
            txtDeTaiCap.Enabled = false;
            txtTrachNhiem.Enabled = false;

            linkDeTai_Them.Enabled = true;
            linkDeTai_Them.Text = "Thêm mới";
            linkDeTai_Them.BackColor = Color.Transparent;
            linkDeTai_Sua.BackColor = Color.Transparent;
            linkDeTai_Xoa.Enabled = true;
            linkDeTai_Sua.Text = "Sửa";
            linkDeTai_Sua.Enabled = true;
            linkDeTai_Huy.Visible= false;

        }

        private void linkCongBo_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTenCongTrinh.Text = "";
            txtNamCongBo.Text = "";
            txtLoaiCongBo.Text = "";
            txtNoiCongBo.Text = "";

            txtTenCongTrinh.Enabled = false;
            txtNamCongBo.Enabled = false;
            txtLoaiCongBo.Enabled = false;
            txtNoiCongBo.Enabled = false;
            linkCongBo_Them.BackColor = Color.Transparent;
            linkCongBo_Sua.BackColor = Color.Transparent;
            linkCongBo_Sua.Enabled = true;
            linkCongBo_Xoa.Enabled = true;
            linkCongBo_Them.Enabled = true;
            linkCongBo_Huy.Visible = false;
            linkCongBo_Them.Text = "Thêm mới";
            linkCongBo_Sua.Text = "Sửa";
             
        }

        private void linkQTCT_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTuNgay.Enabled = false;
            txtTuNgay.Text = "";
            txtDenNgay.Enabled = false;
            txtDenNgay.Text = "";
            txtQT_donvicongtac.Enabled = false;
            txtQT_donvicongtac.Text = "";

            txtCongViecDamNhiem.Enabled = false;
            txtCongViecDamNhiem.Text = "";
            linkQTCT_Them.BackColor = Color.Transparent;
            linkQTCT_Them.Text = "Thêm mới";
            linkQTCT_Sua.BackColor = Color.Transparent;
            linkQTCT_Sua.Text = "Sửa";
            linkQTCT_Xoa.Enabled = true;
            linkQTCT_Sua.Enabled = true; linkQTCT_Them.Enabled = true;


            linkQTCT_Huy.Visible = false;
        }

       


}
}