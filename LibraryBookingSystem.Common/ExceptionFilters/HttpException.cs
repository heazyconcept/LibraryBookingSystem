namespace LibraryBookingSystem.Common .ExceptionFilters
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }

        public HttpException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    public class ResourceNotFoundException : HttpException
    {
        public ResourceNotFoundException() : base(404, "Resource not found.") { }
    }

    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException() : base(401, "Unauthorized access.") { }
    }

    public class BadRequestException : HttpException
    {
        public BadRequestException(string message) : base(400, message) { }
    }

    public class ServerErrorException : HttpException
    {
        public ServerErrorException(string message) : base(500, message) { }
    }
}
