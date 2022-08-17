using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaHome.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserService _service;

        public AuthService(IConfiguration config, IUserService service)
        {
            _config = config;
            _service = service;
        }
    
      

        public  string GenerateAccessToken(string name, string role)
        {
           
            var claims = new[] {
                new Claim("UserName", name),
                new Claim(ClaimTypes.Role , role)
            };

            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var key = new SymmetricSecurityKey(keyBytes);

            var signingCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredintials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public string GenerateRefreshToken(string name, string role)
        {
            var claims = new[] {
                new Claim("UserName", name),
                new Claim(ClaimTypes.Role , role)

            };

            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var key = new SymmetricSecurityKey(keyBytes);

            var signingCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddYears(1),
                signingCredintials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public object RegenerateTokens(string token) {
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal is null) {
                return new { }; 
            }

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string username = jwt.Claims.First(c => c.Type == "UserName").Value;
            string role = jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            var checkedUser = _service.GetUserByName(username);
            if (checkedUser is null) { return new { }; }

            var newRefresh = GenerateRefreshToken(username, role);
            var newAccess = GenerateAccessToken(username, role); 

            return new { AcessToken = newAccess, RefreshToken =  newAccess };

        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
               return null;

            return principal;
        }
    }
}
