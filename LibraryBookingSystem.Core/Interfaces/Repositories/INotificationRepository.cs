using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        List<Notification> ListCustomerNotifications(string customerId);
        List<Notification> ListActiveBookNotifications(string bookId);
    }
}