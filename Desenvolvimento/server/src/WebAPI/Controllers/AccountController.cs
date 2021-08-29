using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Entities.Identity;
using Infrastructure.Data;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppLogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAppLogger<AccountController> logger,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromHeader] string username, [FromHeader] string password)
        {
            var model = new LoginViewModel
            {
                Username = username,
                Password = password
            };

            var userIdentity = await _userManager.FindByNameAsync(model.Username);

            if (userIdentity == null)
                throw new Exception("Login ou Password inválido");

            var resultPasswordSignIn = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!resultPasswordSignIn.Succeeded)
                throw new Exception("Login ou Password inválido");

            var result = new { token = await GenerateSecurityTokenByPayload(userIdentity) };

            return Ok(result);
        }

        private async Task<string> GenerateSecurityTokenByPayload(User userIdentity)
        {
            var user = new Dictionary<string, object>
            {
                { "id", userIdentity.Id },
                { "userName", userIdentity.UserName },
                { "email", userIdentity.Email },
            };

            var roles = await _userManager.GetRolesAsync(userIdentity);
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenAuthentication:SecretKey").Value));
            var key = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(_configuration.GetSection("TokenAuthentication:SecretKey").Value));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var iss = _configuration.GetSection("TokenAuthentication:Issuer").Value;
            var aud = _configuration.GetSection("TokenAuthentication:Audience").Value;
            var nbf = DateTime.UtcNow;
            var exp = DateTime.UtcNow.AddHours(8);
            var iat = DateTime.UtcNow;
            var payload = new JwtPayload(iss, aud, new List<Claim>(), nbf, exp, iat)
            {
                { "roles", roles },
                { "user", user }
            };

            var jwtToken = new JwtSecurityToken(new JwtHeader(signingCredentials), payload);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return jwtTokenHandler.WriteToken(jwtToken);
        }


        [HttpPost("signout")]
        public async Task<ActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.ROLE_USER);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }
                AddErrors(result);
            }
            return BadRequest(GenerateModalStateClientError());
        }
    }
}
