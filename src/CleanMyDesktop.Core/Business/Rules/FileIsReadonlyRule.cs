using System.IO;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
	public class FileIsReadonlyRule : RuleBase
	{
		public override bool Match(FileInfo file)
		{
			return file.IsReadOnly == this.Criteria.To<bool>();
		}
	}
}
