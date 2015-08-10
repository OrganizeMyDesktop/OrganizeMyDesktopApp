using System.IO;
using System.Text.RegularExpressions;
using Skyline.Framework.Core;

namespace Skyline.Framework.Business.Rules
{
	public class FileNameRegexMatchRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return new Regex(this.Criteria, RegexOptions.Compiled).IsMatch(file.FileNameWithoutExtension());
		}
	}
}
