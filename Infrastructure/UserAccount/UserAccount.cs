using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Domain.Entities;
using Ardalis.Result;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using System.Runtime.InteropServices.WindowsRuntime;
using IdentityModel.OidcClient;
using InfrastructureUserAccount;
using Microsoft.Extensions.Configuration;
using IdentityModel.Client;

namespace Infrastructure
{
    public class UserAccount : IUserAccount
    {
        private readonly ILogger<UserAccount> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserAccount()
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<UserAccount>>();
            _configuration = Registry.ServiceProvider.GetService<IConfiguration>();
            _httpClient = Registry.ServiceProvider.GetService<HttpClient>();
        }

        public Task<Result<object>> RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UnregisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Token>> LoginAsync(User user)
        {
            //var discovery = await _httpClient.GetDiscoveryDocumentAsync($"{_httpClient.BaseAddress}");

            var response = await _httpClient.PostAsJsonAsync("api/UserAccount/login", user);
            var token = await response.Content.ReadFromJsonAsync<Token>();// .ReadAsStringAsync();
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token.AccessToken);

            //try
            //{
            //    var tokenResponse = await _httpClient.RequestTokenAsync(new TokenRequest
            //    {
            //        Address = _httpClient.BaseAddress + "connect/token",
            //        ClientId = "WebUi.Client",
            //        ClientSecret = user.Password,
            //        GrantType = "client_credentials"
            //    });
            //}
            //catch(Exception ex)
            //{
            //    var x = ex.Message;
            //}

            //return null;

            return token;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetAuthenticationSchemesAsync()
        {
            return await _httpClient.GetFromJsonAsync<string[]>("api/MobileAuth");
        }

        public async Task<Result<Token>> ExternalLoginAsync(string scheme)
        {
#if false
            var response = await _httpClient.PostAsJsonAsync("api/UserAccount/externallogin", Constants.RedirectUri);
            var token = await response.Content.ReadFromJsonAsync<Token>();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.AccessToken);
#endif

#if true
            Token token = null;
            var serverUrl = "https://10.0.2.2:44392/";// _configuration["Server:URL"];
            ////var discovery = await _httpClient.GetDiscoveryDocumentAsync(serverUrl);

            //var webAuthenticatorUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/MobileAuth/";
            var webAuthenticatorUrl = $"{serverUrl}api/MobileAuth/";


            var options = new OidcClientOptions
            {
                Authority = serverUrl, //$"{_httpClient.BaseAddress.AbsoluteUri}", //Constants.AuthorityUri,
                ClientId = "openid profile Infrastructure.ServerAPI",// "NonWebUi.Client", // Constants.ClientId,
                //ClientSecret = "NonWebUi.Secret",
                RedirectUri = Constants.RedirectUri,
                Scope = "openid profile email api offline_access",//"openid profile Infrastructure.ServerAPI", //"openid Infrastructure.ServerAPI",//Constants.Scope,
//                FilterClaims = false,
                Browser = new InfrastructureUserAccount.Browser(new Uri(webAuthenticatorUrl + scheme).AbsoluteUri),
//                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
            };





            //var redirectUri = new Uri(authenticationUrl + scheme).AbsoluteUri;
            //var redirectUri = "io.identitymodel.native://callback";

            //var options = new OidcClientOptions
            //{
            //    Authority = $"{_httpClient.BaseAddress.AbsoluteUri}",//;authenticationUrl,
            //    //// TODO: TESTING NOW, MOVE THIS CODE TO SERVER, CLIENT CANNOT OWN client id and secret!!!
            //    ClientId = "NonWebUi.Client",//"371183173448-7aeu2jrcs4tvivmob75ar03q6qnp2rbt.apps.googleusercontent.com",
            //    //ClientSecret = "NonWebUi.Secret", // "P88aFDgv7GLhMr4W2JbBMyKk",
            //    Scope = "openid profile email api offline_access", //"Infrastructure.ServerAPI",
            //    RedirectUri = redirectUri,
            //    ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
            //    Browser = new InfrastructureUserAccount.Browser(new Uri(authenticationUrl + scheme).AbsoluteUri)
            //};

            var oidcClient = new OidcClient(options);
            var loginResult = await oidcClient.LoginAsync(new LoginRequest());
            if (loginResult.IsError)
            {
                Console.WriteLine("ERROR: {0}", loginResult.Error);
                return null;
            }


            try
            {
                //WebAuthenticatorResult r = null;

                //if (scheme.Equals("Apple")
                //    && DeviceInfo.Platform == DevicePlatform.iOS
                //    && DeviceInfo.Version.Major >= 13)
                //{
                //    r = await AppleSignInAuthenticator.AuthenticateAsync();
                //}
                //else
                //{
                //    var authUrl = new Uri(authenticationUrl + scheme);
                //    var callbackUrl = new Uri("feedreader://");

                //    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                //}

                token = new Token
                {
                    //AccessToken = r?.AccessToken ?? r?.IdToken
                    AccessToken = loginResult.AccessToken 
                };
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
            catch (Exception ex)
            {

            }
#endif
            return token;
        }



    }

    public static class Constants
    {
        public static string AuthorityUri = "https://demo.identityserver.io";
        ////        public static string RedirectUri = "io.identitymodel.native://callback";
        public static string RedirectUri = "feedreader://";
        public static string ApiUri = "https://demo.identityserver.io/api/";
        public static string ClientId = "interactive.public";
        public static string Scope = "openid profile email api offline_access";
    }
}
