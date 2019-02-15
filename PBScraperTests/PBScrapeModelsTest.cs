using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using PBScraper.Models;

namespace PBScraperTests
{
    [TestClass]
    public class PBScraperModelTests
    {
        [TestMethod]
        public void PBScrape_SavesInstance_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("Wikipedia.org");
            //Act
            newScrape.Save();
            List<PBScrape> scrapedData = newScrape.GetInstanceData();
            int scrapedDataCount = scrapedData.Count;
            //Assert
            Assert.AreEqual(1, scrapedDataCount);
        }
    }
}