using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Infrastructure.Server.Data;
using Domain.Entities;
using System.Security.Claims;
using Infrastructure.Server.Services;
using Infrastructure.Server.Interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Infrastructure.Server
{
    public class Startup
    {
        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.Clients.Add(new Client
                    {
                        ClientId = _configuration["NonWebUiClient:Id"],
                        ClientSecrets = { new Secret(_configuration["NonWebUiClient:Secret"].ToSha256()) },
                        AllowedGrantTypes = { GrantType.ResourceOwnerPassword, "delegation" },
                        AllowedScopes = { "openid profile Infrastructure.ServerAPI offline_access" }
                        //                        AllowedScopes = { "Infrastructure.ServerAPI" }
                    });
                })
  //.AddTokenExchangeForExternalProviders()  //registers an extension grant
  //.AddDefaultTokenExchangeProviderStore()  //registers default in-memory store for providers info
  //.AddDefaultExternalTokenProviders()      //registers providers auth implementations
  //.AddDefaultTokenExchangeProfileService() //registers default profile service
  //.AddDefaultExternalUserStore();          //

                .AddDelegationGrant<ApplicationUser, string>()   // Register the extension grant 
                .AddDefaultSocialLoginValidators(); // Add google, facebook, twitter login support
                ;

            services.AddAuthentication(
                //                options => 
                //              {
                //                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //        }
                )
                //      .AddJwtBearer(options =>
                //    {
                //      var tokenSecret = _configuration["TokenSecret"];
                //    var key = Encoding.UTF8.GetBytes(tokenSecret);
                //  var symmetricKey = new SymmetricSecurityKey(key);
                //                    options.TokenValidationParameters = new TokenValidationParameters
                //                  {
                //                    ValidateIssuer = false,
                //                  ValidateAudience = false,
                //                ValidateIssuerSigningKey = true,
                //              IssuerSigningKey = symmetricKey
                //        };
                //})
                ////.AddCookie()
                .AddGoogle(g =>
                {
                    g.ClientId = _configuration["Authentication:Google:ClientId"];
                    g.ClientSecret = _configuration["Authentication:Google:ClientSecret"];
                    g.SaveTokens = true;
                })
                .AddMicrosoftAccount(ms =>
                {
                    ms.ClientId = _configuration["Authentication:Microsoft:ClientId"];
                    ms.ClientSecret = _configuration["Authentication:Microsoft:ClientSecret"];
                    ms.SaveTokens = true;
                })
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton<ITokenService, TokenService>();

            // Otherwise UserManager.GetUserAsync(User) returns null.
            services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

        }
    }
}
