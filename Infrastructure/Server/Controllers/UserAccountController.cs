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

        public UserAccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<object> Register([FromBody] User user)
        {
            return null;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Token>> Login([FromBody] User user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.Username);
            if (identityUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, 
                    false);
                if (result.Succeeded)
                {
                    var token = _tokenService.GenerateAccessToken(new List<Claim>
                    { 
                        new Claim(ClaimTypes.Name, user.Username)
                    });

                    return this.ToActionResult(Result<Token>.Success(new Token { AccessToken = token }));

                }
            }
            return this.ToActionResult(Result<Token>.Error("Put error message here"));
        }
    }
}
