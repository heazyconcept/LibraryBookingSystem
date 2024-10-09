using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Customers.Implementations
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly INotificationRepository _notificationRepository;

        private readonly ITokenService _tokenService;
        public CustomerService(
            ICustomerRepository customerRepository,
            ICollectionRepository collectionRepository,
            IReservationRepository reservationRepository,
            INotificationRepository notificationRepository,
            ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _collectionRepository = collectionRepository;
            _reservationRepository = reservationRepository;
            _notificationRepository = notificationRepository;
            _tokenService = tokenService;
        }

        public async Task<StandardResponse<dynamic>> RegisterCustomer(RegisterDto request)
        {
           _customerRepository.ValidateExisitingCustomer(request.EmailAddress, request.PhoneNumber);
            var customer = Customer.CreateNew(request);
            await customer.SaveAsync();
            //TODO: Send Email Verification
            return StandardResponse<dynamic>.SuccessMessage("Your registration is successful. Please check your mail for the verification code");
        }
        
        public async Task<StandardResponse<Pagination<CustomerDataDto>>> ListCustomers(int page, int pageSize, string search = "")
        {
            var customers = await _customerRepository.ListCustomers(page, pageSize, search);
            return StandardResponse<Pagination<CustomerDataDto>>.SuccessMessage("Data retrieved successfully", customers);
        }

        public async Task<StandardResponse<CustomerViewDto>> GetCustomer(string customerId)
        {
            var customer = _customerRepository.GetCustomer(customerId);
            var reservations = _reservationRepository.ListCustomerReservations(customerId);
            var collections = _collectionRepository.ListCustomerCollections(customerId);
            var notifications = _notificationRepository.ListCustomerNotifications(customerId);
            var result = customer.ToCustomerViewDto(reservations, collections, notifications);
            return StandardResponse<CustomerViewDto>.SuccessMessage("Data retrieved successfully", result);
        }

        public async Task<StandardResponse<CustomerLoginResponseDto>> CustomerLogin(CustomerLoginRequestDto request, string ipAddress, string userAgent)
        {
            var customer = _customerRepository.GetCustomerByEmail(request.EmailAddress);
            var passwordHash = GeneralUtilities.Encrypt(request.Password, customer.PasswordSalt);
            if (passwordHash != customer.Password)
                throw new BadRequestException("Invalid Credentials");
            
            var token = await _tokenService.CreateNewToken(userAgent, ipAddress, customer.ID, UserType.Customer);
            var userSession = customer.ToUserSession();
            var result = new CustomerLoginResponseDto
            {
                AccessToken = token.TokenString,
                User = userSession
            };
            return StandardResponse<CustomerLoginResponseDto>.SuccessMessage("Login successful", result);
        }
    }
}