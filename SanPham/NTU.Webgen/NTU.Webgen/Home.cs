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
    public partial class Home : UserControl
    {
        String homeXMLFile;// = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\Home.xml";
        String gioithieuHTMLFile;// = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\Home.html";
        String subgioithieuHTMLFile;// = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\SubHome.htm";
        String pubHTMLFolder;// = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/UserChoices/Mau0";
        String ProjectFolder;// = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0";
        String avatar;
        int idMau;
        String htmlAvatarFile;
    
        public Home()
        {
            InitializeComponent();
            htmlEditor1.setButtonVisible("tsbNew");
            htmlEditor1.setButtonVisible("tsbOpen");
            htmlEditor1.setButtonVisible("tsbSave");
            htmlEditor1.setButtonVisible("tsbPrint");
            htmlEditor1.setButtonVisible("tsbPreview");
            htmlEditor1.setButtonVisible("tsbFind");
            htmlEditor1.setButtonVisible("tsbAbout");
            htmlEditor1.setButtonVisible("tsbRemoveFormat");
        }

        public Home(String ProjectFolder, int idMau)
        {
          
            InitializeComponent();
            this.ProjectFolder = ProjectFolder;
            this.homeXMLFile = ProjectFolder+"\\data\\index.xml";
            this.gioithieuHTMLFile = ProjectFolder + "\\index.html";
            this.subgioithieuHTMLFile = ProjectFolder + "\\subindex.htm";
            this.pubHTMLFolder = ProjectFolder.Replace("\\","/");
            this.idMau = idMau;
            link2Index.Text = gioithieuHTMLFile;

            htmlEditor1.setButtonVisible("tsbNew");
            htmlEditor1.setButtonVisible("tsbOpen");
            htmlEditor1.setButtonVisible("tsbSave");
            htmlEditor1.setButtonVisible("tsbPrint");
            htmlEditor1.setButtonVisible("tsbPreview");
            htmlEditor1.setButtonVisible("tsbFind");
            htmlEditor1.setButtonVisible("tsbAbout");
            htmlEditor1.setButtonVisible("tsbRemoveFormat");
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            DialogResult rs = openFileDialog1.ShowDialog();
            if (rs == DialogResult.OK)
            {
                String file =openFileDialog1.FileName;
                int k = file.LastIndexOf('\\');
                htmlAvatarFile = file.Substring(k);
                avatar = ProjectFolder + @"\assets\images" + htmlAvatarFile;
                System.IO.File.Copy(file, avatar, true);
                pictureBox1.ImageLocation = avatar;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             
                XmlTextReader reader = new XmlTextReader(homeXMLFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                //Select the cd node with the matching title
                XmlNode oldCd;
                XmlElement root = doc.DocumentElement;
                oldCd = root.SelectSingleNode("/root/Home");

                String anh = pictureBox1.ImageLocation;
                if (anh != null)
                    anh = anh.Substring(anh.LastIndexOf("\\") + 1);
                else
                    anh = "noAvatar.jpg";
                XmlElement newCd = doc.CreateElement("Home");
                newCd.InnerXml = "<id>1</id>" +
                        	"<HoTen>"+ txtHoTen.Text.Trim() + "</HoTen>" +
                            "<TrinhDo> " + txtTrinhDo.Text.Trim() + " </TrinhDo>" +
                            "<Khoa>" + txtKhoa.Text.Trim() + " </Khoa>" +
                            "<BoMon>" + txtBoMon.Text.Trim() + " </BoMon>" +
                            "<CacMonHocDamNhan>" + txtMonHoc.Text.Trim() + " </CacMonHocDamNhan>" +
                            "<DinhHuongNghienCuu>" + txtDinhHuong.Text.Trim() + " </DinhHuongNghienCuu>" +
                            "<DiaChi>" + txtDiaChi.Text.Trim() + "</DiaChi>" +
                            "<Email>" + txtEmail.Text.Trim() + "</Email>" +
                            "<SoDienThoai>" + txtSDT.Text.Trim() + "</SoDienThoai>" +
                            "<FB>" + txtFB.Text.Trim() + "</FB>" +
                            "<Web>" + txtWeb.Text.Trim() + "</Web>" +
                            "<Others>subindex.htm</Others>" + "<Avatar>" + anh + "</Avatar>";

                root.ReplaceChild(newCd, oldCd);
                CongCu.RenameFile(subgioithieuHTMLFile, subgioithieuHTMLFile+".bak");
                htmlEditor1.save(subgioithieuHTMLFile,ProjectFolder);
                //save the output to a file
                doc.Save(homeXMLFile);
                MessageBox.Show("Đã lưu xong", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
      
        }
        public void EnableTextBox(bool b)
        {
            txtWeb.Enabled = b;
            txtTrinhDo.Enabled = b;
            txtSDT.Enabled = b;
            txtKhoa.Enabled = b;
            txtHoTen.Enabled = b;
            txtFB.Enabled = b;
            txtEmail.Enabled = b;
            txtDiaChi.Enabled = b;
            txtBoMon.Enabled = b;
            pictureBox1.Enabled = b;
            txtMonHoc.Enabled = b;
            txtDinhHuong.Enabled = b;
            htmlEditor1.Enabled = b;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableTextBox(true);
          //  isSua = true;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadXML2Form();
        }

        private void LoadXML2Form() {
            XmlTextReader reader = new XmlTextReader(homeXMLFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNode data = root.SelectSingleNode("Home[id='1']");
            try
            {
                txtHoTen.Text = data.ChildNodes[1].InnerText.Trim();
                txtTrinhDo.Text = data.ChildNodes[2].InnerText.Trim();
                txtKhoa.Text = data.ChildNodes[3].InnerText.Trim();
                txtBoMon.Text = data.ChildNodes[4].InnerText.Trim();
                txtMonHoc.Text = data.ChildNodes[5].InnerText.Trim();
                txtDinhHuong.Text = data.ChildNodes[6].InnerText.Trim();
                txtDiaChi.Text = data.ChildNodes[7].InnerText.Trim();
                txtEmail.Text = data.ChildNodes[8].InnerText.Trim();
                txtSDT.Text = data.ChildNodes[9].InnerText.Trim();
                txtFB.Text = data.ChildNodes[10].InnerText.Trim();
                txtWeb.Text = data.ChildNodes[11].InnerText.Trim();
                String fileAnh = ProjectFolder + @"\assets\images\"+ data.ChildNodes[13].InnerText.Trim();
                
                if (File.Exists(fileAnh)) 
                {
                    pictureBox1.ImageLocation = fileAnh;
                }
               
                htmlEditor1.BodyInnerHTML = CongCu.ReadHTMLFile(subgioithieuHTMLFile);
            }
            catch
            {
                txtHoTen.Text = "";
                txtTrinhDo.Text = "";
                txtKhoa.Text = "";
                txtBoMon.Text = "";
                txtMonHoc.Text = "";
                txtDinhHuong.Text = "";
                txtDiaChi.Text = "";
                txtEmail.Text = "";
                txtSDT.Text = "";
                txtFB.Text = "";
                txtWeb.Text = "";
                pictureBox1.ImageLocation = "";
                htmlEditor1.BodyInnerHTML = "";// CongCu.ReadHTMLFile(subgioithieuHTMLFile);
            }
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
        //    String noiDungMoi = "";
        //    String tempFile = pictureBox1.ImageLocation;
        //    int k = tempFile.LastIndexOf('\\');
        //    String htmlAvatarFile1 ="assets/images/" +tempFile.Substring(k+1);

        //    StringBuilder result = new StringBuilder();
        //    result.Append("<div class='col-sm-3'>");
        //    result.Append("<h3><center><img src='" + htmlAvatarFile1 + "' width=90% height=196px></center></h3></div><div class='col-sm-5'><h3>Thông tin chung</h3>");
        //    result.Append("<b>"+txtTrinhDo.Text + ".</b> " + txtHoTen.Text + "<br>");
        //    result.Append("<b>Đơn vị: </b>" + txtKhoa.Text + "<br>");
        //    result.Append("<b>Tổ/Bộ môn: </b>" + txtBoMon.Text + "<br>");
        //    result.Append("<b>Các môn học đảm nhận: </b><p>" + txtMonHoc.Text + "</p>");
        //    result.Append("<b>Định hướng nghiên cứu chính: </b><p>" + txtDinhHuong.Text + "</p></div><div class='col-sm-4'><h3> Thông tin liên hệ </h3>");
        //    result.Append("<b>Địa chỉ:</b> " + txtDiaChi.Text + "<br>");
        //    result.Append("<b>Email: </b>" + txtEmail.Text + "<br>");
        //    result.Append("<b>Số điện thoại:</b> " + txtSDT.Text + "<br>");
        //    result.Append("<b>Mạng xã hội: </b>" + txtFB.Text + "<br>");
        //    result.Append("<b>Website: </b> <a href=\"http://" + txtWeb.Text + "\">"  + txtWeb.Text + "</a><br></div><div class='col-md-12' style='padding-top:10px'> <h3>Thông tin khác</h3>");
        //    result.Append(CongCu.ReadHTMLFile(subgioithieuHTMLFile)+"</div>");
        //    CongCu.AddMetaInfors(gioithieuHTMLFile, "author", txtHoTen.Text);
        //   // CongCu.AddMetaInfors(gioithieuHTMLFile, "keyword", txtMonHoc.Text);
        //    CongCu.AddMetaInfors(gioithieuHTMLFile, "keywords", txtMonHoc.Text);
        //    CongCu.AddMetaInfors(gioithieuHTMLFile, "descriptions", txtDinhHuong.Text);
        //    //CongCu.AddMetaInfors(gioithieuHTMLFile, "thucainua", txtTrinhDo.Text);
        //    CongCu.ReplaceContent(gioithieuHTMLFile, "gioithieu", result.ToString());
        //    // Dành cho mẫu 4
        //    CongCu.ReplaceContent(gioithieuHTMLFile, "anhTrai", "<img src=\""+ htmlAvatarFile1 +"\" width=100% height=186px>");
        //    CongCu.ReplaceContent(gioithieuHTMLFile, "tenTrai", "website của "+txtHoTen.Text);

        //    CongCu.ReplaceContent(ProjectFolder+"\\publications.html", "anhTrai", "<img src=\"" + htmlAvatarFile1 + "\" width=100% height=186px>");
        //    CongCu.ReplaceContent(ProjectFolder + "\\publications.html", "tenTrai", "website của " + txtHoTen.Text);

        //    CongCu.ReplaceContent(ProjectFolder + "\\calendar.html", "anhTrai", "<img src=\"" + htmlAvatarFile1 + "\" width=100% height=186px>");
        //    CongCu.ReplaceContent(ProjectFolder + "\\calendar.html", "tenTrai", "website của " + txtHoTen.Text);

        //    // Thêm tiêu đề
        //    CongCu.ReplaceTite(gioithieuHTMLFile,"NTU."+txtHoTen.Text+"-Giới thiệu");
        //    //----------------------------
        //    MessageBox.Show("Đã xuất thành công sang trang web: \n" + gioithieuHTMLFile, "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //
            XuatWeb.XuatIndex(homeXMLFile, gioithieuHTMLFile, subgioithieuHTMLFile, ProjectFolder, idMau);
            MessageBox.Show("Xuất xong. Trang của bạn là" + (char)10 + (char)13 + gioithieuHTMLFile, "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadXML2Form();
            EnableTextBox(false);
           // isSua = false;
        }

        private void link2Index_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CongCu.gotoSite(gioithieuHTMLFile);
        }

       
    }
}
