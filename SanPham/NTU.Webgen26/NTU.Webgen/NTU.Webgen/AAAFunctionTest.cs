using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTU.Webgen
{
    public partial class AAAFunctionTest : Form
    {
        public AAAFunctionTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String cvXMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\data\cv.xml";
            String cvHTMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\cv.html";
            XuatWeb.XuatCV(cvXMLFile, cvHTMLFile);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String cvXMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\data\cv.xml";
            String cvHTMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\cv.html";
            XuatWeb.XuatCV(cvXMLFile, cvHTMLFile);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String cvHTMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\cv.html";
            String cvPDFFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\cv.pdf";
            XuatWeb.HTMLToPdf(cvHTMLFile, cvPDFFile);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String bgXMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\data\teaching.xml";
            String bgHTMLFile = @"C:\Users\Mai Cuong Tho\Desktop\TestMau4\teaching.html";
            XuatWeb.XuatBaiGiang("NEC303", bgXMLFile, bgHTMLFile);
        }


        

    }
}
