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

        public One<Book> BookEntity { get; set; }

        [Ignore]
        public Book Book => BookEntity.ToEntityAsync().Result;

        public static Reservation CreateNew(Book book, string iD)
        {
            return new Reservation
            {
                BookId = book.ID,
                CustomerId = iD,
                ExpiryDate = DateTime.Now.AddDays(2),
                Status = ReservationStatus.Reserved,
                BookEntity = new (book)
            };
        }

        public void UpdateStatus(ReservationStatus status)
        {
            Status = status;
        }
    }
}
