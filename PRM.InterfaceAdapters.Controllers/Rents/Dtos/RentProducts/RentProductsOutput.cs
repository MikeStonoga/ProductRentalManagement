using PRM.Domain.Rents.Dtos;
using PRM.UseCases.Rents.RentProducts;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos.RentProducts
{
    public class RentProductsOutput : RentProductsResult
    {
        public RentProductsOutput()
        {
            
        }

        public RentProductsOutput(RentProductsResult rentProductsResult)
        {
            Id = rentProductsResult.Id;
            Code = rentProductsResult.Code;
            Name = rentProductsResult.Name;
            RenterId = rentProductsResult.RenterId;
            RentPeriod = rentProductsResult.RentPeriod;
            DailyPrice = rentProductsResult.DailyPrice;
            DailyLateFee = rentProductsResult.DailyLateFee;
            WasProductDamaged = rentProductsResult.WasProductDamaged;
            DamageFee = rentProductsResult.DamageFee;
            RentedProductsCount = rentProductsResult.RentedProductsCount;
        }
    }
}