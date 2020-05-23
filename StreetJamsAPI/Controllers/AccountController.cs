using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StreetJams.Data;
using StreetJamsAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreetJamsAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest();

            await signInManager.SignInAsync(user, isPersistent: false);

            return Ok(CreateToken(user));
        }

        private UserToken CreateToken(AppUser user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is a secrete phrase"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signinCredentials, claims: claims);

            var res = new UserToken
            {
                Tkn = new JwtSecurityTokenHandler().WriteToken(jwt)
            };

            return res;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Credentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);

            if (!result.Succeeded)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(credentials.Email);

            return Ok(CreateToken(user));
        }


    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }


    public class UserToken
    {
        public string Tkn { get; set; }
    }
}
