using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryBookingSystem.Core.Domains.Tokens.Implementations
{
    public class TokenService : ITokenService
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
            var roles = new List<string> { userType.ToString() };
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userId),
                new("role", userType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _JWT.Issuer,
                audience: _JWT.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}