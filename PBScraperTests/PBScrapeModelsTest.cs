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
            PBScrape newScrape = new PBScrape("Wikipedia test");
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
            PBScrape newScrape = new PBScrape("Email test");
            //Act
            newScrape.SetEmail("hanley.doggo@outlook.com");
            //Assert
            Assert.AreEqual("hanley.doggo@outlook.com", newScrape.GetEmail());
        }
        [TestMethod]
        public void PBScrape_SetsAndRetrievesPhone_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("Phone test");
            //Act
            newScrape.SetPhone("1-800-453-9999");
            //Assert
            Assert.AreEqual("1-800-453-9999", newScrape.GetPhone());
        }
        [TestMethod]
        public void PBScrape_SetsAndRetrievesKeyword_IsTrue()
        {
            //Arrange
            PBScrape newScrape = new PBScrape("Seattle Flowers");
            //Assert
            Assert.AreEqual("Seattle Flowers", newScrape.GetKeyword());
        }
        [TestMethod]
        public void PBScrape_GetsURLTitle_IsTrue()
        {
            //Arrange
            string Keyword = "Wikipedia test";
            string Url = "https://www.wikipedia.org";
            PBScrape newScrape = new PBScrape(Keyword);
            newScrape.SetUrl(Url);
            //Act
            var ParseObject = newScrape.GetTitleHtml(newScrape.GetUrl());
            //Assert
            Assert.AreEqual("Wikipedia", ParseObject);
        }
        [TestMethod]
        public void PBScrape_RetrievedURLSInList_NotEqualToZero()
        {
            //Arrange
            string Keyword = "Sandwhich";
            PBScrape newScrape = new PBScrape(Keyword);
            //Act
            newScrape.GetGoogleResults(newScrape.GetKeyword());
            var urlList = newScrape.GetUrls();
            //Assert
            Assert.AreNotEqual(0, urlList);
        }
        [TestMethod]
        public void PBScrape_RetrievesBingAPI_IsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}