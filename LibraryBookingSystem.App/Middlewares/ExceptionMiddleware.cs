using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Extensions;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data;

namespace LibraryBookingSystem.App.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, Audit<ExceptionMiddleware> _audit)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException ex)
            {
                await HandleHttpExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleOtherExceptionAsync(context, ex, _audit);

            }
        }

        private async Task HandleHttpExceptionAsync(HttpContext context, HttpException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";
            var message = ex.Message.Split("-");
            var errorResponse = StandardResponse<dynamic>.ErrorMessage(message[0], message[1]);
            var jsonResponse = errorResponse.Stringify();
            await context.Response.WriteAsync(jsonResponse);
        }

        private async Task HandleOtherExceptionAsync(HttpContext context, Exception ex, Audit<ExceptionMiddleware> _audit)
        {
            _audit.LogFatal(ex);
            // Handle non-HTTP exceptions here
            // For example, you can log the exception and return a generic error response
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var errorResponse = StandardResponse<dynamic>.SystemError();
            var jsonResponse = errorResponse.Stringify();
            await context.Response.WriteAsync(jsonResponse);
        }

       

    }

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
