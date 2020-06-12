using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RemoveFeedHandler : IRequestHandler<RemoveFeed, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RemoveFeed request, CancellationToken cancellationToken)
        {
            var validator = new RemoveFeedValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {

            }

            await Task.CompletedTask;
            return Result<bool>.Success(true);
        }
    }
}
