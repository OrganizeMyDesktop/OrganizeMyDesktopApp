using System;
using System.Windows.Forms;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop
{
	partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
			Text = String.Format("About {0}", AssemblyHelper.Instance.AssemblyTitle);
			labelProductName.Text = AssemblyHelper.Instance.AssemblyProduct;
			labelVersion.Text = String.Format("Version {0}", AssemblyHelper.Instance.AssemblyVersion);
			labelCopyright.Text = AssemblyHelper.Instance.AssemblyCopyright;
			textBoxDescription.Text = AssemblyHelper.Instance.AssemblyDescription;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
