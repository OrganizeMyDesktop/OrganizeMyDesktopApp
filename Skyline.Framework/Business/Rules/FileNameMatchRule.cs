using System;
using System.IO;
using Skyline.Framework.Core;

namespace Skyline.Framework.Business.Rules
{
	public class FileNameExactMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().Equals(this.Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
