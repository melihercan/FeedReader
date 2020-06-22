using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class RemoveFeed : IRequest<Result<object>>
    {
        public int Id { get; set; }
    }
}
