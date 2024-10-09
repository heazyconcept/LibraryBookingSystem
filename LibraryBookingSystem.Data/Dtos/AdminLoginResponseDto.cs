using LibraryBookingSystem.Data.Sessions;

namespace LibraryBookingSystem.Data.Dtos
{
    public class AdminLoginResponseDto
    {
         public string AccessToken { get; set; }
        public AdminSession User { get; set; }
    }
}