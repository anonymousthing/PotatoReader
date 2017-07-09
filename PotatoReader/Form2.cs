using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoReader
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		public bool search = false;
		public string searchText = "";

		private void button1_Click(object sender, EventArgs e)
		{
			searchText = textBox1.Text;
			search = true;
			this.Close();
		}
	}
}
