using System.Reflection;

namespace CleanMyDesktop.Core.Extensions
{
    public sealed class AssemblyHelper
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

        public string AssemblyTitle { get; }

        public string AssemblyVersion { get; }

        public string AssemblyDescription { get; }

        public string AssemblyProduct { get; }

        public string AssemblyCopyright { get; }

        public string AssemblyCompany { get; }
    }
}
