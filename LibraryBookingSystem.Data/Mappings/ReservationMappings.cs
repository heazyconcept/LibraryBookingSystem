using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class ReservationMappings
    {
        public static ReservationDto ToReservationDataDto(this Reservation? reservation)
        {
            if (reservation == null)
            {
                return new ReservationDto();
            }
            return new ReservationDto
            {
                CustomerId = reservation.CustomerId,
                Status = reservation.Status,
                Book = reservation.Book.ToBookDataDto(),
                ExpiryDate = reservation.ExpiryDate,
                CreatedBy = reservation.CreatedBy,
                CreatedDate = reservation.CreatedOn,
                ModifiedBy = reservation.ModifiedBy,
                ModifiedDate = reservation.ModifiedOn
            };
        }
    }
}