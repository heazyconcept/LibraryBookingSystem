using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Customers.Implementations
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<StandardResponse<dynamic>> RegisterCustomer(RegisterDto request)
        {
           _customerRepository.ValidateExisitingCustomer(request.EmailAddress, request.PhoneNumber);
            var customer = Customer.CreateNew(request);
            await customer.SaveAsync();
            //TODO: Send Email Verification
            return StandardResponse<dynamic>.SuccessMessage("Your registration is successful. Please check your mail for the verification code");
        }
        
    }
}