﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class FeedItem : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public FeedChannel FeedChannel { get; set; }
    }
}
