using _SCREEN_CAPTURE;

namespace PDF_reader
{
	partial class ScreenSaver
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imageProcessBox1 = new _SCREEN_CAPTURE.ImageProcessBox();
			this.tBtn_Finish = new _SCREEN_CAPTURE.ToolButton();
			this.tBtn_Close = new _SCREEN_CAPTURE.ToolButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageProcessBox1
			// 
			this.imageProcessBox1.BackColor = System.Drawing.Color.Black;
			this.imageProcessBox1.BaseImage = null;
			this.imageProcessBox1.CanReset = true;
			this.imageProcessBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.imageProcessBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageProcessBox1.ForeColor = System.Drawing.Color.White;
			this.imageProcessBox1.Location = new System.Drawing.Point(0, 0);
			this.imageProcessBox1.Name = "imageProcessBox1";
			this.imageProcessBox1.Size = new System.Drawing.Size(453, 323);
			this.imageProcessBox1.TabIndex = 0;
			this.imageProcessBox1.Text = "imageProcessBox1";
			this.imageProcessBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageProcessBox1_Paint);
			this.imageProcessBox1.DoubleClick += new System.EventHandler(this.imageProcessBox1_DoubleClick);
			this.imageProcessBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseDown);
			this.imageProcessBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseMove);
			this.imageProcessBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageProcessBox1_MouseUp);
			// 
			// tBtn_Finish
			// 
			this.tBtn_Finish.BtnImage = global::PDF_reader.Properties.Resources.ok;
			this.tBtn_Finish.IsSelected = false;
			this.tBtn_Finish.IsSelectedBtn = false;
			this.tBtn_Finish.IsSingleSelectedBtn = false;
			this.tBtn_Finish.Location = new System.Drawing.Point(30, 3);
			this.tBtn_Finish.Name = "tBtn_Finish";
			this.tBtn_Finish.Size = new System.Drawing.Size(58, 21);
			this.tBtn_Finish.TabIndex = 8;
			this.tBtn_Finish.Text = "Finish ";
			this.tBtn_Finish.Click += new System.EventHandler(this.tBtn_Finish_Click);
			// 
			// tBtn_Close
			// 
			this.tBtn_Close.BtnImage = global::PDF_reader.Properties.Resources.close;
			this.tBtn_Close.IsSelected = false;
			this.tBtn_Close.IsSelectedBtn = false;
			this.tBtn_Close.IsSingleSelectedBtn = false;
			this.tBtn_Close.Location = new System.Drawing.Point(3, 3);
			this.tBtn_Close.Name = "tBtn_Close";
			this.tBtn_Close.Size = new System.Drawing.Size(21, 21);
			this.tBtn_Close.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tBtn_Finish);
			this.panel1.Controls.Add(this.tBtn_Close);
			this.panel1.Location = new System.Drawing.Point(112, 162);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(96, 27);
			this.panel1.TabIndex = 1;
			// 
			// ScreenSaver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(453, 323);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.imageProcessBox1);
			this.Name = "ScreenSaver";
			this.Text = "ScreenSaver";
			this.Load += new System.EventHandler(this.ScreenSaver_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private ToolButton tBtn_Finish;
		private ToolButton tBtn_Close;
		private ImageProcessBox imageProcessBox1;
		private System.Windows.Forms.Panel panel1;
	}
}