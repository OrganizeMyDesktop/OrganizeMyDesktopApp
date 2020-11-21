using System.IO;

namespace CleanMyDesktop.Core.Business
{
	public abstract class RuleBase : IRule
	{
		public string Criteria { get; set; }
		public Action SuccessAction { get; set; }
		public abstract bool Match(FileInfo file);
	}
}
