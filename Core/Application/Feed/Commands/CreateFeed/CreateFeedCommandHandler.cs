using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feed.Commands.CreateFeed
{
    public class CreateFeedCommandHandler : IRequestHandler<CreateFeedCommand>
    {
        public Task<Unit> Handle(CreateFeedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
