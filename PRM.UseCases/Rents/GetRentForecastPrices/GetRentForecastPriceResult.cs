namespace PRM.UseCases.Rents.GetRentForecastPrices
{
    public class GetRentForecastPriceResult
    {
        public decimal RentForecastPrice { get; set; }
        
        public GetRentForecastPriceResult()
        {
            
        }
        
        public GetRentForecastPriceResult(decimal rentForecastPrice)
        {
            RentForecastPrice = rentForecastPrice;
        }
        
    }
}