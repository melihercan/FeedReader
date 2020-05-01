using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FeedChannel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IEnumerator<FeedItem> FeedItems { get; set; }
        ////public Feed Feed { get; set; }
    }
}
