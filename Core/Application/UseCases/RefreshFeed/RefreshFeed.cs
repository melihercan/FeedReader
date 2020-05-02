using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class RefreshFeed : IRequest<FeedChannel>
    {
        public int Id { get; set; }
    }

}
