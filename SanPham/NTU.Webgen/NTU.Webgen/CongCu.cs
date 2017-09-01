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
using iTextSharp.text;
using iTextSharp.text.pdf;
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
           try
           {
               u.HinhAnh = anh.Substring(anh.IndexOf("assets")).Replace("\\", "/");
           }
           catch { u.HinhAnh = ""; }
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

       public static void Replace(String htmlFilePath, String oldText, String newText)
       {
           StreamReader sr = new StreamReader(htmlFilePath);
           String strHTMLPage = sr.ReadToEnd();
           sr.Close();
           strHTMLPage.Replace(oldText, newText);
           StreamWriter sw = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8);
           sw.Write(strHTMLPage);
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

        public static DataTable GetContentAsDataTable(DevComponents.DotNetBar.Controls.DataGridViewX dgv, bool IgnoreHideColumns = false)
        {
            try
            {
                if (dgv.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (IgnoreHideColumns & !col.Visible) continue;
                    if (col.Name == string.Empty) continue;
                    dtSource.Columns.Add(col.Name);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                if (dtSource.Columns.Count == 0) return null;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch { return null; }
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
        // Cho công bố khoa học
        public static void XML2GridbyNode1(String xmlFilePath,String NodeL1,  DevComponents.DotNetBar.Controls.DataGridViewX grdVx)
        {
            XmlTextReader reader = new XmlTextReader(xmlFilePath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_CongTac = root.SelectNodes("/root/"+NodeL1);
            XmlNode[] qtCongTac = Nodes_CongTac.Cast<XmlNode>().ToArray();

            DataTable dtCongTac = CongCu.GetContentAsDataTable(grdVx);
            System.Data.DataSet dataSet = new System.Data.DataSet();

            foreach (XmlNode k in qtCongTac)
            {
                int soCot = k.ChildNodes.Count; 
                Object[] value = new Object[soCot];
                for (int j = 0; j < soCot; j++) value[j] = k.ChildNodes[j].InnerText;
                dtCongTac.LoadDataRow(value, LoadOption.Upsert);
            }
            try { grdVx.DataSource = dtCongTac; }
            catch { grdVx.DataSource = null; }
           
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
                MessageBox.Show("Vui lòng đợi khởi động trình duyệt web");
            
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
            return result;
        }

        public static StringBuilder XML2HTML_TapChi1(String xmlPubFile, String ProjectFoler)
        {
            XmlTextReader reader = new XmlTextReader(xmlPubFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("DSBaiBao/BaiBao");
            
           // List<XElement> ordered = Nodes_Level1.Cast<XElement>().ToList<XElement>();

            
            // Sap xep lai theo Nam
          //  var e = XElement.Load(new XmlNodeReader(doc));


            //  XElement root = XElement.Parse(xml);

            //List<XElement> ordered = e.Elements("BaiBao")
            //                         .OrderByDescending(element => (int)element.Element("Ngay"))
            //                        .ToList();
            //e.RemoveAll();
            //e.Add(ordered);

            StringBuilder result = new StringBuilder();
            result.Append("<b>Bài báo</b><ul>");
            foreach (XmlNode xe in Nodes_Level1)
            {
                String tacgia = xe.ChildNodes[1].InnerText;
                String nam = xe.ChildNodes[2].InnerText;
                String tieudebaibao = xe.ChildNodes[3].InnerText;
                String tentapchi = xe.ChildNodes[4].InnerText;
                String tap = xe.ChildNodes[5].InnerText;
                String so = xe.ChildNodes[6].InnerText;
                String trang = xe.ChildNodes[7].InnerText;
                bool vietnam = true;
                if ( xe.ChildNodes[8].InnerText != "Tiếng Việt") vietnam = false;
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
                //String dinhkem = xe.ChildNodes[9].InnerText;
                //int vt = dinhkem.IndexOf("data\\DinhKem", 0);
                //String pdf = dinhkem.Substring(vt).Replace("\\", "/");
                String pdf = ProjectFoler + @"\data\dinhkem\" + xe.ChildNodes[9].InnerText;
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<span class='citation_title'>" + tieudebaibao + "</span>, ");
                result.Append(tentapchi + ", " + tap + ", " + so + ", " + trang + ", <a href=\"" + pdf + "\"> pdf</a></li>");

            }
            result.Append("</ul>");
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
        public static StringBuilder XML2HTML_BaoCaoHoiThao1(String pubXML, String ProjectFoler)
        {
            XmlTextReader reader = new XmlTextReader(pubXML);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("DSBaoCao1/BaoCao1");

            StringBuilder result = new StringBuilder();
            result.Append("<b>Báo cáo hội thảo (có ấn phẩm)</b><ul>");
            foreach (XmlNode xe in Nodes_Level1)
            {
                String tacgia = xe.ChildNodes[1].InnerText;
                String nam = xe.ChildNodes[2].InnerText;
                String tenbaocao = xe.ChildNodes[3].InnerText;
                String tenHoiThao = xe.ChildNodes[4].InnerText;
                String thoigiandiadiem = xe.ChildNodes[5].InnerText;
                String nhaxuatban = xe.ChildNodes[6].InnerText;
                String noixuatban = xe.ChildNodes[7].InnerText;
                String trang = xe.ChildNodes[8].InnerText;
                bool vietnam = true;
                if (xe.ChildNodes[9].InnerText != "Tiếng Việt") vietnam = false;
                if (vietnam)
                {
                    nhaxuatban = "Nhà xuất bản " + nhaxuatban;
                    trang = "Trang " + trang;
                }
                else
                {
                    nhaxuatban = "Publisher " + nhaxuatban;
                    trang = "Page " + trang;
                }
                //String dinhkem = xe.ChildNodes[10].InnerText;
                //int vt = dinhkem.IndexOf("data\\DinhKem", 0);
                //String pdf = dinhkem.Substring(vt).Replace("\\", "/");
                String pdf = ProjectFoler + @"\data\dinhkem\" + xe.ChildNodes[10].InnerText;
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

        public static StringBuilder XML2HTML_BaoCaoHoiThao0(String xmlPubFile)
        {
            XmlTextReader reader = new XmlTextReader(xmlPubFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("DSBaoCao0/BaoCao0" );
 

            StringBuilder result = new StringBuilder();
            result.Append("<b>Báo cáo hội thảo (KHÔNG có ấn phẩm)</b><ul>");
            foreach (XmlNode xe in Nodes_Level1)
            {
                String tacgia = xe.ChildNodes[1].InnerText;
                String nam = xe.ChildNodes[2].InnerText;
                String tenbaocao = xe.ChildNodes[3].InnerText;
                String tenHoiThao = xe.ChildNodes[4].InnerText;
                String thoigiandiadiem = xe.ChildNodes[5].InnerText;
                String url = xe.ChildNodes[7].InnerText;
                bool vietnam = true;
                if (xe.ChildNodes[6].InnerText != "Tiếng Việt") vietnam = false;
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

        public static StringBuilder XML2HTML_Sach(String xmlpubFile)
        {
            XmlTextReader reader = new XmlTextReader(xmlpubFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlElement root = doc.DocumentElement;
            XmlNodeList Nodes_Level1 = root.SelectNodes("DSSach/Sach");
            StringBuilder result = new StringBuilder();
            result.Append("<b>Sách</b><ul>");
            foreach (XmlNode xe in Nodes_Level1)
            {
                String tacgia = xe.ChildNodes[1].InnerText;
                String nam = xe.ChildNodes[2].InnerText;
                String tensach = xe.ChildNodes[3].InnerText;
                String nhaxb = xe.ChildNodes[4].InnerText;
                String noixb = xe.ChildNodes[5].InnerText;
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<i>" + tensach + "</i>, ");
                result.Append(nhaxb + ", " + noixb + "</li>");
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
            XmlNodeList Nodes_Level1 = root.SelectNodes("DSLienKet/LienKet");
            XmlNode[] nodeLK = Nodes_Level1.Cast<XmlNode>().ToArray();
           
            StringBuilder result = new StringBuilder();
            result.Append("<ul style=\"padding-left: 21px;\">");
            foreach (XmlNode xe in nodeLK)
            {
                String tenLienKet = xe.ChildNodes[1].InnerText;
                String diaChiLienKet =System.Web.HttpUtility.UrlDecode(   xe.ChildNodes[2].InnerText);

                result.Append("<li style=\"padding-bottom: 10px;\">" + tenLienKet + " : <br><a href=\"" + diaChiLienKet + "\"> " + diaChiLienKet + "  </a></li>");
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
       public static void XuatIndex(String xmlIndex, String htmlIndex, String htmlSubIndex, String ProjectFolder, int idMau)
       { 
           //ĐỌc file HTML
           String htmlContent = CongCu.ReadHTMLFile(htmlIndex);
           // Đọc file XML
           XmlTextReader reader = new XmlTextReader(xmlIndex);
           XmlDocument doc = new XmlDocument();
           doc.Load(reader);
           reader.Close();
           // Lấy gốc
           XmlElement root = doc.DocumentElement;
           XmlNode Nodes_Home = root.SelectSingleNode("Home");

           String id = Nodes_Home.ChildNodes[0].InnerText.Trim();
           String hoten = Nodes_Home.ChildNodes[1].InnerText.Trim();
           String trinhdo = Nodes_Home.ChildNodes[2].InnerText.Trim();
           String khoa = Nodes_Home.ChildNodes[3].InnerText.Trim();
           String bomon = Nodes_Home.ChildNodes[4].InnerText.Trim();
           String cacmonday = Nodes_Home.ChildNodes[5].InnerText.Trim();
           String dinhhuongnghiencuu = Nodes_Home.ChildNodes[6].InnerText.Trim();
           String diachi = Nodes_Home.ChildNodes[7].InnerText.Trim();
           String email = Nodes_Home.ChildNodes[8].InnerText.Trim();
           String sdt = Nodes_Home.ChildNodes[9].InnerText.Trim();
           String fb= Nodes_Home.ChildNodes[10].InnerText.Trim();
           String web = Nodes_Home.ChildNodes[11].InnerText.Trim();
           String av = Nodes_Home.ChildNodes[13].InnerText.Trim();
           String avarta;
           if (av!="noAvatar.jpg")
               avarta =@"assets/images/"+av;
           else avarta = @"data/" + av;

           StringBuilder result = new StringBuilder();
           result.Append("<div class='col-sm-3'>");
           result.Append("<h3><center><img src='" + avarta + "' width=90% height=196px></center></h3></div><div class='col-sm-5'><h3>Thông tin chung</h3>");
           result.Append("<b>" + trinhdo + ".</b> " + hoten + "<br>");
           result.Append("<b>Đơn vị: </b>" + khoa + "<br>");
           result.Append("<b>Tổ/Bộ môn: </b>" + bomon+ "<br>");
           result.Append("<b>Các môn học đảm nhận: </b><p>" + cacmonday+ "</p>");
           result.Append("<b>Định hướng nghiên cứu chính: </b><p>" + dinhhuongnghiencuu+ "</p></div><div class='col-sm-4'><h3> Thông tin liên hệ </h3>");
           result.Append("<b>Địa chỉ:</b> " + diachi + "<br>");
           result.Append("<b>Email: </b>" + email + "<br>");
           result.Append("<b>Số điện thoại:</b> " +sdt+ "<br>");
           result.Append("<b>Mạng xã hội: </b>" + fb+ "<br>");
           result.Append("<b>Website: </b> <a href=\"http://" + web + "\">" + web + "</a><br></div><div class='col-md-12' style='padding-top:10px'> <h3>Thông tin khác</h3>");
           result.Append(CongCu.ReadHTMLFile(htmlSubIndex) + "</div>");
           CongCu.AddMetaInfors(htmlIndex, "author", hoten);
           CongCu.AddMetaInfors(htmlIndex, "keywords", cacmonday);
           CongCu.AddMetaInfors(htmlIndex, "descriptions", dinhhuongnghiencuu);
           CongCu.ReplaceContent(htmlIndex, "gioithieu", result.ToString());
           // Dành cho mẫu 4
           if( (idMau == 4)|| (idMau==9)|| (idMau==10))
           {
               CongCu.ReplaceContent(htmlIndex, "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(htmlIndex, "tenTrai",trinhdo+ "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\publications.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\publications.html", "tenTrai", trinhdo + "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\calendar.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\calendar.html", "tenTrai", trinhdo + "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "tenTrai", trinhdo + "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\weblink.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\weblink.html", "tenTrai", trinhdo + "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\cv.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\cv.html", "tenTrai", trinhdo + "." + hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "anhTrai", "<img src=\"" + avarta + "\" width=100% height=186px>");
               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "tenTrai", trinhdo + "." + hoten);

               // Lục các thư mục trong thư mục course, để cập nhật ảnh avata cho cac bài gaigrn
               String[] dskhoahoc=Directory.GetDirectories(ProjectFolder + @"\courses");
               for (int i = 0; i < dskhoahoc.Length; i++)
               {
                   CongCu.ReplaceContent(dskhoahoc[i]+@"\index.html", "anhTrai", "<img src=\"../../" + avarta + "\" width=100% height=186px>");
                   CongCu.ReplaceContent(dskhoahoc[i] + @"\index.html", "anhTrai", "<img src=\"../../" + avarta + "\" width=100% height=186px>");
               }
           }
           // Dành cho mẫu 3,2
           if ((idMau == 3) || (idMau == 2) || (idMau == 8) || (idMau == 6) || (idMau == 5) || (idMau == 0) || (idMau == 1))
           {
               CongCu.ReplaceContent(htmlIndex, "nav3_ten", hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\publications.html", "nav3_ten",  hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\calendar.html", "nav3_ten",  hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "nav3_ten",  hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\weblink.html", "nav3_ten",  hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\cv.html", "nav3_ten",  hoten);

               CongCu.ReplaceContent(ProjectFolder + "\\teaching.html", "nav3_ten",  hoten);

               // Lục các thư mục trong thư mục course, để cập nhật ảnh avata cho cac bài gaigrn
               String[] dskhoahoc = Directory.GetDirectories(ProjectFolder + @"\courses");
               for (int i = 0; i < dskhoahoc.Length; i++)
               {
                   CongCu.ReplaceContent(dskhoahoc[i] + @"\index.html", "nav3_ten", hoten);
                   CongCu.ReplaceContent(dskhoahoc[i] + @"\index.html", "nav3_ten", hoten);
               }
           }
           // Thêm tiêu đề
           CongCu.ReplaceTite(htmlIndex, "Giới thiệu -"+hoten);
           //----------------------------
           MessageBox.Show("Đã xuất thành công sang trang web: \n" + htmlIndex, "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Information);
      
       }
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
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ten_ky", hoten);

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "gioitinh", gioitinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "ngaysinh", ngaysinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "noisinh", noisinh);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "quequan", quequan);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "dantoc", dantoc);

           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "hocvicaonhat", tenHV);
           htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "hocvicaonhat_ky", tenHV);
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
               qtCT.Append("<td><span>" + tu_den + "</span></td>");
               qtCT.Append("<td><span>" + dv + "</span></td>");
               qtCT.Append("<td><span>" + cv + "</span></td>");
           }
           qtCT.Append("</table>");
           CongCu.ReplaceContent(htmlCVFile, "QuaTrinhCongTac", qtCT.ToString());



           // Qua trinh NCKH
           //1. đề tài khoa học
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


               qtNCKH.Append("<td><span>" + STT + "</span></td>");
               qtNCKH.Append("<td><span>" + TenDeTai + "</span></td>");
               qtNCKH.Append("<td><span>" + NamBatDau + "/" + NamHoanThanh + "</span></td>");
               qtNCKH.Append("<td><span>" + DeTaiCap + "</span></td>");
               qtNCKH.Append("<td><span>" + TrachNhiem + "</span></td>");
           }
           qtNCKH.Append("</table>");
           CongCu.ReplaceContent(htmlCVFile, "QuaTrinhNCKH", qtNCKH.ToString());
           //2. Công bố khoa học

           XmlNodeList dsNodeCongBoKH = root.SelectNodes("/root/NghienCuuKhoaHoc/DSCongTrinhKhoaHoc/CongBo");
           XmlNode[] nodeCongBoKH = dsNodeCongBoKH.Cast<XmlNode>().ToArray();
           StringBuilder qtCongBoKhoaHoc = new StringBuilder();
           qtCongBoKhoaHoc.Append("<table width=\"424\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-left: 6px\">"); //bang
           qtCongBoKhoaHoc.Append("<tr><td><b>STT</b></td><td><b>Tên công trình</b></td><td><b>Năm công bố</b></td><td><b>Nơi công bố</b></td></tr>"); //bang

           foreach (XmlNode k in nodeCongBoKH)
           {
               qtCongBoKhoaHoc.Append("<tr>");
               String STT, TenCongTrinh, NamCongBo, NoiCongBo;
               STT = k.ChildNodes[0].InnerText.Trim();
               TenCongTrinh = k.ChildNodes[1].InnerText.Trim();
               NamCongBo = k.ChildNodes[2].InnerText.Trim();
               NoiCongBo = k.ChildNodes[3].InnerText.Trim();

               qtCongBoKhoaHoc.Append("<td><span>" + STT + "</span></td>");
               qtCongBoKhoaHoc.Append("<td><span>" + TenCongTrinh + "</span></td>");
               qtCongBoKhoaHoc.Append("<td><span>" + NamCongBo + "</span></td>");
               qtCongBoKhoaHoc.Append("<td><span>" + NoiCongBo + "</span></td>");
              
           }
           qtCongBoKhoaHoc.Append("</table>");
           CongCu.ReplaceContent(htmlCVFile, "QuaTrinhNCKH_CongBo", qtCongBoKhoaHoc.ToString());


           

           return true;
       }

       public static void HTMLToPdf(string htmlCVFile, string pdfCVFile)
       {
           Document document = new Document();
           PdfWriter.GetInstance(document, new FileStream(pdfCVFile, FileMode.Create));
           document.Open();
           String noiDungCVHTML = layNoiDungCV(htmlCVFile);
           iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
           iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
           hw.Parse(new StringReader(noiDungCVHTML.ToString()));
           document.Close();

       }

       public static string layNoiDungCV(String htmlCVFile)
       {
           String strtoFind = "<section id='NoiDungCV'>";
           String NoiDungHTML = CongCu.ReadHTMLFile(htmlCVFile);
           int intStartIndex = NoiDungHTML.IndexOf(strtoFind, 0) + strtoFind.Length;


           int intEndIndex = NoiDungHTML.LastIndexOf("</section>");

           return NoiDungHTML.Substring(intStartIndex, intEndIndex-intStartIndex);
           
       }


       public static bool XuatBaiGiang(String id, String xmlTeachFile, String htmlTeachHPFile)
       {
           // ĐỌc file HTML
           String htmlContent = CongCu.ReadHTMLFile(htmlTeachHPFile);
           // Đọc file XML
           XmlTextReader reader = new XmlTextReader(xmlTeachFile);
           XmlDocument doc = new XmlDocument();
           doc.Load(reader);
           reader.Close();
           // Lấy gốc
           XmlElement root = doc.DocumentElement;
           XmlNode Nodes_BaiGiang = root.SelectSingleNode("/root/DSHocPhan/HocPhan[id='"+id+"']");

           #region Thông tin học phần
          
               XmlNode nodeThongTin = Nodes_BaiGiang.SelectSingleNode("ThongTin");
               String maHP =nodeThongTin.ChildNodes[0].InnerText;
               String tenHP = nodeThongTin.ChildNodes[1].InnerText;
               String hocTruoc = nodeThongTin.ChildNodes[2].InnerText;
               String tc = nodeThongTin.ChildNodes[3].InnerText;
               String trinhdo = nodeThongTin.ChildNodes[4].InnerText;
               String muctieu = nodeThongTin.ChildNodes[5].InnerText;
               String noidung = nodeThongTin.ChildNodes[6].InnerText;
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "MAHOCPHAN", maHP);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "TENHOCPHAN", tenHP);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "tenHocPhan", tenHP);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "maHocPhan", maHP);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "khoiluong", tc);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "hpHocTruoc", hocTruoc);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "muctieuHP", muctieu);
               htmlContent = ThayNoiDungTrongTheSpan(htmlContent, "noiDungVanTat", noidung);
               // thay xong, đóng file
               CongCu.RenameFile(htmlTeachHPFile, htmlTeachHPFile + ".bak");
               StreamWriter sw = new System.IO.StreamWriter(htmlTeachHPFile, false, Encoding.UTF8);
               sw.Write(htmlContent);
               sw.Close();
               // Thêm các thông tin meta
               CongCu.ReplaceTite(htmlTeachHPFile,tenHP);
               CongCu.AddMetaInfors(htmlTeachHPFile, "keywords", tenHP);
               CongCu.AddMetaInfors(htmlTeachHPFile, "descriptions", noidung); 
           #endregion
           #region Đánh giá kết quả
               XmlNodeList dsDanhGia = Nodes_BaiGiang.SelectNodes("DanhGiaKetQua/HinhThuc");
               XmlNode[] nodeDanhGia = dsDanhGia.Cast<XmlNode>().ToArray();
               StringBuilder danhgiaHocTap = new StringBuilder();
               danhgiaHocTap.Append("<ul>");
               foreach (XmlNode k in nodeDanhGia)
               {
                   String tenDG = k.ChildNodes[1].InnerText;
                   String mota = k.ChildNodes[2].InnerText;
                   String trongso = k.ChildNodes[3].InnerText;
                   String hinhthuclambai= k.ChildNodes[4].InnerText;
                   danhgiaHocTap.Append("<li><b>" + tenDG + "(" + trongso + "):</b>"+mota + ", " +hinhthuclambai+"</li>");
               }
               danhgiaHocTap.Append("</ul>");
               CongCu.ReplaceContent(htmlTeachHPFile, "DanhGiaKetQua", danhgiaHocTap.ToString());
           #endregion


               #region Tài liệu tham khảo
               XmlNodeList dsTaiLieu = Nodes_BaiGiang.SelectNodes("TaiLieuThamKhao/TaiLieu");
               XmlNode[] nodeTaiLieu = dsTaiLieu.Cast<XmlNode>().ToArray();
               StringBuilder tltk = new StringBuilder();
               foreach (XmlNode k in nodeTaiLieu)
               {
                   String STT = k.ChildNodes[0].InnerText;
                   String tenTaiLieu = k.ChildNodes[1].InnerText;
                   String tenTG = k.ChildNodes[2].InnerText;
                   String nhaXB = k.ChildNodes[3].InnerText;
                   String namXB = k.ChildNodes[4].InnerText;
                   String diachiKhaiThac = k.ChildNodes[5].InnerText;
                   
                   String mucdichSuDung = k.ChildNodes[6].InnerText;
                   if (diachiKhaiThac.Contains("%2f"))
                   {
                       diachiKhaiThac = System.Web.HttpUtility.UrlDecode(diachiKhaiThac);
                       tltk.Append("[" + STT + "], " + tenTG + ", <i>" + tenTaiLieu + "</i>, " + namXB + ", " + nhaXB + "  <a href=\"" + diachiKhaiThac + "\">, [ Xem ]</a><br>");
                   }
                   else
                       tltk.Append("[" + STT + "], " + tenTG + ", <i>" + tenTaiLieu + "</i>, " + namXB + ", " + nhaXB + " , ["+diachiKhaiThac+"] <br>");
                   
               }

               CongCu.ReplaceContent(htmlTeachHPFile, "TaiLieuThamKhao", tltk.ToString());
               #endregion

               #region Đề cương học phần
                    XmlNode nodeDeCuong = Nodes_BaiGiang.SelectSingleNode("/root/DSHocPhan/HocPhan/DeCuongHocPhan");
                    String decuong = "<a href=\""+nodeDeCuong.InnerText+"\">Xem tại đây</a>";
                    htmlContent = ThayNoiDungTrongTheSpan(CongCu.ReadHTMLFile(htmlTeachHPFile), "DeCuongHP", decuong);
                    // thay xong, đóng file
                    CongCu.RenameFile(htmlTeachHPFile, htmlTeachHPFile + ".bak");
                    StreamWriter sw1 = new System.IO.StreamWriter(htmlTeachHPFile, false, Encoding.UTF8);
                    sw1.Write(htmlContent);
                    sw1.Close();    
               #endregion
               #region Bài giảng
                     XmlNodeList dsChuong = Nodes_BaiGiang.SelectNodes("BaiGiang/Chuong");
                     XmlNode[] nodeChuong = dsChuong.Cast<XmlNode>().ToArray();
                     StringBuilder chuong = new StringBuilder();

                     chuong.Append("<table>");
                     foreach (XmlNode k in nodeChuong)
                     {
                         String STT = k.ChildNodes[0].InnerText;
                         String tenChuong = k.ChildNodes[1].InnerText;
                         String mota = k.ChildNodes[2].InnerText;
                         String url = k.ChildNodes[3].InnerText;
                         chuong.Append("<tr><td>"+STT+"</td><td>"+tenChuong+"</td><td>"+mota+"</td><td> <a href=\""+ url+"\">Tải về </td></tr>");
                     }
                     chuong.Append("</table>");
                     CongCu.ReplaceContent(htmlTeachHPFile, "BaiGiang", chuong.ToString());
               #endregion

               #region Chính sách khác

               #endregion


               MessageBox.Show("Xuất xong bài giảng", "NTUWebgen", MessageBoxButtons.OK,MessageBoxIcon.Information);
           return true;
       }

       public static void XuatDSBaiGiang(String xmlDSHP,String htmlTeachFile)
       { 
            // Lập danh sách các Tab theo danh mục bài giảng
           // ĐỌc file HTML
           String htmlContent = CongCu.ReadHTMLFile(htmlTeachFile);
           // Đọc file XML
           XmlTextReader readerDSHP = new XmlTextReader(xmlDSHP);
           XmlDocument docDSHP = new XmlDocument();
           docDSHP.Load(readerDSHP);
           readerDSHP.Close();
           // Lấy gốc
           XmlElement rootDSHP = docDSHP.DocumentElement;
          // XmlNode Nodes_BaiGiang = ;
           XmlNodeList Nodes_HP = rootDSHP.SelectNodes("DSHocPhan/HocPhan");
           XmlNode[] hp = Nodes_HP.Cast<XmlNode>().ToArray();

           StringBuilder strTabLink= new StringBuilder();
           strTabLink.Append("<table border=0>");
           int i=1;
           foreach (XmlNode k in hp)
           {
               strTabLink.Append("<tr><TD rowspan=2>"+i.ToString()+
                                  "<img src='courses/" + k.ChildNodes[1].InnerText + "/" + k.ChildNodes[4].InnerText +"' width=100px height=120px>"+
                                  "</TD><td  valign=\"top\"><b>Học phần</b>: <a href='courses/"+k.ChildNodes[1].InnerText+"/index.html'>"+
                                  k.ChildNodes[2].InnerText+" (Mã HP: "+k.ChildNodes[1].InnerText+")</a></td></tr>");
               strTabLink.Append("<tr><td  valign=\"top\"><b>Nội dung tóm tắt</b>: " + k.ChildNodes[3].InnerText + "</td></tr>");
               i++;
               strTabLink.Append("<tr><td></td></tr>");
               strTabLink.Append("<tr><td></td></tr>");
           }
           strTabLink.Append("</table>");
           
           CongCu.ReplaceContent(htmlTeachFile, "tabCacHocPhan", strTabLink.ToString());
                
       }
       public static bool XuatCongBo(String xmlPub, String htmlPub)
       {
           

           return true;
       
       }
   
   }
}
