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
namespace NTU.Webgen
{
    class CongCu
    {
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
         RenameFile(htmlFilePath, htmlFilePath + ".bak");

        // intStartIndex = strHTMLPage.IndexOf("<div class=\"sectionContent\">", 0) + 28;
         String strtoFind = "<section id=\""+ tagClass+ "\">";
         intStartIndex = strHTMLPage.IndexOf(strtoFind, 0) + strtoFind.Length;
        //intStartIndex = strHTMLPage.IndexOf("<p>", intStartIndex) + 3;

         intEndIndex = strHTMLPage.IndexOf("</section>", intStartIndex);

         strNewHTMLPage = strHTMLPage.Substring(0, intStartIndex);
         strNewHTMLPage += strNewDataToBeInserted;
         strNewHTMLPage += strHTMLPage.Substring(intEndIndex);


         StreamWriter sw = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8);
         sw.Write(strNewHTMLPage);
         sw.Close();
        
        
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

            usercontrol.Top = top;
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
            result.Append("<h2>Bài báo</h2><ul>");
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
                    trang = "Trang" + trang;
                }
                else
                {
                    tap = "Vol " + tap;
                    so = "Issue " + so;
                    trang = "P " + trang;
                }

                result.Append("<li>"+tacgia + ", (" + nam + "), " + "<i>" + tieudebaibao + "</i>, ");
                result.Append(tentapchi + ", " + tap + ", " + so + ", " + trang + "</li>");
                


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
            result.Append("<h2>Báo cáo hội thảo (có ấn phẩm)</h2><ul>");
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
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<i>" + tenbaocao + "</i>, ");
                result.Append(tenHoiThao + ", " + thoigiandiadiem + ", " + nhaxuatban+ ", " + noixuatban+ ", " + trang + "</li>");
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
            result.Append("<h2>Báo cáo hội thảo (KHÔNG có ấn phẩm)</h2><ul>");
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
                result.Append("<li>" + tacgia + ", (" + nam + "), " + "<i>" + tenbaocao + "</i>, ");
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
            result.Append("<h2>Sách</h2><ul>");
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
    }


}
