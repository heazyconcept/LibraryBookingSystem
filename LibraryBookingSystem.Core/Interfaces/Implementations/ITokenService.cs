using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Core.Interfaces.Implementations
{
    public interface ITokenService
    {
        Task<Token> CreateNewToken(string userAgent, string ipAddress, string userId, UserType userType);

        Task<Token> GetTokenByValue(string tokenValue);
    }
}