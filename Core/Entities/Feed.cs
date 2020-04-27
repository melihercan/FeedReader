using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace Core.Entities
{
    public class Feed
    {
        public int Id { get; set; }

        public SyndicationFeed SyndicationFeed { get; set; }

    }
}
