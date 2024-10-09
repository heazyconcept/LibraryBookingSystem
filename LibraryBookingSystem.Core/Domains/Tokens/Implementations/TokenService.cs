using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryBookingSystem.Core.Domains.Tokens.Implementations
{
    public class TokenService: ITokenService
    {

        private readonly ITokenRepository _tokenRepository;
        private readonly JWT _JWT;

        public TokenService(ITokenRepository tokenRepository, IOptions<AppSettings> options)
        {
            _tokenRepository = tokenRepository;
            _JWT = options.Value.JWT;
        }

        public async Task<Token> CreateNewToken(string userAgent, string ipAddress, string userId, UserType userType)
        {
            await _tokenRepository.DeactivateUserTokens(userId);
            var tokenString = GenerateToken(userId, userType);
            var token = await _tokenRepository.CreateNewToken(tokenString, userAgent, ipAddress, userId, userType);
            return token;
        }

        public async Task<Token> GetTokenByValue(string tokenValue)
        {
            var token = _tokenRepository.GetTokenByValue(tokenValue);
            return token;
        }

        public string GenerateToken(string userId, UserType userType)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWT.Key);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Role, userType.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _JWT.Issuer,
                Audience = _JWT.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}