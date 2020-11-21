using System.IO;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileSizeEqualsRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			var fileSize = Criteria.To(-1);
			return file.Length == fileSize;
		}
	}
}
