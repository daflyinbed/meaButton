using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace meaButton
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        //https://blog.csdn.net/simpleshao/article/details/83894550

        #region buttonFactory
        private Button ButtonFactory(string FileName)
        {
            Button b = new Button();
            b.Tag = FileName + ".mp3";
            b.Text = FileName;
            b.Font = new System.Drawing.Font("微软雅黑", 14);
            b.AutoSize = true;
            b.Click += new System.EventHandler(this.Button_c);
            this.Controls.Add(b);
            return b;
        }
        private Button ButtonFactory(string FileName,string DirName)
        {
            Button b = new Button();
            b.Tag = DirName + "\\" + FileName + ".mp3";
            b.Text = FileName;
            b.Font = new System.Drawing.Font("微软雅黑", 14);
            b.AutoSize = true;
            b.Click += new System.EventHandler(this.Button_c);
            this.Controls.Add(b);
            return b;
        }
        private Button DirButtonFactory(string DirName)
        {
            Button b = new Button();
            b.Tag = "\\" + DirName;
            b.Text = DirName;
            b.Font = new System.Drawing.Font("微软雅黑", 14);
            b.AutoSize = true;
            b.Click += new System.EventHandler(this.Button_dir);
            this.Controls.Add(b);
            return b;
        }
        #endregion
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);


            //获取文件
            //string[] files = Directory.GetFiles(Application.StartupPath, "*.mp3");
            List<string> fileList = new List<string>();
            List<string> DirList = new List<string>();
            List<List<string>> fIDList = new List<List<string>>();
            List<Button> buttonList = new List<Button>();
            DirectoryInfo folder = new DirectoryInfo(Application.StartupPath);
            
            foreach(DirectoryInfo di in folder.GetDirectories())
            {
                DirList.Add(di.Name);
                List<string> temp = new List<string>();
                foreach(FileInfo f in di.GetFiles("*.mp3"))
                {
                    temp.Add(f.Name.Split('.')[0]);
                }
                fIDList.Add(temp);
            }
            foreach (FileInfo f in folder.GetFiles("*.mp3"))
            {
                //Console.WriteLine(f.Name.Split('.')[0]);
                fileList.Add(f.Name.Split('.')[0]);
            }

            int x = 0;

            Button cDButton = new Button();
            cDButton.Tag = "";
            cDButton.Text = "打开文件夹";
            cDButton.Font = new System.Drawing.Font("微软雅黑", 14);
            cDButton.AutoSize = true;
            cDButton.Click += new System.EventHandler(this.Button_dir);
            this.Controls.Add(cDButton);
            buttonList.Add(cDButton);

            #region 确定当前目录的mp3的按钮的坐标
            for (int i = 0; i < fileList.Count; i++)
            {
                Button b = ButtonFactory(fileList[i]);
                Button pre = buttonList[buttonList.Count-1];
                if (pre.Location.Y + pre.Size.Height * 2 + 10 < this.ClientSize.Height)
                {
                    b.Location = new System.Drawing.Point(x, pre.Location.Y + pre.Size.Height + 20);
                }
                else
                {
                    int bx = 0;
                    foreach (Button button in buttonList)
                    {
                        Console.WriteLine(button.Right);
                        if (button.Right > bx)
                        {
                            bx = button.Right;
                        }
                    }
                    x = bx + 20;
                    b.Location = new System.Drawing.Point(x, 0);
                }
                buttonList.Add(b);
            }
            #endregion

            for(int i = 0; i < DirList.Count; i++)
            {
                int bx = 0;
                foreach (Button button in buttonList)
                {
                    Console.WriteLine(button.Right);
                    if (button.Right > bx)
                    {
                        bx = button.Right;
                    }
                }
                x = bx + 20;
                Button dirbutton = DirButtonFactory(DirList[i]);
                dirbutton.Location = new System.Drawing.Point(x, 0);
                buttonList.Add(dirbutton);
                for(int j = 0; j < fIDList[i].Count; j++)
                {
                    Button pre = buttonList[buttonList.Count - 1];
                    Button b = ButtonFactory(fIDList[i][j], DirList[i]);
                    if (pre.Location.Y + pre.Size.Height * 2 + 10 < this.ClientSize.Height)
                    {
                        b.Location = new System.Drawing.Point(x, pre.Location.Y + pre.Size.Height + 20);
                    }
                    else
                    {
                        bx = 0;
                        foreach (Button button in buttonList)
                        {
                            Console.WriteLine(button.Right);
                            if (button.Right > bx)
                            {
                                bx = button.Right;
                            }
                        }
                        x = bx + 20;
                        b.Location = new System.Drawing.Point(x, 0);
                    }
                    buttonList.Add(b);
                }
            }

        }
    }

}

