using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, IEnumerable<FeedChannel>>
    {
        public async Task<IEnumerable<FeedChannel>> Handle(GetAllFeeds request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;

        }
    }


}
