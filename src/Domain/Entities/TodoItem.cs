﻿using FeedReader.Domain.Common;
using FeedReader.Domain.Enums;
using System;

namespace FeedReader.Domain.Entities
{
    public class TodoItem : AuditableEntity
    {
        public long Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }


        public TodoList List { get; set; }
    }
}
