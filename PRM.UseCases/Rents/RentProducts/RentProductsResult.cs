using System;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Rents;

namespace PRM.UseCases.Rents.RentProducts
{
    public class RentProductsResult : Rent
    {
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