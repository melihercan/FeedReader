using Application.Interfaces;
using Ardalis.Result;
using Domain.Entities;
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
    public class LoginHandler : IRequestHandler<Login, Result<Token>>
    {
        private readonly ILogger<LoginHandler> _logger;
        private readonly IUserAccount _userAccount;

        public LoginHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<LoginHandler>>();
            _userAccount = ServiceCollectionExtension.ServiceProvider.GetService<IUserAccount>();
        }

        public async Task<Result<Token>> Handle(Login request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userAccount.LoginAsync(request.User);
            }
            catch (Exception ex)
            {
                return Result<Token>.Error(ex.Message);
            }
        }
    }
}
