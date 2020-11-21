using System;
using System.IO;
using Xunit;
using CleanMyDesktop.Core.Business.Rules;

namespace CleanMyDesktop.Core.Tests.Business.Rules
{
    public class FileIsReadonlyRuleTests
    {
        [Fact]
        public void TestRuleFail()
        {
            var rule = new FileIsReadonlyRule { Criteria = "true" };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfilereadonly.txt");
            Assert.False(rule.Match(fileInfo));
        }

        [Fact]
        public void TestRuleSucceed()
        {
            var rule = new FileIsReadonlyRule { Criteria = "false" };
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfilereadonly.txt");
            Assert.True(rule.Match(fileInfo));
        }
    }
}
