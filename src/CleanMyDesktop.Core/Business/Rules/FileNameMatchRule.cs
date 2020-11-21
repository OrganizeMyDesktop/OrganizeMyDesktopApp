using System;
using System.IO;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileNameExactMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().Equals(Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
