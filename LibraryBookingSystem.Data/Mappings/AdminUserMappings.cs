using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Sessions;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class AdminUserMappings
    {

        public static AdminSession ToAdminSession (this AdminUser customer)
        {
            return new AdminSession
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserId = customer.ID
            };
        }
        
    }
}