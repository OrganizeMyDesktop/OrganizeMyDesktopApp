using System.IO;
using System.Text.RegularExpressions;
using CleanMyDesktop.Core.Extensions;

namespace CleanMyDesktop.Core.Business.Rules
{
    public class FileNameRegexMatchRule : RuleBase
    {
        public override bool Match(FileInfo file)
        {
            return new Regex(Criteria, RegexOptions.Compiled).IsMatch(file.FileNameWithoutExtension());
        }
    }
}
