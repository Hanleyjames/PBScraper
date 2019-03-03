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
        public void PBScrape_SetsAndRetrievesEmail_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("https://youtube.com");
            //Act
            newScrape.SetEmail("hanley.doggo@outlook.com");
            //Assert
            Assert.AreEqual(newScrape.GetEmail(), "hanley.doggo@outlook.com");
        }
        [TestMethod]
        public void PBScrape_SetsAndRetrievesPhone_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("https://Outlook.com");
            //Act
            newScrape.SetPhone("1-800-453-9999");
            //Assert
            Assert.AreEqual(newScrape.GetPhone(), "1-800-453-9999");
        }
        [TestMethod]
        public void PBScrape_SetsAndRetrievesKeyword_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("https://Google.com");
            //Act
            newScrape.SetKeyword("Seattle Flowers");
            //Assert
            Assert.AreEqual(newScrape.GetKeyword(), "Seattle Flowers");
        }
        [TestMethod]
        public void PBScrape_GetsURLTitle_IsTrue()
        {
            //Arrange
            string url = "https://www.wikipedia.org";
            PBScrape newScrape = new PBScrape(url);
            //Act
            var ParseObject = newScrape.GetTitleHtml(url);
            //Assert
            Assert.AreEqual("Wikipedia", ParseObject);
        }
    }
}