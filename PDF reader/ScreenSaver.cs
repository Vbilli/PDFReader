using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _SCREEN_CAPTURE;

namespace PDF_reader
{
	public partial class ScreenSaver : Form
	{
		private MouseHook m_MHook;
		private List<Bitmap> m_layer;       //记录历史图层

		private bool m_isStartDraw;
		private Point m_ptOriginal;
		private Point m_ptCurrent;
		private Bitmap m_bmpLayerCurrent;
		private Bitmap m_bmpLayerShow;
		public ScreenSaver()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			this.Location = new Point(0, 0);
			this.Size = new Size(Screen.PrimaryScreen.Bounds.Width,
				Screen.PrimaryScreen.Bounds.Height);
			this.TopMost = true;
			this.ShowInTaskbar = false;

			m_MHook = new MouseHook();
			this.FormClosing += (s, e) => { m_MHook.UnLoadHook(); this.DelResource(); };
			imageProcessBox1.MouseLeave += (s, e) => this.Cursor = Cursors.Default;
			//后期一些操作历史记录图层
			m_layer = new List<Bitmap>();
		}


		private void DelResource()
		{
			if (m_bmpLayerCurrent != null) m_bmpLayerCurrent.Dispose();
			if (m_bmpLayerShow != null) m_bmpLayerShow.Dispose();
			m_layer.Clear();
			imageProcessBox1.DeleResource();
			GC.Collect();
		}

		private void imageProcessBox1_DoubleClick(object sender, EventArgs e)
		{
			Clipboard.SetImage(m_bmpLayerCurrent);
			this.Close();
		}

		private void imageProcessBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (imageProcessBox1.Cursor != Cursors.SizeAll &&
				imageProcessBox1.Cursor != Cursors.Default)
				panel1.Visible = false;         //表示改变选取大小 隐藏工具条
												//若果在选取类点击 并且有选择工具
			if (e.Button == MouseButtons.Left && imageProcessBox1.IsDrawed)
			{
				if (imageProcessBox1.SelectedRectangle.Contains(e.Location))
				{
					m_ptOriginal = e.Location;
					//if (tBtn_Text.IsSelected)
					//{         //如果选择的是绘制文本 弹出文本框
					//	textBox1.Location = e.Location;
					//	textBox1.Visible = true;
					//	textBox1.Focus();
					//	return;
					//}
					m_isStartDraw = true;
					Cursor.Clip = imageProcessBox1.SelectedRectangle;
				}
			}
		}

		private void imageProcessBox1_MouseMove(object sender, MouseEventArgs e)
		{
			m_ptCurrent = e.Location;
			//根据是否选择有工具决定 鼠标指针样式
			if (imageProcessBox1.SelectedRectangle.Contains(e.Location) && imageProcessBox1.IsDrawed)
				this.Cursor = Cursors.Cross;
			else if (!imageProcessBox1.SelectedRectangle.Contains(e.Location))
				this.Cursor = Cursors.Default;

			if (imageProcessBox1.IsStartDraw && panel1.Visible)   //在重置选取的时候 重置工具条位置(成立于移动选取的时候)
				this.SetToolBarLocation();
			#region toolbox
			//if (m_isStartDraw)
			//{        //如果在区域内点下那么绘制相应图形
			//	using (Graphics g = Graphics.FromImage(m_bmpLayerShow))
			//	{
			//		int tempWidth = 1;
			//		if (toolButton2.IsSelected) tempWidth = 3;
			//		if (toolButton3.IsSelected) tempWidth = 5;
			//		Pen p = new Pen(colorBox1.SelectedColor, tempWidth);

			//		#region   绘制矩形

			//		//if (tBtn_Rect.IsSelected)
			//		//{
			//		//	int tempX = e.X - m_ptOriginal.X > 0 ? m_ptOriginal.X : e.X;
			//		//	int tempY = e.Y - m_ptOriginal.Y > 0 ? m_ptOriginal.Y : e.Y;
			//		//	g.Clear(Color.Transparent);
			//		//	g.DrawRectangle(p, tempX - imageProcessBox1.SelectedRectangle.Left, tempY - imageProcessBox1.SelectedRectangle.Top, Math.Abs(e.X - m_ptOriginal.X), Math.Abs(e.Y - m_ptOriginal.Y));
			//		//	imageProcessBox1.Invalidate();
			//		//}

			//		#endregion

			//		#region    绘制圆形

			//		//if (tBtn_Ellipse.IsSelected)
			//		//{
			//		//	g.DrawLine(Pens.Red, 0, 0, 200, 200);
			//		//	g.Clear(Color.Transparent);
			//		//	g.DrawEllipse(p, m_ptOriginal.X - imageProcessBox1.SelectedRectangle.Left, m_ptOriginal.Y - imageProcessBox1.SelectedRectangle.Top, e.X - m_ptOriginal.X, e.Y - m_ptOriginal.Y);
			//		//	imageProcessBox1.Invalidate();
			//		//}

			//		#endregion

			//		#region    绘制箭头

			//		//if (tBtn_Arrow.IsSelected)
			//		//{
			//		//	g.Clear(Color.Transparent);
			//		//	System.Drawing.Drawing2D.AdjustableArrowCap lineArrow =
			//		//		new System.Drawing.Drawing2D.AdjustableArrowCap(4, 4, true);
			//		//	p.CustomEndCap = lineArrow;
			//		//	g.DrawLine(p, (Point)((Size)m_ptOriginal - (Size)imageProcessBox1.SelectedRectangle.Location), (Point)((Size)m_ptCurrent - (Size)imageProcessBox1.SelectedRectangle.Location));
			//		//	imageProcessBox1.Invalidate();
			//		//}

			//		#endregion

			//		#region    绘制线条

			//		//if (tBtn_Brush.IsSelected)
			//		//{
			//		//	Point ptTemp = (Point)((Size)m_ptOriginal - (Size)imageProcessBox1.SelectedRectangle.Location);
			//		//	p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
			//		//	g.DrawLine(p, ptTemp, (Point)((Size)e.Location - (Size)imageProcessBox1.SelectedRectangle.Location));
			//		//	m_ptOriginal = e.Location;
			//		//	imageProcessBox1.Invalidate();
			//		//}

			//		#endregion

			//		p.Dispose();
			//	}
			//}
			#endregion
		}

		private void imageProcessBox1_MouseUp(object sender, MouseEventArgs e)
		{
			if (!imageProcessBox1.IsDrawed)
			{       //如果没有成功绘制选取 继续禁用窗体
				this.Enabled = false;
				imageProcessBox1.IsDrawOperationDot = false;
			}
			else if (!panel1.Visible)
			{            //否则显示工具条
				this.SetToolBarLocation();  //重置工具条位置
				panel1.Visible = true;
				m_bmpLayerCurrent = imageProcessBox1.GetResultBmp();    //获取选取图形
				m_bmpLayerShow = new Bitmap(m_bmpLayerCurrent.Width, m_bmpLayerCurrent.Height);
			}
			//如果移动了选取位置 重新获取选取的图形
			if (imageProcessBox1.Cursor == Cursors.SizeAll && m_ptOriginal != e.Location)
				m_bmpLayerCurrent = imageProcessBox1.GetResultBmp();

			if (!m_isStartDraw) return;
			m_isStartDraw = false;
			//this.SetLayer();        //将绘制的图形绘制到历史图层中
			Cursor.Clip = Rectangle.Empty;
		}
		//绘制后期操作
		private void imageProcessBox1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (m_layer.Count > 0)  //绘制保存的历史记录的最后一张图
				g.DrawImage(m_layer[m_layer.Count - 1], imageProcessBox1.SelectedRectangle.Location);
			if (m_bmpLayerShow != null)     //绘制当前正在拖动绘制的图形(即鼠标点下还没有抬起确认的图形)
				g.DrawImage(m_bmpLayerShow, imageProcessBox1.SelectedRectangle.Location);
		}

		private void tBtn_Finish_Click(object sender, EventArgs e)
		{
			Clipboard.SetImage(m_bmpLayerCurrent);
			PictureBox pb = new PictureBox();
			//Transparent t = new Transparent();
			//t.Location = new Point(imageProcessBox1.Location.X, imageProcessBox1.Location.Y);

			this.Close();
		}

		private void SetToolBarLocation()
		{
			int tempX = imageProcessBox1.SelectedRectangle.Left;
			int tempY = imageProcessBox1.SelectedRectangle.Bottom + 5;
			int tempHeight = 0;
			if (tempY + panel1.Height + tempHeight >= this.Height)
				tempY = imageProcessBox1.SelectedRectangle.Top - panel1.Height - 10 - imageProcessBox1.Font.Height;

			if (tempY - tempHeight <= 0)
			{
				if (imageProcessBox1.SelectedRectangle.Top - 5 - imageProcessBox1.Font.Height >= 0)
					tempY = imageProcessBox1.SelectedRectangle.Top + 5;
				else
					tempY = imageProcessBox1.SelectedRectangle.Top + 10 + imageProcessBox1.Font.Height;
			}
			if (tempX + panel1.Width >= this.Width)
				tempX = this.Width - panel1.Width - 5;
			panel1.Left = tempX;
			//panel2.Left = tempX;
			panel1.Top = tempY;
			//panel2.Top = imageProcessBox1.SelectedRectangle.Top > tempY ? tempY - tempHeight : panel1.Bottom + 2;
		}

		private void ScreenSaver_Load(object sender, EventArgs e)
		{
			this.InitMember();
			imageProcessBox1.BaseImage = this.GetScreen();
			m_MHook.SetHook();
			m_MHook.MHookEvent += new MouseHook.MHookEventHandler(m_MHook_MHookEvent);
			imageProcessBox1.IsDrawOperationDot = false;
			this.BeginInvoke(new MethodInvoker(() => this.Enabled = false));
			//timer1.Interval = 500;
			//timer1.Enabled = true;
		}

		private void InitMember()
		{
			panel1.Visible = false;
			//panel2.Visible = false;
			panel1.BackColor = Color.White;
			//panel2.BackColor = Color.White;
			panel1.Paint += (s, e) => e.Graphics.DrawRectangle(Pens.SteelBlue, 0, 0, panel1.Width - 1, panel1.Height - 1);
			//panel2.Paint += (s, e) => e.Graphics.DrawRectangle(Pens.SteelBlue, 0, 0, panel2.Width - 1, panel2.Height - 1);


			tBtn_Close.Click += (s, e) => this.Close();

		}

		private Bitmap GetScreen()
		{
			Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
					Screen.PrimaryScreen.Bounds.Height);
			if (this.isCaptureCursor)
			{     //是否捕获鼠标
				  //如果直接将捕获当的鼠标画在bmp上 光标不会反色 指针边框也很浓 也就是说
				  //尽管bmp上绘制了图像 绘制鼠标的时候还是以黑色作为鼠标的背景 然后在将混合好的鼠标绘制到图像 会很别扭
				  //所以 干脆直接在桌面把鼠标绘制出来再截取桌面
				using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
				{   //传入0默认就是桌面 Win32.GetDesktopWindow()也可以
					Win32.PCURSORINFO pci;
					pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Win32.PCURSORINFO));
					Win32.GetCursorInfo(out pci);
					if (pci.hCursor != IntPtr.Zero)
					{
						Cursor cur = new Cursor(pci.hCursor);
						g.CopyFromScreen(0, 0, 0, 0, bmp.Size); //在桌面绘制鼠标前 先在桌面绘制一下当前的桌面图像
																//如果不绘制当前桌面 那么cur.Draw的时候会是用历史桌面的快照 进行鼠标的混合 那么到时候混出现底色(测试中就是这样的)
						cur.Draw(g, new Rectangle((Point)((Size)MousePosition - (Size)cur.HotSpot), cur.Size));
					}
				}
			}
			//做完以上操作 才开始捕获桌面图像
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
			}
			return bmp;
		}

		private bool isCaptureCursor;
		/// <summary>
		/// 获取或设置是否捕获鼠标
		/// </summary>
		public bool IsCaptureCursor
		{
			get { return isCaptureCursor; }
			set { isCaptureCursor = value; }
		}

		private void m_MHook_MHookEvent(object sender, MHookEventArgs e)
		{
			//如果窗体禁用 调用控件的方法设置信息显示位置
			if (!this.Enabled)      //貌似Hook不能精确坐标(Hook最先执行执 执行完后的坐标可能与执行时传入的坐标发生了变化 猜测是这样) 所以放置了一个timer检测
				imageProcessBox1.SetInfoPoint(MousePosition.X, MousePosition.Y);
			//鼠标点下恢复窗体禁用
			if (e.MButton == ButtonStatus.LeftDown || e.MButton == ButtonStatus.RightDown)
			{
				this.Enabled = true;
				imageProcessBox1.IsDrawOperationDot = true;
			}

			#region 在imageProcessBox_MouseUp中完成了
			//if (e.MButton == ButtonStatus.LeftUp) {
			//    //if (imageProcessBox1.SelectedRectangle.Width < 5
			//    //    || imageProcessBox1.SelectedRectangle.Height < 5) {
			//    //    //如果选取区域不符要求 向控件模拟鼠标抬起 然后禁用窗体
			//    //    //(Hook事件先于控件事件 禁用控件时模拟一个鼠标抬起)
			//    //    Win32.SendMessage(imageProcessBox1.Handle, Win32.WM_LBUTTONUP,
			//    //        IntPtr.Zero, (IntPtr)(MousePosition.Y << 16 | MousePosition.X));
			//    //    this.Enabled = false;
			//    //    imageProcessBox1.IsDrawOperationDot = false;
			//    //} else
			//    //    this.SetToolBarLocation();  //重置工具条位置
			//}
			#endregion

			#region 右键抬起

			if (e.MButton == ButtonStatus.RightUp)
			{
				if (!imageProcessBox1.IsDrawed) //没有绘制那么退出(直接this.Close右键将传递到下面)
					this.BeginInvoke(new MethodInvoker(() => this.Close()));
				//有绘制的情况 情况继续禁用窗体
				this.Enabled = false;
				imageProcessBox1.ClearDraw();
				imageProcessBox1.CanReset = true;
				imageProcessBox1.IsDrawOperationDot = false;
				m_layer.Clear();    //清空历史记录
				m_bmpLayerCurrent = null;
				m_bmpLayerShow = null;
				//ClearToolBarBtnSelected();
				panel1.Visible = false;
				//panel2.Visible = false;
			}

			#endregion

			#region 找寻窗体

			if (!this.Enabled)
			{
				this.FoundAndDrawWindowRect();
			}

			#endregion
		}

		//根据鼠标位置找寻窗体平绘制边框
		private void FoundAndDrawWindowRect()
		{
			Win32.LPPOINT pt = new Win32.LPPOINT();
			pt.X = MousePosition.X; pt.Y = MousePosition.Y;
			IntPtr hWnd = Win32.ChildWindowFromPointEx(Win32.GetDesktopWindow(), pt,
				Win32.CWP_SKIPINVISIBL | Win32.CWP_SKIPDISABLED);
			if (hWnd != IntPtr.Zero)
			{
				IntPtr hTemp = hWnd;
				while (true)
				{
					Win32.ScreenToClient(hTemp, out pt);
					hTemp = Win32.ChildWindowFromPointEx(hTemp, pt, Win32.CWP_All);
					if (hTemp == IntPtr.Zero || hTemp == hWnd)
						break;
					hWnd = hTemp;
					pt.X = MousePosition.X; pt.Y = MousePosition.Y; //坐标还原为屏幕坐标
				}
				Win32.LPRECT rect = new Win32.LPRECT();
				Win32.GetWindowRect(hWnd, out rect);
				imageProcessBox1.SetSelectRect(new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top));
			}
		}
	}
}
