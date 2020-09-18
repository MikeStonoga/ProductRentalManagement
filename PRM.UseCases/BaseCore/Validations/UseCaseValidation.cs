using System.Threading.Tasks;

namespace PRM.UseCases.BaseCore.Validations
{
    public interface IValidateUseCaseRequirement<TRequirement, TRequirementValidationResult> 
    {
        Task<UseCaseResult<TRequirementValidationResult>> Validate(TRequirement requirement);
    }
    
    public abstract class UseCaseRequirementValidation<TRequirement, TRequirementValidationResult> : IValidateUseCaseRequirement<TRequirement, TRequirementValidationResult>
    {
        public abstract Task<UseCaseResult<TRequirementValidationResult>> Validate(TRequirement requirement);

    }
}