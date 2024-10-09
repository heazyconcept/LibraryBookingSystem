namespace LibraryBookingSystem.Data.Dtos
{
    public class BookCollectionRequestDto
    {
        public string BookId { get; set; }
        public string CustomerId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}