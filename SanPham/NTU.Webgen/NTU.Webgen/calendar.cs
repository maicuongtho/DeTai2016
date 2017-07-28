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
    public partial class calendar : UserControl
    {
        bool isSua = false;
        String carXMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\calendar.xml";
        String carHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\calendar.html";
        String subCarHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\SubCalendar.htm";
        String ggCarHTMLFile = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0\GgCalendar.htm";
        String pubHTMLFolder = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/UserChoices/Mau0";
        String ProjectFolder = @"F:\LocalRepository\DeTai2016\SanPham\NTU.Webgen\NTU.Webgen\bin\Debug\UserChoices\Mau0";
        public calendar()
        {
            InitializeComponent();
            isSua = false;
            txtURLLich.Enabled = false;
            htmlEditor1.Enabled = false;
            htmlEditor1.setButtonVisible("tsbNew");
            htmlEditor1.setButtonVisible("tsbOpen");
            htmlEditor1.setButtonVisible("tsbSave");
            htmlEditor1.setButtonVisible("tsbPrint");
            htmlEditor1.setButtonVisible("tsbPreview");
            htmlEditor1.setButtonVisible("tsbFind");
            htmlEditor1.setButtonVisible("tsbAbout");
            htmlEditor1.setButtonVisible("tsbRemoveFormat");
        }

        public calendar(String ProjectFolder)
        {
            InitializeComponent();
            isSua = false;

            this.ProjectFolder = ProjectFolder;
            this.carXMLFile = ProjectFolder + "\\data\\calendar.xml";
            this.carHTMLFile = ProjectFolder + "\\calendar.html";
            this.pubHTMLFolder = ProjectFolder.Replace("\\", "/");
            this.ggCarHTMLFile = ProjectFolder + "\\ggCalendar.htm";
            this.subCarHTMLFile = ProjectFolder + "\\subCalendar.htm";
            txtURLLich.Enabled = false;
            htmlEditor1.Enabled = false;
            htmlEditor1.setButtonVisible("tsbNew");
            htmlEditor1.setButtonVisible("tsbOpen");
            htmlEditor1.setButtonVisible("tsbSave");
            htmlEditor1.setButtonVisible("tsbPrint");
            htmlEditor1.setButtonVisible("tsbPreview");
            htmlEditor1.setButtonVisible("tsbFind");
            htmlEditor1.setButtonVisible("tsbAbout");
            htmlEditor1.setButtonVisible("tsbRemoveFormat");
        }


        private void btnHelp_Click(object sender, EventArgs e)
        {
            new VideoPlayer().ShowDialog();
        }

        private void calendar_Load(object sender, EventArgs e)
        {
            txtURLLich.Text = CongCu.ReadHTMLFile(ggCarHTMLFile);
            htmlEditor1.BodyInnerHTML = CongCu.ReadHTMLFile(subCarHTMLFile);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            isSua = true;
            txtURLLich.Enabled = true;
            htmlEditor1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                CongCu.RenameFile(ggCarHTMLFile, ggCarHTMLFile + ".bak");
                //System.IO.File.Create(ggCarHTMLFile);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(ggCarHTMLFile);
                writer.Write(txtURLLich.Text);
                writer.Close();


             
                CongCu.RenameFile(subCarHTMLFile, subCarHTMLFile + ".bak");
                htmlEditor1.save(subCarHTMLFile);
                //save the output to a file
              
                isSua = false;
                txtURLLich.Enabled = false;
                htmlEditor1.Enabled = false;

            }
        }

        private void btnXuatWeb_Click(object sender, EventArgs e)
        {
            String noiDungMoi = "";
            StringBuilder result = new StringBuilder();
            
            
           
            result.Append("<div id='lichgoogle'>");
            result.Append("<h2>LỊCH GOOGLE</h2>");
            result.Append("<iframe src=\"");
            result.Append(CongCu.ReadHTMLFile(ggCarHTMLFile).Replace("<BODY scroll=auto>","").Replace("</BODY>",""));
            result.Append("\" style=\"border: 0\" width=\"100%\" height=\"600\" frameborder=\"0\" scrolling=\"no\"></iframe></div>");

            result.Append("<div id='lich0'><h2> Thông tin thêm</h2> ");
            result.Append(CongCu.ReadHTMLFile(subCarHTMLFile) + "</div>");
        //     CongCu.AddMetaInfors(gioithieuHTMLFile, "author", txtHoTen.Text);
            // CongCu.AddMetaInfors(gioithieuHTMLFile, "keyword", txtMonHoc.Text);
         //   CongCu.AddMetaInfors(gioithieuHTMLFile, "keywords", txtMonHoc.Text);
         //   CongCu.AddMetaInfors(gioithieuHTMLFile, "descriptions", txtDinhHuong.Text);
         //   CongCu.AddMetaInfors(gioithieuHTMLFile, "thucainua", txtTrinhDo.Text);
            CongCu.ReplaceContent(carHTMLFile, "lich", result.ToString());


            // Dành cho mẫu 4
            UserInfo u = CongCu.getUserInfo(ProjectFolder + "\\data\\index.xml");
            CongCu.ReplaceContent(carHTMLFile, "anhTrai", "<img src=\"" + u.HinhAnh + "\" width=100% height=186px>");
            CongCu.ReplaceContent(carHTMLFile, "tenTrai", "website của " + u.HoTen);
            // Thêm tiêu đề
            CongCu.ReplaceTite(carHTMLFile, "NTU. " + u.HoTen + "-Báo cáo hội thảo");

            MessageBox.Show("Đã xuất thành công sang trang web: \n" + carHTMLFile, "Thông báo");
        }
    }
}
