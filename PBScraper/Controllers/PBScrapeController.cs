using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using PBScraper.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace PBScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PBScrapeController : ControllerBase
    {
        public readonly string _emailRegex = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$";
        public readonly string _phoneRegex = @"(?:(?:\+?([1-9]|[0-9][0-9]|[0-9][0-9][0-9])\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([0-9][1-9]|[0-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?";
        // GET: api/PBScrape
        [HttpGet]
        public string Get()
        {
            List <PBScrape> allScrapes = PBScrape.GetAll();
            Dictionary <string, string> allKeys = new Dictionary <string, string>();
            for (int i = 0; i < allScrapes.Count; i++)
            {
                allKeys.Add("id", allScrapes[i].GetId().ToString());
                allKeys.Add("keyword", allScrapes[i].GetKeyword());
                allKeys.Add("url", allScrapes[i].GetUrl());
                allKeys.Add("email", allScrapes[i].GetEmail());
                allKeys.Add("phone", allScrapes[i].GetPhone());

            }
            string json = JsonConvert.SerializeObject(allKeys);
            return json;
        }

        // GET: api/PBScrape/5
        [HttpGet("{keyword}", Name = "Get")]
        public string Get(string keyword)
        {
            
            return "value";
        }

        // POST: api/PBScrape
        [HttpPost]
        public void Post([FromBody] string keyword)
        {
            PBScrape newScrape = new PBScrape();
            List<object> newList = new List<object> { newScrape.GetGoogleResults(keyword) };
            foreach(var url in newList)
            {
                List<string> parsedDiv = new List<string> { newScrape.ParseDiv(url.ToString()).ToString() };
                bool emailMatchBool = false;
                bool phoneMatchBool = false;
                foreach(var div in parsedDiv)
                {
                    //Email
                    string emailPattern = _emailRegex;
                    string phonePattern = _phoneRegex;
                    Match emailMatch = Regex.Match(div, emailPattern);
                    Match phoneMatch = Regex.Match(div, phonePattern);
                    if(emailMatchBool == false)
                    {
                        if (emailMatch.Success)
                        {
                            emailMatchBool = true;
                            newScrape.SetEmail(emailMatch.Value);
                        }
                        else
                        {
                            newScrape.SetEmail("No Match Found");
                        }
                    }
                    if(phoneMatchBool == false)
                    {
                        if (phoneMatch.Success)
                        {
                            phoneMatchBool = true;
                            newScrape.SetPhone(phoneMatch.Value);
                        }
                        else
                        {
                            newScrape.SetPhone("No Match Found");
                        }
                    }


                }

            }

        }

        // PUT: api/PBScrape/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
