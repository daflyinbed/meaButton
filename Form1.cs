using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace meaButton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Button_c(object sender, EventArgs e)
        {
            Console.WriteLine(Application.StartupPath+"\\"+(sender as Button).Tag);
            clsMCI cm = new clsMCI();
            cm.FileName = Application.StartupPath + "\\" + (sender as Button).Tag;
            cm.play();
        }
        private void Button_dir(object sender, EventArgs e)
        {
            Console.WriteLine((sender as Button).Tag);
            OpenFolder(Application.StartupPath + (sender as Button).Tag);
        }
        private void OpenFolder(String Path)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e," + Path;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
