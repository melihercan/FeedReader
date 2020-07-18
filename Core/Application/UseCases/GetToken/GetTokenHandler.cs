using Application.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Application.UseCases
{
    public class GetTokenHandler : IRequestHandler<GetToken, Result<Token>>
    {
        private readonly ITokenRepository _tokenRepository;

        public GetTokenHandler()
        {
            _tokenRepository = ServiceCollectionExtension.ServiceProvider.GetService<ITokenRepository>();
        }

        public async Task<Result<Token>> Handle(GetToken request, CancellationToken cancellationToken)
        {
            return Result<Token>.Success(await _tokenRepository.RetrieveAsync());
        }
    }
}
