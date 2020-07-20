using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TokenRepository
{
    internal static class TokenRepositoryFactory
    {
        internal static ITokenRepository GetTokenRepository(string ui)
        {
            ITokenRepository tokenRepository = ui switch
            {
                Ui.Console => new FileToken(),
                Ui.Desktop => new FileToken(),
                Ui.Mobile => new MobileToken(),
                Ui.Web => new FileToken(),
                _ => throw new Exception($"No token repository found for the selected UI: {ui}"),
            };

            return tokenRepository;
        }
    }
}
