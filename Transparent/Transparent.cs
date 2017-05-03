using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent
{
	public class Transparent : PictureBox
	{
		public Transparent()
		{
			this.ForeColor = Color.White;
			this.BackColor = Color.Black;
			this.Dock = DockStyle.Fill;

			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque, true);
			this.BackColor = Color.Transparent;

		}
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle = 0x20;
				return cp;
			}
		}

	}
}
