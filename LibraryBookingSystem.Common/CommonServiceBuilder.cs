using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Common.LoggingMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBookingSystem.Common
{
    public static class CommonServiceBuilder
    {
        public static IServiceCollection AddCommonService(this IServiceCollection services)
        {
            services.AddScoped(typeof(Audit<>));
            //services.AddTransient<RequestBodyLoggingMiddleware>();
            //services.AddTransient<ResponseBodyLoggingMiddleware>();
            return services;
        }

        public static IApplicationBuilder ConfigureCommonApp(this IApplicationBuilder app, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            //app.UseRequestBodyLogging();
            //app.UseResponseBodyLogging();
            GeneralUtilities.LoggerFactory = loggerFactory;
            return app;
        }
    }
}
