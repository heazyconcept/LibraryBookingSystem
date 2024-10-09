using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
       void ValidateExisitingCustomer(string email, string phoneNumber);
       Customer? GetCustomer(string customerId);
    }
}