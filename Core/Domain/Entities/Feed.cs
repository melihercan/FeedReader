using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace Domain.Entities
{
    public class Feed
    {
        public const string Url = "url";

        public int Id { get; set; }
        public FeedChannel FeedChannel { get; set; }
        public SyndicationFeed SyndicationFeed { get; set; }
    }
}
