using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public List<Notification> ListCustomerNotifications(string customerId)
        {
            var notifications = DB.Queryable<Notification>().Where(x => x.CustomerId == customerId).ToList();
            return notifications;
        }

    }
}