using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PBScraperTests
{
    [TestClass]
    public class AssertionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            int result = 2 + 2;
            Assert.AreEqual(4, result);
        }
    }
}
