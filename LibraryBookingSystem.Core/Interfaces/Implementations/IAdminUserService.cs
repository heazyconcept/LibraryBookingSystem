using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;

namespace LibraryBookingSystem.Core.Interfaces.Implementations
{
    public interface IAdminUserService
    {
        Task<StandardResponse<dynamic>> CreateAdmin(CreateAdminDto request);
        Task<StandardResponse<CustomerLoginResponseDto>> AdminLogin(CustomerLoginRequestDto request, string ipAddress, string userAgent);
    }
}