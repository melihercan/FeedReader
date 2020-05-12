using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FeedItem
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public FeedChannel FeedChannel { get; set; }
    }
}
