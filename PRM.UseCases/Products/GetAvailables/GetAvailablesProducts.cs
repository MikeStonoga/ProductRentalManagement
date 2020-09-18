using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.Products.GetAvailables
{
    public interface IGetAvailablesProducts : IBaseUseCase<GetAllResponse<Product>>
    {
    }
    
    public class GetAvailablesProducts : BaseUseCase<GetAllResponse<Product>>, IGetAvailablesProducts
    {
        private readonly IReadOnlyPersistenceGateway<Product> _products;

        public GetAvailablesProducts(IReadOnlyPersistenceGateway<Product> products)
        {
            _products = products;
        }

        public override async Task<UseCaseResult<GetAllResponse<Product>>> Execute()
        {
            var availableProducts = await _products.GetAll(p => p.IsAvailable);
            return UseCasesResponses.GetUseCaseResult(availableProducts);
        }
    }
}