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
    public class RegisterHandler : IRequestHandler<Register, Result<object>>
    {
        private readonly ILogger<RegisterHandler> _logger;
        private readonly IUserAccount _userAccount;

        public RegisterHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<RegisterHandler>>();
            _userAccount = ServiceCollectionExtension.ServiceProvider.GetService<IUserAccount>();
        }

        public async Task<Result<object>> Handle(Register request, CancellationToken cancellationToken)
        {
            try
            {
                await _userAccount.RegisterAsync(request.User);
                return Result<object>.Success(null);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.Message);
            }
        }
    }
}
