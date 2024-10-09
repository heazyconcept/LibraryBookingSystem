using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class NotificationMappings
    {

        public static NotificationDto ToNotificationDataDto(this Notification? notification)
        {
            if (notification == null)
            {
                return new NotificationDto();
            }
            return new NotificationDto
            {
                CustomerId = notification.CustomerId,
                BookId = notification.BookId,
                BookName = notification.BookName,
                NotificationStatus = notification.NotificationStatus,
                Message = notification.Message,
                CreatedBy = notification.CreatedBy,
                CreatedDate = notification.CreatedOn,
                ModifiedBy = notification.ModifiedBy,
                ModifiedDate = notification.ModifiedOn
            };
        }
        
    }
}