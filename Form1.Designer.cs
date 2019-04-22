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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
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
            List<Button> buttonList = new List<Button>();
            DirectoryInfo folder = new DirectoryInfo(Application.StartupPath);
            foreach (FileInfo f in folder.GetFiles("*.mp3"))
            {
                Console.WriteLine(f.Name.Split('.')[0]);
                fileList.Add(f.Name.Split('.')[0]);
            }
            int x = 0;
            for (int i = 0; i < fileList.Count; i++)
            {
                Button b = new Button();
                b.Text = fileList[i];
                b.Font = new System.Drawing.Font("微软雅黑", 14);
                b.AutoSize = true;
                b.Click += new System.EventHandler(this.Button_c);
                this.Controls.Add(b);
                if (i != 0)
                {
                    Button pre = buttonList[i - 1];
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
                }
                buttonList.Add(b);
            }

        }
        #endregion
    }
}

