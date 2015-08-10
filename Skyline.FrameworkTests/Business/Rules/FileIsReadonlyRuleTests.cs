using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Skyline.Framework.Business.Rules.Tests
{
	[TestClass()]
	public class FileIsReadonlyRuleTests
	{
		[TestMethod()]
		public void TestRuleFail()
		{
			var rule = new FileIsReadonlyRule { Criteria = "true" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfilereadonly.txt");
			Assert.IsFalse(rule.Match(fileInfo));
		}

		[TestMethod()]
		public void TestRuleSucceed()
		{
			var rule = new FileIsReadonlyRule { Criteria = "false" };
			var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\testfilereadonly.txt");
			Assert.IsTrue(rule.Match(fileInfo));
		}
	}
}
