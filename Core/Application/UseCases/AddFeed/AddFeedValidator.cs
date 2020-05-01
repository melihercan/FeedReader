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
        }
    }
}
