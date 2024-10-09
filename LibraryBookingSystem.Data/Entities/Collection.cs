using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Enums;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Collections")]
    public class Collection:EntityBase
    {
        public string CustomerId { get; set; }  
        public string BookId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public CollectionStatus Status { get; set; }
    }
}
