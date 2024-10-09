using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Sessions;

namespace LibraryBookingSystem.Core.Interfaces.Implementations
{
    public interface IBookService
    {
        Task<StandardResponse<Pagination<BookDataDto>>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null);
        StandardResponse<BookDataDto> GetBookDetails(string id);
        Task<StandardResponse<dynamic>> ReserveBook(string bookId, UserSessions user);

        Task<StandardResponse<dynamic>> CancelReservation(string bookId, UserSessions user);

        Task<StandardResponse<dynamic>> NotifyBookAvailability(string bookId, UserSessions user);

        Task<StandardResponse<BookDataDto>> CreateBook(BookRequestDto request, AdminSession admin);
        Task<StandardResponse<BookDataDto>> UpdateBook(string bookId, BookRequestDto request, AdminSession admin);

        Task<StandardResponse<dynamic>> ProcessBookCollection(BookCollectionRequestDto request, AdminSession admin);

        Task<StandardResponse<dynamic>> ProcessBookReturn(BookReturnRequestDto request, AdminSession admin);

        Task<StandardResponse<dynamic>> ProcessBookReservation(BookReservationRequestDto request, AdminSession admin);

        Task<StandardResponse<Pagination<CollectionDto>>> ListCollections(int page, int pageSize);

        Task<StandardResponse<Pagination<ReservationDto>>> ListReservations(int page, int pageSize);
    }
}