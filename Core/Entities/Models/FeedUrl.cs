using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class FeedUrl : BaseModel
    {
        public string Link { get; set; }
        public IList<FeedChannel> FeedChannels { get; set; }
    }
}
