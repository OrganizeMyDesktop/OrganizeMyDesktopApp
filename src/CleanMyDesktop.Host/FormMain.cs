using System;
using System.Windows.Forms;

namespace CleanMyDesktop
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var form = new FormAbout())
			{
				form.ShowDialog();
			}
		}
	}
}
