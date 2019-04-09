using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBScraper.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace PBScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PBScrapeController : ControllerBase
    {
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
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PBScrape
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
