using System;
using System.Threading.Tasks;
using PRM.Domain.Products;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.Products.GetRentalHistories
{
    public interface IGetProductRentalHistory : IBaseUseCase<Guid, GetAllResponse<ProductRentalHistory>>
    {
    }
    
    public class GetProductRentalHistory : BaseUseCase<Guid, GetAllResponse<ProductRentalHistory>>, IGetProductRentalHistory
    {
        private readonly IReadOnlyPersistenceGateway<ProductRentalHistory> _productRentalHistories;

        public GetProductRentalHistory(IReadOnlyPersistenceGateway<ProductRentalHistory> productRentalHistories)
        {
            _productRentalHistories = productRentalHistories;
        }

        public override async Task<UseCaseResult<GetAllResponse<ProductRentalHistory>>> Execute(Guid productId)
        {
            var getRentalHistory = await _productRentalHistories.GetAll(history => history.ProductId == productId);
            return UseCasesResponses.GetUseCaseResult(getRentalHistory);
        }
    }
}