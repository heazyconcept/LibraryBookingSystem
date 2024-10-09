using LibraryBookingSystem.Core.Domains.AdminUsers;
using LibraryBookingSystem.Core.Domains.Books;
using LibraryBookingSystem.Core.Domains.Customers;
using LibraryBookingSystem.Core.Domains.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Core
{
    public static class DomainServiceBuilder
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddAdminUserServices();
            services.AddBookServices();
            services.AddCustomerServices();
            services.AddTokenServices();
            return services;
        }

    }
}
