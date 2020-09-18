using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRM.Domain.Renters;
using PRM.Infrastructure.ApplicationDelivery.WebApiHost.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore;
using PRM.InterfaceAdapters.Controllers.Renters;
using PRM.InterfaceAdapters.Controllers.Renters.Dtos;
using PRM.InterfaceAdapters.Controllers.Renters.Dtos.GetBirthDaysOnPeriods;
using PRM.InterfaceAdapters.Controllers.Renters.Dtos.GetLastRenterRents;
using PRM.InterfaceAdapters.Controllers.Renters.Dtos.RentalHistory;
using PRM.UseCases.Renters;

namespace PRM.Infrastructure.ApplicationDelivery.WebApiHost.Renters
{
    public class RenterController : BaseManipulationWebController<Renter, RenterInput, RenterOutput, IRenterUseCasesManipulationInteractor, IRenterManipulationController>, IRenterManipulationController
    {
        public RenterController(IRenterUseCasesManipulationInteractor useCaseInteractor, IRenterManipulationController manipulationController) : base(useCaseInteractor, manipulationController)
        {
        }

        [HttpPost]
        public async Task<ApiResponse<GetAllResponse<Renter, RenterOutput>>> GetBirthDaysOnPeriod([FromBody] GetBirthDaysOnPeriodInput input)
        {
            return await ReadOnlyController.GetBirthDaysOnPeriod(input);
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<RenterRentalHistory, RenterRentalHistoryOutput>>> GetRentalHistory([FromQuery] Guid renterId)
        {
            return await ReadOnlyController.GetRentalHistory(renterId);
        }

        [HttpGet]
        public async Task<ApiResponse<GetLastRenterRentOutput>> GetLastRent([FromQuery] Guid renterId)
        {
            return await ReadOnlyController.GetLastRent(renterId);
        }

        [HttpGet]
        public async Task<ApiResponse<GetProductsPerRentAverageOutput>> GetProductsPerRentAverage([FromQuery] Guid renterId)
        {
            return await ReadOnlyController.GetProductsPerRentAverage(renterId);
        }
    }
}