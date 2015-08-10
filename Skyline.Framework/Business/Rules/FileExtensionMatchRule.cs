using System;
using System.IO;

namespace Skyline.Framework.Business.Rules
{
	public class FileExtensionMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.Extension.Equals(this.Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
