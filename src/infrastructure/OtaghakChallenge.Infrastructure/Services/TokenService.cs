using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Infrastructure.Services
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly IRepository<UserRole> _repositoryRoles;

        public TokenService(IConfiguration configuration, IRepository<UserRole> repositoryRoles)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfiguration:Key"] ?? string.Empty));
            _repositoryRoles = repositoryRoles;
        }
        public async Task<string> CreateToken(User user)
        {
            long userRoleId = await _repositoryRoles.GetAll().Where(a => a.UserId == user.Id).Select(a => a.RoleId).FirstOrDefaultAsync();

            var claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                new("Name", user.Name?.ToString() ?? ""),
                new("RoleId", userRoleId.ToString() ?? ""),
            };

            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["JWTConfiguration:Issuer"],
                Audience = _configuration["JWTConfiguration:Audience"],
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = cred,
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.Run(() => tokenHandler.WriteToken(token)).ConfigureAwait(false);
        }
    }
}
