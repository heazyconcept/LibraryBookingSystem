using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class CustomerMappings
    {
        public static CustomerDataDto ToCustomerDataDto(this Customer? customer)
        {
            if (customer == null)
            {
                return new CustomerDataDto();
            }
            return new CustomerDataDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PhoneNumber = customer.PhoneNumber,
                EmailAddress = customer.EmailAddress,
                CreatedBy = customer.CreatedBy,
                CreatedDate = customer.CreatedOn,
                ModifiedBy = customer.ModifiedBy,
                ModifiedDate = customer.ModifiedOn
            };
        }

        public static CustomerViewDto ToCustomerViewDto(this Customer? customer, List<Reservation>? reservations, List<Collection> collections, List<Notification> notifications)
        {
            if (customer == null)
            {
                return new CustomerViewDto();
            }
            return new CustomerViewDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PhoneNumber = customer.PhoneNumber,
                EmailAddress = customer.EmailAddress,
                CustomerId = customer.ID,
                Collections = collections.Select(x => x.ToCollectionDataDto()).ToList(),
                Notifications = notifications.Select(x => x.ToNotificationDataDto()).ToList(),
                Reservations = reservations.Select(x => x.ToReservationDataDto()).ToList(),
                CreatedBy = customer.CreatedBy,
                CreatedDate = customer.CreatedOn,
                ModifiedBy = customer.ModifiedBy,
                ModifiedDate = customer.ModifiedOn
            };
        }
    }
}