using System;
using System.IO;
using Xunit;
using CleanMyDesktop.Core.Business.Rules;

namespace CleanMyDesktop.Core.Tests.Business.Rules
{
	public class FileNameStartsWithRuleTests
	{
		[Fact]
		public void TestRuleFail()
		{
			var rule = new FileNameStartsWithRule { Criteria = "file" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.False(rule.Match(fileInfo));
		}

		[Fact]
		public void TestRuleSucceed()
		{
			var rule = new FileNameStartsWithRule { Criteria = "test" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.True(rule.Match(fileInfo));
		}
	}
}
