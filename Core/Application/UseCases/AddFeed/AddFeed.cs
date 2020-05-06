using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class AddFeed : IRequest<Result<FeedChannel>>
    {
        public string Url { get; set; }
    }
}
