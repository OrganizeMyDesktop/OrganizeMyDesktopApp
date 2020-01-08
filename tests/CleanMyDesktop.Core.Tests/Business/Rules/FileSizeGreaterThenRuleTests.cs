using System;
using System.IO;
using Xunit;
using CleanMyDesktop.Core.Business.Rules;

namespace CleanMyDesktop.Core.Tests.Business.Rules
{
    public class FileSizeGreaterThenRuleTests
	{
		[Fact]
		public void TestRuleFail()
		{
			var rule = new FileSizeGreaterThenRule { Criteria = "1107" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.False(rule.Match(fileInfo));
		}

		[Fact]
		public void TestRuleSucceed()
		{
			var rule = new FileSizeGreaterThenRule { Criteria = "1105" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.True(rule.Match(fileInfo));
		}
	}
}
