using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Reservation GetActiveUserReservation(string bookId, string customerId);
    }
}