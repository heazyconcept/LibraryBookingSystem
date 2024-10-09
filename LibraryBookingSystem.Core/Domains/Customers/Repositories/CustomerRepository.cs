using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Entities;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void ValidateExisitingCustomer(string email, string phoneNumber)
        {
           var existing =  DB.Queryable<Customer>().Where(x => x.EmailAddress.ToLower() == email.ToLower() || x.PhoneNumber == phoneNumber).FirstOrDefault();
           if(existing != null)
                throw new BadRequestException("Customer already exists");
        }

        public Customer? GetCustomer(string customerId)
        {
            var customer = DB.Queryable<Customer>().Where(x=>x.ID == customerId).FirstOrDefault() ?? throw new BadRequestException("Customer not found");
            return customer;
        }
    }
}