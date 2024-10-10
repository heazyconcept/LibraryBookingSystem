using LibraryBookingSystem.Data.Common;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Sessions;
using MongoDB.Entities;

namespace LibraryBookingSystem.Data.Entities
{
    [Collection("Books")]
    public class Book:EntityBase
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string ShelfNumber { get; set; }
        public BookStatus BookStatus { get; set; }
        public DateTime AvailableDate { get; set; }
        public DateTime ReservedOrCollectedDate { get; set; }
        public string ReservedOrCollectedBy { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public One<Customer> ReservedOrCollectedByCustomerEntity { get; set; }

        [Ignore]
        public Customer? ReservedOrCollectedByCustomer => ReservedOrCollectedByCustomerEntity?.ToEntityAsync().Result;

        public static Book CreateNew(BookRequestDto request, AdminSession adminSession)
        {
            return new Book
            {
                Name = request.Name,
                Genre = request.Genre,
                ShelfNumber = request.ShelfNumber,
                Author = request.Author,
                Publisher = request.Publisher,
                ISBN = request.ISBN,
                Description = request.Description,
                BookStatus = BookStatus.Available,
                AvailableDate = DateTime.Now,
                CreatedBy = adminSession.Email
            };
        }

        public void UpdateBook(BookRequestDto request, AdminSession admin)
        {
            Name = request.Name;
            Genre = request.Genre;
            ShelfNumber = request.ShelfNumber;
            Author = request.Author;
            Publisher = request.Publisher;
            ISBN = request.ISBN;
            Description = request.Description;
            ModifiedBy = admin.Email;
        }

          public void UpdateStatus(BookStatus status)
        {
            BookStatus = status;
        }
    }
}
