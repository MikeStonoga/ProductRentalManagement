using System;
using System.Linq;
using System.Threading.Tasks;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Products;
using PRM.Domain.Renters;
using PRM.Domain.Rents.Dtos;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;
using PRM.UseCases.BaseCore.Validations;

namespace PRM.UseCases.Rents.Validations.Requirements.Rents
{
    public interface IValidateRentRequirement : IValidateUseCaseRequirement<RentRequirement, RentRequirementValidationResult>
    {
        Task<UseCaseResult<RentRequirementValidationResult>> ValidateForForecast(RentRequirement requirement);
    }
    public class ValidateRentRequirement : UseCaseRequirementValidation<RentRequirement, RentRequirementValidationResult>, IValidateRentRequirement
    {
        private readonly IReadOnlyPersistenceGateway<Renter> _renter;
        private readonly IReadOnlyPersistenceGateway<Product> _products; 

        public ValidateRentRequirement(IReadOnlyPersistenceGateway<Renter> renter, IReadOnlyPersistenceGateway<Product> products)
        {
            _renter = renter;
            _products = products;
        }

        public override async Task<UseCaseResult<RentRequirementValidationResult>> Validate(RentRequirement requirement)
        {
            var isTryingToRentWithoutProducts = requirement.ProductsIds == null; 
            if (isTryingToRentWithoutProducts) return UseCasesResponses.Failure<RentRequirementValidationResult>("Trying to Rent without products");
            
            var productsToRent = await _products.GetByIds(requirement.ProductsIds.ToList());
            if (!productsToRent.Success) return UseCasesResponses.Failure<RentRequirementValidationResult>(productsToRent.Message);

            var renter = await _renter.GetById(requirement.RenterId);
            if (!renter.Success) return UseCasesResponses.Failure<RentRequirementValidationResult>(renter.Message);
            
            var rentPeriod = DateRangeProvider.GetDateRange(requirement.StartDate, requirement.EndDate);
            if (!rentPeriod.Success) return UseCasesResponses.Failure<RentRequirementValidationResult>(rentPeriod.Message);
            
            var validationResult = new RentRequirementValidationResult
            {
                Products = productsToRent.Response,
                RentPeriod = rentPeriod.Result,
                Renter = renter.Response
            };

            return UseCasesResponses.Success(validationResult);
        }

        public async Task<UseCaseResult<RentRequirementValidationResult>> ValidateForForecast(RentRequirement requirement)
        {
            var isTryingToRentWithoutProducts = requirement.ProductsIds == null; 
            if (isTryingToRentWithoutProducts) return UseCasesResponses.Failure<RentRequirementValidationResult>("Trying to Rent without products");
            
            var productsToRent = await _products.GetByIds(requirement.ProductsIds.ToList());
            if (!productsToRent.Success) return UseCasesResponses.Failure<RentRequirementValidationResult>(productsToRent.Message);

            var renter = new PersistenceResponse<Renter>
            {
                Success = true,
                Response = new Renter(
                    id: Guid.NewGuid(),
                    name: "Forecast",
                    code: "123",
                    creatorId: Guid.NewGuid(), 
                    new GovernmentRegistrationDocumentCode("99999999999"),
                    new Email("forecast@forecast.com"),
                    new Phone("041", "999999999"),
                    new BirthDate(DateTime.Now.AddYears(-20)),
                    personImage: new byte[] {}
                )
            };
            
            var rentPeriod = DateRangeProvider.GetDateRange(requirement.StartDate, requirement.EndDate);
            if (!rentPeriod.Success) return UseCasesResponses.Failure<RentRequirementValidationResult>(rentPeriod.Message);
            
            var validationResult = new RentRequirementValidationResult
            {
                Products = productsToRent.Response,
                RentPeriod = rentPeriod.Result,
                Renter = renter.Response
            };

            return UseCasesResponses.Success(validationResult);
        }
    }
}