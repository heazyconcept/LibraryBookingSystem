using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
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
       
    }
}