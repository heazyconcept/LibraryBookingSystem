using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Sessions;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Implementations
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;

        public BookService(IBookRepository bookRepository, ICustomerRepository customerRepository, IReservationRepository reservationRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<StandardResponse<Pagination<BookDataDto>>> ListBooks(int page, int pageSize, string search = "", bool? isAvailable = null)
        {
            var result  = await _bookRepository.ListBooks(page, pageSize, search, isAvailable);
            return StandardResponse<Pagination<BookDataDto>>.SuccessMessage("Books retrieved successfully", result);
        }

        public  StandardResponse<BookDataDto> GetBookDetails(string id)
        {
            var result =  _bookRepository.GetBook(id);
            return StandardResponse<BookDataDto>.SuccessMessage("Book retrieved successfully", result);
        }
        
        public async Task<StandardResponse<dynamic>> ReserveBook(string bookId, UserSessions user)
        {
            var book = _bookRepository.GetBook(bookId);
            if(book.BookStatus != BookStatus.Available)
                return StandardResponse<dynamic>.ErrorMessage("Book is not available for reservation, You can be notified when it is available");
            var customer = _customerRepository.GetCustomer(user.UserId);
            var result = await _bookRepository.ReserveBook(bookId, customer);
            var newReservation = Reservation.CreateNew(bookId, customer.ID);  
            await newReservation.SaveAsync();  
            //TODO: Schedule a job to expire the reservation after 2 days
            return StandardResponse<dynamic>.SuccessMessage("Book reserved successfully", result);
        }

        public async Task<StandardResponse<dynamic>> CancelReservation(string bookId, UserSessions user)
        {
            var book = _bookRepository.GetBook(bookId);
            if(book.BookStatus != BookStatus.Reserved)
                throw new BadRequestException("Book is not reserved");
            if(book.ReservedOrCollectedBy.CustomerId != user.UserId)
                throw new BadRequestException("You are not authorized to cancel this reservation");
            var customer = _customerRepository.GetCustomer(user.UserId);
            var reservation = _reservationRepository.GetActiveUserReservation(bookId, customer.ID);
            reservation.UpdateStatus(ReservationStatus.Cancelled);
            await reservation.SaveAsync();
            return StandardResponse<dynamic>.SuccessMessage("Reservation cancelled successfully");
        }

        public async Task<StandardResponse<dynamic>> NotifyBookAvailability(string bookId, UserSessions user)
        {
            var book = _bookRepository.GetBook(bookId);
            if(book.BookStatus == BookStatus.Available)
                throw new BadRequestException("This book is already available for collection");
            var customer = _customerRepository.GetCustomer(user.UserId);
            var notification = Notification.CreateNew(bookId, customer.ID, book.Name);
            await notification.SaveAsync();
            //TODO: Send notification to customer when the book is available
            return StandardResponse<dynamic>.SuccessMessage("You will be notified when the book is available");
        }
    }
}