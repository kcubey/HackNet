using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNetTests
{
	[TestClass]
	public class GeneralTests
	{
		[TestMethod]
		public void DBConn()
		{
			Assert.IsTrue(HackNet.Global.SqlConnAvailable);
		}

		[TestMethod]
		public void TestMode()
		{
			Assert.IsTrue(HackNet.Global.IsInUnitTest);
		}

	}
}
