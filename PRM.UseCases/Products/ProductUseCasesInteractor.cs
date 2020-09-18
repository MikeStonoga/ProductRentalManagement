using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Products.CheckAvailabilities;
using PRM.UseCases.Products.CheckProductAvailabilities;
using PRM.UseCases.Products.GetAvailables;

namespace PRM.UseCases.Products
{
    public interface IProductUseCasesReadOnlyInteractor : IBaseUseCaseReadOnlyInteractor<Product>
    {
        Task<UseCaseResult<CheckProductAvailabilityResult>> CheckProductAvailability(Guid productId);
        Task<UseCaseResult<GetAllResponse<Product>>> GetAvailablesProducts();
        Task<UseCaseResult<GetAllResponse<Product>>> GetUnavailablesProducts();
    }
        
    public class ProductUseCasesReadOnlyInteractor : BaseUseCaseReadOnlyInteractor<Product>, IProductUseCasesReadOnlyInteractor
    {
        private readonly ICheckProductAvailability _checkProductAvailability;
        private readonly IGetAvailablesProducts _getAvailablesProducts;
        private readonly IGetUnavailablesProducts _getUnavailablesProducts;
        
        public ProductUseCasesReadOnlyInteractor(IReadOnlyPersistenceGateway<Product> readOnlyPersistenceGateway, ICheckProductAvailability checkProductAvailability, IGetAvailablesProducts getAvailablesProducts, IGetUnavailablesProducts getUnavailablesProducts) : base(readOnlyPersistenceGateway)
        {
            _checkProductAvailability = checkProductAvailability;
            _getAvailablesProducts = getAvailablesProducts;
            _getUnavailablesProducts = getUnavailablesProducts;
        }

        public async Task<UseCaseResult<CheckProductAvailabilityResult>> CheckProductAvailability(Guid productId)
        {
            return await UseCasesResponses.GetUseCaseExecutionResponse<ICheckProductAvailability, Guid, CheckProductAvailabilityResult>(_checkProductAvailability, productId);
        }

        public async Task<UseCaseResult<GetAllResponse<Product>>> GetAvailablesProducts()
        {
            return await UseCasesResponses.GetUseCaseExecutionResponse<IGetAvailablesProducts, GetAllResponse<Product>>(_getAvailablesProducts);
        }

        public async Task<UseCaseResult<GetAllResponse<Product>>> GetUnavailablesProducts()
        {
            return await UseCasesResponses.GetUseCaseExecutionResponse<IGetUnavailablesProducts, GetAllResponse<Product>>(_getUnavailablesProducts);
        }
    }

    public interface IProductUseCasesManipulationInteractor : IBaseUseCaseManipulationInteractor<Product>, IProductUseCasesReadOnlyInteractor
    {

    }

    public class ProductUseCasesManipulationInteractor : BaseUseCaseManipulationInteractor<Product, IProductUseCasesReadOnlyInteractor>, IProductUseCasesManipulationInteractor
    {
        public ProductUseCasesManipulationInteractor(IManipulationPersistenceGateway<Product> persistenceGateway, IProductUseCasesReadOnlyInteractor useCasesReadOnlyInteractor, IManipulationPersistenceGateway<ProductRentalHistory> productRentalHistories) : base(persistenceGateway, useCasesReadOnlyInteractor)
        {
        }

        public async Task<UseCaseResult<CheckProductAvailabilityResult>> CheckProductAvailability(Guid productId)
        {
            return await UseCasesReadOnlyInteractor.CheckProductAvailability(productId);
        }

        public async Task<UseCaseResult<GetAllResponse<Product>>> GetAvailablesProducts()
        {
            return await UseCasesReadOnlyInteractor.GetAvailablesProducts();
        }

        public async Task<UseCaseResult<GetAllResponse<Product>>> GetUnavailablesProducts()
        {
            return await UseCasesReadOnlyInteractor.GetUnavailablesProducts();
        }
    }
}