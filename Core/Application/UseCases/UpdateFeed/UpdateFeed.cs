using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class UpdateFeed : IRequest<Result<FeedChannel>>
    {
        public FeedChannel FeedChannel { get; set; }
    }
}
