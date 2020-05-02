using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class RemoveFeed : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
