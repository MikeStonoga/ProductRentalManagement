using PRM.Domain.Rents;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos
{
    public class RentOutput : Rent
    {

        public RentOutput()
        {
               
        }

        public RentOutput(Rent rent)
        {
            Id = rent.Id;
            Name = rent.Name;
            Code = rent.Code;
            CreatorId = rent.CreatorId;
            CreationTime = rent.CreationTime;
            LastModifierId = rent.LastModifierId;
            LastModificationTime = rent.LastModificationTime;
            RenterId = rent.RenterId;
            RentPeriod = rent.RentPeriod;
            DailyPrice = rent.DailyPrice;
            DailyLateFee = rent.DailyLateFee;
            WasProductDamaged = rent.WasProductDamaged;
            DamageFee = rent.DamageFee;
            Discount = rent.Discount;
            RentedProductsCount = rent.RentedProductsCount;
        }
        
    }
}