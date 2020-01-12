using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class FeedChannel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IList<FeedItem> FeedItems { get; set; }
        public FeedUrl FeedUrl { get; set; }

    }
}
