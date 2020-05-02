using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class RemoveFeedValidator : AbstractValidator<RemoveFeed>
    {
        public RemoveFeedValidator()
        {
            RuleFor(removeFeed => removeFeed.Id).Must(id =>
            {
                //// TODO: Validate feed
                return true;
            }).WithMessage("Invalid feed");
        }
    }
}
