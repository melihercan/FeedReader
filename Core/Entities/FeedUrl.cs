using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class FeedUrl
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public IList<FeedChannel> FeedChannels { get; set; }
    }
}
