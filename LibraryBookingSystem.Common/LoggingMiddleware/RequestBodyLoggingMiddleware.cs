using LibraryBookingSystem.Common.Extensions;
using LibraryBookingSystem.Common.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace LibraryBookingSystem.Common.LoggingMiddleware
{
    public class RequestBodyLoggingMiddleware : IMiddleware
    {
        private readonly Audit<RequestBodyLoggingMiddleware> _audit;
        public RequestBodyLoggingMiddleware(Audit<RequestBodyLoggingMiddleware> audit)
        {
            _audit = audit;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var method = context.Request.Method;
            // Ensure the request body can be read multiple times
            context.Request.EnableBuffering();
            // Only if we are dealing with POST or PUT, GET and others shouldn't have a body
            if (context.Request.Body.CanRead)
            {
                // Leave stream open so next middleware can read it
                using var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 512, leaveOpen: true);
                var requestBody = await reader.ReadToEndAsync();
                // Reset stream position, so next middleware can read it
                context.Request.Body.Position = 0;
                // Write request body to App Insights
                //var requestTelemetry = context.Features.Get<RequestTelemetry>();
                //requestTelemetry?.Properties.Add("RequestBody", requestBody);
                //TODO: dont log any hangifre request
                var auth = context.Request.Headers["Authorization"].ToString();
                var otherHeaders = context.Request.Headers.Where(x => x.Key != "Authorization").ToList();
                var requestToLog = new
                {
                    content = requestBody.IsNotNullOrEmpty() && GeneralUtilities.IsValidJson(requestBody) ? GeneralUtilities.RemoveSensitiveData(requestBody) : string.Empty,
                    Params = context.Request.Query.Count > 0 ? context.Request.Query.Stringify() : string.Empty,
                    auth = auth.IsNullOrEmpty() ? "Empty" : "***obfuscated***",
                    headers = otherHeaders.Stringify(),
                };
                _audit.LogRequest(context.Request.Path, requestToLog.Stringify(), context.Request.Path);
            }
            // Call next middleware in the pipeline
            await next(context);
        }
    }
}
