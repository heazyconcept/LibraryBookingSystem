using LibraryBookingSystem.Data.Common;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("AdminUsers")]
    public class AdminUser:EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
    }
}
