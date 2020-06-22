using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class FeedItem
    {
        public int FeedItemId { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }

        public int FeedChannelId { get; set; }
        
        [JsonIgnore]
        public FeedChannel FeedChannel { get; set; }
    }
}
