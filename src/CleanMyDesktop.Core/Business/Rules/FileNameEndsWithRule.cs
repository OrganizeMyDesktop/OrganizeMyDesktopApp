using System;
using System.IO;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileNameEndsWithRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().EndsWith(Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
