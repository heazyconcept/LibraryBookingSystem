using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        List<Notification> ListCustomerNotifications(string customerId);
    }
}