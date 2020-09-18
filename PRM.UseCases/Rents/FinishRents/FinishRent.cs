using System.Linq;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.Domain.Rents;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.Rents.FinishRents
{
    public interface IFinishRent : IBaseUseCase<FinishRentRequirement, FinishRentResult>
    {
        
    }
    
    public class FinishRent : BaseUseCase<FinishRentRequirement, FinishRentResult>, IFinishRent
    {
        private readonly IManipulationPersistenceGateway<Rent> _rents;
        private readonly IManipulationPersistenceGateway<Product> _products;
        private readonly IManipulationPersistenceGateway<ProductRentalHistory> _productRentalHistories;

        public FinishRent(IManipulationPersistenceGateway<Rent> rents, IManipulationPersistenceGateway<Product> products, IManipulationPersistenceGateway<ProductRentalHistory> productRentalHistories)
        {
            _rents = rents;
            _products = products;
            _productRentalHistories = productRentalHistories;
        }

        public override async Task<UseCaseResult<FinishRentResult>> Execute(FinishRentRequirement requirement)
        {
            
            var rentToFinish = await _rents.GetById(requirement.RentId);
            if (!rentToFinish.Success) return UseCasesResponses.Failure<FinishRentResult>(rentToFinish.Message);

            var finishRentResponse = rentToFinish.Response.FinishRent(requirement.DamageFee, requirement.Discount);
            if (!finishRentResponse.Success) return UseCasesResponses.Failure<FinishRentResult>(finishRentResponse.Message);
            
            // TODO: UnitOfWork
            var rentProducts = await _productRentalHistories.GetAll(history => history.RentId == requirement.RentId);
            var productsToTurnAvailableIds = rentProducts.Response.Items.Select(e => e.ProductId).ToList();
            var productsToTurnAvailable = await _products.GetByIds(productsToTurnAvailableIds);
            
            foreach (var product in productsToTurnAvailable.Response)
            {
                product.MarkAsAvailable();
                await _products.Update(product);
            }

            await _rents.Update(finishRentResponse.Result);
            
            var finishRentResult = new FinishRentResult(finishRentResponse.Result.CurrentRentPaymentValue);
            return UseCasesResponses.Success(finishRentResult);
        }
    }
}