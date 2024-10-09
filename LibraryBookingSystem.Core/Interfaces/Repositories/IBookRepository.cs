using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Pagination<BookDataDto>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null);
        BookDataDto? GetBook(string id);
        Task<BookDataDto> ReserveBook(string bookId, Customer customer);
    }
}