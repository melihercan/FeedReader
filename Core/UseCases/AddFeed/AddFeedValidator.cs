using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class AddFeedValidator : AbstractValidator<AddFeed>
    {
        public AddFeedValidator()
        {
        }
    }
}
