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
            Console.WriteLine(Application.StartupPath+"\\"+(sender as Button).Text+".mp3");
            clsMCI cm = new clsMCI();
            cm.FileName = Application.StartupPath + "\\" + (sender as Button).Text + ".mp3";
            cm.play();
        }
    }
}
