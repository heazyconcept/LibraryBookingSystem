using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;
using LibraryBookingSystem.Data.Sessions;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Repositories
{
    public class BookRepository: IBookRepository
    {
        public async Task<Pagination<Book>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null)
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

            
            var response = await Pagination<Book>.CreateAsync(query, page, pageSize);
            return response;
        }

        public  BookDataDto? GetBook(string id)
        {
            var book = DB.Queryable<Book>().Where(x => x.ID == id).FirstOrDefault() ?? throw new BadRequestException("bookErr-Book not found");
            return book?.ToBookDataDto();
        }

         public  Book? GetBookRaw(string id)
        {
            var book = DB.Queryable<Book>().Where(x => x.ID == id).FirstOrDefault() ?? throw new BadRequestException("bookErr-Book not found");
            return book;
        }

        public async Task<BookDataDto> ReserveBook(string bookId, Customer customer, AdminSession admin = null)
        {
            var book = DB.Queryable<Book>().Where(x => x.ID == bookId).FirstOrDefault() ?? throw new BadRequestException("bookErr-Book not found");
            book.BookStatus = BookStatus.Reserved;
            book.ReservedOrCollectedBy = customer.ID;
            book.ReservedOrCollectedDate = DateTime.Now;
            book.ReservedOrCollectedByCustomerEntity = new(customer);
            book.AvailableDate = DateTime.Now.AddDays(2);
            book.ModifiedBy = admin?.Email;
            await book.SaveAsync();
            return book.ToBookDataDto();
        }

        public async Task<BookDataDto> CollectBook(Book book, Customer customer, AdminSession admin, DateTime returnDate)
        {
          
            book.BookStatus = BookStatus.Collected;
            book.ReservedOrCollectedDate = DateTime.Now;
            book.AvailableDate = returnDate;
            book.ReservedOrCollectedBy = customer.ID;
            book.ReservedOrCollectedByCustomerEntity = new(customer);
            book.ModifiedBy = admin.Email;
            await book.SaveAsync();
            return book.ToBookDataDto();
        }

        public async Task ReturnBook(Book book, AdminSession admin)
        {
            book.BookStatus = BookStatus.Available;
            book.ReservedOrCollectedDate = DateTime.Now;
            book.AvailableDate = DateTime.Now;
            book.ReservedOrCollectedBy = null;
            book.ReservedOrCollectedByCustomerEntity = null;
            book.ModifiedBy = admin.Email;
            await book.SaveAsync();
        }

        public async Task CancelReservation(Book book)
        {
            book.BookStatus = BookStatus.Available;
            book.ReservedOrCollectedDate = DateTime.Now;
            book.AvailableDate = DateTime.Now;
            book.ReservedOrCollectedBy = null;
            book.ReservedOrCollectedByCustomerEntity = null;
            await book.SaveAsync();
        }
        
    }
}