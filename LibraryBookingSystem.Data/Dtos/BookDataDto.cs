using LibraryBookingSystem.Data.Enums;

namespace LibraryBookingSystem.Data.Dtos
{
    public class BookDataDto:IncludeAudit
    {
        public string BookId { get; set; }
         public string Name { get; set; }
        public string Genre { get; set; }
        public string ShelfNumber { get; set; }
        public BookStatus BookStatus { get; set; }
        public DateTime AvailableDate { get; set; }
        public DateTime ReservedOrCollectedDate { get; set; }
        public CustomerDataDto? ReservedOrCollectedBy { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }

      
    }
}