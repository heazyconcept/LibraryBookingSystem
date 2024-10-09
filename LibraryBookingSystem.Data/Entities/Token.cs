using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Tokens")]
    public class Token:EntityBase
    {
        public string UserId { get; set; }
        public string TokenString { get; set; }

        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }

        public static Token CreateNew(string tokenString, string userAgent, string ipAddress, string userId, UserType userType)
        {
            return new Token
            {
                TokenString = tokenString,
                UserAgent = userAgent,
                IpAddress = ipAddress,
                UserId = userId,
                IsActive = true,
                UserType = userType
            };
        }
    }
}