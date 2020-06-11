using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebUi.Models
{
    public class FeedUrl
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
