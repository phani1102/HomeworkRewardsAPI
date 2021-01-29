using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WorkRewardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;

        public LoginController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Validate(string username,string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                //var user = await GetUser(username, password);

                //if (user != null)
                //{
                //create claims details based on the user information
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    //new Claim("Id", user.UserId.ToString()),
                    //new Claim("FirstName", user.FirstName),
                    //new Claim("LastName", user.LastName),
                    new Claim("UserName", username),
                    new Claim("Password", password),
                    //new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                 return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                //}
                //else
                //{
                //    return BadRequest("Invalid credentials");
                //}
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
