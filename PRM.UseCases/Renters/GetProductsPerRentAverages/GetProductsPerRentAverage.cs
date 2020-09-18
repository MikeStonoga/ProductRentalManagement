using System;
using System.Linq;
using System.Threading.Tasks;
using PRM.Domain.Renters;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Rents;

namespace PRM.UseCases.Renters.GetProductsPerRentAverages
{
    public interface IGetProductsPerRentAverage: IBaseUseCase<Guid, GetProductsPerRentAverageResult>
    {
    }

    public class GetProductsPerRentAverage : BaseUseCase<Guid, GetProductsPerRentAverageResult>, IGetProductsPerRentAverage
    {
        private readonly IReadOnlyPersistenceGateway<RenterRentalHistory> _rentersRentalHistories;
        private readonly IRentUseCasesReadOnlyInteractor _rentReadOnlyUseCases;

        public GetProductsPerRentAverage(IReadOnlyPersistenceGateway<RenterRentalHistory> rentersRentalHistories, IRentUseCasesReadOnlyInteractor rentReadOnlyUseCases)
        {
            _rentersRentalHistories = rentersRentalHistories;
            _rentReadOnlyUseCases = rentReadOnlyUseCases;
        }

        public override async Task<UseCaseResult<GetProductsPerRentAverageResult>> Execute(Guid requirement)
        {
            var history = await _rentersRentalHistories.GetAll(r => r.RenterId == requirement);
            if (!history.Success) return UseCasesResponses.Failure<GetProductsPerRentAverageResult>(history.Message);

            var rentIds = history.Response.Items.Select(r => r.RentId).ToList();
            var rents = await _rentReadOnlyUseCases.GetByIds(rentIds);
            if (!rents.Success) return UseCasesResponses.Failure<GetProductsPerRentAverageResult>(rents.Message);

            var hasAnyRent = rents.Result.Count != 0;
            if (!hasAnyRent) return UseCasesResponses.Success(new GetProductsPerRentAverageResult{ProductsPerRentAverage = 0M}, "Doesnt have any rent yet");

            var productsPerRentAverage = (decimal) rents.Result.Sum(r => r.RentedProductsCount)  / rents.Result.Count;
            var result = new GetProductsPerRentAverageResult
            {
                ProductsPerRentAverage = productsPerRentAverage
            };
            return UseCasesResponses.Success(result);
        }
    }
}