using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTU.Webgen
{
    public partial class Login : Form
    {
        String ProjectFolder = @"F:\LocalRepository\DeTai2016\TestMau1";
        public Login()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        class DangNhap {
            public String tenDN;
            public String mk;
            public String TenDayDu;
        }

        // Đăng nhập
        private void button2_Click(object sender, EventArgs e)
        {
            String ftpurl = "ftp://" + txtHost.Text; // e.g. ftp://serverip/foldername/foldername
            String ftpurlDB = "ftp://" + txtHost.Text + "/zzzABCXYZMNKOPQ/acc.txt"; // e.g. ftp://serverip/foldername/foldername
            String ftpusername = "NTUWebgen"; // e.g. username
            String ftppassword = "Maicuongtho123"; // e.g. password
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpurlDB);
            ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);
            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            try
            {
                Stream ftpStream = ftp.GetResponse().GetResponseStream();
                StreamReader r = new StreamReader(ftpStream);
                // r.r
                List<DangNhap> dsMK = new List<DangNhap>();
                bool dangnhapOK = false;
                while (true)
                {
                    try
                    {
                        String tam = r.ReadLine();
                        if (tam == "") break;
                        DangNhap k = new DangNhap();
                        int pt1 = tam.IndexOf("|");
                        int pt2 = tam.LastIndexOf("|");
                        k.tenDN = tam.Substring(0, pt1);
                        k.mk = tam.Substring(pt2 + 1);
                        k.TenDayDu = tam.Substring(pt1 + 1, pt2 - pt1);

                        if ((k.tenDN == txtTenDN.Text) && (k.mk == txtMK.Text))
                        {
                            dangnhapOK = true;

                            new UploadDownload(k.tenDN, ftpusername, ftppassword, ftpurl, ProjectFolder).ShowDialog();
                            this.Close();
                            break;
                        }
                    }
                    catch (Exception ex) { break; }
                }
                if (!dangnhapOK) MessageBox.Show("Đăng nhập sai", "NTUWebgen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                r.Close();
                ftpStream.Close();
            }
            catch (Exception eee)
            {
                MessageBox.Show("Lỗi khi kết nối máy chủ","NTUWebgen", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        // Tạo file mật khẩu
        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
