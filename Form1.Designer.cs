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
		private Button ButtonFactory(string FileName,string DirName)
		{
			if (FileName.Equals("打开文件夹"))
			{
				FileName = "";
			}
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
			b.BackColor = System.Drawing.Color.Khaki;
			this.Controls.Add(b);
			return b;
		}
		#endregion

		private void buildUI()
		{
			this.Controls.Clear();
			//获取文件
			//string[] files = Directory.GetFiles(Application.StartupPath, "*.mp3");
			List<string> fileList = new List<string>();
			List<string> DirList = new List<string>();
			List<List<string>> fIDList = new List<List<string>>();
			List<Button> buttonList = new List<Button>();
			List<Button> folderButtonList = new List<Button>();
			DirectoryInfo folder = new DirectoryInfo(Application.StartupPath);

			foreach (FileInfo f in folder.GetFiles("*.mp3"))
			{
				//Console.WriteLine(f.Name.Split('.')[0]);
				fileList.Add(f.Name.Split('.')[0]);
			}
			fIDList.Add(fileList);
			DirList.Add("打开文件夹");
			foreach (DirectoryInfo di in folder.GetDirectories())
			{
				DirList.Add(di.Name);
				List<string> temp = new List<string>();
				foreach (FileInfo f in di.GetFiles("*.mp3"))
				{
					temp.Add(f.Name.Split('.')[0]);
				}
				fIDList.Add(temp);
			}
			const int offsetX = 0;
			const int offsetY = 0;
			int x = 0 + offsetX;
			int y = 0 + offsetY;
			int maxDirLength = 0;

			foreach (string dirName in DirList)
			{
				Button dirbutton = DirButtonFactory(dirName);
				folderButtonList.Add(dirbutton);
				if (dirbutton.Size.Width > maxDirLength)
				{
					maxDirLength = dirbutton.Size.Width;
				}
			}

			Button pre;
			for (int i = 0; i < folderButtonList.Count; i++)
			{
				folderButtonList[i].Location = P(0, y);
				buttonList.Add(folderButtonList[i]);
				for (int j = 0; j < fIDList[i].Count; j++)
				{
					Button b = ButtonFactory(fIDList[i][j], DirList[i]);
					pre = buttonList[buttonList.Count - 1];
					if (pre.Left + pre.Size.Width + 10 + b.Width > this.ClientSize.Width)//换行
					{
						x = maxDirLength + 10 + offsetX;
						y = pre.Top + pre.Size.Height + 10;
						//Console.WriteLine("换行:" + x + "," + y);
					}
					else
					{
						x = pre.Left + pre.Size.Width + 10;
						if (x < maxDirLength + 10 + offsetX)
						{
							x = maxDirLength + 10 + offsetX;
						}
						//Console.WriteLine(x + "," + y);
					}
					b.Location = P(x, y);
					buttonList.Add(b);
				}
				pre = buttonList[buttonList.Count - 1];
				y = pre.Top + pre.Size.Height + 10;
			}
		}

		private System.Drawing.Point P(int x,int y)
		{
			return new System.Drawing.Point(x, y);
		}
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
			this.ResizeEnd += new System.EventHandler(this.MyFromResize);
			buildUI();
		}
	}
}

