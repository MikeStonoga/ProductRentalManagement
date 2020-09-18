using System;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Extensions;
using PRM.Domain.Renters;
using PRM.Domain.Rents;

namespace PRM.Domain.Products
{
    public class ProductRentalHistory : FullAuditedEntity
    {
        #region Properties
        public Guid RentId { get; set; }
        public Guid ProductId { get; set; }
        #endregion

        #region Constructors

        public ProductRentalHistory()
        {
            
        }
        
        public ProductRentalHistory(Rent rent, Product product, Renter renter, Guid creatorId)
        {
            Name = product.Name + " - " + renter.Name + " - " + rent.RentPeriod.StartDate.FormatDate() + " - " + rent.RentPeriod.EndDate.FormatDate(); 
            RentId = rent.Id;
            ProductId = product.Id;
            CreatorId = creatorId;
        }
        #endregion
    }
}