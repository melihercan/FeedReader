using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileAuthController : ControllerBase
    {
        const string callbackScheme = "feedreader";

        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly IConfiguration _configuration;

        public MobileAuthController(IAuthenticationSchemeProvider authenticationSchemeProvider, IConfiguration configuration)
        {
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _configuration = configuration;
        }

        [HttpGet("{scheme}")]
        public async Task Get([FromRoute] string scheme)
        {
            var auth = await Request.HttpContext.AuthenticateAsync(scheme);

            if (!auth.Succeeded
                || auth?.Principal == null
                || !auth.Principal.Identities.Any(id => id.IsAuthenticated)
                || string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
            {
                // Not authenticated, challenge.
                await Request.HttpContext.ChallengeAsync(scheme);
            }
            else
            {
                var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;
                string email = string.Empty;
                email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
                // Get parameters to send back to the callback.
                var qs = new Dictionary<string, string>
                {
                    { "access_token", auth.Properties.GetTokenValue("access_token") },
                    { "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
                    { "expires", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
                    { "email", email }
                };

                // Build the result URL.
                var url = callbackScheme + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                // Redirect to final URL.
                Request.HttpContext.Response.Redirect(url);
            }
        }

        [HttpGet()]
        public async Task<string[]> Get()
        {
            var schemeProviders = await _authenticationSchemeProvider.GetAllSchemesAsync();
            var schemes = new List<string> { };
            foreach (var schemeProvider in schemeProviders)
            {
                var name = schemeProvider.Name;
                if (name.StartsWith("Identity") || name.StartsWith("idsrv"))
                {
                    continue;
                }
                schemes.Add(name);
            }

            return schemes.ToArray();
        }


        [HttpPost]
        public async Task<ActionResult<Token>> ExchangeToken([FromBody]string externalToken)
        {
            var httpClient = new HttpClient();
            var oidcUrl = "https://localhost:44392";
            //var baseUrl = "https://192.168.1.40:5001"; //// TODO; for iOS debugging solve SSL problem.
            //var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            //// TODO: Check if there is a way to access identity server directly instead of using end points (HTTP)?
            var discovery = await httpClient.GetDiscoveryDocumentAsync(oidcUrl);
            if (!discovery.IsError)
            {
                try
                {
                    var tokenResponse = await httpClient.RequestTokenAsync(new TokenRequest
                    {
                        Address = discovery.TokenEndpoint,
                        ClientId = _configuration["NonWebUiClient:Id"],
                        ClientSecret = _configuration["NonWebUiClient:Secret"],
                        GrantType = "",
                        Parameters =
                    {
                        { "scope", "Infrastructure.ServerAPI"},
                        { "provider", "google"},
                        { "email", "melihercan-google@gmail.com"},
                        { "external_token", "externalToken"},
                    }
                    });

                //var r = await httpClient.PostAsJsonAsync .PostAsync(discovery.TokenEndpoint, HttpContent. { });

                    if (!tokenResponse.IsError)
                    {
                        return this.ToActionResult(Result<Token>.Success(new Token
                        {
                            AccessToken = tokenResponse.AccessToken,
                            RefreshToken = tokenResponse.RefreshToken,
                            //AccessTokenExpiresIn = tokenResponse.ExpiresIn,
                        }));
                    }
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }

            }


            return this.ToActionResult(Result<Token>.Error("Put error message here"));

        }


    }
}
