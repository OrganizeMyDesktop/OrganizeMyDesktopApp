using System;
using System.IO;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileNameStartsWithRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().StartsWith(Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
