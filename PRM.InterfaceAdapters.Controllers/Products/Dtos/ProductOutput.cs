using System;
using PRM.Domain.Products;
using PRM.Domain.Products.Enums;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos
{
    public class ProductOutput
    {
        public Guid Id { get; }

        public string Code { get; }

        public string Name { get; }

        public DateTime CreationTime { get; }

        public Guid CreatorId { get; }

        public DateTime? LastModificationTime { get; }

        public Guid? LastModifierId { get; }

        public decimal RentDailyPrice { get; }

        public decimal RentDailyLateFee { get; }

        public string Description { get; }

        public ProductStatus Status { get; }
        public ProductOutput()
        {
            
        }
        
        public ProductOutput(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            CreationTime = product.CreationTime;
            CreatorId = product.CreatorId;
            LastModificationTime = product.LastModificationTime;
            LastModifierId = product.LastModifierId;
            RentDailyPrice = product.RentDailyPrice;
            RentDailyLateFee = product.RentDailyLateFee;
            Description = product.Description;
            Status = product.Status;
        }
    }
}