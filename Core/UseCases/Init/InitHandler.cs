using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class InitHandler : IRequestHandler<Init, IObservable<FeedChannel>>
    {
        public async Task<IObservable<FeedChannel>> Handle(Init request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}
