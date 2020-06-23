using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedSourceController : ControllerBase
    {
        // GET: api/FeedSource
        [HttpGet]
        public ActionResult<FeedChannel> Get(string url)
        {
            try
            {
                var syndicationFeed = SyndicationFeed.Load(XmlReader.Create(url));
                var feedChannel = new FeedChannel
                {
                    Title = syndicationFeed.Title.Text,
                    Description = syndicationFeed.Description.Text,
                    Link = syndicationFeed.Links[0].Uri.AbsoluteUri,
                    ImageUrl = syndicationFeed.ImageUrl?.ToString(),
                    LastUpdateTime = DateTime.Now,
                    FeedItems = syndicationFeed.Items.Select(item => new FeedItem
                    {
                        IsRead = false,
                        Title = item.Title.Text,
                        Description = item.Summary.Text,
                        Link = item.Links[0].Uri.AbsoluteUri,
                        PublishDate = item.PublishDate.DateTime,
                        ImageUrl = item.Links
                            .Where(link => link.MediaType != null && link.MediaType.Contains("image"))
                            .FirstOrDefault()?.Uri.AbsoluteUri,
                    }).ToList()
                };

                return feedChannel;
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
