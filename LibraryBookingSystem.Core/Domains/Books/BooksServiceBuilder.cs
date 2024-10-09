using LibraryBookingSystem.Core.Domains.Books.Implementations;
using LibraryBookingSystem.Core.Domains.Books.Repositories;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Core.Domains.Books
{
    public static class BooksServiceBuilder
    {
         public static IServiceCollection AddBookServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            return services;
        }
    }
}