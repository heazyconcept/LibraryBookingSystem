namespace LibraryBookingSystem.Data.Dtos
{
    public class BookRequestDto
    {
          public string Name { get; set; }
        public string Genre { get; set; }
        public string ShelfNumber { get; set; }
         public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
    }
}