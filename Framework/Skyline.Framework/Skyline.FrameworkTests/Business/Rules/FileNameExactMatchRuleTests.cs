using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Skyline.Framework.Business.Rules.Tests
{
	[TestClass()]
	public class FileNameExactMatchRuleTests
	{
		[TestMethod()]
		public void TestRuleFail()
		{
			var rule = new FileNameExactMatchRule { Criteria = "filetest" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsFalse(rule.Match(fileInfo));
		}

		[TestMethod()]
		public void TestRuleSucceed()
		{
			var rule = new FileNameExactMatchRule { Criteria = "testfile" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfile.txt");
			Assert.IsTrue(rule.Match(fileInfo));
		}
	}
}
