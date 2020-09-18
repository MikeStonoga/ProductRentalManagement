using PRM.Domain.Products;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos
{
    public class ProductOutput : Product
    {

        public ProductOutput()
        {
            
        }
        
        public ProductOutput(Product product)
        {
            Description = product.Description;
            Status = product.Status;
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            CreationTime = product.CreationTime;
            CreatorId = product.CreatorId;
            DeleterId = product.DeleterId;
            DeletionTime = product.DeletionTime;
            IsDeleted = product.IsDeleted;
            LastModificationTime = product.LastModificationTime;
            LastModifierId = product.LastModifierId;
            RentDailyPrice = product.RentDailyPrice;
            RentDailyLateFee = product.RentDailyLateFee;
        }
    }
}