using System.IO;
using Skyline.Framework.Core;

namespace Skyline.Framework.Business.Rules
{
	public class FileNameContainsRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.FileNameWithoutExtension().Contains(this.Criteria);
		}
	}
}
