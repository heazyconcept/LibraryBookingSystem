using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryBookingSystem.App.Filters
{
    public class AdminUserSerssionFilter: IAsyncActionFilter
    {
        private readonly ITokenService _tokenService;
        private readonly IAdminUsersRepository _adminUsersRepository;

        public AdminUserSerssionFilter(ITokenService tokenService, IAdminUsersRepository adminUsersRepository)
        {
            _tokenService = tokenService;
            _adminUsersRepository = adminUsersRepository;
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
            if(token.UserType != Data.Enums.UserType.Admin)
            {
                context.Result = new ForbidResult();
                return;
            }
            var admin =  await _adminUsersRepository.GetAdminUser(token.UserId);
            var adminSession = admin.ToAdminSession();
            context.HttpContext.Items["AdminSession"] = adminSession;
            await next();
        }
    }
}