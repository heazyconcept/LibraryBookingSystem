using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Data.Dtos
{
    public class NotificationDto:IncludeAudit
    {
         public string CustomerId { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public string Message { get; set; }

    }
}