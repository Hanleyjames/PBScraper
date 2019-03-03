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
        private static List<string> _urls = new List<string> { };
        public Regex _findEmail = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$");
        public Regex _findNumber = new Regex(@"(?:(?:\+?([1-9]|[0-9][0-9]|[0-9][0-9][0-9])\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([0-9][1-9]|[0-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?");
        public Regex _findUrl = new Regex(@"~(?:\b[a-z\d.-]+://[^<>\s]+|\b(?:(?:(?:[^\s!@#$%^&*()_=+[\]{}\|;:',.<>/?]+)\.)+(?:ac|ad|aero|ae|af|ag|ai|al|am|an|ao|aq|arpa|ar|asia|as|at|au|aw|ax|az|ba|bb|bd|be|bf|bg|bh|biz|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|cat|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|coop|com|co|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|edu|ee|eg|er|es|et|eu|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gg|gh|gi|gl|gm|gn|gov|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|info|int|in|io|iq|ir|is|it|je|jm|jobs|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|mil|mk|ml|mm|mn|mobi|mo|mp|mq|mr|ms|mt|museum|mu|mv|mw|mx|my|mz|name|na|nc|net|ne|nf|ng|ni|nl|no|np|nr|nu|nz|om|org|pa|pe|pf|pg|ph|pk|pl|pm|pn|pro|pr|ps|pt|pw|py|qa|re|ro|rs|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sv|sy|sz|tc|td|tel|tf|tg|th|tj|tk|tl|tm|tn|to|tp|travel|tr|tt|tv|tw|tz|ua|ug|uk|um|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|xn--0zwm56d|xn--11b5bs3a9aj6g|xn--80akhbyknj4f|xn--9t4b11yi5a|xn--deba0ad|xn--g6w251d|xn--hgbk6aj7f53bba|xn--hlcj6aya9esc7a|xn--jxalpdlp|xn--kgbechtv|xn--zckzah|ye|yt|yu|za|zm|zw)|(?:(?:[0-9]|[1-9]\d|1\d{2}|2[0-4]\d|25[0-5])\.){3}(?:[0-9]|[1-9]\d|1\d{2}|2[0-4]\d|25[0-5]))(?:[;/][^#?<>\s]*)?(?:\?[^#<>\s]*)?(?:#[^<>\s]*)?(?!\w))~iS");
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
        //Parse Title
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
