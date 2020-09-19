using System;
using System.Threading.Tasks;
using PRM.Domain.Rents;
using PRM.Domain.Rents.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Rents.Validations.Requirements.Rents;

namespace PRM.UseCases.Rents.GetRentForecastPrices
{
    public interface IGetRentForecastPrice : IBaseUseCase<GetRentForecastPriceRequirement, GetRentForecastPriceResult>
    {
    }
    
    public class GetRentForecastPrice : BaseUseCase<GetRentForecastPriceRequirement, GetRentForecastPriceResult>, IGetRentForecastPrice
    {
        private readonly IValidateRentRequirement _validateRentRequirement;

        public GetRentForecastPrice(IValidateRentRequirement validateRentRequirement)
        {
            _validateRentRequirement = validateRentRequirement;
        }

        public override async Task<UseCaseResult<GetRentForecastPriceResult>> Execute(GetRentForecastPriceRequirement requirement)
        {
            var rentRequirement = new RentRequirement
            {
                RenterId = Guid.NewGuid(),
                EndDate = requirement.EndDate,
                StartDate = requirement.StartDate,
                ProductsIds = requirement.ProductsIds
            };
            
            var validationResponse = await _validateRentRequirement.ValidateForForecast(rentRequirement);
            if (!validationResponse.Success) return UseCasesResponses.Failure<GetRentForecastPriceResult>(validationResponse.Message);
            
            var rentForecastPrice = new Rent(Guid.NewGuid(), requirement.CreatorId, validationResponse.Result.RentPeriod, validationResponse.Result.Products, validationResponse.Result.Renter).PriceWithoutFees;
            
            var rentForecastPriceResult =new GetRentForecastPriceResult(rentForecastPrice);
            return UseCasesResponses.Success(rentForecastPriceResult);
        }
    }
}