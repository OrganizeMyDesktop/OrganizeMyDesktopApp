using System;
using System.IO;
using Skyline.Framework.Core;

namespace Skyline.Framework.Business.Rules
{
	public class FileNameEndsWithRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().EndsWith(this.Criteria, StringComparison.OrdinalIgnoreCase);
		}
	}
}
