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
            grdVx.DataSource = dataSet.Tables[0];
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

        public static StringBuilder XML2HTML_TapChi(String xmlFile, String nodeName)
        {
            XmlTextReader reader = new XmlTextReader(xmlFile);
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
            result.Append("<ol>");
            foreach (XElement xe in ordered)
            {
                String tacgia = xe.Element("TacGia").Value.ToString();
                String nam = xe.Element("Ngay").Value.ToString();
                String tieudebaibao = xe.Element("TieuDeBaiBao").Value.ToString();
                String tentapchi = xe.Element("TenTapChi").Value.ToString();
                String tap = xe.Element("Tap").Value.ToString();
                String so = xe.Element("So").Value.ToString();
                String trang = xe.Element("Trang").Value.ToString();
                result.Append("<li>"+tacgia + ", (" + nam + ")," + "<i>" + tieudebaibao + "</i>,");
                result.Append(tentapchi + ", " + tap + ", " + so + ", " + trang + "</li>");
            }
            result.Append("</ol>");

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
    }


}
