using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRM.Domain.Products;
using PRM.Infrastructure.ApplicationDelivery.WebApiHost.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore;
using PRM.InterfaceAdapters.Controllers.Products;
using PRM.InterfaceAdapters.Controllers.Products.Dtos;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.CheckProductAvailabilities;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.GetLastProductRents;
using PRM.InterfaceAdapters.Controllers.Products.Dtos.RentalHistory;
using PRM.UseCases.Products;

namespace PRM.Infrastructure.ApplicationDelivery.WebApiHost.Products
{

    public class ProductController : BaseManipulationWebController<Product, ProductInput, ProductOutput, IProductUseCasesManipulationInteractor, IProductManipulationController>, IProductManipulationController
    {
        public ProductController(IProductUseCasesManipulationInteractor useCaseInteractor, IProductManipulationController manipulationController) : base(useCaseInteractor, manipulationController)
        {
        }

        [HttpGet]
        public async Task<ApiResponse<CheckProductAvailabilityOutput>> CheckProductAvailability([FromQuery] Guid productId)
        {
            return await ReadOnlyController.CheckProductAvailability(productId);
        }

        [HttpGet]
        public async Task<ApiResponse<GetLastProductRentOutput>> GetLastRent([FromQuery] Guid productId)
        {
            return await ReadOnlyController.GetLastRent(productId);
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetAvailables()
        {
            return await ReadOnlyController.GetAvailables();
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Product, ProductOutput>>> GetUnavailables()
        {
            return await ReadOnlyController.GetUnavailables();
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<ProductRentalHistory, ProductRentalHistoryOutput>>> GetRentalHistory([FromQuery] Guid productId)
        {
            return await ReadOnlyController.GetRentalHistory(productId);
        }
    }
}