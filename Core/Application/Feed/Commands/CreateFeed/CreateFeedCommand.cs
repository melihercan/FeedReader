using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Feed.Commands.CreateFeed
{
    public class CreateFeedCommand : IRequest
    {
        public string FeedUrl { get; set; }

    }
}
