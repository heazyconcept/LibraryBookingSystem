using LibraryBookingSystem.Core.Domains.Customers.Implementations;
using LibraryBookingSystem.Core.Domains.Customers.Repositories;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Core.Domains.Customers
{
    public static class CustomerServiceBuilder
    {
         public static IServiceCollection AddCustomerServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}