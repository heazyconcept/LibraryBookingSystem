namespace LibraryBookingSystem.Data.Dtos
{
    public class CustomerDataDto:IncludeAudit
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}