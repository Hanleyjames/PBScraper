using System;
using System.Collections.Generic;
using System.IO;
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
            HtmlDocument htmlDoc = new HtmlDocument();
            string urlResponse = URLRequest(url);
            htmlDoc.LoadHtml(urlResponse);
            var titleNode = htmlDoc.DocumentNode.SelectNodes("//title");
            return titleNode[0].InnerText;
        }

        static string URLRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 6000;
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";
            string responseContent = null;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        responseContent = streamreader.ReadToEnd();
                    }
                }
            }
            return (responseContent);

        }
    }
}
