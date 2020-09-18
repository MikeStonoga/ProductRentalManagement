using PRM.Domain.Rents;
using PRM.UseCases.Rents.GetRentForecastPrices;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetRentForecastPrices
{
    public class GetRentForecastPriceOutput : GetRentForecastPriceResult
    {
        public GetRentForecastPriceOutput()
        {
            
        }

        public GetRentForecastPriceOutput(GetRentForecastPriceResult result)
        {
            RentForecastPrice = result.RentForecastPrice;
        }
    }
}