using LibraryBookingSystem.Data.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookingSystem.App.Controllers
{
    public class BaseController : ControllerBase
    {
        protected UserSessions UserSessions => HttpContext.Items["UserSessions"] as UserSessions;

        protected AdminSession AdminSession => HttpContext.Items["AdminSession"] as AdminSession;

        protected string userAgent => Request.Headers["User-Agent"];

        protected string ipAddress => Request.HttpContext.Connection.RemoteIpAddress.ToString();
    }
}