using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace NTU.Webgen
{
    public partial class ChonMau : UserControl
    {
        String TempPath;
        String webPath;
        String thumucGoc;
        String thumucChon;
      
        public ChonMau()
        {
            InitializeComponent();
          //  ReSizeGroup();
            thumucGoc = System.AppDomain.CurrentDomain.BaseDirectory;
            TempPath = thumucGoc+"Templates\\Mau0";
            webPath = TempPath.Replace("\\", "/");
        }

       

        private void linkLabel0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CongCu.gotoSite(webPath + "/publications.html"); 
        }



        void ReSizeGroup() {

            int ws = SystemInformation.VirtualScreen.Width;
            int hs = SystemInformation.VirtualScreen.Height;
            int h = hs / 3 - 50;
            groupPanel1.Height = h;
            groupPanel2.Height = h; 
            groupPanel3.Height = h;
            groupPanel4.Height = h;
            groupPanel5.Height = h;
            groupPanel6.Height = h;
            groupPanel7.Height = h;
            groupPanel8.Height = h;
            groupPanel9.Height = h;
            groupPanel10.Height = h;
           

        }

        private void b0Chon_Click(object sender, EventArgs e)
        {
           thumucChon = thumucGoc + "UserChoices\\Mau0";
           if (!File.Exists(thumucChon + "\\config.ntu"))
           {
               Directory.CreateDirectory(thumucChon);
               File.Create(thumucChon + "\\config.ntu");

               this.Enabled = false;
               CongCu.DirectoryCopy(TempPath, thumucChon, true);
               this.Enabled = true;
               MessageBox.Show("Chọn mẫu xong, Mời hiệu chỉnh dữ liệu", "Thông báo");
           }
           else
               MessageBox.Show("Bạn đã chọn mẫu này", "Cảnh báo");

        }

      

    }
}
