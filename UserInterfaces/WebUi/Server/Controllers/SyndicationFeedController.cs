using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SyndicationFeedController : ControllerBase
    {
        // GET: api/SyndicationFeed
        [HttpGet]
        public SyndicationFeed Get(string url)
        {
            return SyndicationFeed.Load(XmlReader.Create(url));
        }
    }
}
