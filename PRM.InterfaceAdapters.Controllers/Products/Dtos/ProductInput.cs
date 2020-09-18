using PRM.Domain.Products;
using PRM.InterfaceAdapters.Controllers.BaseCore;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos
{
    public class ProductInput : Product, IAmManipulationInput<Product>
    {
        public Product MapToEntity()
        {
            return new Product
            {
                Description = Description,
                Status = Status,
                Id = Id,
                Code = Code,
                Name = Name,
                CreationTime = CreationTime,
                CreatorId = CreatorId,
                DeleterId = DeleterId,
                DeletionTime = DeletionTime,
                IsDeleted = IsDeleted,
                LastModificationTime = LastModificationTime,
                LastModifierId = LastModifierId,
                RentDailyPrice = RentDailyPrice,
                RentDailyLateFee = RentDailyLateFee,
            };
        }
    }
}