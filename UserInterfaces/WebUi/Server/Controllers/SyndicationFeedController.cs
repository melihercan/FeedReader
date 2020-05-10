using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyndicationFeedController : ControllerBase
    {
        // GET: api/SyndicationFeed
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SyndicationFeed/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SyndicationFeed
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SyndicationFeed/5
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
