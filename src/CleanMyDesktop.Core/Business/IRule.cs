using System.IO;

namespace CleanMyDesktop.Core.Business
{
    interface IRule
	{
		bool Match(FileInfo file);
	}
}
