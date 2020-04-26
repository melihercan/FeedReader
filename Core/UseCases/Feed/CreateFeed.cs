using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases.Feed
{
    public class CreateFeed : IRequest
    {
        public string FeedUrl { get; set; }
    }

    public class CreateFeedHandler<CreateFeed>
    {
        public async Task Handle(CreateFeed request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }

    public class CreateFeedCommandValidator : AbstractValidator<CreateFeed>
    {
        public CreateFeedCommandValidator()
        {
        }
    }
}
