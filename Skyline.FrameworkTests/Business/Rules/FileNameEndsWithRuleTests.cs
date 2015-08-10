using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Skyline.Framework.Business.Rules.Tests
{
	[TestClass()]
	public class FileNameEndsWithRuleTests
	{
		[TestMethod()]
		public void TestRuleFail()
		{
			var rule = new FileNameEndsWithRule { Criteria = "test" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsFalse(rule.Match(fileInfo));
		}

		[TestMethod()]
		public void TestRuleSucceed()
		{
			var rule = new FileNameEndsWithRule { Criteria = "file" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsTrue(rule.Match(fileInfo));
		}
	}
}
