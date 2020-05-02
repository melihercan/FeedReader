using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class AddFeedValidator : AbstractValidator<AddFeed>
    {
        public AddFeedValidator()
        {
            RuleFor(addFeed => addFeed.Url).Must(url =>
            {
                //// TODO: Validate URL
                return true;
            }).WithMessage("Not a valid feed URL");
        }
    }
}
