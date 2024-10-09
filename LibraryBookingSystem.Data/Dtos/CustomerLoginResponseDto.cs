using LibraryBookingSystem.Data.Sessions;

namespace LibraryBookingSystem.Data.Dtos
{
    public class CustomerLoginResponseDto
    {
        public string AccessToken { get; set; }
        public UserSessions User { get; set; }
    }
}