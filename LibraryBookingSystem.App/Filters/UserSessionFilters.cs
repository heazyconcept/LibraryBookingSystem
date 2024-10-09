using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryBookingSystem.App.Filters
{
    public class UserSessionFilters: IAsyncActionFilter
    {

        private readonly ITokenService _tokenService;
        private readonly ICustomerRepository _customerRepository;
        public UserSessionFilters(ITokenService tokenService, ICustomerRepository customerRepository)
        {
            _tokenService = tokenService;
            _customerRepository = customerRepository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
           ActionExecutionDelegate next)
        {
            var authentication = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authentication))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var authToken = authentication.ToString().Split(" ")[1];
            var token = await _tokenService.GetTokenByValue(authToken);
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (!token.IsActive)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if(token.UserType != Data.Enums.UserType.Customer)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var customer =  _customerRepository.GetCustomer(token.UserId);
            var userSession = customer.ToUserSession();
            context.HttpContext.Items["UserSessions"] = userSession;
            await next();
        }
    }
}