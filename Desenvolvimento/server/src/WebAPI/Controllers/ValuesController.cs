using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {

        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("token")]
        public IActionResult Token()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Administrador"),
                    new Claim("roles", "admin"),
                    new Claim("nome", "userData.Nome"),
                    new Claim("emailPrincipal", "userData.EmailPrincipal"),
                    new Claim("cpf", "userData.Cpf"),
                    new Claim("nomeEntidade", "userData.NomeEntidade"),
                    new Claim("nomeGrupoGuardiao", "userData.NomeGrupoGuardiao")
                };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "SegSync");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;

            var test = Base64UrlEncoder.Encode(_configuration.GetSection("TokenAuthentication:SecretKey").Value);

            var key = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(_configuration.GetSection("TokenAuthentication:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("TokenAuthentication:Issuer").Value,
                audience: _configuration.GetSection("TokenAuthentication:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: creds);

            //var resultado = new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token)
            //};

            //Request.HttpContext.Response.Headers.Add("Authorization", resultado.token);

            var resultado = "null"; //new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(resultado);
        }

        // POST api/values/test

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "TEST")]
        [HttpPost("test")]
        public IActionResult Test()
        {
            return Ok();
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return new { id = id, name = $"GET api/auth/{id}" };
        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody] object dummy)
        {
            return dummy;
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        [HttpPut]
        public object Put([FromBody] object dummy)
        {
            return dummy;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            return new { id = id, name = $"DELETE api/auth/{id}" };
        }

        // GET api/values
        [HttpGet("problema")]
        public void GetProblema()
        {
            Task.Delay(3000).Wait();
        }
    }
}
