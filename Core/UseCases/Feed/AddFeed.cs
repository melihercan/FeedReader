using MediatR;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases.Feed
{
    public class AddFeed : IRequest<IObservable<SyndicationFeed>>
    {
        public string Url { get; set; }
    }

    public class AddFeedHandler : IRequestHandler<AddFeed, IObservable<SyndicationFeed>>
    {
        public async Task<IObservable<SyndicationFeed>> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}
