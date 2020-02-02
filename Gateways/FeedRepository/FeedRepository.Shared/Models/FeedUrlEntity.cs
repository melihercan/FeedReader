using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeedRepository.Shared.Models
{
    public class FeedUrlEntity : FeedUrl
    {
        public int TenantId { get; set; }
    }
}
