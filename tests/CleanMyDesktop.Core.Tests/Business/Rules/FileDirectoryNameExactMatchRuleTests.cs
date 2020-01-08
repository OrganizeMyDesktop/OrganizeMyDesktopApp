using System;
using System.IO;
using Xunit;
using CleanMyDesktop.Core.Business.Rules;

namespace CleanMyDesktop.Core.Tests.Business.Rules
{
    public class FileDirectoryNameExactMatchRuleTests
    {
        [Fact]
        public void TestRuleFail()
        {

            var rule = new FileDirectoryNameExactMatchRule { Criteria = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mustfail") };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
            Assert.False(rule.Match(fileInfo));
        }

        [Fact]
        public void TestRuleSucceed()
        {
            var rule = new FileDirectoryNameExactMatchRule { Criteria = Path.TrimEndingDirectorySeparator(AppDomain.CurrentDomain.BaseDirectory) };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"testfile.txt");
            Assert.True(rule.Match(fileInfo));
        }
    }
}
