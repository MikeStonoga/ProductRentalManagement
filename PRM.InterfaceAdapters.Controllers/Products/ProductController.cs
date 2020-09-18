using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Controllers.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore.Extensions;
using PRM.InterfaceAdapters.Controllers.Products.Dtos;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.CheckProductAvailabilities;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.GetLastProductRents;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.RentalHistory;
using PRM.UseCases.Products;
using PRM.UseCases.Products.CheckAvailabilities;
using PRM.UseCases.Products.GetLastProductRents;

namespace PRM.InterfaceAdapters.Controllers.Products
{
    public interface IProductReadOnlyController : IBaseReadOnlyController<Product, ProductOutput>
    {
        Task<ApiResponse<CheckProductAvailabilityOutput>> CheckProductAvailability(Guid productId);
        Task<ApiResponse<GetLastProductRentOutput>> GetLastRent(Guid productId);
        Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetAvailables();
        Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetUnavailables();
        Task<ApiResponse<GetAllResponse<ProductRentalHistory, ProductRentalHistoryOutput>>> GetRentalHistory(Guid productId);
    }

    public class ProductReadOnlyController : BaseReadOnlyController<Product, ProductOutput, IProductUseCasesReadOnlyInteractor>, IProductReadOnlyController
    {
        private readonly IProductRentalHistoryUseCasesReadOnlyInteractor _productRentalHistoryUseCasesReadOnlyInteractor;
        public ProductReadOnlyController(IProductUseCasesReadOnlyInteractor useCaseReadOnlyInteractor, IProductRentalHistoryUseCasesReadOnlyInteractor productRentalHistoryUseCasesReadOnlyInteractor) : base(useCaseReadOnlyInteractor)
        {
            _productRentalHistoryUseCasesReadOnlyInteractor = productRentalHistoryUseCasesReadOnlyInteractor;
        }

        public async Task<ApiResponse<CheckProductAvailabilityOutput>> CheckProductAvailability(Guid productId)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<Guid, CheckProductAvailabilityResult, Guid, CheckProductAvailabilityOutput>(UseCaseReadOnlyInteractor.CheckProductAvailability, productId);
        }

        public async Task<ApiResponse<GetLastProductRentOutput>> GetLastRent(Guid productId)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<Guid, GetLastProductRentResult, Guid, GetLastProductRentOutput>(_productRentalHistoryUseCasesReadOnlyInteractor.GetLastProductRent, productId);
        }

        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetAvailables()
        {
            return await ApiResponses.GetUseCaseInteractorResponse<Product, ProductOutput>(UseCaseReadOnlyInteractor.GetAvailablesProducts);
        }

        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetUnavailables()
        {
            return await ApiResponses.GetUseCaseInteractorResponse<Product, ProductOutput>(UseCaseReadOnlyInteractor.GetUnavailablesProducts);
        }

        public async Task<ApiResponse<GetAllResponse<ProductRentalHistory, ProductRentalHistoryOutput>>> GetRentalHistory(Guid productId)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<ProductRentalHistory, ProductRentalHistoryOutput>(_productRentalHistoryUseCasesReadOnlyInteractor.GetRentalHistory, productId);
        }
    }

    public interface IProductManipulationController : IBaseManipulationController<Product, ProductInput, ProductOutput>, IProductReadOnlyController
    {

    }

    public class ProductManipulationController : BaseManipulationController<Product, ProductInput, ProductOutput, IProductUseCasesManipulationInteractor, IProductReadOnlyController>, IProductManipulationController
    {
        public ProductManipulationController(IProductUseCasesManipulationInteractor useCaseInteractor, IProductReadOnlyController readOnlyController) : base(useCaseInteractor, readOnlyController)
        {
        }

        public async Task<ApiResponse<CheckProductAvailabilityOutput>> CheckProductAvailability(Guid productId)
        {
            return await ReadOnlyController.CheckProductAvailability(productId);
        }

        public async Task<ApiResponse<GetLastProductRentOutput>> GetLastRent(Guid productId)
        {
            return await ReadOnlyController.GetLastRent(productId);
        }

        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetAvailables()
        {
            return await ReadOnlyController.GetAvailables();
        }

        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetUnavailables()
        {
            return await ReadOnlyController.GetUnavailables();
        }

        public async Task<ApiResponse<GetAllResponse<ProductRentalHistory, ProductRentalHistoryOutput>>> GetRentalHistory(Guid productId)
        {
            return await ReadOnlyController.GetRentalHistory(productId);
        }
    }
}