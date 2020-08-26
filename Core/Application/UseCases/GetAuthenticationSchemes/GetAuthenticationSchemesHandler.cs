using Application.Interfaces;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Application.UseCases
{
    public class GetAuthenticationSchemesHandler : IRequestHandler<GetAuthenticationSchemes, Result<string[]>>
    {
        private readonly ILogger<GetAuthenticationSchemesHandler> _logger;
        private readonly IUser _user;

        public GetAuthenticationSchemesHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<GetAuthenticationSchemesHandler>>();
            _user = ServiceCollectionExtension.ServiceProvider.GetService<IUser>();
        }

        public async Task<Result<string[]>> Handle(GetAuthenticationSchemes request, 
            CancellationToken cancellationToken)
        {
            try
            {
                return Result<string[]>.Success(await _user.GetAuthenticationSchemesAsync());
            }
            catch(Exception ex)
            {
                _logger.LogError($"#### E X C E P T I O N : {ex.Message}");
                return Result<string[]>.Error(ex.Message);
            }
        }
    }
}
