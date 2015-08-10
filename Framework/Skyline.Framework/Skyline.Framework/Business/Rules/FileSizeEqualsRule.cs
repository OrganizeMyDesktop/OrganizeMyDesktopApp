using Skyline.Framework.Core;
using System.IO;

namespace Skyline.Framework.Business.Rules
{
	public class FileSizeEqualsRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			var fileSize = this.Criteria.To<int>(-1);
			return file.Length == fileSize;
		}
	}
}
