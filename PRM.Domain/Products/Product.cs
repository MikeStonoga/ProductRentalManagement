using System;
using System.ComponentModel.DataAnnotations;
using PRM.Domain.BaseCore;
using PRM.Domain.Products.Enums;

namespace PRM.Domain.Products
{
    public sealed class Product : FullAuditedEntity
    {
        #region Properties

        public string Description { get; set; }
        public ProductStatus Status { get;  set; }
        public bool IsAvailable => Status == ProductStatus.Available;
        public bool IsUnavailable => Status == ProductStatus.Unavailable;
        
        private decimal _rentDailyPrice;
        public decimal RentDailyPrice
        {
            get => _rentDailyPrice;
            set
            {
                if (value == 0M) throw new ValidationException("Product must have a rent daily price");
                _rentDailyPrice = value;
            }
        }
        
        private decimal _rentDailyLateFee;

        public decimal RentDailyLateFee
        {
            get => _rentDailyLateFee;
            set
            {
                if (value == 0M) throw new ValidationException("Product must have a rent daily late fee");
                _rentDailyLateFee = value;
            }
        }

        #endregion

        #region Constructors

        public Product(Guid id, string name, string code, Guid creatorId, string description, decimal rentDailyPrice, decimal rentDailyLateFee) : base(id, name, code, creatorId)
        {
            Description = description;
            Status = ProductStatus.Available;
            RentDailyPrice = rentDailyPrice;
            RentDailyLateFee = rentDailyLateFee;
        }

        #endregion

        #region Methods

        public void MarkAsAvailable()
        {
            Status = ProductStatus.Available;
        }

        public void MarkAsUnavailable()
        {
            Status = ProductStatus.Unavailable;
        }
        
        #endregion
        
    }
}