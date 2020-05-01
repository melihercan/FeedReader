using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
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
