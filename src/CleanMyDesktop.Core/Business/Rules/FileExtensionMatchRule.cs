using System;
using System.IO;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileExtensionMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.Extension.Equals(Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
