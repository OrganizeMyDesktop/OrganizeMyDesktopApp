using System;
using System.IO;

namespace Skyline.Framework.Business.Rules
{
	public class FileDirectoryNameExactMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.Directory != null && file.Directory.FullName.Equals(this.Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
