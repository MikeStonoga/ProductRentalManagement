using PRM.Domain.Products;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos.RentalHistory
{
    public class ProductRentalHistoryOutput : ProductRentalHistory
    {
        public ProductRentalHistoryOutput()
        {
            
        }
        public ProductRentalHistoryOutput(ProductRentalHistory history)
        {
            Id = history.Id;
            Code = history.Code;
            Name = history.Name;
            CreationTime = history.CreationTime;
            CreatorId = history.CreatorId;
            DeleterId = history.DeleterId;
            DeletionTime = history.DeletionTime;
            IsDeleted = history.IsDeleted;
            LastModificationTime = history.LastModificationTime;
            LastModifierId = history.LastModifierId;
            RentId = history.RentId;
            ProductId = history.ProductId;
        }
    }
}