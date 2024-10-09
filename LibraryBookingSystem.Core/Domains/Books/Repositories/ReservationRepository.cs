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
    public class ReservationRepository: IReservationRepository
    {
        
       public Reservation GetActiveUserReservation(string bookId, string customerId)
       {
            var reservation = DB.Queryable<Reservation>().Where(x => x.BookId == bookId && x.CustomerId == customerId && x.Status == ReservationStatus.Reserved).FirstOrDefault() ?? throw new BadRequestException("Reservation not found");
            return reservation;
       }

       public List<Reservation> ListCustomerReservations(string customerId)
       {
           var reservations =  DB.Queryable<Reservation>().Where(x => x.CustomerId == customerId).ToList();
           return reservations;
       }

        public async Task<Pagination<ReservationDto>> ListReservations(int page, int pageSize)
        {
            var query = DB.Queryable<Reservation>().AsQueryable();

            var mappedResult = query.Select(x=> new ReservationDto
            {
                Book = x.Book.ToBookDataDto(),
                CustomerId = x.CustomerId,
                Status = x.Status,
                ExpiryDate = x.ExpiryDate,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedOn,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedOn
            });
            var response = await Pagination<ReservationDto>.CreateAsync(mappedResult, page, pageSize);
            return response;
        }
       
    }
}