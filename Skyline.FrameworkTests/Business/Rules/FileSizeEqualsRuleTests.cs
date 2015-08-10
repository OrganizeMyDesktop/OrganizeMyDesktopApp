using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Skyline.Framework.Business.Rules.Tests
{
	[TestClass()]
	public class FileSizeEqualsRuleTests
	{
		[TestMethod()]
		public void TestRuleFail()
		{
			var rule = new FileSizeEqualsRule { Criteria = "1107" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsFalse(rule.Match(fileInfo));
		}

		[TestMethod()]
		public void TestRuleSucceed()
		{
			var rule = new FileSizeEqualsRule { Criteria = "1106" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsTrue(rule.Match(fileInfo));
		}
	}
}
