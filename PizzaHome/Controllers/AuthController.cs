using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public ActionResult Authenticate() {

            var claims = new[] {
                new Claim("FullName", "SJKdkas"),
                new Claim(JwtRegisteredClaimNames.Sub,"user_id")
            };

            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var key = new SymmetricSecurityKey(keyBytes);

            var signingCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),
                signingCredintials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { accessToken =  tokenString});
        }
    }
}
