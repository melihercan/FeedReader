using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebUi.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SyndicationFeedController : ControllerBase
    {


        // GET: api/SyndicationFeed


        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<SyndicationFeedController> logger;

        //public SyndicationFeedController(ILogger<SyndicationFeedController> logger)
        //{
        //    this.logger = logger;
        //}

        //[HttpGet]
        //public string Get(string url)
        //{
        //    var rng = new Random();
        //    return JsonConvert.SerializeObject( /*Enumerable.Range(1, 5).Select(index =>*/ new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(0),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    /*.ToArray()*/;
        //}


        [HttpGet]
        public string Get(string url)
        {
            // Returning feed itself throws error. Therefore we convert into JSON first.
            var feed = SyndicationFeed.Load(XmlReader.Create(url));
            var json = JsonConvert.SerializeObject(feed);
            return json;
        }
    }
}
