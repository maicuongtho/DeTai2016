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

namespace NTU.Webgen
{
    public partial class ProjectExplorer : UserControl
    {
        String pubHTMLFolder;// = @"F:/LocalRepository/DeTai2016/SanPham/NTU.Webgen/NTU.Webgen/bin/Debug/UserChoices/Mau0";
        public ProjectExplorer()
        {
            InitializeComponent();
            DirectoryInfo directoryInfo = new DirectoryInfo(pubHTMLFolder);
            if (directoryInfo.Exists)
            {
                treeView1.AfterSelect += treeView1_AfterSelect;
                BuildTree(directoryInfo, treeView1.Nodes);
                treeView1.ExpandAll();
            }
        }

        public ProjectExplorer(String pubHTMLFolder)
        {
            this.pubHTMLFolder = pubHTMLFolder;
            InitializeComponent();
            DirectoryInfo directoryInfo = new DirectoryInfo(pubHTMLFolder);
            if (directoryInfo.Exists)
            {
                treeView1.AfterSelect += treeView1_AfterSelect;
                BuildTree(directoryInfo, treeView1.Nodes);
                treeView1.ExpandAll();
            }
        }


        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Name.EndsWith(".html"))
                curNode.Nodes.Add(file.FullName, file.Name);
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.Name.EndsWith("css"))
            //{
            //    this.richTextBox1.Clear();
            //    StreamReader reader = new StreamReader(e.Node.Name);
            //    this.richTextBox1.Text = reader.ReadToEnd();
            //    reader.Close();
            //}
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show( treeView1.SelectedNode.Text);
            CongCu.gotoSite(pubHTMLFolder +"\\"+ treeView1.SelectedNode.Text); 
        }
    }
}
