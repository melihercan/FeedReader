using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class AddFeed : IRequest
    {
        public string Url { get; set; }
    }
}
