using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Data.Dtos
{
    public class ReservationDto:IncludeAudit
    {
         public string CustomerId { get; set; }
        public BookDataDto Book { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ReservationStatus Status { get; set; }
        
    }
}