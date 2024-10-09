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

        public One<Book> BookEntity { get; set; }

        [Ignore]
        public Book Book => BookEntity.ToEntityAsync().Result;

        public static Collection CreateNew(Book book, string customerId, DateTime expectedReturnDate)
        {
            return new Collection
            {
                BookId = book.ID,
                CustomerId = customerId,
                ExpectedReturnDate = expectedReturnDate,
                Status = CollectionStatus.Collected,
                BookEntity = new (book)
            };
        }
    }
}
