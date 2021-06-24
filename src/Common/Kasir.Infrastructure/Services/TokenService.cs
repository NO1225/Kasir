using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kasir.Application.Common.Interfaces;
using Kasir.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kasir.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private readonly JwtOptions _jwtOptions;

        public TokenService(IConfiguration configuration,IIdentityService identityService)
        {
            _configuration = configuration;
            _identityService = identityService;

            _jwtOptions = _configuration.GetSection("JwtOptions").Get<JwtOptions>();
        }

        public async Task<string> CreateJwtSecurityTokenAsync(string id)
        {
            
            var roles = await _identityService.GetUserRolesAsync(id);

            var userName = await _identityService.GetUserNameAsync(id);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.NameIdentifier,id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.Lifetime),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;

            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, id),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //};

            //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //var token = new JwtSecurityToken(
            //    issuer: _configuration["JWT:ValidIssuer"],
            //    audience: _configuration["JWT:ValidAudience"],
            //    expires: DateTime.Now.AddDays(90),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //);
            // return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
