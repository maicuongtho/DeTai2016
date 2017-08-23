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
using DevComponents.DotNetBar;
using System.IO;

namespace NTU.Webgen
{
    public partial class CacMonHoc : UserControl
    {
        ChuongTrinh parentForm;
        static String xmlFile = @"\data\teaching_list.xml";
        static String htmlFile = @"\teaching.html";
        String ProjectFolder;
        String fullXMLFile;
        String fullHTMLFile;
        bool thaydoi;
        public CacMonHoc()
        {
            InitializeComponent();
            this.ProjectFolder = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4";
            this.fullXMLFile = ProjectFolder + xmlFile;
            this.fullHTMLFile = (ProjectFolder + htmlFile);
            thaydoi = false;
            LoadDSMonHoc();
            btnSave.Enabled = false;
        }

        public CacMonHoc(string ProjectFolder)
        {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.fullXMLFile = ProjectFolder + xmlFile;
            this.fullHTMLFile = (ProjectFolder + htmlFile);
            thaydoi = false;
            btnSave.Enabled = false;
            LoadDSMonHoc();
        }

        public CacMonHoc(string ProjectFolder, ChuongTrinh parent)
        {
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.fullXMLFile = ProjectFolder + xmlFile;
            this.fullHTMLFile = (ProjectFolder + htmlFile);
            parentForm = parent;
            thaydoi = false;
            btnSave.Enabled = false;
            LoadDSMonHoc();
        }


     
        private void btnSave_Click(object sender, EventArgs e)
        {
            LuuDSMonHoc();
            LoadDSMonHoc();
           // ThemCacMonHocMoi();
            CapNhatSangFileChiTietMonHoc();
            thaydoi = false;
            btnSave.Enabled = false;
        }

        private void linkHP_Them_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkHP_Them.Text == "Thêm mới")
            {
                txtHP_Ma.Enabled = true;
                txtHP_Ma.Text = "";

                txtHP_Ten.Text = "";
                txtHP_Ten.Enabled = true;
                txtHP_MucTieu.Text = "";
                txtHP_MucTieu.Enabled = true;
                pictureBox1.Enabled = true;
                linkHP_Them.BackColor = Color.Red;
                linkHP_Them.Text = "Xác nhận thêm";
                linkHP_Xoa.Enabled = false;
                linkHP_Sua.Enabled = false;
                linkHP_Huy.Text = "Hủy bỏ thêm";
                linkHP_Huy.Visible = true;
            }
            else
            {

                if ((txtHP_Ma.Text.Length < 3) || (txtHP_Ten.Text.Length <3))
                    MessageBox.Show("Vui lòng nhập mã,tên học phần", "NTUWebgen Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataTable dtHP = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
                    dtHP.LoadDataRow(new[]
                                      {
                                      (dataGridViewX_DSHP.Rows.Count+1).ToString(),
                                      txtHP_Ma.Text,
                                      txtHP_Ten.Text, txtHP_MucTieu.Text, "", pictureBox1.ImageLocation                                     
                                       
                                      }, LoadOption.Upsert);
                    try {
                        dataGridViewX_DSHP.DataSource = dtHP; 
                        dataGridViewX_DSHP.Refresh();
                        }
                    catch (Exception ex) { dataGridViewX_DSHP.DataSource = null; }
                    int i = 0;

                }
                txtHP_Ma.Enabled = false;
                txtHP_Ma.Text = "";

                txtHP_Ten.Text = "";
                txtHP_Ten.Enabled = false;
                txtHP_MucTieu.Text = "";
                txtHP_MucTieu.Enabled = false;
                pictureBox1.Enabled = false;
                linkHP_Them.BackColor = Color.Transparent;
                linkHP_Them.Text = "Thêm mới";
                linkHP_Xoa.Enabled = true;
                linkHP_Sua.Enabled = true;
                 
                linkHP_Huy.Visible = false;
                thaydoi = true;
                btnSave.Enabled = true;

            }
        }

        private void linkHP_Xoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblHangChon_HP.Text == "-1") MessageBox.Show("Vui lòng chọn học phần cần xóa", "NTUWebgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DataTable dtHP = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
                int i = Int16.Parse(lblHangChon_HP.Text);
                DialogResult rs = MessageBox.Show("Bạn có chắc xóa bản ghi này", "NTUWebgen Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rs == DialogResult.Yes)
                {
                    dtHP.Rows.RemoveAt(i);
                    try { dataGridViewX_DSHP.DataSource = dtHP; dataGridViewX_DSHP.Refresh(); }
                    catch (Exception ex) { dataGridViewX_DSHP.DataSource = null; }
                    MessageBox.Show("Đã xóa xong", "NTUWebgen Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblHangChon_HP.Text = "-1";
                    txtHP_Ma.Text = "";
                    txtHP_Ten.Text = "";
                    txtHP_MucTieu.Text = "";
                    thaydoi = true;
                    btnSave.Enabled = true;
                }
            }
        }

        private void linkHP_Sua_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            if (linkHP_Sua.Text == "Sửa")
            {
                if (lblHangChon_HP.Text == "-1") MessageBox.Show("Vui lòng chọn học phần cần sửa", "NTU Webgen Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    txtHP_Ma.Enabled = true;
                    txtHP_Ten.Enabled = true;
                    txtHP_MucTieu.Enabled = true;
                    linkHP_Sua.Text = "Xác nhận sửa";
                    pictureBox1.Enabled = true;
                    linkHP_Sua.BackColor = Color.Red;
                    linkHP_Them.Enabled = false;
                    linkHP_Xoa.Enabled = false;
                    linkHP_Huy.Text = "Hủy bỏ sửa";
                    linkHP_Huy.Visible = true;
                }
            }
            else
            {
                DataTable dtMoi = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
                int i = Int16.Parse(lblHangChon_HP.Text);
                dtMoi.Rows[i]["BienTapHP"] = "";
                dtMoi.Rows[i]["MaHP"] = txtHP_Ma.Text;
                dtMoi.Rows[i]["TenHP"] = txtHP_Ten.Text;
                dtMoi.Rows[i]["MucTieuHP"] = txtHP_MucTieu.Text;
                dtMoi.Rows[i]["AnhCover"] = pictureBox1.ImageLocation;
                try { dataGridViewX_DSHP.DataSource = dtMoi; dataGridViewX_DSHP.Refresh(); }
                catch (Exception ex) { dataGridViewX_DSHP.DataSource = null; }

                txtHP_Ten.Enabled = false;
                txtHP_Ma.Enabled = false;
                txtHP_MucTieu.Enabled = false;
                pictureBox1.Enabled = false;
                linkHP_Sua.Text = "Sửa";
                linkHP_Sua.BackColor = Color.Transparent;
                linkHP_Them.Enabled = true;
                linkHP_Xoa.Enabled = true;
               
                linkHP_Huy.Visible = false;
                thaydoi = true;
                btnSave.Enabled = true;
            }
        }

        private void linkHP_Huy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkHP_Them.Enabled = true; linkHP_Them.BackColor = Color.Transparent; linkHP_Them.Text = "Thêm mới";
            linkHP_Xoa.Enabled = true;
            linkHP_Sua.Enabled = true; linkHP_Sua.BackColor = Color.Transparent; linkHP_Sua.Text = "Sửa";
            linkHP_Huy.Visible = false;
            txtHP_Ma.Text = "";
            txtHP_Ten.Text = "";
            thaydoi = true;
            btnSave.Enabled = true;
        }

        private void dataGridViewX_DSHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblHangChon_HP.Text!="-1")

            if (e.ColumnIndex == 4)
            {
               DataTable dtHP = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
               String mahocphan = dtHP.Rows[e.RowIndex][1].ToString(); 
               if (parentForm.superTabControlWindows.Tabs.Contains(mahocphan))
                {
                    var t = (SuperTabItem)parentForm.superTabControlWindows.Tabs[mahocphan];
                    parentForm.superTabControlWindows.SelectedTab = t;
                }


                else
                {
                    HocPhanPage hp = new HocPhanPage(mahocphan,ProjectFolder);
                    CongCu.AddTab(mahocphan, "Học phần:" + mahocphan, parentForm.superTabControlWindows, hp, false, 10);
                }
            }
        }
        void LoadDSMonHoc() {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_HP = root.SelectNodes("/root/DSHocPhan/HocPhan");
            XmlNode[] hp = Nodes_HP.Cast<XmlNode>().ToArray();

            DataTable dtHP = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
            dtHP.Clear();
            int i = 0;
            foreach (XmlNode k in hp)
            {
               // String STT = k.ChildNodes[0].InnerText;
                i++;
                String ma = k.ChildNodes[1].InnerText;
                String ten =k.ChildNodes[2].InnerText;
                String muctieu = k.ChildNodes[3].InnerText;
                String cover = k.ChildNodes[4].InnerText;
                dtHP.LoadDataRow(new[]
                                      {
                                      i.ToString(),
                                      ma, ten, muctieu, 
                                      "chi tiết", cover
                                      }, LoadOption.Upsert);
                
            }
            try { dataGridViewX_DSHP.DataSource = dtHP; }
            catch { dataGridViewX_DSHP.DataSource = null; }
        }

        void LuuDSMonHoc() {
            // Đọc file XML
            XmlTextReader reader = new XmlTextReader(fullXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNode Nodes_HP = root.SelectSingleNode("DSHocPhan");
            XmlElement xmlENodes_HP = Nodes_HP as XmlElement;
            Nodes_HP.InnerText = "";
            DataTable dtTarget = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
            foreach (DataRow r in dtTarget.Rows)
            {
                //Copye anh cover

                String fileanh = r[5].ToString();
                String tenFile = fileanh;
                if (fileanh.Contains("\\"))
                {
                    tenFile = fileanh.Substring(fileanh.LastIndexOf("\\") + 1);
                    File.Copy(fileanh, ProjectFolder + @"\courses\" + r[1].ToString() + @"\" + tenFile);
                }
                XmlElement newNode = doc.CreateElement("HocPhan");
                newNode.InnerXml =
                      "<id>" + r[0] + "</id>" +
                      "<MaHP>" + r[1] + "</MaHP>" +
                      "<Ten>" + r[2] + "</Ten>" +
                      "<MucTieu>"+r[3]+"</MucTieu>"+
                      "<AnhCover>" + tenFile + "</AnhCover>";
                xmlENodes_HP.AppendChild(newNode);

                   // Tạo các thư mục môn học nếu chưa có, với tên là mã môn học
                String courseMonhoc = ProjectFolder + @"\courses\" + r[1];
                if (!Directory.Exists(courseMonhoc))
                {
                    Directory.CreateDirectory(courseMonhoc);
                    //và copy file môn học mẫu vào
                    String fileweb = courseMonhoc = ProjectFolder + @"\courses\" + r[1] + @"\index.html"; 
                    File.Copy(ProjectFolder + @"\courses\"+"courseTemplate-empty.html",fileweb);
                    //CongCu.Replace(fileweb ,"assets/", "../../assets/");
                }

            }

            doc.Save(fullXMLFile);
            MessageBox.Show("Lưu thành công", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewX_DSHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtHP_Id.Text = dataGridViewX_DSHP.SelectedRows[0].Cells[0].Value.ToString();
            txtHP_Ma.Text = dataGridViewX_DSHP.SelectedRows[0].Cells[1].Value.ToString();
            txtHP_Ten.Text = dataGridViewX_DSHP.SelectedRows[0].Cells[2].Value.ToString();
            txtHP_MucTieu.Text = dataGridViewX_DSHP.SelectedRows[0].Cells[3].Value.ToString();
            pictureBox1.ImageLocation = ProjectFolder + @"\courses\" + txtHP_Ma.Text + @"\" + dataGridViewX_DSHP.SelectedRows[0].Cells[5].Value.ToString();
            lblHangChon_HP.Text = e.RowIndex.ToString();
        }


        void ThemCacMonHocMoi() {
            //----- thêm môn học vào file teaching.xml
            // Đọc file XML
            XmlTextReader readerTeach = new XmlTextReader(ProjectFolder + @"\data\teaching.xml");
            XmlDocument docTeach = new XmlDocument();
            docTeach.Load(readerTeach);
            readerTeach.Close();
            // Lấy gốc
            XmlElement rootTeach = docTeach.DocumentElement;
            // Lấy danh sách học phần đã có trong xml
            XmlNode nodeDS = rootTeach.SelectSingleNode("DSHocPhan");
            XmlNodeList Nodes_BaiGiang = rootTeach.SelectNodes("DSHocPhan/HocPhan");

            // Lấy danh sách trong bảng
            DataTable bangMonhoc = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
            for (int i = 0; i < bangMonhoc.Rows.Count; i++)
            { 
                // mã trong xml
                String maXML="";
                try {
                    // đã có
                    maXML = Nodes_BaiGiang.Item(i).ChildNodes[0].InnerText;
                    }
                catch { // không có
                      // Tạo node mới

                        XmlNode newNode = docTeach.CreateElement("HocPhan");
                        String mamon = bangMonhoc.Rows[i]["MaHP"].ToString();
                        String tenmon = bangMonhoc.Rows[i]["TenHP"].ToString();
                        String muctieu = bangMonhoc.Rows[i]["MucTieuHP"].ToString();
                        newNode.InnerXml =
                        "<id>"+mamon+"</id>" +
                        "<ThongTin>" +
                           "<MaHP>"+mamon+"</MaHP>" +
                           "<TenHP>"+tenmon+"</TenHP>" +
                           "<HocPhan_HocTruoc></HocPhan_HocTruoc>" +
                           "<KhoiLuong></KhoiLuong>" +
                           "<DaoTaoTrinhDo> </DaoTaoTrinhDo>" +
                           "<MucTieu>"+ muctieu+" </MucTieu>" +
                           "<NoiDungVanTat></NoiDungVanTat>" +
                        "</ThongTin>" +
                        "<DanhGiaKetQua>" +
                        "</DanhGiaKetQua>" +
                        "<TaiLieuThamKhao>" +
                        "</TaiLieuThamKhao>" +
                        "<DeCuongHocPhan></DeCuongHocPhan>" +
                        "<BaiGiang>" +
                        "</BaiGiang>" +
                        "<ChinhSachKhac>" +
                        "chinhsachkhac.htm" +
                        " </ChinhSachKhac>" +
                        "<DSThongBao>" +
                        "</DSThongBao>";

                        // thêm vào sau
                        nodeDS.AppendChild(newNode);
                    }

            }

            docTeach.Save(ProjectFolder + @"\data\teaching.xml");


            //----------------------------------------
        
        
        
        }

        void CapNhatSangFileChiTietMonHoc()
        {
            //----- thêm môn học vào file teaching.xml
            // Đọc file XML
            XmlTextReader readerTeach = new XmlTextReader(ProjectFolder + @"\data\teaching.xml");
            XmlDocument docTeach = new XmlDocument();
            docTeach.Load(readerTeach);
            readerTeach.Close();
            // Lấy gốc
            XmlElement rootTeach = docTeach.DocumentElement;
            // Lấy danh sách học phần đã có trong xml
            XmlNode nodeDS = rootTeach.SelectSingleNode("DSHocPhan");
           

            // Lấy danh sách trong bảng
            DataTable bangMonhoc = CongCu.GetContentAsDataTable(dataGridViewX_DSHP);
            for (int i = 0; i < bangMonhoc.Rows.Count; i++)
            {
                String maTrongBang = bangMonhoc.Rows[i]["MaHP"].ToString();
                String mamon = bangMonhoc.Rows[i]["MaHP"].ToString();
                String tenmon = bangMonhoc.Rows[i]["TenHP"].ToString();
                String muctieu = bangMonhoc.Rows[i]["MucTieuHP"].ToString();
                XmlNode Node_BaiGiang = rootTeach.SelectSingleNode("DSHocPhan/HocPhan[id='" + maTrongBang +"']");
                if (Node_BaiGiang == null) // Nếu chưa có trong file teachingXML
                {// Tạo node mới
                    XmlNode newNode = docTeach.CreateElement("HocPhan");
                   
                    newNode.InnerXml =
                    "<id>" + mamon + "</id>" +
                    "<ThongTin>" +
                       "<MaHP>" + mamon + "</MaHP>" +
                       "<TenHP>" + tenmon + "</TenHP>" +
                       "<HocPhan_HocTruoc></HocPhan_HocTruoc>" +
                       "<KhoiLuong></KhoiLuong>" +
                       "<DaoTaoTrinhDo> </DaoTaoTrinhDo>" +
                       "<MucTieu>" + muctieu + " </MucTieu>" +
                       "<NoiDungVanTat></NoiDungVanTat>" +
                    "</ThongTin>" +
                    "<DanhGiaKetQua>" +
                    "</DanhGiaKetQua>" +
                    "<TaiLieuThamKhao>" +
                    "</TaiLieuThamKhao>" +
                    "<DeCuongHocPhan></DeCuongHocPhan>" +
                    "<BaiGiang>" +
                    "</BaiGiang>" +
                    "<ChinhSachKhac>" +
                    "chinhsachkhac.htm" +
                    " </ChinhSachKhac>" +
                    "<DSThongBao>" +
                    "</DSThongBao>";
                    // thêm vào sau
                    nodeDS.AppendChild(newNode);
                }
                else 
                { // Cập nhật node
                    XmlNode updateNode = Node_BaiGiang;
                    updateNode.ChildNodes[1].ChildNodes[0].InnerText = mamon;
                    updateNode.ChildNodes[1].ChildNodes[1].InnerText = tenmon;
                    updateNode.ChildNodes[1].ChildNodes[5].InnerText = muctieu;
                    nodeDS.ReplaceChild(updateNode, Node_BaiGiang);
                }

            }

            // Duyệt ngược để xử lý trường hợp bị xóa ở file danh sách, nhưng ở file chi tiết vẫn còn
            List<String> dsMaMon_Bang = new List<String>();
            for (int i = 0; i < bangMonhoc.Rows.Count; i++)
                dsMaMon_Bang.Add(bangMonhoc.Rows[i]["MaHP"].ToString()); 
              
            foreach (XmlNode no in nodeDS)
            {
                String mamonhoc = no.ChildNodes[0].InnerText;
                if (!dsMaMon_Bang.Contains(mamonhoc))
                {
                    nodeDS.RemoveChild(no); // Xóa node
                    // xóa thư mục lưu trữ
                    String thumuc = ProjectFolder+@"\courses\"+mamonhoc;
                    if (Directory.Exists(thumuc)) Directory.Delete(thumuc,true);
                }
            }

            docTeach.Save(ProjectFolder + @"\data\teaching.xml");


            //----------------------------------------



        }

        private void btnCacHocPhanXuatWeb_Click(object sender, EventArgs e)
        {
            XuatWeb.XuatDSBaiGiang(fullXMLFile, fullHTMLFile);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Files (*.jpg)|PNG Files (*.png)|*.png|*.jpg|GIF Files (*.gif)|*.gif";
            DialogResult rs = openFileDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                String file = openFileDialog1.FileName;

                pictureBox1.ImageLocation = file;
            }
        }
    }
}
