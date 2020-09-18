using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Products.CheckAvailabilities;

namespace PRM.UseCases.Products.CheckProductAvailabilities
{
    public interface ICheckProductAvailability : IBaseUseCase<Guid, CheckProductAvailabilityResult>
    {
    }
    
    public class CheckProductAvailability : BaseUseCase<Guid, CheckProductAvailabilityResult>, ICheckProductAvailability
    {
        private readonly IReadOnlyPersistenceGateway<Product> _products;
        private readonly IProductRentalHistoryUseCasesReadOnlyInteractor _productsRentalHistories;

        public CheckProductAvailability(IProductRentalHistoryUseCasesReadOnlyInteractor productsRentalHistories, IReadOnlyPersistenceGateway<Product> products)
        {
            _productsRentalHistories = productsRentalHistories;
            _products = products;
        }


        public override async Task<UseCaseResult<CheckProductAvailabilityResult>> Execute(Guid productId)
        {
            var productToCheckAvailability = await _products.GetById(productId);
            if (!productToCheckAvailability.Success) return UseCasesResponses.Failure<CheckProductAvailabilityResult>(productToCheckAvailability.Message);

            if (productToCheckAvailability.Response.IsAvailable)
            {
                return UseCasesResponses.Success(new CheckProductAvailabilityResult(productToCheckAvailability.Response.IsAvailable));
            }

            var lastProductRent = await _productsRentalHistories.GetLastProductRent(productId);
            
            return !lastProductRent.Success
                ? UseCasesResponses.Failure<CheckProductAvailabilityResult>(lastProductRent.Message)
                : UseCasesResponses.Success(new CheckProductAvailabilityResult(lastProductRent.Result.LastProductRent.RentPeriod.EndDate));
        }
    }
}