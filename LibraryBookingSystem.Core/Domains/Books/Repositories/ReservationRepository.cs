using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
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

       public List<Reservation> ListCustomerReservations(string customerId)
       {
           var reservations =  DB.Queryable<Reservation>().Where(x => x.CustomerId == customerId).ToList();
           return reservations;
       }

        public async Task<Pagination<Reservation>> ListReservations(int page, int pageSize)
        {
            var query = DB.Queryable<Reservation>().AsQueryable();

           
            var response = await Pagination<Reservation>.CreateAsync(query, page, pageSize);
            return response;
        }

        public async Task UpdateReservation(string bookId, string customerId)
        {
            try
            {
                var reservation = GetActiveUserReservation(bookId, customerId);
                reservation.Status = ReservationStatus.Collected;
                await reservation.SaveAsync();
            }
            catch (HttpException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Reservation?> ExpireReservation(string reservationId)
        {
            var reservation = DB.Queryable<Reservation>().Where(x => x.ID == reservationId).FirstOrDefault();
            if(reservation != null && reservation.Status == ReservationStatus.Reserved){
                reservation.Status = ReservationStatus.Expired;
                await reservation.SaveAsync();
                return reservation;
            }
            return null;
        }   
    }
}