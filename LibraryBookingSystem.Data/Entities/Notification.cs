using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Notifications")]
    public class Notification:EntityBase
    {
        public string CustomerId { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public string Message { get; set; }

        public static Notification CreateNew(string bookId, string customerId, string bookName)
        {
            return new Notification
            {
                BookId = bookId,
                NotificationStatus = NotificationStatus.Pending,
                Message = $"this book {bookName} is available for collection/Reservation",
                CustomerId = customerId,
                BookName = bookName
            };
        }
    }
}
