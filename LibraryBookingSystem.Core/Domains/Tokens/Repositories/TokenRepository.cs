using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Tokens.Repositories
{
    public class TokenRepository:ITokenRepository
    {
        public Token? GetTokenByValue(string tokenValue)
        {
            return DB.Queryable<Token>().Where(x => x.TokenString == tokenValue).FirstOrDefault();
        }

        public Token? GetActiveTokenByUserId(string userId)
        {
            return DB.Queryable<Token>().Where(x => x.UserId == userId && x.IsActive).FirstOrDefault();
        }

        public async Task DeactivateUserTokens(string userId)
        {
                 await DB.Update<Token>()
                .Match(x => x.UserId == userId)
                .Modify(x => x.IsActive, false)
                .ExecuteAsync();
        }

        public async Task<Token> CreateNewToken(string tokenString, string userAgent, string ipAddress, string userId, UserType userType)
        {
            var newToken = Token.CreateNew(tokenString, userAgent, ipAddress, userId, userType);

            await newToken.SaveAsync();
            return newToken;

        }
    }
}