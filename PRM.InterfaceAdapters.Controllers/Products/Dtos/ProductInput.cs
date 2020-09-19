using System;
using PRM.Domain.Products;
using PRM.Domain.Products.Enums;
using PRM.InterfaceAdapters.Controllers.BaseCore;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos
{
    public class ProductInput : IAmManipulationInput<Product>
    {
        
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

        public decimal RentDailyPrice { get; set; }

        public decimal RentDailyLateFee { get; set; }

        public string Description { get; set; }

        public ProductStatus Status { get; set; }
        
        public Product MapToEntity()
        {
            return new Product
            {
                Id = Id,
                Code = Code,
                Name = Name,
                CreationTime = CreationTime,
                CreatorId = CreatorId,
                LastModificationTime = LastModificationTime,
                LastModifierId = LastModifierId,
                RentDailyPrice = RentDailyPrice,
                RentDailyLateFee = RentDailyLateFee,
                Description = Description,
                Status = Status
            };
        }
    }
}