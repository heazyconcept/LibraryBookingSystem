using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Reservation GetActiveUserReservation(string bookId, string customerId);
        List<Reservation> ListCustomerReservations(string customerId);
        Task<Pagination<ReservationDto>> ListReservations(int page, int pageSize);
    }
}