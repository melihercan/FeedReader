using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserAccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);


            return this.ToActionResult(Result<Token>.Success(null));
        }


    }
}
