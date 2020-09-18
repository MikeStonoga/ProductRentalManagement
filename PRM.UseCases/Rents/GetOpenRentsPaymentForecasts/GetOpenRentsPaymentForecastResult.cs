namespace PRM.UseCases.Rents.GetOpenRentsPaymentForecasts
{
    public class GetOpenRentsPaymentForecastResult
    {
        public decimal PaymentForecast { get; set; }

        public GetOpenRentsPaymentForecastResult()
        {
            
        }
        public GetOpenRentsPaymentForecastResult(decimal openRentsPaymentForecast)
        {
            PaymentForecast = openRentsPaymentForecast;
        }
    }
}