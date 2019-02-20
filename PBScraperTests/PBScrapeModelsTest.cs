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

        [TestMethod]
        public void PBScrape_SetsAndRetrievesInformation_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("youtube.com");
            //Act
            newScrape.SetEmail("hanley.doggo@outlook.com");
            //Assert
            Assert.AreEqual(newScrape.GetEmail(), "hanley.doggo@outlook.com");
        }
    }
}