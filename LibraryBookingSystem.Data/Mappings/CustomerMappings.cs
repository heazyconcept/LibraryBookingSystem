using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class CustomerMappings
    {
        public static CustomerDataDto ToCustomerDataDto(this Customer customer)
        {
            return new CustomerDataDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PhoneNumber = customer.PhoneNumber,
                EmailAddress = customer.EmailAddress
            };
        }
    }
}