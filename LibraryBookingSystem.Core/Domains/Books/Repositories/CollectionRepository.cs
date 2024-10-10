using LibraryBookingSystem.Common.ExceptionFilters;
using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Core.Interfaces.Repositories;
using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;
using LibraryBookingSystem.Data.Enums;
using LibraryBookingSystem.Data.Mappings;
using MongoDB.Entities;

namespace LibraryBookingSystem.Core.Domains.Books.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {

        public Collection GetUserActiveCollection(string customerId, string bookId)
        {
            var collection = DB.Queryable<Collection>()
                .Where(c => c.CustomerId == customerId && c.BookId == bookId && c.Status != CollectionStatus.Returned)
                .FirstOrDefault() ?? throw new BadRequestException("collectionErr-Collection not found");
            return collection;
        }

        public async Task<Collection> UpdateForReturn(string customerId, string bookId)
        {
            var collection = GetUserActiveCollection(customerId, bookId);
            collection.Status = CollectionStatus.Returned;
            collection.ActualReturnDate = DateTime.Now;
            await collection.SaveAsync();
            return collection;
        }

        public List<Collection> ListCustomerCollections(string customerId)
        {
            var collections = DB.Queryable<Collection>().Where(x => x.CustomerId == customerId).ToList();
            return collections;
        }

        public async Task<Pagination<Collection>> ListCollections(int page, int pageSize)
        {
            var query = DB.Queryable<Collection>().AsQueryable();

           
            var response = await Pagination<Collection>.CreateAsync(query, page, pageSize);
            return response;
        }

    }
}