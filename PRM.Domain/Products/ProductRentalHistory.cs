using System;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Extensions;
using PRM.Domain.Renters;
using PRM.Domain.Rents;

namespace PRM.Domain.Products
{
    public sealed class ProductRentalHistory : FullAuditedEntity
    {
        #region Properties
        public Guid RentId { get; private set; }
        public Guid ProductId { get; private set; }
        #endregion

        #region Constructors

        private ProductRentalHistory(){ }
        
        public ProductRentalHistory(Guid id, Rent rent, Product product, Renter renter) 
            : base(
                id, 
                name: product.Name + " - " + renter.Name + " - " + rent.RentPeriod.StartDate.FormatDate() + " - " + rent.RentPeriod.EndDate.FormatDate(), 
                code: Guid.NewGuid().ToString().Substring(0, 5), 
                rent.CreatorId
                )
        {
            RentId = rent.Id;
            ProductId = product.Id;
        }
        #endregion
    }
}