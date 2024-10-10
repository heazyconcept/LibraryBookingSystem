using LibraryBookingSystem.Core.Interfaces.Repositories;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Implementations
{
    public class NotificationJob
    {

        private readonly INotificationRepository _notificationRepository;

        public NotificationJob(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SendNotification(string bookId)
        {
            var notifications = _notificationRepository.ListActiveBookNotifications(bookId);
            if(notifications.Count != 0)
            {
                // Send notification
                foreach (var notification in notifications)
                {
                    // Send notification
                    notification.NotificationStatus = Data.Enums.NotificationStatus.Sent;
                    await notification.SaveAsync();
                }
            }
        }
    }
}