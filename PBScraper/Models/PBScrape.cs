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
using MySql;
using MySql.Data.MySqlClient;

namespace PBScraper.Models
{
    public class PBScrape
    {
        private int _id;
        private string _keyword;
        private string _url;
        private string _email;
        private string _phone;
        private bool _foundNumber;
        private bool _foundEmail;
        private static List<PBScrape> _allScrapesStatic = new List<PBScrape> { };
        private List<string> _urls = new List<string> { };
        private readonly string _api = "AIzaSyAwaNkJAWCWn6lzvglnRbqtS1y7tbNUJSY";
        private readonly string _searchEngineId = "015153167064412439961:9t3cwc_ifrm";
        public Regex _findNumber = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$");
        public Regex _findEmail = new Regex(@"(?:(?:\+?([1-9]|[0-9][0-9]|[0-9][0-9][0-9])\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([0-9][1-9]|[0-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?");
        //URL Regex from https://mathiasbynens.be/demo/url-regex look to improve and rewrite later

        public PBScrape(string Keyword = "Bongos", int Id = 0, string Url = "https://Bongos.com", string Email = "Bongos@Bongos.Com", string Phone = "333-333-3333")
        {
            _id = Id;
            _keyword = "Bongos";
            _url = "https://Bongos.com";
            _email = "Bongos@Bongos.Com";
            _phone = "333-333-3333";
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
        public void SetEmailTrue()
        {
            _foundEmail = true;
        }
        public void SetEmailFalse()
        {
            _foundEmail = false;
        }
        public void SetPhoneTrue()
        {
            _foundNumber = true;
        }
        public void SetPhoneFalse()
        {
            _foundNumber = false;
        }

        public void Save()
        {
            List<PBScrape> allData = PBScrape.GetAll();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO pbscrape ( keyword, url, email, phone)
            VALUES ( @keyword, @url, @email, @phone);";
            cmd.Parameters.Add(new MySqlParameter("@keyword", this._keyword));
            cmd.Parameters.Add(new MySqlParameter("@url", this._url));
            cmd.Parameters.Add(new MySqlParameter("@email", this._email));
            cmd.Parameters.Add(new MySqlParameter("@phone", this._phone));
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            //write get all
        }

        public List<PBScrape> GetInstanceData()
        {
            return _allScrapesStatic;
        }
        public static List<PBScrape> GetAll()
        {
            List<PBScrape> allData = new List<PBScrape> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM pbscrape;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int instanceId = rdr.GetInt32(0);
                string instanceKeyword = rdr.GetString(1);
                string instanceURL = rdr.GetString(2);
                string instancePhone = rdr.GetString(3);
                string instanceEmail = rdr.GetString(4);
                PBScrape newInstance = new PBScrape(instanceKeyword, instanceId, instanceURL, instanceEmail, instancePhone);
                allData.Add(newInstance);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            //write get all
            return allData;
        }
        //Parse HTML from given url
        public object ParseDiv(string url)
        {
            string html = url;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            //Rewrite to write each div to list
            var nodes = htmlDoc.DocumentNode.SelectNodes("//div");
            List<string> divlist = new List<string>{ };
            foreach (var node in nodes)
            {
                divlist.Add(node.ToString());
            }
            return divlist;
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
                while (Results != null)
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
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM pbscrape;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}
