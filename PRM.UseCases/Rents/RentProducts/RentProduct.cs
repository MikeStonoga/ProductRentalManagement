using System.Threading.Tasks;
using PRM.Domain.BaseCore.Dtos;
using PRM.Domain.Products;
using PRM.Domain.Renters;
using PRM.Domain.Rents;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.Rents.Validations;
using PRM.UseCases.Rents.Validations.Requirements.Rents;

namespace PRM.UseCases.Rents.RentProducts
{
    public interface IRentProducts : IBaseUseCase<RentProductsRequirement, RentProductsResult>
    {
    }
    
    public class RentProducts : BaseUseCase<RentProductsRequirement, RentProductsResult>, IRentProducts
    {
        private readonly IValidateRentRequirement _validateRentRequirement;
        private readonly IManipulationPersistenceGateway<Rent> _rents;
        private readonly IManipulationPersistenceGateway<RenterRentalHistory> _renterRentalHistories;
        private readonly IManipulationPersistenceGateway<Product> _products; 
        private readonly IManipulationPersistenceGateway<ProductRentalHistory> _productRentalHistories;


        public RentProducts(IManipulationPersistenceGateway<Rent> rents, IManipulationPersistenceGateway<RenterRentalHistory> renterRentalHistories, IManipulationPersistenceGateway<Product> products, IManipulationPersistenceGateway<ProductRentalHistory> productRentalHistories, IReadOnlyPersistenceGateway<Renter> renter, IValidateRentRequirement validateRentRequirement)
        {
            _rents = rents;
            _renterRentalHistories = renterRentalHistories;
            _products = products;
            _productRentalHistories = productRentalHistories;
            _validateRentRequirement = validateRentRequirement;
        }

        public override async Task<UseCaseResult<RentProductsResult>> Execute(RentProductsRequirement requirement)
        {
            var validationResponse = await _validateRentRequirement.Validate(requirement);
            if (!validationResponse.Success) return UseCasesResponses.Failure<RentProductsResult>(validationResponse.Message);
            
            var rentToCreate = new Rent(validationResponse.Result.RentPeriod, validationResponse.Result.Products, validationResponse.Result.Renter);
                
            
            var rentProductsResponse = rentToCreate.RentProducts();
            if (!rentProductsResponse.Success) return UseCasesResponses.Failure<RentProductsResult>(rentProductsResponse.Message);
            
            
            var rentCreated = await Persist(rentProductsResponse, validationResponse);
            
            var rentProductsResult = new RentProductsResult(rentCreated);
            return UseCasesResponses.Success(rentProductsResult);
        }
        
        
        
        private async Task<Rent> Persist(DomainResponseDto<Rent> rentProductsResponse, UseCaseResult<RentRequirementValidationResult> validationResponse)
        {
            // TODO: UnitOfWork
            var rentCreatedResponse = await _rents.Create(rentProductsResponse.Result);
            await _renterRentalHistories.Create(new RenterRentalHistory(rentCreatedResponse.Response, validationResponse.Result.Renter, rentCreatedResponse.Response.CreatorId));
            
            foreach (var product in validationResponse.Result.Products)
            {
                product.MarkAsUnavailable();
                await _products.Update(product);
                await _productRentalHistories.Create(new ProductRentalHistory(rentCreatedResponse.Response, product, validationResponse.Result.Renter, rentCreatedResponse.Response.CreatorId));
            }

            return rentCreatedResponse.Response;
        }
    }
}