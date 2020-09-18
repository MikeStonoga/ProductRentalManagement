using System;
using System.Threading.Tasks;
using PRM.Domain.Renters;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.UseCases.BaseCore;
using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.Renters.GetRentalHistories
{
    public interface IGetRenterRentalHistory : IBaseUseCase<Guid, GetAllResponse<RenterRentalHistory>>
    {
    }
    
    public class GetRenterRentalHistory : BaseUseCase<Guid, GetAllResponse<RenterRentalHistory>>, IGetRenterRentalHistory
    {
        private readonly IReadOnlyPersistenceGateway<RenterRentalHistory> _rentersRentalHistories;

        public GetRenterRentalHistory( IReadOnlyPersistenceGateway<RenterRentalHistory> rentersRentalHistories)
        {
            _rentersRentalHistories = rentersRentalHistories;
        }

        public override async Task<UseCaseResult<GetAllResponse<RenterRentalHistory>>> Execute(Guid renterId)
        {
            var getRentalHistory = await _rentersRentalHistories.GetAll(history => history.RenterId == renterId);
            return UseCasesResponses.GetUseCaseResult(getRentalHistory);
        }
    }
}