using System;
using System.Threading.Tasks;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.Infrastructure.Authentication.Users.UseCases
{
    public interface IUserUseCasesReadOnlyInteractor : IBaseUseCaseReadOnlyInteractor<User>
    {
    }
        
    public class UserUseCasesReadOnlyInteractor : BaseUseCaseReadOnlyInteractor<User>, IUserUseCasesReadOnlyInteractor
    {
        
        public UserUseCasesReadOnlyInteractor(IReadOnlyPersistenceGateway<User> readOnlyPersistenceGateway) : base(readOnlyPersistenceGateway)
        {
        }

    }

    public interface IUserUseCasesManipulationInteractor : IBaseUseCaseManipulationInteractor<User>, IUserUseCasesReadOnlyInteractor
    {
    }

    public class UserUseCasesManipulationInteractor : BaseUseCaseManipulationInteractor<User, IUserUseCasesReadOnlyInteractor>, IUserUseCasesManipulationInteractor
    {
        private readonly IManipulationPersistenceGateway<User> _persistenceGateway;
        public UserUseCasesManipulationInteractor(IManipulationPersistenceGateway<User> persistenceGateway, IUserUseCasesReadOnlyInteractor useCasesReadOnlyInteractor) : base(persistenceGateway, useCasesReadOnlyInteractor)
        {
            _persistenceGateway = persistenceGateway;
        }
        
        public override async Task<UseCaseResult<User>> Create(User userToCreate)
        {
            var validationResponse = new PersistenceResponse<User>();
            if (userToCreate.BirthDate == DateTime.MinValue)
            {
                validationResponse.Success = false;
                validationResponse.Message = "BirthDate is Required";
                return UseCasesResponses.GetUseCaseResult(validationResponse);
            }
            
            var alreadyHasLogin = await _persistenceGateway.First(user => user.Login == userToCreate.Login);
            if (alreadyHasLogin.Success)
            {
                validationResponse.Success = false;
                validationResponse.Message = "AlreadyHasLogin";
                return UseCasesResponses.GetUseCaseResult(validationResponse);
            }
            
            var alreadyHasEmail = await _persistenceGateway.First(user => string.Equals(user.Email, userToCreate.Email, StringComparison.CurrentCultureIgnoreCase));
            if (alreadyHasEmail.Success)
            {
                validationResponse.Success = false;
                validationResponse.Message = "AlreadyHasEmail";
                return UseCasesResponses.GetUseCaseResult(validationResponse);
            }
            
            var persistenceResponse = await _persistenceGateway.Create(userToCreate);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }
    }
}