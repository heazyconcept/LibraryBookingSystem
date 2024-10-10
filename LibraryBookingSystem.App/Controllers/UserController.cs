using LibraryBookingSystem.App.Filters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Implementations;
using LibraryBookingSystem.Data;
using LibraryBookingSystem.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookingSystem.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IBookService _bookService;

        public UserController(ICustomerService customerService, IBookService bookService)
        {
            _customerService = customerService;
            _bookService = bookService;
        }

        [Route("register")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            return Ok(await _customerService.RegisterCustomer(request));
        }

        [Route("login")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<CustomerLoginResponseDto>))]
        public async Task<IActionResult> Login([FromBody] CustomerLoginRequestDto request)
        {
            return Ok(await _customerService.CustomerLogin(request, ipAddress, userAgent));
        }

        [Authorize]
        [ServiceFilter(typeof(UserSessionFilters))]
        [Route("books")]
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<Pagination<BookDataDto>>))]
        public async Task<IActionResult> ListBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "", [FromQuery] bool? isAvailable = null)
        {
            return Ok(await _bookService.ListBooks(page, pageSize, search, isAvailable));
        }

        [Authorize]
        [ServiceFilter(typeof(UserSessionFilters))]
        [Route("book/{bookId}")]
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<BookDataDto>))]
        public async Task<IActionResult> GetBookDetails([FromRoute] string bookId)
        {
            return Ok(_bookService.GetBookDetails(bookId));
        }

        [Authorize]
        [ServiceFilter(typeof(UserSessionFilters))]
        [Route("book/{bookId}/reserve")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> ReserveBook([FromRoute] string bookId)
        {
            return Ok(await _bookService.ReserveBook(bookId, UserSessions));
        }

        [Authorize]
        [ServiceFilter(typeof(UserSessionFilters))]
        [Route("book/{bookId}/reservation/cancel")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> CancelReservation([FromRoute] string bookId)
        {
            return Ok(await _bookService.CancelReservation(bookId, UserSessions));
        }

        [Authorize]
        [ServiceFilter(typeof(UserSessionFilters))]
        [Route("book/{bookId}/notify")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> NotifyUserofAvalability([FromRoute] string bookId)
        {
            return Ok(await _bookService.NotifyBookAvailability(bookId, UserSessions));
        }

    }
}