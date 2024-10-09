using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;

namespace LibraryBookingSystem.Core.Domains.AdminUsers.Implementations
{
    public class AdminUserService:IAdminUserService
    {
        private readonly IAdminUsersRepository _adminUserRepository;
        private readonly ITokenService _tokenService;

        public AdminUserService(IAdminUsersRepository adminUserRepository, ITokenService tokenService)
        {
            _adminUserRepository = adminUserRepository;
            _tokenService = tokenService;
        }

        public async Task<StandardResponse<dynamic>> CreateAdmin(CreateAdminDto request)
        {
            var admin = await _adminUserRepository.CreateAdminUser(request.FirstName, request.LastName, request.Email, request.Password);
            return StandardResponse<dynamic>.SuccessMessage("Admin created successfully");
        }

        public async Task<StandardResponse<CustomerLoginResponseDto>> AdminLogin(CustomerLoginRequestDto request, string ipAddress, string userAgent)
        {
            var adminUser = await _adminUserRepository.GetAdminUserByEmail(request.EmailAddress);
            var passwordHash = GeneralUtilities.Encrypt(request.Password, adminUser.PasswordSalt);
            if (passwordHash != adminUser.Password)
                throw new BadRequestException("Invalid Credentials");
            
            var token = await _tokenService.CreateNewToken(userAgent, ipAddress, adminUser.ID, UserType.Admin);
            var userSession = adminUser.ToAdminSession();
            var result = new AdminLoginResponseDto
            {
                AccessToken = token.TokenString,
                User = userSession
            };
            return StandardResponse<CustomerLoginResponseDto>.SuccessMessage("Login successful", result);
        }


    }
}