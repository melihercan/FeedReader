using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebUi.Models
{
    public class FeedUrl
    {
        [Required]
        public string Url { get; set; }
    }
}
