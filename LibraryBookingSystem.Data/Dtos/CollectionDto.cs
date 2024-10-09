using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Data.Dtos
{
    public class CollectionDto:IncludeAudit
    {
        public string CustomerId { get; set; }
        public BookDataDto Book { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public CollectionStatus Status { get; set; }
    }
}