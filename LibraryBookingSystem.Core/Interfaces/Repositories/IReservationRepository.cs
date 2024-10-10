using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Reservation GetActiveUserReservation(string bookId, string customerId);
        List<Reservation> ListCustomerReservations(string customerId);
        Task<Pagination<Reservation>> ListReservations(int page, int pageSize);

        Task UpdateReservation(string bookId, string customerId);

        Task<Reservation?> ExpireReservation(string reservationId);
    }
}