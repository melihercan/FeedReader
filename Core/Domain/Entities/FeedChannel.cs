using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FeedChannel
    {
        public int FeedChannelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }

        public List<FeedItem> FeedItems { get; set; } = new List<FeedItem>();

        public List<ApplicationUserFeedChannel> ApplicationUsersLink { get; set; }
    }
}
