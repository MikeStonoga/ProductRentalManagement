using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Dtos;
using PRM.Domain.BaseCore.Extensions;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Products;
using PRM.Domain.Products.Extensions;
using PRM.Domain.Renters;
using PRM.Domain.Rents.Enums;

namespace PRM.Domain.Rents
{
    public sealed class Rent : FullAuditedEntity
    {
        #region Properties
        public Guid RenterId { get; set; }
        private RentStatus Status { get; set; }
        public DateRange RentPeriod { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal DailyLateFee { get; set; }
        public bool WasProductDamaged { get; set; }
        public decimal DamageFee { get; set; }
        public decimal Discount { get; set; }
        public int RentedProductsCount { get; set; }

        public decimal CurrentRentPaymentValue => PriceWithDiscount;
        public decimal PriceWithFees => PriceWithoutFees + LateFee + DamageFee;
        public decimal PriceWithDiscount => PriceWithFees - Discount;
        public decimal PriceWithoutFees => DailyPrice * (RentDays - LateDays) - DamageFee;
        public decimal PriceWithoutFeesWithDiscount => PriceWithoutFees - Discount;
        public decimal AverageTicket => CurrentRentPaymentValue / RentedProductsCount;
        public decimal AverageTicketWithDiscount => PriceWithFees / RentedProductsCount;
        public decimal AverageTicketWithoutFees => PriceWithoutFees / RentedProductsCount;
        public decimal AverageTicketWithoutFeesWithDiscount => PriceWithoutFeesWithDiscount / RentedProductsCount;
        public int RentDays => RentPeriod.Days + LateDays;
        public decimal LateFee => IsLate ? DailyLateFee * LateDays : 0;
        public bool IsLate => DateTime.Now > RentPeriod.EndDate;
        public int LateDays => IsLate ? DateTime.Now.Date.Subtract(RentPeriod.EndDate.Date).Days : 0;
        public bool IsOpen => Status == RentStatus.Open;
        public bool IsClosed => Status == RentStatus.Closed;
        public bool IsFinished => IsClosed;

        #endregion

        #region Constructors
        
        private Rent() {}
        
        public Rent(Guid id, Guid creatorId, DateRange rentPeriod, List<Product> productsToRent, Renter renter) 
            : base(
                id, 
                name: renter.Name + " - " + rentPeriod.StartDate + " - " +  productsToRent.Count + " products", 
                code: Guid.NewGuid().ToString().Substring(0, 5), 
                creatorId
                )
        {
            var isTryingToRentWithoutProducts = productsToRent == null || productsToRent.Count == 0;
            if (isTryingToRentWithoutProducts) throw new ValidationException("Trying to create a Rent without any Products");

            bool IsUnavailableProduct(Product product) => product.IsUnavailable;
            var hasUnavailableProduct = productsToRent.Any(IsUnavailableProduct); 
            if (hasUnavailableProduct) throw new ValidationException(productsToRent.GetProductsWithErrorMessage("Trying to rent unavailable products:", IsUnavailableProduct));
            RentedProductsCount = productsToRent.Count;

            SetName("Created: " + DateTime.Now.FormatDate() + " - Start: " + rentPeriod.StartDate.FormatDate() + " - End: " + rentPeriod.EndDate.FormatDate());
            DailyPrice = productsToRent.Sum(p => (p.RentDailyPrice));
            RentPeriod = rentPeriod;
            DailyLateFee = productsToRent.Sum(p => p.RentDailyLateFee);
            RenterId = renter.Id;
        }
        
        #endregion

        #region Methods
        
        public DomainResponseDto<Rent> RentProducts()
        {
            Status = RentStatus.Open;
            return this.GetSuccessResponse("Rented");
        }

        public DomainResponseDto<Rent> FinishRent(decimal damageFee = 0, decimal discount = 0)
        {
            if (IsFinished) return DomainValidations.Failure<Rent>("Already finished: " + LastModificationTime?.FormatDate());

            var wasProductsDamaged = damageFee != 0M;
            if (wasProductsDamaged) AddDamageFee(damageFee);
            
            var hasDiscount = discount != 0M;
            if (hasDiscount) AddDiscount(discount);

            Status = RentStatus.Closed;
            
            return this.GetSuccessResponse("RentFinished");
        }

        public void AddDiscount(decimal discount)
        {
            Discount = discount;
        }

        public void AddDamageFee(decimal damageFee)
        {
            WasProductDamaged = true;
            DamageFee = damageFee;
        }
        
        #endregion
    }
    
}