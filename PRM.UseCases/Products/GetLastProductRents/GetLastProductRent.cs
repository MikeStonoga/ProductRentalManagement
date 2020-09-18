using System;
using System.Linq;
using System.Threading.Tasks;
using PRM.Domain.Rents;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Products.GetRentalHistories;
using PRM.UseCases.Rents;

namespace PRM.UseCases.Products.GetLastProductRents
{
    public interface IGetLastProductRent : IBaseUseCase<Guid, GetLastProductRentResult>
    {
    }
    
    public class GetLastProductRent : BaseUseCase<Guid, GetLastProductRentResult>,  IGetLastProductRent
    {
        private readonly IRentUseCasesReadOnlyInteractor _rentsUsesCases;
        private readonly IGetProductRentalHistory _getProductRentalHistory;

        public GetLastProductRent(IRentUseCasesReadOnlyInteractor rentsUsesCases, IGetProductRentalHistory getProductRentalHistory)
        {
            _rentsUsesCases = rentsUsesCases;
            _getProductRentalHistory = getProductRentalHistory;
        }

        public override async Task<UseCaseResult<GetLastProductRentResult>> Execute(Guid productId)
        {
            var rentalHistory = await _getProductRentalHistory.Execute(productId);
            if (!rentalHistory.Success) return UseCasesResponses.Failure<GetLastProductRentResult>(rentalHistory.Message);
            if (rentalHistory.Result.TotalCount == 0) return UseCasesResponses.Success<GetLastProductRentResult>("Dont have any rent yet");

            var orderedByMostRecent = rentalHistory.Result.Items.OrderByDescending(history => history.CreationTime).ToList();
            var lastProductRentId = orderedByMostRecent[0].RentId;

            var lastProductRent = await _rentsUsesCases.GetById(lastProductRentId);
            
            return !lastProductRent.Success 
                ? UseCasesResponses.Failure<GetLastProductRentResult>(lastProductRent.Message) 
                : UseCasesResponses.Success(new GetLastProductRentResult(lastProductRent.Result));
        }
    }
}