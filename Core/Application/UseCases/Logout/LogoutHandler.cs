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
    public class LogoutHandler : IRequestHandler<Logout, Result<object>>
    {
        private readonly ILogger<LogoutHandler> _logger;
        private readonly IUserAccount _userAccount;

        public LogoutHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<LogoutHandler>>();
            _userAccount = ServiceCollectionExtension.ServiceProvider.GetService<IUserAccount>();

        }

        public async Task<Result<object>> Handle(Logout request, CancellationToken cancellationToken)
        {
            try
            {
                await _userAccount.LogoutAsync();
                return Result<object>.Success(null);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.Message);
            }
        }
    }
}
