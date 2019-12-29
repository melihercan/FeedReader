using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Feed.Commands.CreateFeed
{
    public class CreateFeedCommandValidator : AbstractValidator<CreateFeedCommand>
    {
        public CreateFeedCommandValidator()
        {
        }
    }
}
