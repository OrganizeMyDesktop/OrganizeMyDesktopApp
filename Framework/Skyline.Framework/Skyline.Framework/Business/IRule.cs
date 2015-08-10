namespace Skyline.Framework.Business
{
	using System.IO;

	interface IRule
	{
		bool Match(FileInfo file);
	}
}
