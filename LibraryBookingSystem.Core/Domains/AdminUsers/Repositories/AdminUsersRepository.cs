using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.AdminUsers.Repositories
{
    public class AdminUsersRepository:IAdminUsersRepository
    {
        
        public async Task<AdminUser> CreateAdminUser(string firstName, string lastName, string email, string password)
        {
            var newAdmin = AdminUser.CreateNew(firstName, lastName, email, password);
            await newAdmin.SaveAsync();
            return newAdmin;
        }

        public async Task<AdminUser?> GetAdminUserByEmail(string email)
        {
            var admin = DB.Queryable<AdminUser>().Where(x => x.Email == email).FirstOrDefault() ?? throw new BadRequestException("authErr-Invalid Credentials");
            return admin;
        }

         public async Task<AdminUser?> GetAdminUser(string id)
        {
            var admin = DB.Queryable<AdminUser>().Where(x => x.ID == id).FirstOrDefault() ?? throw new BadRequestException("adminErr-Admin Not found");
            return admin;
        }
    }
}