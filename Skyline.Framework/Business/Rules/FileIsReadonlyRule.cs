using System.IO;
using Skyline.Framework.Core;

namespace Skyline.Framework.Business.Rules
{
	public class FileIsReadonlyRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.IsReadOnly == this.Criteria.To<bool>();
		}
	}
}
