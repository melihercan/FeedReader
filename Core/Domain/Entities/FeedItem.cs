﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public FeedChannel FeedChannel { get; set; }


    }
}
