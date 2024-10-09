using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Customers")]
    public class Customer: EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string VerificationCode { get; set; }

        public VerificationStatus VerificationStatus { get; set; }  

        public static Customer CreateNew(RegisterDto request)
        {
             var PasswordSalt = GeneralUtilities.ComputeSha256Hash(GeneralUtilities.GenerateCode(10));
            var passwordHash = GeneralUtilities.Encrypt(request.Password, PasswordSalt);
            return new Customer
            {
                Address = request.Address,
                City = request.City,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = passwordHash,
                PasswordSalt = PasswordSalt,
                PhoneNumber = request.PhoneNumber,
                State = request.State,
                IsDeleted = false
            };
        }
    }
}
