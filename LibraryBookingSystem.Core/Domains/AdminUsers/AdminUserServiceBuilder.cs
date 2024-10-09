using LibraryBookingSystem.Core.Domains.AdminUsers.Implementations;
using LibraryBookingSystem.Core.Domains.AdminUsers.Repositories;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Core.Domains.AdminUsers
{
    public static class AdminUserServiceBuilder
    {
        public static IServiceCollection AddAdminUserServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminUsersRepository, AdminUsersRepository>();
            return services;
        }
    }
}