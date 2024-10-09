using Microsoft.AspNetCore.Builder;

namespace LibraryBookingSystem.Common.LoggingMiddleware
{
    public static class ApplicationInsightExtensions
    {
        public static IApplicationBuilder UseRequestBodyLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestBodyLoggingMiddleware>();
        }
        public static IApplicationBuilder UseResponseBodyLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseBodyLoggingMiddleware>();
        }
    }
}
