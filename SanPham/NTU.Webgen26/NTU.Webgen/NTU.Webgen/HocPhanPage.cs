﻿using System;
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
    public partial class HocPhanPage : UserControl
    {
        static String xmlFile = @"\data\teaching.xml";
        static String htmlFile = @"\index.html";
        String ProjectFolder;
        String CourseFolder;
        String fullXMLFile;
        String fullHTMLFile;
        bool isThemMoi, isSua;
        String MaHocPhan;
        public HocPhanPage()
        {
            InitializeComponent();
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            this.fullXMLFile = ProjectFolder + xmlFile;
            MaHocPhan = "NEC303";
            this.CourseFolder = ProjectFolder + @"\courses\"+MaHocPhan;

            LoadBaiGiang("NEC303", fullXMLFile);
            this.fullHTMLFile = (CourseFolder + htmlFile);
            linkWebCourse.Text = fullHTMLFile;
        }


        public HocPhanPage(String Idhocphan, String ProjectFolder) {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.fullXMLFile = ProjectFolder + xmlFile;
            
            this.MaHocPhan = Idhocphan;
            this.CourseFolder = ProjectFolder + @"\courses\" + MaHocPhan;
            this.fullHTMLFile = (CourseFolder + htmlFile);
            LoadBaiGiang(MaHocPhan, fullXMLFile);
            linkWebCourse.Text = fullHTMLFile;
        }

        private void dataGridViewX2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MessageBox.Show(e.RowIndex.ToString());
            }
        }
        // Lưu bài giảng lại, vào file XML
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBaiGiang(txtMaHP.Text, fullXMLFile);

        }
    

        private void SaveBaiGiang(String idHocPhan, String xmlTeachFile) {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(xmlTeachFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;
            XmlNode Nodes_BaiGiang = root.SelectSingleNode("/root/DSHocPhan/HocPhan[id='" + idHocPhan + "']");
            XmlElement xmlENodes_BaiGiang = Nodes_BaiGiang as XmlElement;
            
            #region Lưu thông tin học phần
            XmlNode oldNodeThongTin = Nodes_BaiGiang.SelectSingleNode("ThongTin");
            XmlElement newNodeThongTin = doc.CreateElement("ThongTin");
            newNodeThongTin.InnerXml=
                "<MaHP>" + txtMaHP.Text + "</MaHP>" +
                "<TenHP>" + txtTenHP.Text + "</TenHP>" +
                "<HocPhan_HocTruoc>" + txtHPHocTruoc.Text + "</HocPhan_HocTruoc>" +
                "<KhoiLuong>" + txtKhoiLuong.Text + "</KhoiLuong>" +
    			"<DaoTaoTrinhDo>"+txtTrinhDo.Text +"</DaoTaoTrinhDo>"+
    			"<MucTieu>" +txtMucTieu.Text +"</MucTieu>"+
    			"<NoiDungVanTat>"  +txtNoiDung.Text +"</NoiDungVanTat>";
            xmlENodes_BaiGiang.ReplaceChild(newNodeThongTin, oldNodeThongTin);
           
            #endregion
            #region Lưu phần đánh giá
            XmlNode nodeDanhGia = Nodes_BaiGiang.SelectSingleNode("DanhGiaKetQua");
            nodeDanhGia.InnerText = ""; 
            DataTable dtTarget = CongCu.GetContentAsDataTable(grvDanhGia);
            foreach (DataRow r in dtTarget.Rows) {
                XmlElement newNode = doc.CreateElement("HinhThuc");
                newNode.InnerXml =
                      "<id>" + r[0] + "</id>" +
                      "<TenHinhThuc>" + r[1] + "</TenHinhThuc>" +
                      "<MoTaThem>" + r[2] + "</MoTaThem>" +
                      "<TrongSo>" + r[3] + "</TrongSo>" +
                      "<HinhThucLamBai>" + r[4] + "</HinhThucLamBai>";
                nodeDanhGia.AppendChild(newNode);
            }
           
            #endregion
            #region Đê cương học phần
            XmlNode oldNodeDeCuong = Nodes_BaiGiang.SelectSingleNode("DeCuongHocPhan");
            //1. Upload or No
            String fileDeCuongTrongXML = oldNodeDeCuong.InnerText;
            String fileDeCuongTrongTextBox = txtDeCuong.Text;
            
            if (fileDeCuongTrongXML != fileDeCuongTrongTextBox)
            {
                String filenameCopy = fileDeCuongTrongTextBox.Substring(fileDeCuongTrongTextBox.LastIndexOf("\\")+1);
                File.Copy(fileDeCuongTrongTextBox,CourseFolder+"\\"+filenameCopy,true);
                oldNodeDeCuong.InnerXml = filenameCopy;
                txtDeCuong.Text = filenameCopy;
            }
            
            #endregion
            #region Tài liệu tham khảo
            XmlNode nodeTLTK = Nodes_BaiGiang.SelectSingleNode("TaiLieuThamKhao");
            nodeTLTK.InnerText = "";
            dtTarget = CongCu.GetContentAsDataTable(gdvTaiLieuThamKhao);
            foreach (DataRow r in dtTarget.Rows)
            {
                XmlElement newNode = doc.CreateElement("TaiLieu");
                String dckt = r[5].ToString();
                if (dckt.Contains("/")) dckt = System.Web.HttpUtility.UrlEncode(dckt);
                newNode.InnerXml =
                           "<id>" + r[0] + "</id>" +
                          "<TenTaiLieu>" + r[1] + "</TenTaiLieu>" +
                          "<TacGia>" + r[2] + "</TacGia>" +
                          "<NhaXuatBan>" + r[3] + "</NhaXuatBan>" +
                          "<NamXuatBan>" + r[4] + "</NamXuatBan>" +
                          "<DiaChiKhaiThac>" + dckt + "</DiaChiKhaiThac>" +
                          "<MucDichSuDung>" +  r[6] + "</MucDichSuDung>";
                nodeTLTK.AppendChild(newNode);
            }
            #endregion

            #region Bài giang chi tiet
            XmlNode nodeChiTiet = Nodes_BaiGiang.SelectSingleNode("BaiGiang");
            nodeChiTiet.InnerText = "";
            dtTarget = CongCu.GetContentAsDataTable(grdBaiGiang);
            foreach (DataRow r in dtTarget.Rows)
            {
                XmlElement newNode = doc.CreateElement("Chuong");
                String f = r[3].ToString();
                if (f.LastIndexOf("\\") > 0) f = f.Substring(f.LastIndexOf("\\") + 1); 
                newNode.InnerXml =
                           "<id>"+r[0]+"</id>"+
                          "<TenChuong>"+r[1]+"</TenChuong>"+
                          "<MoTaNoiDung>"+r[2]+"</MoTaNoiDung>"+
                          "<Download>"+f+"</Download>";
               nodeChiTiet.AppendChild(newNode);
               // Up chuong
               String fileChuongTrongBang = r[3].ToString();
               if (fileChuongTrongBang.LastIndexOf("\\") > 0)
               {
                   try
                   {
                       String filenameCopy = fileChuongTrongBang.Substring(fileChuongTrongBang.LastIndexOf("\\") + 1);
                       File.Copy(fileChuongTrongBang, CourseFolder + "\\" + filenameCopy, true);
                       r[3] = filenameCopy;
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message, "Lỗi khi copy file vào thư mục website", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Asterisk);
                   }
               }

            }
            #endregion

            doc.Save(fullXMLFile);
            MessageBox.Show("Lưu thành công", "NTUWebgen.Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void  LoadBaiGiang(String idHocPhan, String xmlTeachFile)
        {
            
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(xmlTeachFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            // Lấy gốc
            XmlElement root = doc.DocumentElement;
            XmlNode Nodes_BaiGiang = root.SelectSingleNode("/root/DSHocPhan/HocPhan[id='" + idHocPhan + "']");
            //XmlElement bg = root. 
            #region Thông tin học phần

            XmlNode nodeThongTin = Nodes_BaiGiang.SelectSingleNode("ThongTin");
            String maHP = nodeThongTin.ChildNodes[0].InnerText;
            String tenHP = nodeThongTin.ChildNodes[1].InnerText;
            String hocTruoc = nodeThongTin.ChildNodes[2].InnerText;
            String tc = nodeThongTin.ChildNodes[3].InnerText;
            String trinhdo = nodeThongTin.ChildNodes[4].InnerText;
            String muctieu = nodeThongTin.ChildNodes[5].InnerText;
            String noidung = nodeThongTin.ChildNodes[6].InnerText;

            txtMaHP.Text = maHP;
            txtTenHP.Text = tenHP;
            txtHPHocTruoc.Text = hocTruoc;
            txtKhoiLuong.Text = tc;
            txtMucTieu.Text = muctieu;
            txtNoiDung.Text = noidung;
            txtTrinhDo.Text = trinhdo;

            #endregion
            #region Đánh giá kết quả
            XmlNodeList dsDanhGia = Nodes_BaiGiang.SelectNodes("DanhGiaKetQua/HinhThuc");
            XmlNode[] nodeDanhGia = dsDanhGia.Cast<XmlNode>().ToArray();
            DataTable dtTarget = CongCu.GetContentAsDataTable(grvDanhGia);
            foreach (XmlNode k in nodeDanhGia)
            {
                String stt = k.ChildNodes[0].InnerText;
                String tenDG = k.ChildNodes[1].InnerText;
                String mota = k.ChildNodes[2].InnerText;
                String trongso = k.ChildNodes[3].InnerText;
                String hinhthuclambai = k.ChildNodes[4].InnerText;

                dtTarget.LoadDataRow(new[]
                                  {
                                      stt,
                                      tenDG,
                                      mota,
                                      trongso,
                                      hinhthuclambai
                                  }, LoadOption.Upsert);
           }
           try { grvDanhGia.DataSource = dtTarget;}
           catch (Exception e) { grvDanhGia.DataSource = null;}

            #endregion


            #region Tài liệu tham khảo
            XmlNodeList dsTaiLieu = Nodes_BaiGiang.SelectNodes("TaiLieuThamKhao/TaiLieu");
            XmlNode[] nodeTaiLieu = dsTaiLieu.Cast<XmlNode>().ToArray();
            DataTable dtTaiLieu = CongCu.GetContentAsDataTable(gdvTaiLieuThamKhao);

           
            foreach (XmlNode k in nodeTaiLieu)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tenTaiLieu = k.ChildNodes[1].InnerText;
                String tenTG = k.ChildNodes[2].InnerText;
                String nhaXB = k.ChildNodes[3].InnerText;
                String namXB = k.ChildNodes[4].InnerText;
                String diachiKhaiThac = k.ChildNodes[5].InnerText;
                if (diachiKhaiThac.Contains("%2f")) diachiKhaiThac = System.Web.HttpUtility.UrlDecode(diachiKhaiThac);
                String mucdichSuDung = k.ChildNodes[6].InnerText;
              
                dtTaiLieu.LoadDataRow(new[]
                                      {
                                      STT,
                                      tenTaiLieu,
                                      tenTG,
                                      nhaXB,
                                      namXB,
                                      diachiKhaiThac,mucdichSuDung
                                      }, LoadOption.Upsert);
            }
            try { gdvTaiLieuThamKhao.DataSource = dtTaiLieu; }
            catch (Exception e) { gdvTaiLieuThamKhao.DataSource = null; }
           
            #endregion

            #region Đề cương học phần
            XmlNode nodeDeCuong = Nodes_BaiGiang.SelectSingleNode("DeCuongHocPhan");
            txtDeCuong.Text = nodeDeCuong.InnerText;
           
            #endregion
            #region Bài giảng
            XmlNodeList dsChuong = Nodes_BaiGiang.SelectNodes("BaiGiang/Chuong");
            XmlNode[] nodeChuong = dsChuong.Cast<XmlNode>().ToArray();
            DataTable dtBaiGiang= CongCu.GetContentAsDataTable(grdBaiGiang);
            
            foreach (XmlNode k in nodeChuong)
            {
                String STT = k.ChildNodes[0].InnerText;
                String tenChuong = k.ChildNodes[1].InnerText;
                String mota = k.ChildNodes[2].InnerText;
                String url = k.ChildNodes[3].InnerText;
                dtBaiGiang.LoadDataRow(new[]
                                      {
                                      STT,
                                      tenChuong,
                                      mota,
                                      url 
                                      }, LoadOption.Upsert);
            }
            try { grdBaiGiang.DataSource = dtBaiGiang; }
            catch  { grdBaiGiang.DataSource = null; }
          
            #endregion

            #region Chính sách khác

            #endregion

        }

        //---------------
        #region Đánh giá
        private void grvDanhGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblID.Text = grvDanhGia.SelectedRows[0].Cells["STT"].Value.ToString();
                txtLoaiDanhGia.Text = grvDanhGia.SelectedRows[0].Cells["LoaiDanhGia"].Value.ToString();
                txtMoTa.Text = grvDanhGia.SelectedRows[0].Cells["MoTa"].Value.ToString();
                txtTrongSo.Text = grvDanhGia.SelectedRows[0].Cells["TrongSo"].Value.ToString();
                txtHinhThucThucHien.Text = grvDanhGia.SelectedRows[0].Cells["HinhThucThucHien"].Value.ToString();
                lblHangChon_DanhGia.Text = e.RowIndex.ToString();
            }
            catch {

                txtLoaiDanhGia.Text = "";
                txtMoTa.Text = "";
                txtTrongSo.Text = "";
                txtHinhThucThucHien.Text = "";
                lblHangChon_DanhGia.Text = "-1";
            }
        }

        private void linkThemDanhGia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkThemDanhGia.Text == "Thêm đánh giá")
            {
                txtLoaiDanhGia.Enabled = true;
                txtLoaiDanhGia.Text = "";

                txtMoTa.Enabled = true;
                txtMoTa.Text = "";

                txtTrongSo.Enabled = true;
                txtTrongSo.Text = "";

                txtHinhThucThucHien.Enabled = true;
                txtHinhThucThucHien.Text = "";
                linkThemDanhGia.Text = "Lưu đánh giá";
            }
            else
            {
                
                if ((txtLoaiDanhGia.Text.Length <= 3) || (txtTrongSo.Text.Length == 0))
                    MessageBox.Show("Vui lòng nhập tối thiểu 2 nội dung: Loại đánh giá + Trọng số", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtDanhGia = CongCu.GetContentAsDataTable(grvDanhGia);
                    dtDanhGia.LoadDataRow(new[]
                                      {
                                      (dtDanhGia.Rows.Count+1).ToString(),
                                      txtLoaiDanhGia.Text,
                                      txtMoTa.Text,
                                      txtTrongSo.Text,
                                      txtHinhThucThucHien.Text
                                      }, LoadOption.Upsert);
                    try { grvDanhGia.DataSource = dtDanhGia; grvDanhGia.Refresh(); }
                    catch (Exception ex) { grvDanhGia.DataSource = null; }

                }
                txtLoaiDanhGia.Enabled = false;
                txtLoaiDanhGia.Text = "";

                txtMoTa.Enabled = false;
                txtMoTa.Text = "";

                txtTrongSo.Enabled = false;
                txtTrongSo.Text = "";

                txtHinhThucThucHien.Enabled = false;
                txtHinhThucThucHien.Text = "";

                linkThemDanhGia.Text = "Thêm đánh giá";

            }
        }

        private void linkXoaDanhGia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_DanhGia.Text == "-1") MessageBox.Show("Vui lòng chọn loại đánh giá cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtDanhGia = CongCu.GetContentAsDataTable(grvDanhGia);
                int i = Int16.Parse(lblHangChon_DanhGia.Text);
                dtDanhGia.Rows.RemoveAt(i);
                try { grvDanhGia.DataSource = dtDanhGia; grvDanhGia.Refresh(); }
                catch (Exception ex) { grvDanhGia.DataSource = null; }
                MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblHangChon_DanhGia.Text = "-1";
                txtLoaiDanhGia.Text = "";
                txtMoTa.Text = "";
                txtTrongSo.Text = "";
                txtHinhThucThucHien.Text = "";

            }
        }

        private void linkSuaDanhGia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSuaDanhGia.Text == "Sửa đánh giá")
            {
                if (lblHangChon_DanhGia.Text == "-1") MessageBox.Show("Vui lòng chọn loại đánh giá cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtLoaiDanhGia.Enabled = true;
                    txtMoTa.Enabled = true;
                    txtTrongSo.Enabled = true;
                    txtHinhThucThucHien.Enabled = true;
                    linkSuaDanhGia.Text = "Cập nhật sửa";
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(grvDanhGia);
                int i = Int16.Parse(lblHangChon_DanhGia.Text);
                dtMoi.Rows[i]["LoaiDanhGia"] = txtLoaiDanhGia.Text;
                dtMoi.Rows[i]["MoTa"] = txtMoTa.Text;
                dtMoi.Rows[i]["TrongSo"] = txtTrongSo.Text;
                dtMoi.Rows[i]["HinhThucThucHien"] = txtHinhThucThucHien.Text;

                try { grvDanhGia.DataSource = dtMoi; grvDanhGia.Refresh(); }
                catch (Exception ex) { grvDanhGia.DataSource = null; }

                txtLoaiDanhGia.Enabled = false;
                txtMoTa.Enabled = false;
                txtTrongSo.Enabled = false;
                txtHinhThucThucHien.Enabled = false;
                linkSuaDanhGia.Text = "Sửa đánh giá";
            }
        }
        #endregion
        //--------------------
        #region Tài liệu tham khảo
        private void gdvTaiLieuThamKhao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblIDTaiLieu.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["STTTaiLieu"].Value.ToString();
                txtTenTaiLieu.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["TenTaiLieu"].Value.ToString();
                txtTacGia.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["TacGia"].Value.ToString();
                txtNhaXB.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["NhaXuatBan"].Value.ToString();
                txtNamXB.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["NamXuatBan"].Value.ToString();
                txtMucDichSuDung.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["MucDichSuDung"].Value.ToString();
                txtDiaChiKhaiThac.Text = gdvTaiLieuThamKhao.SelectedRows[0].Cells["DiaChiKhaiThac"].Value.ToString();
                lblHangChon_TaiLieu.Text = e.RowIndex.ToString();
            }
            catch {

                lblIDTaiLieu.Text = "";
                txtTenTaiLieu.Text = "";
                txtTacGia.Text = "";
                txtNhaXB.Text = "";
                txtNamXB.Text = "";
                txtMucDichSuDung.Text = "";
                txtDiaChiKhaiThac.Text = ""; lblHangChon_TaiLieu.Text = "-1";
            }
        }

        private void linkThemTaiLieu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkThemTaiLieu.Text == "Thêm tài liệu")
            {
                txtTenTaiLieu.Text = "";
                txtTenTaiLieu.Enabled = true;

                txtTacGia.Text = "";
                txtTacGia.Enabled = true;

                txtNhaXB.Text = "";
                txtNhaXB.Enabled = true;

                txtNamXB.Text = "";
                txtNamXB.Enabled = true;

                txtMucDichSuDung.Text = "";
                txtMucDichSuDung.Enabled = true;
                txtDiaChiKhaiThac.Text = "";
                txtDiaChiKhaiThac.Enabled = true;

                linkThemTaiLieu.Text = "Lưu thêm mới";
            }
            else
            {
                
                if ((txtTenTaiLieu.Text.Length <= 3))
                    MessageBox.Show("Vui lòng nhập tối thiểu tên tài liệu", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtTaiLieu = CongCu.GetContentAsDataTable(gdvTaiLieuThamKhao);
                    dtTaiLieu.LoadDataRow(new[]
                                      {
                                      (dtTaiLieu.Rows.Count+1).ToString(),
                                      txtTenTaiLieu.Text,
                                      txtTacGia.Text,
                                      txtNhaXB.Text,
                                      txtNamXB.Text,
                                      txtMucDichSuDung.Text,
                                      txtDiaChiKhaiThac.Text
                                      }, LoadOption.Upsert);
                    try { gdvTaiLieuThamKhao.DataSource = dtTaiLieu; gdvTaiLieuThamKhao.Refresh(); }
                    catch  { gdvTaiLieuThamKhao.DataSource = null; }

                    txtTenTaiLieu.Text = "";
                    txtTenTaiLieu.Enabled = false;

                    txtTacGia.Text = "";
                    txtTacGia.Enabled = false;

                    txtNhaXB.Text = "";
                    txtNhaXB.Enabled = false;

                    txtNamXB.Text = "";
                    txtNamXB.Enabled = false;

                    txtMucDichSuDung.Text = "";
                    txtMucDichSuDung.Enabled = false;
                    txtDiaChiKhaiThac.Text = "";
                    txtDiaChiKhaiThac.Enabled = false;

                    linkThemTaiLieu.Text = "Thêm tài liệu";
                }
                 

            }
        }

        private void linkXoaTaiLieu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_TaiLieu.Text == "-1") MessageBox.Show("Vui lòng chọn tài liệu cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtTL = CongCu.GetContentAsDataTable(gdvTaiLieuThamKhao);
                int i = Int16.Parse(lblHangChon_TaiLieu.Text);
                dtTL.Rows.RemoveAt(i);
                try { gdvTaiLieuThamKhao.DataSource = dtTL; gdvTaiLieuThamKhao.Refresh(); }
                catch (Exception ex) { gdvTaiLieuThamKhao.DataSource = null; }
                MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblHangChon_TaiLieu.Text = "-1";
                txtTenTaiLieu.Text = "";
                txtTacGia.Text = "";
                txtNhaXB.Text = "";
                txtNamXB.Text = "";
                txtMucDichSuDung.Text = "";
                txtDiaChiKhaiThac.Text = "";
               
            }
        }

        private void linkSuaTaiLieu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSuaTaiLieu.Text == "Sửa tài liệu")
            {
                if (lblHangChon_TaiLieu.Text == "-1") MessageBox.Show("Vui lòng chọn tài liệu cần sửa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTenTaiLieu.Enabled = true;
                    txtTacGia.Enabled = true;
                    txtNhaXB.Enabled = true;
                    txtNamXB.Enabled = true;
                    txtMucDichSuDung.Enabled = true;
                    txtDiaChiKhaiThac.Enabled = true;
                    linkSuaTaiLieu.Text = "Cập nhật sửa";
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(gdvTaiLieuThamKhao);
                int i = Int16.Parse(lblHangChon_TaiLieu.Text);
                dtMoi.Rows[i]["TenTaiLieu"] = txtTenTaiLieu.Text;
                dtMoi.Rows[i]["TacGia"] = txtTacGia.Text;
                dtMoi.Rows[i]["NhaXuatBan"] = txtNhaXB.Text;
                dtMoi.Rows[i]["NamXuatBan"] = txtNamXB.Text;
                dtMoi.Rows[i]["MucDichSuDung"] = txtMucDichSuDung.Text;
                dtMoi.Rows[i]["DiaChiKhaiThac"] = txtDiaChiKhaiThac.Text;
                try { gdvTaiLieuThamKhao.DataSource = dtMoi; gdvTaiLieuThamKhao.Refresh(); }
                catch  { gdvTaiLieuThamKhao.DataSource = null; }


                txtTenTaiLieu.Enabled = false;
                txtTacGia.Enabled = false;
                txtNhaXB.Enabled = false;
                txtNamXB.Enabled = false;
                txtMucDichSuDung.Enabled = false;
                txtDiaChiKhaiThac.Enabled = false;
                linkSuaTaiLieu.Text = "Sửa tài liệu";
            }
        }
    

        #endregion
        #region Bài giảng

        private void grdBaiGiang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblIDBaiGiang.Text = grdBaiGiang.SelectedRows[0].Cells["STTBaiGiang"].Value.ToString();
                txtTenChuDe.Text = grdBaiGiang.SelectedRows[0].Cells["TenChuDe"].Value.ToString();
                txtMoTaNoiDungChuDe.Text = grdBaiGiang.SelectedRows[0].Cells["MoTaNoiDung"].Value.ToString();
                txtFileDinhKem.Text = grdBaiGiang.SelectedRows[0].Cells["FileBaiGiang"].Value.ToString();
                lblHangChon_BaiGiang.Text = e.RowIndex.ToString();
            }
            catch
            {
                lblIDBaiGiang.Text = "";
                txtTenChuDe.Text = "";
                txtMoTaNoiDungChuDe.Text = "";
                txtFileDinhKem.Text = "";
                lblHangChon_BaiGiang.Text = "-1";
            }
        }

    

        private void linkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkThemChuDe.Text == "Thêm mới")
            {
                txtTenChuDe.Enabled = true;
                txtTenChuDe.Text = "";
                txtMoTaNoiDungChuDe.Enabled = true;
                txtMoTaNoiDungChuDe.Text = "";
                btnDinhKem.Enabled = true;
                linkThemChuDe.Text = "Lưu thêm mới";
            }
            else
            {
                String loi = null;
                if ((txtTenChuDe.Text.Length <= 3) || (txtMoTaNoiDungChuDe.Text.Length == 0))
                    MessageBox.Show("Vui lòng nhập tối thiểu 2 nội dung: Tên chủ đề + Mô tả", "NTU Webgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtChuDe = CongCu.GetContentAsDataTable(grdBaiGiang);
                    dtChuDe.LoadDataRow(new[]
                                      {
                                      (dtChuDe.Rows.Count+1).ToString(),
                                      txtTenChuDe.Text,
                                      txtMoTaNoiDungChuDe.Text,
                                      txtFileDinhKem.Text 
                                      }, LoadOption.Upsert);
                    try { grdBaiGiang.DataSource = dtChuDe; grdBaiGiang.Refresh(); }
                    catch (Exception ex) { grdBaiGiang.DataSource = null; }
              
                }

                txtTenChuDe.Enabled = false;
                txtTenChuDe.Text = "";
                txtMoTaNoiDungChuDe.Enabled = false;
                txtMoTaNoiDungChuDe.Text = "";
                btnDinhKem.Enabled = false;
                linkThemChuDe.Text = "Thêm mới";

            }
        }

        private void linkXoaChuDe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_BaiGiang.Text == "-1") MessageBox.Show("Vui lòng chọn chủ đề/chương cần xóa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtChuDe = CongCu.GetContentAsDataTable(grdBaiGiang);
                int i = Int16.Parse(lblHangChon_BaiGiang.Text);
                dtChuDe.Rows.RemoveAt(i);
                try { grdBaiGiang.DataSource = dtChuDe; grdBaiGiang.Refresh(); }
                catch (Exception ex) { grdBaiGiang.DataSource = null; }
                MessageBox.Show("Đã xóa xong", "NTU Webgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblHangChon_BaiGiang.Text = "-1";

            }
        }

        private void linkSuaChuDe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkSuaChuDe.Text == "Sửa")
            {
                if (lblHangChon_BaiGiang.Text == "-1") MessageBox.Show("Vui lòng chọn chủ đề/chương cần sửa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtTenChuDe.Enabled = true;
                    txtMoTaNoiDungChuDe.Enabled = true;
                    btnDinhKem.Enabled = true;
                    linkSuaChuDe.Text = "Cập nhật sửa";
                }
            }
            else 
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(grdBaiGiang);
                int i = Int16.Parse(lblHangChon_BaiGiang.Text);
                dtMoi.Rows[i]["TenChuDe"] = txtTenChuDe.Text;
                dtMoi.Rows[i]["MoTaNoiDung"] = txtMoTaNoiDungChuDe.Text;
                dtMoi.Rows[i]["FileBaiGiang"] = txtFileDinhKem.Text;
                try { grdBaiGiang.DataSource = dtMoi; grdBaiGiang.Refresh(); }
                catch (Exception ex) { grdBaiGiang.DataSource = null; }

                txtTenChuDe.Enabled = false;
                txtMoTaNoiDungChuDe.Enabled = false;
                btnDinhKem.Enabled = false;
                linkSuaChuDe.Text = "Sửa";
            }
        }

        private void btnDinhKem_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog1.ShowDialog();
            if (rs == DialogResult.OK) txtFileDinhKem.Text = openFileDialog1.FileName;
        }
        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        // Đề cương học phần
        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialog2.ShowDialog();
            if (rs == DialogResult.OK) txtDeCuong.Text = openFileDialog2.FileName;
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            XuatWeb.XuatBaiGiang(MaHocPhan, fullXMLFile, fullHTMLFile);
        }

        

        //-----------------------
    }
}
