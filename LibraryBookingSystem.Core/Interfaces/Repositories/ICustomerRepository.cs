using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
       void ValidateExisitingCustomer(string email, string phoneNumber);
       Customer? GetCustomer(string customerId);
       Task<Pagination<CustomerDataDto>> ListCustomers(int page, int pageSize, string search = "");
       Customer? GetCustomerByEmail(string email);
    }
}