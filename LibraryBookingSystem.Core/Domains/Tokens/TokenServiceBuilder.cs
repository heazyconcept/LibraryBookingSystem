using LibraryBookingSystem.Core.Domains.Tokens.Implementations;
using LibraryBookingSystem.Core.Domains.Tokens.Repositories;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Core.Domains.Tokens
{
    public static class TokenServiceBuilder
    {
        public static IServiceCollection AddTokenServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            return services;
        }
    }
}