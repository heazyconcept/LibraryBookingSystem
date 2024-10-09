
using Microsoft.Extensions.Logging;

namespace LibraryBookingSystem.Common.Helpers
{
    public class Audit<T> where T : class
    {
        private readonly ILogger _logger;

        public Audit(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            if (GeneralUtilities.IsValidJson(message))
            {
                message = GeneralUtilities.RemoveSensitiveData(message);
            }

            _logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogFatal(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        public void LogRequest(string endpoint, string request, string idenitifer)
        {
            if (GeneralUtilities.IsValidJson(request))
            {
                request = GeneralUtilities.RemoveSensitiveData(request);
            }

            LogInfo("Request: " + endpoint + " for [" + idenitifer + "] => " + request);
        }

        public void LogResponse(string endpoint, string response, string idenitifer)
        {
            if (GeneralUtilities.IsValidJson(response))
            {
                response = GeneralUtilities.RemoveSensitiveData(response);
            }

            LogInfo("Response: " + endpoint + " for [" + idenitifer + "] => " + response);
        }
    }
}
