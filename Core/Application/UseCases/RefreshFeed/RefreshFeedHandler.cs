using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RefreshFeedHandler : IRequestHandler<RefreshFeed, FeedChannel>
    {
        public async Task<FeedChannel> Handle(RefreshFeed request, CancellationToken cancellationToken)
        {
            var validator = new RefreshFeedValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {

            }

            await Task.CompletedTask;
            return null;

        }
    }
}
