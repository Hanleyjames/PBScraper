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
        public void PBScrape_SavesStaticVariables_IsTrue()
        {
            PBScrape newScrape = new PBScrape();
            
            string Keyword = newScrape.GetKeyword();
            bool TestBool = false;
            if (Keyword == "Bongos")
            {
                TestBool = true;
            }
            Assert.AreEqual(TestBool, true);
        }
        [TestMethod]
        public void PBScrape_CountsDatabaseEntries_1()
        {
            PBScrape.ClearAll();
            PBScrape newScrape = new PBScrape();
            newScrape.Save();
            List<PBScrape> allScrapes = PBScrape.GetAll();
            int count = allScrapes.Count;
            Assert.AreEqual(1, count);
            
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
            PBScrape newScrape = new PBScrape();
            //Assert
            newScrape.SetKeyword("Seattle Flowers");
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
            object ParseObject = newScrape.GetTitleHtml(newScrape.GetUrl());
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
            List<string> urlList = newScrape.GetUrls();
            //Assert
            Assert.AreNotEqual(0, urlList);
        }
        [TestMethod]
        public void PBScrape_FindEmail_IsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
        [TestMethod]
        public void PBScrape_FindPhoneNumber_IsTrue()
        {

        }
        [TestMethod]
        public void PBScrape_SaveUrlList_IsTrue()
        {
            ////Arrange
            //string Keyword = "Seattle Bouldering";
            //PBScrape newScrape = new PBScrape(Keyword);
            ////Act
            //newScrape.GetGoogleResults(newScrape.GetKeyword());
            //List<string> urlList = newScrape.GetUrls();
            //newScrape.SaveURLInstanceList(urlList);
            //PBScrape.ClearAll();
            ////Assert
            //Assert.Fail();
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