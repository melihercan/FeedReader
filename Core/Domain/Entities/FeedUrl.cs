using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FeedUrl
    {
        public string Link { get; set; }
        public IList<FeedChannel> FeedChannels { get; set; }

    }
}
