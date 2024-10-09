namespace LibraryBookingSystem.Data.Dtos
{
    public class CustomerViewDto:CustomerDataDto
    {
        public List<ReservationDto> Reservations { get; set; }
        public List<CollectionDto> Collections { get; set; }
        public List<NotificationDto> Notifications { get; set; }
    }
}