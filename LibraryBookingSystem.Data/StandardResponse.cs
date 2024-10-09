using LibraryBookingSystem.Common.Extensions;
using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Data
{
    public class StandardResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string Code { get; set; }

        public static StandardResponse<T> SuccessMessage(string message, dynamic data = null)
        {

            return new StandardResponse<T>
            {
                Data = (T)data,
                Message = message,
                Status = true,
                Code = "00"
            };
        }

        public static StandardResponse<T> ErrorMessage(string code, string message = "")
        {
            return new StandardResponse<T>
            {
                Message = message ?? GenericMessage.Error.ToString(),
                Status = false,
                Code = code
            };
        }
        public static StandardResponse<T> TokenMessage()
        {
            return new StandardResponse<T>
            {
                Message = GenericMessage.TokenError.ToString(),
                Status = false
            };
        }
        public static StandardResponse<T> SystemError(string message = "")
        {
            return new StandardResponse<T>
            {
                Message = message.IsNullOrEmpty() ? "Unknown Error Occurred. Please try again later" : message,
                Status = false,
                Code = "500"
            };
        }

       
    }
}
