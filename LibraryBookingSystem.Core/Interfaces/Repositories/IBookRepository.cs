using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Sessions;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Pagination<Book>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null);
        BookDataDto? GetBook(string id);
         Book? GetBookRaw(string id);
        Task<BookDataDto> ReserveBook(string bookId, Customer customer, AdminSession admin = null);
        Task<BookDataDto> CollectBook(Book book, Customer customer, AdminSession admin, DateTime returnDate);

       Task ReturnBook(Book book, AdminSession admin);
        Task CancelReservation(Book book);
    }
}