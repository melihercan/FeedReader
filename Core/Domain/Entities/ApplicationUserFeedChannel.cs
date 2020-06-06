using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationUserFeedChannel
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int FeedChannelId { get; set; }
        public FeedChannel FeedChannel { get; set; }
    }
}
