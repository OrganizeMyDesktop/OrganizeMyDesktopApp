using System;
using System.IO;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileDirectoryNameExactMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.Directory?.FullName.Equals(Criteria, StringComparison.OrdinalIgnoreCase) == true;
		}
	}
}
