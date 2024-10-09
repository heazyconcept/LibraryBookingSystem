using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Repositories
{
    public class BookRepository: IBookRepository
    {
        public async Task<Pagination<BookDataDto>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null)
        {
            var query = DB.Queryable<Book>().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.ToLower())
                                         || x.Author.ToLower().Contains(search.ToLower())
                                         || x.Publisher.ToLower().Contains(search.ToLower())
                                         || x.ISBN.ToLower().Contains(search.ToLower())
                                         || x.Genre.ToLower().Contains(search.ToLower()));
            
            }
            if (isAvailable.HasValue && isAvailable.Value)
            {
                query = query.Where(x => x.BookStatus == BookStatus.Available);
            }
            else if (isAvailable.HasValue && !isAvailable.Value)
            {
                query = query.Where(x => x.BookStatus != BookStatus.Available);
            }

            var mappedResult = query.Select(x=> new BookDataDto
            {
                Name = x.Name,
                Genre = x.Genre,
                ShelfNumber = x.ShelfNumber,
                BookStatus = x.BookStatus,
                AvailableDate = x.AvailableDate,
                ReservedOrCollectedDate = x.ReservedOrCollectedDate,
                ReservedOrCollectedBy = x.ReservedOrCollectedByCustomer.ToCustomerDataDto(),
                Author = x.Author,
                Publisher = x.Publisher,
                ISBN = x.ISBN,
                Description = x.Description
            });
            var response = await Pagination<BookDataDto>.CreateAsync(mappedResult, page, pageSize);
            return response;
        }

        public  BookDataDto? GetBook(string id)
        {
            var book = DB.Queryable<Book>().Where(x => x.ID == id).FirstOrDefault() ?? throw new BadRequestException("Book not found");
            return book?.ToBookDataDto();
        }

        public async Task<BookDataDto> ReserveBook(string bookId, Customer customer)
        {
            var book = DB.Queryable<Book>().Where(x => x.ID == bookId).FirstOrDefault() ?? throw new BadRequestException("Book not found");
            book.BookStatus = BookStatus.Reserved;
            book.ReservedOrCollectedBy = customer.ID;
            book.ReservedOrCollectedDate = DateTime.Now;
            book.ReservedOrCollectedByCustomerEntity = new(customer);
            await book.SaveAsync();
            return book.ToBookDataDto();
        }
    }
}