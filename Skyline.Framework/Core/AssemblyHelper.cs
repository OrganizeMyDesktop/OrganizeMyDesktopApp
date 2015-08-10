using System.Reflection;

namespace Skyline.Framework.Core
{
	public class AssemblyHelper : Singleton<AssemblyHelper>
	{
		private AssemblyHelper()
		{
			var asm = Assembly.GetEntryAssembly();
			var attributes = asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			if (attributes.Length > 0)
			{
				var titleAttribute = (AssemblyTitleAttribute)attributes[0];
				if (titleAttribute.Title != "")
				{
					AssemblyTitle = titleAttribute.Title;
				}
			}
			AssemblyVersion = asm.GetName().Version.ToString();
			attributes = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
			AssemblyDescription = attributes.Length > 0 ? ((AssemblyDescriptionAttribute)attributes[0]).Description : "";
			attributes = asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			AssemblyProduct = attributes.Length > 0 ? ((AssemblyProductAttribute)attributes[0]).Product : "";
			attributes = asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			AssemblyCopyright = attributes.Length > 0 ? ((AssemblyCopyrightAttribute)attributes[0]).Copyright : "";
			attributes = asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
			AssemblyCompany = attributes.Length > 0 ? ((AssemblyCompanyAttribute)attributes[0]).Company : "";
		}

		public string AssemblyTitle { get; set; }

		public string AssemblyVersion { get; set; }

		public string AssemblyDescription { get; set; }

		public string AssemblyProduct { get; set; }

		public string AssemblyCopyright { get; set; }

		public string AssemblyCompany { get; set; }
	}
}
