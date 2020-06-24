using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Application.Interfaces;

namespace Application.UseCases
{
    public class RemoveFeedHandler : IRequestHandler<RemoveFeed, Result<object>>
    {
        private readonly ILogger<RemoveFeedHandler> _logger;
        private readonly IFeedRepository _feedRepository;

        public RemoveFeedHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<RemoveFeedHandler>>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<object>> Handle(RemoveFeed request, CancellationToken cancellationToken)
        {
            try
            {
                await _feedRepository.RemoveFeedChannelAsync(request.Id);
                return Result<object>.Success(null);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.Message);
            }
        }
    }
}
