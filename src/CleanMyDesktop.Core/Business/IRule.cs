using System.IO;

namespace CleanMyDesktop.Core.Business
{
    public interface IRule
    {
        bool Match(FileInfo file);
    }
}
