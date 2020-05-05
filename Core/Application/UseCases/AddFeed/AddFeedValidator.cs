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
                return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }).WithMessage("Not a valid URL");
        }
    }
}
