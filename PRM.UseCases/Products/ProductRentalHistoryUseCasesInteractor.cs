using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Products.GetLastProductRents;
using PRM.UseCases.Products.GetRentalHistories;

namespace PRM.UseCases.Products
{
    public interface IProductRentalHistoryUseCasesReadOnlyInteractor : IBaseUseCaseReadOnlyInteractor<ProductRentalHistory>
    {
        Task<UseCaseResult<GetAllResponse<ProductRentalHistory>>> GetRentalHistory(Guid productId);
        Task<UseCaseResult<GetLastProductRentResult>> GetLastProductRent(Guid productId);
    }
        
    public class ProductRentalHistoryUseCasesReadOnlyInteractor : BaseUseCaseReadOnlyInteractor<ProductRentalHistory>, IProductRentalHistoryUseCasesReadOnlyInteractor
    {
        private readonly IGetProductRentalHistory _getProductRentalHistory;
        private readonly IGetLastProductRent _getLastProductRent;

        public ProductRentalHistoryUseCasesReadOnlyInteractor(IReadOnlyPersistenceGateway<ProductRentalHistory> readOnlyPersistenceGateway, IGetLastProductRent getLastProductRent, IGetProductRentalHistory getProductRentalHistory) : base(readOnlyPersistenceGateway)
        {
            _getLastProductRent = getLastProductRent;
            _getProductRentalHistory = getProductRentalHistory;
        }

        public async Task<UseCaseResult<GetAllResponse<ProductRentalHistory>>> GetRentalHistory(Guid productId)
        {
            return await UseCasesResponses.GetUseCaseExecutionResponse<IGetProductRentalHistory, Guid, GetAllResponse<ProductRentalHistory>>(_getProductRentalHistory, productId);
        }

        public async Task<UseCaseResult<GetLastProductRentResult>> GetLastProductRent(Guid productId)
        {
            return await UseCasesResponses.GetUseCaseExecutionResponse<IGetLastProductRent, Guid, GetLastProductRentResult>(_getLastProductRent, productId);
        }
    }
}