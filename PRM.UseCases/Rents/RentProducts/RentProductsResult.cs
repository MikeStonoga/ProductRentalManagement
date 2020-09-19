using System;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Rents;

namespace PRM.UseCases.Rents.RentProducts
{
    public class RentProductsResult
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid RenterId { get; set; }

        public DateRange RentPeriod { get; set; }

        public decimal DailyPrice { get; set; }

        public decimal DailyLateFee { get; set; }

        public bool WasProductDamaged { get; set; }

        public decimal DamageFee { get; set; }

        public int RentedProductsCount { get; set; }
        
        public RentProductsResult()
        {
            RentPeriod = new DateRange(DateTime.MinValue, DateTime.MaxValue);
        }
        
        public RentProductsResult(Rent rent)
        {
            Id = rent.Id;
            Code = rent.Code;
            Name = rent.Name;
            RenterId = rent.RenterId;
            RentPeriod = rent.RentPeriod;
            DailyPrice = rent.DailyPrice;
            DailyLateFee = rent.DailyLateFee;
            WasProductDamaged = rent.WasProductDamaged;
            DamageFee = rent.DamageFee;
            RentedProductsCount = rent.RentedProductsCount;
        }
    }
}