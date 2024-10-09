using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class BookMappings
    {
        public static BookDataDto ToBookDataDto(this Book book)
        {
            return new BookDataDto
            {
                BookId = book.ID,
                Name = book.Name,
                Author = book.Author,
                ISBN = book.ISBN,
                BookStatus = book.BookStatus,
                Description = book.Description,
                AvailableDate = book.AvailableDate,
                Genre = book.Genre,
                Publisher = book.Publisher,
                ReservedOrCollectedBy = book.ReservedOrCollectedByCustomer.ToCustomerDataDto(),
                ReservedOrCollectedDate = book.ReservedOrCollectedDate,
                ShelfNumber = book.ShelfNumber
            };
        }
    }
}