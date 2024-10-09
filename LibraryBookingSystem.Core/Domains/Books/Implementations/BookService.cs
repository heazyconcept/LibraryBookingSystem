using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;
using LibraryBookingSystem.Data.Sessions;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Implementations
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICollectionRepository _collectionRepository;

        public BookService(IBookRepository bookRepository, ICustomerRepository customerRepository, IReservationRepository reservationRepository, ICollectionRepository collectionRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _reservationRepository = reservationRepository;
            _collectionRepository = collectionRepository;
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
            var book = _bookRepository.GetBookRaw(bookId);
            if(book.BookStatus != BookStatus.Available)
                return StandardResponse<dynamic>.ErrorMessage("Book is not available for reservation, You can be notified when it is available");
            var customer = _customerRepository.GetCustomer(user.UserId);
            var result = await _bookRepository.ReserveBook(bookId, customer);
            var newReservation = Reservation.CreateNew(book, customer.ID);  
            await newReservation.SaveAsync();  
            //TODO: Schedule a job to expire the reservation after 2 days
            return StandardResponse<dynamic>.SuccessMessage("Book reserved successfully", result);
        }

        public async Task<StandardResponse<dynamic>> CancelReservation(string bookId, UserSessions user)
        {
            var book = _bookRepository.GetBookRaw(bookId);
            if(book.BookStatus != BookStatus.Reserved)
                throw new BadRequestException("Book is not reserved");
            if(book.ReservedOrCollectedByCustomer.ID != user.UserId)
                throw new BadRequestException("You are not authorized to cancel this reservation");
            var customer = _customerRepository.GetCustomer(user.UserId);
            var reservation = _reservationRepository.GetActiveUserReservation(bookId, customer.ID);
            reservation.UpdateStatus(ReservationStatus.Cancelled);
            await _bookRepository.CancelReservation(book);
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

        public async Task<StandardResponse<BookDataDto>> CreateBook(BookRequestDto request, AdminSession admin)
        {
            var book = Book.CreateNew(request, admin);
            await book.SaveAsync();
            return StandardResponse<BookDataDto>.SuccessMessage("Book created successfully", book.ToBookDataDto());
        }

        public async Task<StandardResponse<BookDataDto>> UpdateBook(string bookId, BookRequestDto request, AdminSession admin)
        {
            var book = _bookRepository.GetBookRaw(bookId);
            book.UpdateBook(request, admin);
            await book.SaveAsync();
            return StandardResponse<BookDataDto>.SuccessMessage("Book updated successfully", book.ToBookDataDto());
        }

        public async Task<StandardResponse<dynamic>> ProcessBookCollection(BookCollectionRequestDto request, AdminSession admin)
        {
            var book = _bookRepository.GetBookRaw(request.BookId);
            if (book.BookStatus != BookStatus.Available)
            {
                if (book.BookStatus == BookStatus.Collected)
                    throw new BadRequestException("Book has already been collected");
                if (book.BookStatus == BookStatus.Reserved && book.ReservedOrCollectedByCustomer.ID != request.CustomerId)
                    throw new BadRequestException("This book is reserved by another user");
            }
            var customer = _customerRepository.GetCustomer(request.CustomerId);
            await _bookRepository.CollectBook(book, customer, admin, request.ExpectedReturnDate);
            var newCollection = Collection.CreateNew(book, customer.ID, request.ExpectedReturnDate);
            await newCollection.SaveAsync();
            return StandardResponse<dynamic>.SuccessMessage("Book collected successfully");
        }

        public async Task<StandardResponse<dynamic>> ProcessBookReturn(BookReturnRequestDto request, AdminSession admin)
        {
            var book = _bookRepository.GetBookRaw(request.BookId);
            if (book.BookStatus != BookStatus.Collected)
                throw new BadRequestException("Book has not been collected");
            if (book.ReservedOrCollectedByCustomer.ID != request.CustomerId)
                throw new BadRequestException("This book was not collected by this user");
            
            var collection = _collectionRepository.UpdateForReturn(request.CustomerId, request.BookId);
            await _bookRepository.ReturnBook(book, admin);
            //TODO: Notify customer that the book is available
            return StandardResponse<dynamic>.SuccessMessage("Book returned successfully");
        }

        public async Task<StandardResponse<dynamic>> ProcessBookReservation(BookReservationRequestDto request, AdminSession admin)
        {
            var book = _bookRepository.GetBookRaw(request.BookId);
            if (book.BookStatus != BookStatus.Available)
                throw new BadRequestException("Book is not available for reservation");
            var customer = _customerRepository.GetCustomer(request.CustomerId);
            await _bookRepository.ReserveBook(request.BookId, customer, admin);
            var newReservation = Reservation.CreateNew(book, customer.ID);
            await newReservation.SaveAsync();
            return StandardResponse<dynamic>.SuccessMessage("Book reserved successfully");
        }

        public async Task<StandardResponse<Pagination<CollectionDto>>> ListCollections(int page, int pageSize)
        {
            var result = await _collectionRepository.ListCollections(page, pageSize);
            return StandardResponse<Pagination<CollectionDto>>.SuccessMessage("Collections retrieved successfully", result);
        }

        public async Task<StandardResponse<Pagination<ReservationDto>>> ListReservations(int page, int pageSize)
        {
            var result = await _reservationRepository.ListReservations(page, pageSize);
            return StandardResponse<Pagination<ReservationDto>>.SuccessMessage("Reservations retrieved successfully", result);
        }


    }
}