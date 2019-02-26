using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;

namespace PBScraper.Models
{
    public class PBScrape
    {
        private int _id;
        private string _keyword;
        private string _url;
        private string _email;
        private string _phone;
        private static List<PBScrape> _instancedata = new List<PBScrape> { };

        public PBScrape(string Url, int Id = 0, string Keyword = "keyword not set", string Email = "Email not set", string Phone = "Phone not set")
        {
            _id = Id;
            _keyword = Keyword;
            _url = Url;
            _email = Email;
            _phone = Phone;
        }

        public int GetId()
        {
            return _id;
        }
        
        public string GetKeyword()
        {
            return _keyword;
        }

        public string GetUrl()
        {
            return _url;
        }
        
        public string GetEmail()
        {
            return _email;
        }

        public string GetPhone()
        {
            return _phone;
        }

        public void SetId(int Id)
        {
            _id = Id;
        }

        public void SetKeyword(string Keyword)
        {
            _keyword = Keyword;
        }

        public void SetUrl(string Url)
        {
            _url = Url;
        }

        public void SetEmail(string Email)
        {
            _email = Email;
        }
        public void SetPhone(string Phone)
        {
            _phone = Phone;
        }

        public void Save()
        {
            _instancedata.Add(this);
        }

        public List<PBScrape> GetInstanceData()
        {
            return _instancedata;
        }

        //Parse HTML from given url
        public object ParseHtml(string url)
        {
            string html = url;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//body");
            return node;
        }

        public object GetTitleHtml(string url)
        {
            string html = url;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(@html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//title");
            return node;
        }

    }
}
