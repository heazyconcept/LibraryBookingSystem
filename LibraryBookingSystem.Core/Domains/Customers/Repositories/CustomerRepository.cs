using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Dtos;
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

        public Customer? GetCustomerByEmail(string email)
        {
            var customer = DB.Queryable<Customer>().Where(x=>x.EmailAddress == email).FirstOrDefault() ?? throw new BadRequestException("Invalid Credentials");
            return customer;
        }

        public async Task<Pagination<CustomerDataDto>> ListCustomers(int page, int pageSize, string search = "")
        {
            var query = DB.Queryable<Customer>().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.EmailAddress.ToLower().Contains(search.ToLower())
                                         || x.PhoneNumber.ToLower().Contains(search.ToLower())
                                         || x.FirstName.ToLower().Contains(search.ToLower())
                                         || x.LastName.ToLower().Contains(search.ToLower()));
            
            }
          

            var mappedResult = query.Select(x=> new CustomerDataDto
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAddress = x.EmailAddress,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                City = x.City,
                CustomerId = x.ID,
                State = x.State,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedOn,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedOn
            });
            var response = await Pagination<CustomerDataDto>.CreateAsync(mappedResult, page, pageSize);
            return response;
        }
    }
}