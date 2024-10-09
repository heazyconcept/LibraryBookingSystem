using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IAdminUsersRepository
    {
        Task<AdminUser> CreateAdminUser(string firstName, string lastName, string email, string password);

        Task<AdminUser?> GetAdminUserByEmail(string email);

        Task<AdminUser?> GetAdminUser(string id);
    }
}