using LibraryBookingSystem.Data.Dtos;
using LibraryBookingSystem.Data.Entities;

namespace LibraryBookingSystem.Data.Mappings
{
    public static class CollectionMappings
    {
        
        public static CollectionDto ToCollectionDataDto(this Collection collection)
        {
            if (collection == null)
            {
                return new CollectionDto();
            }
            return new CollectionDto
            {
                CustomerId = collection.CustomerId,
                Book = collection.Book.ToBookDataDto(),
                ExpectedReturnDate = collection.ExpectedReturnDate,
                ActualReturnDate = collection.ActualReturnDate,
                Status = collection.Status,
                CreatedBy = collection.CreatedBy,
                CreatedDate = collection.CreatedOn,
                ModifiedBy = collection.ModifiedBy,
                ModifiedDate = collection.ModifiedOn
            };
        }

    }
}