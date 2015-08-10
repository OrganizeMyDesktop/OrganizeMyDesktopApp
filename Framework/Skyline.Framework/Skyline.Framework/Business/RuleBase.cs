namespace Skyline.Framework.Business
{
	abstract class RuleBase : IRule
	{
		public Action SuccessAction { get; set; }
		public abstract bool Match(System.IO.FileInfo file);
	}
}
