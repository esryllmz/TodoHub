using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Tokens.Configurations;
using TodoHub.Models.Dtos.Tokens;
using TodoHub.Models.Entities;
using TodoHub.Services.Abstracts;

namespace TodoHub.Services.Concretes
{
    public class JwtService : IJwtService
    {
        private readonly TokenOption _tokenOption;
        private readonly UserManager<User> _userManager;
        public JwtService(IOptions<TokenOption> tokenOption, UserManager<User> userManager)
        {
            _tokenOption = tokenOption.Value;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var secretKey = SecurityKeyHelper.GetSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials sc = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                claims: await GetClaims(user, _tokenOption.Audience),
                expires: accessTokenExpiration,
                signingCredentials: sc
              );

            var handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            return new TokenResponseDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration
            };
        }


        private async Task<IEnumerable<Claim>> GetClaims(User user, List<string> audiences)
        {
            var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("email", user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            userClaims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));

            return userClaims;
        }
    }
}
