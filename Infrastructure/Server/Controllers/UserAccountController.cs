using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Server.Interfaces;
using System.Security.Claims;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using IdentityModel.OidcClient;
using System.IO;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;


        public UserAccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<object>> Register([FromBody] User user)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = user.Username,
            };
            var result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (result.Succeeded)
            {
                return this.ToActionResult(Result<object>.Success(null));
            }

            return this.ToActionResult(Result<Token>.Error(
                result.Errors.Select(x => x.Description).Aggregate((a, b) => a + "; " + b)));
        }

        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult<object>> Logout()
        {
            //            var user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            return this.ToActionResult(Result<object>.Success(null));
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Token>> Login([FromBody] User user)
        {
            var httpClient = new HttpClient();

            var oidcUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            // Android emulator uses hard coded address 10.0.2.2. Replace it with localhost.
            if(oidcUrl.Contains("10.0.2.2"))
            {
                oidcUrl = oidcUrl.Replace("10.0.2.2", "localhost");
            }

            //// TODO: Check if there is a way to access identity server directly instead of using end points (HTTP)?
            var discovery = await httpClient.GetDiscoveryDocumentAsync(oidcUrl);
            if (!discovery.IsError)
            {
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = discovery.TokenEndpoint,
                    ClientId = _configuration["NonWebUiClient:Id"],
                    ClientSecret = _configuration["NonWebUiClient:Secret"],
                    UserName = user.Username,
                    Password = user.Password,
                    Scope = "Infrastructure.ServerAPI",
                });
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

            return this.ToActionResult(Result<Token>.Error("Login failed"));
        }

        [HttpPost]
        [Route("externallogin")]
        public async Task<ActionResult<Token>> ExternalLogin([FromBody] string redirectUri)
        {
            var oidcUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            // Android emulator uses hard coded address 10.0.2.2. Replace it with localhost.
            if (oidcUrl.Contains("10.0.2.2"))
            {
                oidcUrl = oidcUrl.Replace("10.0.2.2", "localhost");
            }

            var options = new OidcClientOptions
            {
                Authority = oidcUrl,
                ClientId = "NonWebUi.Client",
                RedirectUri = redirectUri,
                Scope = "openid profile Infrastructure.ServerAPI",
                FilterClaims = false,
                Browser = new Browser(redirectUri),
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
            };

            var oidcClient = new OidcClient(options);
            var loginResult = await oidcClient.LoginAsync(new LoginRequest());
            if (loginResult.IsError)
            {
                Console.WriteLine("ERROR: {0}", loginResult.Error);
                return null;
            }

            return new Token
            {
                AccessToken = loginResult.AccessToken,
            };
        }
    }
}
