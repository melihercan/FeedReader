﻿using System;
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
        private static int _id = 0;

        //string[] imageUrls =
        //{
        //    "https://icons.feedercdn.com/www.bbc.co.uk",
        //    "https://i4.hurimg.com/i/hurriyet/75/900x350/5ebad82a18c773176020a28b.jpg"
        //};
        //static int index = 0;

        // GET: api/FeedSource
        [HttpGet]
        public FeedChannel Get(string url)
        {
            var syndicationFeed = SyndicationFeed.Load(XmlReader.Create(url));
            var feedChannel = new FeedChannel
            {
                Id = _id++,
                Title = syndicationFeed.Title.Text,
                Description = syndicationFeed.Description.Text,
                Link = syndicationFeed.Links[0].Uri.AbsoluteUri,
                ImageUrl = /*imageUrls[index++ % 2],*/ syndicationFeed.ImageUrl?.ToString(),
                FeedItems = syndicationFeed.Items.Select(item => new FeedItem
                {
                    Id = _id++,
                    IsRead = false,
                    Title = item.Title.Text,
                    Description = item.Summary.Text,
                    Link = item.Links[0].Uri.AbsoluteUri,
                    PublishDate = item.PublishDate.DateTime,
                    ImageUrl = null, //item.Links.

                    //// TODO; GET FEED IMAGE????
                    ///
                }).ToList()
            };

            return feedChannel;
        }
    }
}
