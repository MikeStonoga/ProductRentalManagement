using PRM.UseCases.Rents.GetOpenRentsPaymentForecasts;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetOpenRentsPaymentForecasts
{
    public class GetOpenRentsPaymentForecastOutput : GetOpenRentsPaymentForecastResult
    {
        public GetOpenRentsPaymentForecastOutput()
        {
        }

        public GetOpenRentsPaymentForecastOutput(GetOpenRentsPaymentForecastResult result)
        {
            PaymentForecast = result.PaymentForecast;
        }
    }
}