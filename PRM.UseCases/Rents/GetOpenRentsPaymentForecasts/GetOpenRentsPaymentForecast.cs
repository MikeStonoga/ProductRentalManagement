using System.Linq;
using System.Threading.Tasks;
using PRM.Domain.Rents;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.Rents.GetOpenRentsPaymentForecasts
{
    public interface IGetOpenRentsPaymentForecast : IBaseUseCase<GetOpenRentsPaymentForecastRequirement, GetOpenRentsPaymentForecastResult>
    {
    }
    public class GetOpenRentsPaymentForecast : BaseUseCase<GetOpenRentsPaymentForecastRequirement, GetOpenRentsPaymentForecastResult>, IGetOpenRentsPaymentForecast
    {
        private readonly IReadOnlyPersistenceGateway<Rent> _rents;

        public GetOpenRentsPaymentForecast(IReadOnlyPersistenceGateway<Rent> rents)
        {
            _rents = rents;
        }

        public override async Task<UseCaseResult<GetOpenRentsPaymentForecastResult>> Execute(GetOpenRentsPaymentForecastRequirement requirement)
        {
               
            var openRents = await _rents.GetAll(
                r => !r.IsFinished && r.RentPeriod.EndDate <= requirement.TargetDate.Date.AddDays(1).AddTicks(-1)
                );
            if (!openRents.Success) return UseCasesResponses.Failure<GetOpenRentsPaymentForecastResult>(openRents.Message);

            var openRentsPaymentForecast = openRents.Response.Items.Sum(r => r.CurrentRentPaymentValue);
            var result = new GetOpenRentsPaymentForecastResult(openRentsPaymentForecast);
            return UseCasesResponses.Success(result);
        }
    }
}