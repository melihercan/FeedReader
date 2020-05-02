using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class RefreshFeedValidator : AbstractValidator<RefreshFeed>
    {
        public RefreshFeedValidator()
        {
            RuleFor(refreshFeed => refreshFeed.Id).Must(id =>
            {
                //// TODO: Validate feed
                return true;
            }).WithMessage("Invalid feed");
        }
    }
}
