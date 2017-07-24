using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTU.Webgen
{
    public partial class GiangDay : UserControl
    {
        public GiangDay()
        {
            InitializeComponent();
        }

        private void btnChonDeCuong_Click(object sender, EventArgs e)
        {
           DialogResult rs = openFileDialogDeCuong.ShowDialog();
           if (rs == DialogResult.OK) txtDeCuong.Text = openFileDialogDeCuong.FileName;
        }

        private void btnChonGiaoTrinh_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialogBaiGiang.ShowDialog();
            if (rs == DialogResult.OK) txtDeCuong.Text = openFileDialogBaiGiang.FileName;
        }

        private void btnUploadTaiLieu_Click(object sender, EventArgs e)
        {
            DialogResult rs = openFileDialogThamKhao.ShowDialog();
            String[] f;
            if (rs == DialogResult.OK) f = openFileDialogBaiGiang.FileNames;

        }
    }
}
