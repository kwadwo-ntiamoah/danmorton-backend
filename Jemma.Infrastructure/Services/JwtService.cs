using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jemma.Infrastructure.Services
{
    public class JwtService(IConfiguration _config) : IJwtService
    {
        public string GenerateToken(string username, List<string> roles)
        {
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]!);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sid, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, username));

            var tokenKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}