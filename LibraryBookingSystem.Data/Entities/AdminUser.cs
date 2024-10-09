using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Common;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("AdminUsers")]
    public class AdminUser : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public static AdminUser CreateNew(string firstName, string lastName, string email, string password)
        {
            var PasswordSalt = GeneralUtilities.ComputeSha256Hash(GeneralUtilities.GenerateCode(10));
            var passwordHash = GeneralUtilities.Encrypt(password, PasswordSalt);
            return new AdminUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                PasswordSalt = passwordHash
            };
        }
    }
}
