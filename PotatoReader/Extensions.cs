using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader
{
	static class Extensions
	{
		public static Size Scale(this Size s, float f)
		{
			return new Size((int)(s.Width * f), (int)(s.Height * f));
		}
	}
}
