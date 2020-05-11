using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedSourceController : ControllerBase
    {
        // GET: api/FeedSource
        [HttpGet]
        public FeedChannel Get(string url)
        {
            var syndicationFeed = SyndicationFeed.Load(XmlReader.Create(url));
            var feedChannel = new FeedChannel
            {
                Id = 0,
                Title = syndicationFeed.Title.Text,
                Description = syndicationFeed.Description.Text,
                Link = syndicationFeed.Links[0].Uri.AbsoluteUri,
                FeedItems = syndicationFeed.Items.Select(item => new FeedItem
                {
                    Id = 0,
                    Title = item.Title.Text,
                    Description = item.Summary.Text,
                    Link = item.Links[0].Uri.AbsoluteUri,
                }).ToList()
            };

            return feedChannel;
        }
    }
}
