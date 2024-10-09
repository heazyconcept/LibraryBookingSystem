using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;

namespace LibraryBookingSystem.Core.Interfaces.Implementations
{
    public interface ICustomerService
    {
        Task<StandardResponse<dynamic>> RegisterCustomer(RegisterDto request);

        Task<StandardResponse<Pagination<CustomerDataDto>>> ListCustomers(int page, int pageSize, string search = "");

        Task<StandardResponse<CustomerViewDto>> GetCustomer(string customerId);

        Task<StandardResponse<CustomerLoginResponseDto>> CustomerLogin(CustomerLoginRequestDto request, string ipAddress, string userAgent);
    }
}