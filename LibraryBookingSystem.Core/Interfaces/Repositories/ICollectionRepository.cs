using LibraryBookingSystem.Common.Helpers;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Core.Interfaces.Repositories
{
    public interface ICollectionRepository
    {
        Collection GetUserActiveCollection(string customerId, string bookId);

        Task<Collection> UpdateForReturn(string customerId, string bookId);

        List<Collection> ListCustomerCollections(string customerId);

        Task<Pagination<Collection>> ListCollections(int page, int pageSize);
    }
}