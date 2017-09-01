using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Net;

namespace NTU.Webgen
{
    public partial class UploadDownload : DevComponents.DotNetBar.OfficeForm
    {
        String thumuccanhan;
        String ftpusername, ftppassword, ftpurl;
        String ProjectFolder;
        public UploadDownload()
        {
            InitializeComponent();
        }

        public UploadDownload(String tenDN, String _ftpusername, String _ftppassword, String _ftpurl, String _ProjectFolder)
        {
            InitializeComponent();
            thumuccanhan = tenDN;
            ftpusername =_ftpusername;
            ftppassword = _ftppassword;
            ftpurl = _ftpurl;
            ProjectFolder = _ProjectFolder;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            recursiveDirectory(ProjectFolder, ftpurl+"/"+thumuccanhan);

        }

        private void recursiveDirectory(string dirPath, string uploadPath)
        {
            string[] files = Directory.GetFiles(dirPath, "*.*");
            string[] subDirs = Directory.GetDirectories(dirPath);

            // Upload cac file trong thu muc dirPath
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                string ftpfullpath = uploadPath + "/" + filename;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(file);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }

            //string currentDir = ftpurl;
            foreach (string subDir in subDirs)
            {
                String fulluploadPath = uploadPath + "/" + subDir.Substring(subDir.LastIndexOf("\\") + 1);
                try
                {
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(fulluploadPath);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpusername, ftppassword);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                    //Up file trong thu muc
                    recursiveDirectory(subDir, fulluploadPath);

                }
                catch (Exception ex)
                {
                    recursiveDirectory(subDir, fulluploadPath);
                }
            }
        }


        private void btnDown_Click(object sender, EventArgs e)
        {

        }

    }
}