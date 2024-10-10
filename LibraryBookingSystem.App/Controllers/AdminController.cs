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
    public class AdminController : BaseController
    {
        private readonly IAdminUserService _adminService;
        private readonly IBookService _bookService;

        public AdminController(IAdminUserService adminService, IBookService bookService)
        {
            _adminService = adminService;
            _bookService = bookService;
        }

        // [Route("")]
        // [HttpPost]
        // [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        // public async Task<IActionResult> AddAdmin([FromBody] CreateAdminDto request)
        // {
        //     return Ok(await _adminService.CreateAdmin(request));
        // }

        [Route("login")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<CustomerLoginResponseDto>))]
        public async Task<IActionResult> Login([FromBody] CustomerLoginRequestDto request)
        {
            return Ok(await _adminService.AdminLogin(request, ipAddress, userAgent));
        }

        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<BookDataDto>))]
        public async Task<IActionResult> CreateBook([FromBody] BookRequestDto request)
        {
            return Ok(await _bookService.CreateBook(request, AdminSession));
        }

        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("books")]
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<Pagination<BookDataDto>>))]
        public async Task<IActionResult> ListBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "", [FromQuery] bool? isAvailable = null)
        {
            return Ok(await _bookService.ListBooks(page, pageSize, search, isAvailable));
        }

        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/{bookId}")]
        [HttpPut]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<BookDataDto>))]
        public async Task<IActionResult> UpdateBook([FromRoute] string bookId, [FromBody] BookRequestDto request)
        {
            return Ok(await _bookService.UpdateBook(bookId, request, AdminSession));
        }

        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/collection")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> ProcessBookCollection([FromBody] BookCollectionRequestDto request)
        {
            return Ok(await _bookService.ProcessBookCollection(request, AdminSession));
        }

        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/return")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> ProcessBookReturn([FromBody] BookReturnRequestDto request)
        {
            return Ok(await _bookService.ProcessBookReturn(request, AdminSession));
        }


        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/reservation")]
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<dynamic>))]
        public async Task<IActionResult> ProcessBookReservation([FromBody] BookReservationRequestDto request)
        {
            return Ok(await _bookService.ProcessBookReservation(request, AdminSession));
        }


        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/collections")]
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<Pagination<CollectionDto>>))]
        public async Task<IActionResult> ListCollections([FromQuery] int page = 1, int pageSize = 10)
        {
            return Ok(await _bookService.ListCollections(page, pageSize));
        }


        [Authorize]
        [ServiceFilter(typeof(AdminUserSerssionFilter))]
        [Route("book/reservations")]
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(StandardResponse<Pagination<ReservationDto>>))]
        public async Task<IActionResult> ListReservations([FromQuery] int page = 1, int pageSize = 10)
        {
            return Ok(await _bookService.ListReservations(page, pageSize));
        }
    }
}