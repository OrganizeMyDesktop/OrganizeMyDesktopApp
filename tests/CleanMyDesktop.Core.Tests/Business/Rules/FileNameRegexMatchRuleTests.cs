using System;
using System.IO;
using Xunit;
using CleanMyDesktop.Core.Business.Rules;

namespace CleanMyDesktop.Core.Tests.Business.Rules
{
    public class FileNameRegexMatchRuleTests
    {
        [Fact]
        public void TestRuleFail()
        {
            var rule = new FileNameRegexMatchRule { Criteria = "^[0-9]+$" };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
            Assert.False(rule.Match(fileInfo));
        }

        [Fact]
        public void TestRuleSucceed()
        {
            var rule = new FileNameRegexMatchRule { Criteria = "^[0-9]+$" };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\1234567890.txt");
            Assert.True(rule.Match(fileInfo));
        }
    }
}
