using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Data;
namespace NTU.Webgen
{
   public class UserInfo {
        String hoten;
        String hinhanh;
        public UserInfo() {hoten="";hinhanh="";}
        public UserInfo(String hoten, String hinhanh) {this.hoten=hoten;this.hinhanh=hinhanh;}
        public String HoTen {
            get {return hoten;}
            set {hoten = value;}
        }
       public String HinhAnh {
        get {return hinhanh;} set {hinhanh=value;}
       }
   }
   public  class CongCu
    {
       public static UserInfo getUserInfo(String homeXMLFile)
       {
           XmlTextReader reader = new XmlTextReader(homeXMLFile);
           XmlDocument doc = new XmlDocument();
           doc.Load(reader);
           reader.Close();

           //Select the cd node with the matching title
           XmlNode oldCd;
           XmlElement root = doc.DocumentElement;
           oldCd = root.SelectSingleNode("/root/Home[id='1']");
           UserInfo u = new UserInfo();
           u.HoTen = oldCd.ChildNodes[1].InnerText;
           String anh = oldCd.ChildNodes[13].InnerText;

           u.HinhAnh = anh.Substring(anh.IndexOf("assets")).Replace("\\","/");
           return u;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="newTitle"></param>
       public static void ReplaceTite(String htmlFilePath, String newTitle)
       {
           StreamReader sr = new StreamReader(htmlFilePath);
           String strHTMLPage = sr.ReadToEnd();
           sr.Close();
           int intStartIndex = strHTMLPage.IndexOf("<title>",0);
           int intEndIndex = strHTMLPage.IndexOf("</title>", intStartIndex);
           String strNewHTMLPage = strHTMLPage.Substring(0, intStartIndex+7);
           strNewHTMLPage += newTitle;
           strNewHTMLPage += strHTMLPage.Substring(intEndIndex);
           StreamWriter sw = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8);
           sw.Write(strNewHTMLPage);
           sw.Close();

       }
        /// <summary>
        /// Thêm thông tin meta vào phần Head, tiện cho SEO
        /// </summary>
        /// <param name="htmlFilePath"></param>
        /// <param name="metaName"></param>
        /// <param name="metaContent"></param>
        public static void AddMetaInfors(String htmlFilePath, String metaName, String metaContent)
        {
            // Đọc file
            StreamReader sr = new StreamReader(htmlFilePath);
            String strHTMLPage = sr.ReadToEnd();
            sr.Close();
            // Tìm vị trí để chèn thông tin meta
            int intStartIndex = strHTMLPage.IndexOf("</title>", 0) + 8;
            int intEndIndex = intStartIndex;
            
            //File html mới
            StringBuilder newHtmlPage = new StringBuilder();
            newHtmlPage.Append(strHTMLPage.Substring(0,intStartIndex));
            // kiêm tra thông tin meta đã tồn tại chưa
            String metaInfo = "<meta name=\""+ metaName+"\"";
            int intMeTaPos = strHTMLPage.IndexOf(metaInfo);
            if (metaName.Equals("keywords")) metaContent.Replace(';', ',');
            if (intMeTaPos <= 0) // nếu chưa tồn tại
            {
                newHtmlPage.Append("<meta name=\"" + metaName + "\"");
                newHtmlPage.Append(" content=\"" + metaContent.Trim() + "\">");
            }
            else // đã có, cập nhật nội dung
            {
                newHtmlPage.Append(strHTMLPage.Substring(intMeTaPos, metaInfo.Length));
                newHtmlPage.Append(" content=\"" + metaContent.Trim() + "\">");
                //Xóa nội dung cũ
                int k = strHTMLPage.IndexOf(" content=\"", intMeTaPos)+10;
                int k1 = strHTMLPage.IndexOf("\">", k);
                strHTMLPage.Remove(k, k1 - k);
                intEndIndex = k1+2;

            }

            newHtmlPage.Append(strHTMLPage.Substring(intEndIndex));
            RenameFile(htmlFilePath, htmlFilePath + ".bak.meta");
            StreamWriter sw = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8);
            sw.Write(newHtmlPage);
            sw.Close();
        }
        public static void ReplaceContent(String htmlFilePath, String tagClass, String strNewDataToBeInserted)
        { 
        
         string strHTMLPage = "";
         string strNewHTMLPage = "";
         int intStartIndex = 0;
         int intEndIndex = 0;
      //   string strNewDataToBeInserted = "Assumes you loaded this string with the data you want inserted";

         StreamReader sr = new StreamReader(htmlFilePath);
         strHTMLPage = sr.ReadToEnd();
         sr.Close();
   

        // intStartIndex = strHTMLPage.IndexOf("<div class=\"sectionContent\">", 0) + 28;
         String strtoFind = "<section id=\""+ tagClass+ "\">";
         intStartIndex = strHTMLPage.IndexOf(strtoFind, 0) + strtoFind.Length;
        //intStartIndex = strHTMLPage.IndexOf("<p>", intStartIndex) + 3;

         intEndIndex = strHTMLPage.IndexOf("</section>", intStartIndex);

         strNewHTMLPage = strHTMLPage.Substring(0, intStartIndex);
         strNewHTMLPage += strNewDataToBeInserted;
         strNewHTMLPage += strHTMLPage.Substring(intEndIndex);

         if (intStartIndex != 0)
         {
             RenameFile(htmlFilePath, htmlFilePath + ".bak");
             StreamWriter sw = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8);
             sw.Write(strNewHTMLPage);
             sw.Close();
         }
        
        
        }
        public static void RenameFile(String oldFilePath, String newFilePath) {
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            File.Move(oldFilePath, newFilePath);
        }

        public static void XML2Grid(String xmlFilePath,  DevComponents.DotNetBar.Controls.DataGridViewX grdVx) {
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.ReadXml(xmlFilePath);
            try
            {
                grdVx.DataSource = dataSet.Tables[0];
            }
            catch (Exception e) {
                grdVx.DataSource = null;
            }
        }

        public static void XML2_LienKetGrid(String xmlFilePath, DevComponents.DotNetBar.Controls.DataGridViewX grdVx)
        {
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.ReadXml(xmlFilePath);
            DataTable dtTarget = dataSet.Tables[0].Clone();
            foreach (DataRow dr in dataSet.Tables[0].Rows) {

                dtTarget.LoadDataRow(new[]
                                  {
                                      dr[0], // default value
                                      dr[1],
                                      System.Web.HttpUtility.UrlDecode(dr[2].ToString())
                                  }, LoadOption.Upsert);
            }



            try
            {
                grdVx.DataSource = dtTarget;
            }
            catch (Exception e)
            {
                grdVx.DataSource = null;
            }
        }
        public static void gotoSite(string url)
        {
           
                System.Diagnostics.Process.Start(url);
            
        }


     

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                
                file.CopyTo(temppath, true);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
      

        public static void AddTab(String tabName, String tabTitle, SuperTabControl tabControl, System.Windows.Forms.Control usercontrol, bool center, int top) {


         
            SuperTabItem newTab = tabControl.CreateTab(tabTitle);
            newTab.Name = tabName;
            SuperTabControlPanel panel = (SuperTabControlPanel)newTab.AttachedControl;
            if (center)
            {
                panel.Width = SystemInformation.VirtualScreen.Width;

                usercontrol.Left = (panel.ClientSize.Width - usercontrol.Width) / 2;
            }
            usercontrol.Left = 5;
            usercontrol.Top = top;
            //if (dock.Equals("Fill")) { 
            usercontrol.Dock = DockStyle.Fill;
            //}
            panel.Controls.Add(usercontrol);
         
            tabControl.SelectedTab= newTab;

        }

        public static StringBuilder XML2HTML_TapChi(String tapchi_xmlFile, String nodeName)
        {
            XmlTextReader reader = new XmlTextReader(tapchi_xmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("/DSBaiBao/"+nodeName);

            // Sap xep lai theo Nam
            var e = XElement.Load(new XmlNodeReader(doc));


          //  XElement root = XElement.Parse(xml);

            List<XElement> ordered = e.Elements("BaiBao")
                                     .OrderByDescending(element => (int)element.Element("Ngay"))
                                    .ToList();
            e.RemoveAll();
            e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<b>Bài báo</b><ul>");
            foreach (XElement xe in ordered)
            {
                String tacgia = xe.Element("TacGia").Value.ToString();
                String nam = xe.Element("Ngay").Value.ToString();
                String tieudebaibao = xe.Element("TieuDeBaiBao").Value.ToString();
                String tentapchi = xe.Element("TenTapChi").Value.ToString();
                String tap = xe.Element("Tap").Value.ToString();
                String so = xe.Element("So").Value.ToString();
                String trang = xe.Element("Trang").Value.ToString();
                bool vietnam = true;
                if (xe.Element("NgonNgu").Value.ToString() != "Tiếng Việt") vietnam = false;
                if (vietnam)
                {
                    tap = "Tập " + tap;
                    so = "Số " + so;
                    trang = "Trang " + trang;
                }
                else
                {
                    tap = "Vol " + tap;
                    so = "Issue " + so;
                    trang = "P " + trang;
                }
                String dinhkem = xe.Element("DinhKem").Value.ToString();
                int vt = dinhkem.IndexOf("data\\DinhKem", 0);
                String pdf = dinhkem.Substring(vt).Replace("\\","/");
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<span class='citation_title'>" + tieudebaibao + "</span>, ");
                result.Append(tentapchi + ", " + tap + ", " + so + ", " + trang + ", <a href=\"" + pdf+ "\"> pdf</a></li>");
                


            }
            result.Append("</ul>");

            //            // ------
            //String s = "";
            //StringBuilder result = new StringBuilder();
            //result.Append("<ol>");
            //foreach (XmlNode n in Nodes_Level1)   // Danh sachs node
            //{   
            //    result.Append("<li>");
            //    // Duyet cac node Level 2 (i=0/id; 1/Tacgia 2/Nam)

            //    for (int i = 1; i < n.ChildNodes.Count; i++ )
            //    {
            //        XmlNode l2 = n.ChildNodes[i];
            //        if (i==2)  result.Append(" (" + l2.InnerText + "), ");
            //        else if (i==3)  result.Append("<i>" + l2.InnerText + " </i>, "); 
            //        else if (i==n.ChildNodes.Count-1)  result.Append(l2.InnerText);
            //        else result.Append(l2.InnerText + ", ");
            //    }
            //    result.Append("</li>");
           
            //}
            //result.Append("</ol>");
            return result;
        }



        public static StringBuilder XML2HTML_BaoCaoHoiThao(String baocao1_xmlFile, String nodeName)
        {
            XmlTextReader reader = new XmlTextReader(baocao1_xmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("/DSBaoCao1/" + nodeName);

            // Sap xep lai theo Nam
            var e = XElement.Load(new XmlNodeReader(doc));


            //  XElement root = XElement.Parse(xml);

            List<XElement> ordered = e.Elements("BaoCao1")
                                     .OrderByDescending(element => (int)element.Element("Nam"))
                                    .ToList();
            e.RemoveAll();
            e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<b>Báo cáo hội thảo (có ấn phẩm)</b><ul>");
            foreach (XElement xe in ordered)
            {
                String tacgia = xe.Element("TacGia").Value.ToString();
                String nam = xe.Element("Nam").Value.ToString();
                String tenbaocao= xe.Element("TenBaoCao").Value.ToString();
                String tenHoiThao = xe.Element("TenHoiThao").Value.ToString();
                String thoigiandiadiem = xe.Element("ThoiGianDiaDiem").Value.ToString();
                String nhaxuatban = xe.Element("NhaXuatBan").Value.ToString();
                String noixuatban = xe.Element("NoiXuatBan").Value.ToString();
                String trang = xe.Element("Trang").Value.ToString();
                bool vietnam = true;
                if (xe.Element("NgonNgu").Value.ToString() != "Tiếng Việt") vietnam = false;
                if (vietnam)
                {
                    nhaxuatban = "Nhà xuất bản " + nhaxuatban;
                    trang = "Trang " + trang;
                }
                else {
                    nhaxuatban = "Publisher " + nhaxuatban;
                    trang = "Page " + trang;    
                }
                String dinhkem = xe.Element("DinhKem").Value.ToString();
                int vt = dinhkem.IndexOf("data\\DinhKem", 0);
                String pdf = dinhkem.Substring(vt).Replace("\\", "/");
                
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<span class='citation_title'>" + tenbaocao + "</span>, ");
                result.Append(tenHoiThao + ", " + thoigiandiadiem + ", " + nhaxuatban + ", " + noixuatban + ", " + trang + ", <a href=\"" + pdf + "\"> pdf</a></li>");
            }
            result.Append("</ul>");

         
            return result;
        }



        public static StringBuilder XML2HTML_BaoCaoHoiThao0(String baocao0_xmlFile, String nodeName)
        {
            XmlTextReader reader = new XmlTextReader(baocao0_xmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("/DSBaoCao0/" + nodeName);

            // Sap xep lai theo Nam
            var e = XElement.Load(new XmlNodeReader(doc));


            //  XElement root = XElement.Parse(xml);

            List<XElement> ordered = e.Elements("BaoCao0")
                                     .OrderByDescending(element => (int)element.Element("Nam"))
                                    .ToList();
            e.RemoveAll();
            e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<b>Báo cáo hội thảo (KHÔNG có ấn phẩm)</b><ul>");
            foreach (XElement xe in ordered)
            {
                String tacgia = xe.Element("TacGia").Value.ToString();
                String nam = xe.Element("Nam").Value.ToString();
                String tenbaocao = xe.Element("TenBaoCao").Value.ToString();
                String tenHoiThao = xe.Element("TenHoiThao").Value.ToString();
                String thoigiandiadiem = xe.Element("ThoiGianDiaDiem").Value.ToString();
                String url = xe.Element("URL").Value.ToString();
                bool vietnam = true;
                if (xe.Element("NgonNgu").Value.ToString() != "Tiếng Việt") vietnam = false;
                if (vietnam)
                {
                    url = "Liên kết " + url;
                }
                else
                {
                    url = "Link " + url;
                }
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<span class='citation_title'>" + tenbaocao + "</span>, ");
                result.Append(tenHoiThao + ", " + thoigiandiadiem + ", <a href='" + url + "'>" + url + "</a></li>");
            }
            result.Append("</ul>");


            return result;
        }


        public static StringBuilder XML2HTML_Sach(String sach_xmlFile, String nodeName)
        {
            XmlTextReader reader = new XmlTextReader(sach_xmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("/DSSach/" + nodeName);

            // Sap xep lai theo Nam
            var e = XElement.Load(new XmlNodeReader(doc));


            //  XElement root = XElement.Parse(xml);

            List<XElement> ordered = e.Elements("Sach")
                                     .OrderByDescending(element => (int)element.Element("NamXB"))
                                    .ToList();
            e.RemoveAll();
            e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<b>Sách</b><ul>");
            foreach (XElement xe in ordered)
            {
                String tacgia = xe.Element("TacGia").Value.ToString();
                String nam = xe.Element("NamXB").Value.ToString();
                String tensach = xe.Element("TenSach").Value.ToString();
                String nhaxb = xe.Element("NhaXB").Value.ToString();
                String noixb = xe.Element("NoiXB").Value.ToString();
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<i>" + tensach + "</i>, ");
                result.Append(nhaxb + ", " + noixb +"</li>");
            }
            result.Append("</ul>");


            return result;
        }


        public static StringBuilder XML2HTML_LieKet(String xmlFile)
        {
            XmlTextReader reader = new XmlTextReader(xmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("/DSSach/LienKet");

         
            var e = XElement.Load(new XmlNodeReader(doc));


            //  XElement root = XElement.Parse(xml);

            List<XElement> ordered = e.Elements("LienKet").ToList();
            e.RemoveAll();
            e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<ul>");
            foreach (XElement xe in ordered)
            {
                String tenLienKet = xe.Element("TieuDe").Value.ToString();
                String diaChiLienKet = xe.Element("DiaChi").Value.ToString();
                 
                result.Append("<li>" + tenLienKet + " : <a href=\"" + diaChiLienKet + "\"> "+ diaChiLienKet+"  </a></li>");
            }
            result.Append("</ul>");


            return result;
        }

         

        public static String ReadHTMLFile(String htmlFilePath)
        {
            StreamReader sr = new StreamReader(htmlFilePath);
            String strHTMLPage = sr.ReadToEnd();
            sr.Close();
            return strHTMLPage;
        }


        public static void Text2File(String str, String filename)
        {
            StreamWriter outputFile = new StreamWriter(filename);
            outputFile.WriteLine(str);
            outputFile.Close();
        }
        
       

    }

   public class XuatWeb {
       public static bool XuatLienKet(String xmlLienKetPath, String htmlLienKetPath)
       {
           String noiDungMoi = CongCu.XML2HTML_LieKet(xmlLienKetPath).ToString();
           CongCu.ReplaceContent(htmlLienKetPath, "LienKet", noiDungMoi);
           MessageBox.Show("Đã xuất thành công sang trang web: \n" + htmlLienKetPath, "Thông báo");
           return true; 
       }
       public static String ThayNoiDungTrongTheSpan(String NoiDungFileCu, String idThe, String noiDungThe) {

           String strtoFind = "<span id=\"" + idThe + "\">";
           int intStartIndex = NoiDungFileCu.IndexOf(strtoFind, 0) + strtoFind.Length;
           int intEndIndex = NoiDungFileCu.IndexOf("</span>", intStartIndex);

           String strNewHTMLPage = NoiDungFileCu.Substring(0, intStartIndex);
           strNewHTMLPage += noiDungThe;
           strNewHTMLPage += NoiDungFileCu.Substring(intEndIndex);
           return strNewHTMLPage; 
          
       }
       public static bool XuatCV(String xmlCVFile, String htmlCVFile)
       {
           // ĐỌc file HTML
           String htmlContent = CongCu.ReadHTMLFile(htmlCVFile);
           // Đọc file XML
           XmlTextReader reader = new XmlTextReader(xmlCVFile);
           XmlDocument doc = new XmlDocument();
           doc.Load(reader);
           reader.Close();
           // Lấy gốc
           XmlElement root = doc.DocumentElement;
           //Lây thông tin chung
           XmlNode Nodes_Level1_ThongTinChung = root.SelectSingleNode("/root/TuThuat");
           
           String hoten = Nodes_Level1_ThongTinChung.ChildNodes[0].InnerText.Trim();
           String gioitinh = Nodes_Level1_ThongTinChung.ChildNodes[1].InnerText.Trim();
           String ngaysinh = Nodes_Level1_ThongTinChung.ChildNodes[2].InnerText.Trim();
           String noisinh = Nodes_Level1_ThongTinChung.ChildNodes[3].InnerText.Trim();
           String quequan = Nodes_Level1_ThongTinChung.ChildNodes[4].InnerText.Trim();
           String dantoc = Nodes_Level1_ThongTinChung.ChildNodes[5].InnerText.Trim();
           
           XmlNode nodeHocVi = Nodes_Level1_ThongTinChung.SelectSingleNode("HocViCaoNhat");
           String tenHV = nodeHocVi.ChildNodes[0].InnerText.Trim();
           String namnhanHV = nodeHocVi.ChildNodes[1].InnerText.Trim();
           String nuocnhanHV = nodeHocVi.ChildNodes[2].InnerText.Trim();
           
           XmlNode nodeChucDanh = Nodes_Level1_ThongTinChung.SelectSingleNode("ChucDanhKhoaHoc");
           String tenChucDanh = nodeChucDanh.ChildNodes[0].InnerText.Trim();
           String nambonhiemChucDanh = nodeChucDanh.ChildNodes[1].InnerText.Trim();

           XmlNode nodeDonViCongTac = Nodes_Level1_ThongTinChung.SelectSingleNode("DonViCongTac");
           String tenDonVi = nodeDonViCongTac.ChildNodes[0].InnerText.Trim();
           String chucvu = nodeDonViCongTac.ChildNodes[1].InnerText.Trim();

           XmlNode nodeLienLac = Nodes_Level1_ThongTinChung.SelectSingleNode("LienLac");
           String noiOHienNay = nodeLienLac.ChildNodes[0].InnerText.Trim();
           String diachi = nodeLienLac.ChildNodes[1].InnerText.Trim();
           String dtCoQuan = nodeLienLac.ChildNodes[2].InnerText.Trim();
           String dtNR = nodeLienLac.ChildNodes[3].InnerText.Trim();
           String didong = nodeLienLac.ChildNodes[4].InnerText.Trim();
           String fax = nodeLienLac.ChildNodes[5].InnerText.Trim();
           String email = nodeLienLac.ChildNodes[6].InnerText.Trim();

           DateTime d = DateTime.Now;
           String ngay = d.Day.ToString();
           String thang = d.Month.ToString();
           String nam = d.Year.ToString();
           //-----hết lấy thông tin chung
           // Đổ thông tin chung ra HTML

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ngay", ngay);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "thang", thang);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "nam", nam);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ten", hoten);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "gioitinh", gioitinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ngaysinh", ngaysinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "noisinh", noisinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "quequan", quequan);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dantoc", dantoc);

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "hocvicaonhat", tenHV);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "nam_nuoc_nhan_hv", tenHV+","+namnhanHV);

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "chucdanh", tenChucDanh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "nambonhiem", nambonhiemChucDanh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "chucvu", chucvu);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dvcongtac", tenDonVi);

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "cho_o", noiOHienNay);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dcLienLac", diachi);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dtCQ",    dtCoQuan+ "     ");
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dtNR", dtNR + "     ");
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dtDD", didong + "     ");
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "fax", fax);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "mail", email);
            

           // Đào tạo Dai hoc 1
           XmlNode DaiHoc1 = root.SelectSingleNode("/root/QuaTrinhDaoTao/DaiHoc1");
           String heDaoTao = DaiHoc1.ChildNodes[0].InnerText.Trim();
           String noiDaoTao = DaiHoc1.ChildNodes[1].InnerText.Trim();
           String nuocDaoTao = DaiHoc1.ChildNodes[2].InnerText.Trim();
           String chuyenganhDaoTao = DaiHoc1.ChildNodes[3].InnerText.Trim();
           String namTotNghiep = DaiHoc1.ChildNodes[4].InnerText.Trim();
           // Đào tạo Dai hoc 2
           XmlNode DaiHoc2 = root.SelectSingleNode("/root/QuaTrinhDaoTao/DaiHoc2");
           String chuyenganhDaoTao2 = DaiHoc2.ChildNodes[0].InnerText.Trim();
           String namTotNghiep2 = DaiHoc2.ChildNodes[1].InnerText.Trim();

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "hedaotao", heDaoTao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "noidaotao", noiDaoTao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "nuocdaotao", nuocDaoTao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "chuyennganhdaotao_daihoc", chuyenganhDaoTao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "namtotnghiep_daihoc", namTotNghiep);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "bang2", chuyenganhDaoTao2);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "namtotnghiep_daihoc2", namTotNghiep2);


           //Thạc sĩ
           XmlNode ThS = root.SelectSingleNode("/root/QuaTrinhDaoTao/ThacSi");
          
           String thacsi_noidaotao = ThS.ChildNodes[0].InnerText.Trim();
           String thacsi_nuocdaotao = ThS.ChildNodes[1].InnerText.Trim();
           String thacsi_chuyennganh = ThS.ChildNodes[2].InnerText.Trim();
           String thacsi_namcapbang = ThS.ChildNodes[3].InnerText.Trim();

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ThS_ChuyenNganh", thacsi_chuyennganh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ThS_NamCapBang", thacsi_namcapbang);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ThS_noidaotao", thacsi_noidaotao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ThS_nuocdaotao", thacsi_nuocdaotao);

           // Tiến sĩ

           //Thạc sĩ
           XmlNode TS = root.SelectSingleNode("/root/QuaTrinhDaoTao/TienSi");

           String ts_noidaotao = TS.ChildNodes[0].InnerText.Trim();
           String ts_nuocdaotao = TS.ChildNodes[1].InnerText.Trim();
           String ts_chuyennganh = TS.ChildNodes[2].InnerText.Trim();
           String ts_namcapbang = TS.ChildNodes[4].InnerText.Trim();
           String ts_luanan = TS.ChildNodes[5].InnerText.Trim();

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TS_ChuyenNganh", ts_chuyennganh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TS_NamCapBang", ts_namcapbang);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TS_NoiDaoTao", ts_noidaotao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TS_NuocDaoTao", ts_nuocdaotao);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TS_TenLuanAn", ts_luanan);

           // Ngoại ngữ
           XmlNode NN1 = root.SelectSingleNode("/root/QuaTrinhDaoTao/NgoaiNgu1");

           String tenNN1 = NN1.ChildNodes[1].InnerText.Trim();
           String levelNN1 = NN1.ChildNodes[2].InnerText.Trim();

           // Ngoại ngữ 2
           XmlNode NN2 = root.SelectSingleNode("/root/QuaTrinhDaoTao/NgoaiNgu2");
           String tenNN2 = NN2.ChildNodes[1].InnerText.Trim();
           String levelNN2 = NN2.ChildNodes[2].InnerText.Trim();
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "NN1", tenNN1);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "NN1_level", levelNN1);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "NN2", tenNN2);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "NN2_level", levelNN2);
           


           // Chứng chỉ
           var cc = XElement.Load(new XmlNodeReader(doc));
           XmlNodeList dsNodeTinHoc = root.SelectNodes("/root/QuaTrinhDaoTao/DSTinHoc/TinHoc");
           XmlNode[] nodeArray = dsNodeTinHoc.Cast<XmlNode>().ToArray();
           StringBuilder listCC = new StringBuilder();
           foreach (XmlNode n in dsNodeTinHoc)
           {
               String tenCC = n.ChildNodes[1].InnerText.Trim(); 
               listCC.Append(tenCC+";  ");
           }
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TinHoc", listCC.ToString());
           CongCu.RenameFile(htmlCVFile,htmlCVFile+".bak");
           StreamWriter sw = new System.IO.StreamWriter(htmlCVFile, false, Encoding.UTF8);
           sw.Write(htmlContent);
           sw.Close();
            

           // Quas trinh cong tac
           XmlNodeList dsNodeQTCT = root.SelectNodes("/root/DSQuaTrinhCongTac/QuaTrinhCongTac");
           XmlNode[] nodeCongTac = dsNodeQTCT.Cast<XmlNode>().ToArray();
           StringBuilder qtCT = new StringBuilder();
           qtCT.Append("<table width=\"424\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-left: 6px\">"); //bang
           qtCT.Append("<tr><td><b>Thời gian</b></td><td><b>Nơi công tác</b></td><td><b>Công việc đảm nhiệm</b></td></tr>"); //bang
          
           foreach (XmlNode c in nodeCongTac)
           {
               qtCT.Append("<tr>");
               String tu_den = c.ChildNodes[1].InnerText.Trim()+"-"+c.ChildNodes[2].InnerText.Trim();
               String dv=c.ChildNodes[3].InnerText.Trim();
               String cv=c.ChildNodes[4].InnerText.Trim();
               qtCT.Append("<td>"+tu_den+"</td>");
               qtCT.Append("<td>"+dv+"</td>");
               qtCT.Append("<td>"+cv+"</td>");
           }
           qtCT.Append("</table>");
           CongCu.ReplaceContent(htmlCVFile, "QuaTrinhCongTac", qtCT.ToString());



           // Qua trinh NCKH
           XmlNodeList dsNodeNCKH = root.SelectNodes("/root/NghienCuuKhoaHoc/DSDeTaiKH/DeTaiKH");
           XmlNode[] nodeNCKH = dsNodeNCKH.Cast<XmlNode>().ToArray();
           StringBuilder qtNCKH = new StringBuilder();
           qtNCKH.Append("<table width=\"424\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-left: 6px\">"); //bang
           qtNCKH.Append("<tr><td><b>STT</b></td><td><b>Tên đề tài</b></td><td><b>Năm bắt đầu/Năm hoàn thành</b></td><td><b>Đề tài cấp (NN, Bộ, ngành, trường)</b></td><td><b>Trách nhiệm tham gia trong đề tài</b></td></tr>"); //bang

           foreach (XmlNode k in nodeNCKH)
           {
               qtNCKH.Append("<tr>");
               String STT, TenDeTai, NamBatDau, NamHoanThanh, DeTaiCap, TrachNhiem;
               STT = k.ChildNodes[0].InnerText.Trim();
               TenDeTai = k.ChildNodes[1].InnerText.Trim();
               NamBatDau = k.ChildNodes[2].InnerText.Trim();
               NamHoanThanh = k.ChildNodes[3].InnerText.Trim();
               DeTaiCap = k.ChildNodes[4].InnerText.Trim();
               TrachNhiem = k.ChildNodes[5].InnerText.Trim();


               qtNCKH.Append("<td>" + STT + "</td>");
               qtNCKH.Append("<td>" + TenDeTai + "</td>");
               qtNCKH.Append("<td>" + NamBatDau + "/" + NamHoanThanh + "</td>");
               qtNCKH.Append("<td>" + DeTaiCap + "</td>");
               qtNCKH.Append("<td>" + TrachNhiem + "</td>");
           }
           qtNCKH.Append("</table>");

           CongCu.ReplaceContent(htmlCVFile, "QuaTrinhNCKH", qtNCKH.ToString());

           MessageBox.Show("OK");
           return true;
       }
    
   
   }
}
