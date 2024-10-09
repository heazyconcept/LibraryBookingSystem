using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Token? GetTokenByValue(string tokenValue);

        Token? GetActiveTokenByUserId(string userId);
        Task<Token> CreateNewToken(string tokenString, string userAgent, string ipAddress, string userId, UserType userType);
        Task DeactivateUserTokens(string userId);
    }
}