using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoReader
{
	class ToastPanel : Panel
	{
		int updateState = 0;
		float updatePercent = 0;
		Color shadowColor = Color.FromArgb(200, 0, 0, 0);
		Font font = new Font("Arial", 10);

		public void SetUpdateStatus(int status, float percent)
		{
			if (status > 0)
				this.Visible = true;
			updateState = status;
			updatePercent = percent;
			this.BackColor = shadowColor;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
		}
		

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Brush brush = new SolidBrush(updateState == 1 ? Color.Yellow : Color.LimeGreen);
			string updateProgress = "";
			if (updateState == 1)
				updateProgress = "Updating - " + ((int)Math.Round(updatePercent * 100)) + "%";
			else if (updateState == 2)
				updateProgress = "Finished downloading update - restarting application";
			e.Graphics.FillRectangle(brush, 0, Height - 5, Width * updatePercent, 5);
			e.Graphics.DrawString(updateProgress, font, Brushes.White, new Point(5, 5));
		}
	}
}
