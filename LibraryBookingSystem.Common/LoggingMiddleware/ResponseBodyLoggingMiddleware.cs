using LibraryBookingSystem.Common.Extensions;
using LibraryBookingSystem.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace LibraryBookingSystem.Common.LoggingMiddleware
{
    public class ResponseBodyLoggingMiddleware : IMiddleware
    {
        private readonly Audit<ResponseBodyLoggingMiddleware> _audit;
        public ResponseBodyLoggingMiddleware(Audit<ResponseBodyLoggingMiddleware> audit)
        {
            _audit = audit;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            var originalBodyStream = context.Response.Body;
            try
            {
                // Swap out stream with one that is buffered and suports seeking
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;
                // hand over to the next middleware and wait for the call to return
                await next(context);
                // Read response body from memory stream
                memoryStream.Position = 0;
                var reader = new StreamReader(memoryStream);
                var responseBody = await reader.ReadToEndAsync();
                // Copy body back to so its available to the user agent
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBodyStream);
                // Write response body to App Insights
                //var requestTelemetry = context.Features.Get<RequestTelemetry>();

                //requestTelemetry?.Properties.Add("ResponseBody", responseBody);
                //TODO: dont log any hangifre request
                if (GeneralUtilities.IsValidJson(responseBody)) responseBody = GeneralUtilities.RemoveSensitiveData(responseBody);
                responseBody = responseBody.IsNullOrEmpty() ? "" : responseBody;
                _audit.LogInfo("Response: " + context.Request.Path + " for [" + context.Request.Path + "] => " + responseBody);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}
