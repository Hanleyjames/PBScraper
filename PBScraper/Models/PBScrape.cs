using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
//using HtmlAgilityPack;

namespace PBScraper.Models
{
    public class PBScrape
    {
        private int _id;
        private string _keyword;
        private string _url;
        private string _email;
        private string _phone;

        public PBScrape(int Id = 0, string Keyword = "keyword not set",string Url = "URL not set", string Email = "Email not set", string Phone = "Phone not set")
        {

        }
    }
}
