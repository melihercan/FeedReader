using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed>
    {
        public async Task<Unit> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new Unit();
        }
    }
}
