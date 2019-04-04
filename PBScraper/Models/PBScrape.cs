using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;

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
        private List<string> _urls = new List<string> { };
        private readonly string _api = "AIzaSyAwaNkJAWCWn6lzvglnRbqtS1y7tbNUJSY";
        private readonly string _searchEngineId = "015153167064412439961:9t3cwc_ifrm";
        public Regex _findNumber = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$");
        public Regex _findEmail = new Regex(@"(?:(?:\+?([1-9]|[0-9][0-9]|[0-9][0-9][0-9])\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([0-9][1-9]|[0-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?");
        //URL Regex from https://mathiasbynens.be/demo/url-regex look to improve and rewrite later

        public PBScrape(string Keyword, int Id = 0, string Url = "URL not set", string Email = "Email not set", string Phone = "Phone not set")
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
        
        public List<string> GetUrls()
        {
            return _urls;
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
        public object ParseDiv(string url)
        {
            string html = url;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            //Rewrite to write each div to list
            var node = htmlDoc.DocumentNode.SelectSingleNode("//body");
            return node;
        }

        //Parse Title
        public object GetTitleHtml(string url)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            string urlResponse = URLRequest(url);
            htmlDoc.LoadHtml(urlResponse);
            var titleNode = htmlDoc.DocumentNode.SelectNodes("//title");
            return titleNode[0].InnerText;
        }
        //Match Regex


        //Request Url with Method and timeout
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

        //Get Google Results
        public object GetGoogleResults(string Keyword)
        {
            var customSearchGet = new CustomsearchService(new Google.Apis.Services.BaseClientService.Initializer { ApiKey = _api });
            string query = Keyword;
            var listRequest = customSearchGet.Cse.List(query);
            listRequest.Cx = _searchEngineId;
            IList<Google.Apis.Customsearch.v1.Data.Result> Results = new List<Google.Apis.Customsearch.v1.Data.Result>();
            byte count = 0;
            try 
            { 
                while(Results != null)
                {
                    listRequest.Start = count * 10 + 1;
                    Results = listRequest.Execute().Items;
                    if (Results != null)
                        foreach (var item in Results)
                            _urls.Add(item.Link);
                    count++;
                }
                return _urls;
            }
            catch (Exception ex)
            {
                return ex; 
            }
        }

        public void SaveURLInstanceList(List<string> url)
        {
            //Method to be run over each element of _urls list
            
        }
        public void FindAndSetEmail(List<string> body)
        {
            //Method to use regex over body list
        }
        public void FindAndSetPhone(List<string> body)
        {
            //Method to use regex over body list
        }
    }
}
