using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PBScraperTests
{
    [TestClass]
    public class AssertionTests
    {
        [TestMethod]
        public void AssertionTest_TwoPlusTwoEqualsFour_IsTrue()
        {
            int result = 2 + 2;
            Assert.AreEqual(4, result);
        }
    }
}
