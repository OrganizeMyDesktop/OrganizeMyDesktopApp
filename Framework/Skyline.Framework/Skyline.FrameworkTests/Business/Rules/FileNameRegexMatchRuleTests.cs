using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Skyline.Framework.Business.Rules.Tests
{
	[TestClass()]
	public class FileNameRegexMatchRuleTests
	{
		[TestMethod()]
		public void TestRuleFail()
		{
			var rule = new FileNameRegexMatchRule { Criteria = @"^[0-9]+$" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsFalse(rule.Match(fileInfo));
		}

		[TestMethod()]
		public void TestRuleSucceed()
		{
			var rule = new FileNameRegexMatchRule { Criteria = @"^[0-9]+$" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\1234567890.txt");
			Assert.IsTrue(rule.Match(fileInfo));
		}
	}
}
