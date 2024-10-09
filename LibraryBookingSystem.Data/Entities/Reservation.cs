using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Reservations")]
    public class Reservation:EntityBase
    {
        
        public string CustomerId { get; set; }  
        public string BookId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ReservationStatus Status { get; set; }

        public static Reservation CreateNew(string bookId, string iD)
        {
            return new Reservation
            {
                BookId = bookId,
                CustomerId = iD,
                ExpiryDate = DateTime.Now.AddDays(2),
                Status = ReservationStatus.Reserved
            };
        }

        public void UpdateStatus(ReservationStatus status)
        {
            Status = status;
        }
    }
}
