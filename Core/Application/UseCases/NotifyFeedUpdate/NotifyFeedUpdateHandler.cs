using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class NotifyFeedUpdateHandler : IRequestHandler<NotifyFeedUpdate, Result<IObservable<FeedChannel>>>
    {
        public Task<Result<IObservable<FeedChannel>>> Handle(NotifyFeedUpdate request, 
            CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
