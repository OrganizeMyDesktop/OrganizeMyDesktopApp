using System.Reflection;

namespace CleanMyDesktop.Core.Extensions
{
    public class AssemblyHelper : Singleton<AssemblyHelper>
    {
        private AssemblyHelper()
        {
            var asm = Assembly.GetEntryAssembly();
            AssemblyVersion = asm.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            AssemblyDescription = asm.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            AssemblyProduct = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            AssemblyCopyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            AssemblyCompany = asm.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
        }

        public string AssemblyTitle { get; set; }

        public string AssemblyVersion { get; set; }

        public string AssemblyDescription { get; set; }

        public string AssemblyProduct { get; set; }

        public string AssemblyCopyright { get; set; }

        public string AssemblyCompany { get; set; }
    }
}
