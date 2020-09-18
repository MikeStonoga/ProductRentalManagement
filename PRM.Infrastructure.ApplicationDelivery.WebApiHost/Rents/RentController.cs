using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Rents;
using PRM.Infrastructure.ApplicationDelivery.WebApiHost.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore.Dtos;
using PRM.InterfaceAdapters.Controllers.Rents;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.FinishRents;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetOpenRentsPaymentForecasts;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetRentForecastPrices;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.RentProducts;
using PRM.UseCases.Rents;

namespace PRM.Infrastructure.ApplicationDelivery.WebApiHost.Rents
{
    public interface IRentController : IBaseReadOnlyWebController<Rent, RentOutput>, IRentManipulationController
    {
    }
    public class RentController : BaseReadOnlyWebController<Rent, RentOutput, IRentReadOnlyController, IRentUseCasesReadOnlyInteractor>, IRentController
    {
        private readonly IRentManipulationController _rentManipulationController;
        public RentController(IRentUseCasesManipulationInteractor useCasesInteractor, IRentReadOnlyController readOnlyController, IRentManipulationController rentManipulationController) : base(useCasesInteractor, readOnlyController)
        {
            _rentManipulationController = rentManipulationController;
        }

        [HttpPost]
        public async Task<ApiResponse<RentProductsOutput>> RentProducts([FromBody] RentProductsInput input)
        {
            return await _rentManipulationController.RentProducts(input);
        }

        [HttpPut]
        public async Task<ApiResponse<FinishRentOutput>> FinishRent([FromBody] FinishRentInput input)
        {
            return await _rentManipulationController.FinishRent(input);
        }

        [HttpPost]
        public async Task<ApiResponse<GetRentForecastPriceOutput>> GetRentForecastPrice([FromBody] GetRentForecastPriceInput input)
        {
            return await _rentManipulationController.GetRentForecastPrice(input);
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRents()
        {
            return await _rentManipulationController.GetOpenRents();
        }

        [HttpPost]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRentsFromPeriod([FromBody] PeriodInput input)
        {
            return await _rentManipulationController.GetOpenRentsFromPeriod(input);
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenLateRents()
        {
            return await _rentManipulationController.GetOpenLateRents();
        }
        
        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenNotLateRents()
        {
            return await _rentManipulationController.GetOpenNotLateRents();
        }

        [HttpGet]
        public async Task<ApiResponse<int>> GetLateDays([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetLateDays(rentId);
        }
        
        [HttpGet]
        public async Task<ApiResponse<DateRange>> GetRentPeriod([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetRentPeriod(rentId);
        }

        [HttpGet]
        public async Task<ApiResponse<decimal>> GetRentAverageTicket([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetRentAverageTicket(rentId);
        }

        [HttpGet]
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithDiscount([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetRentAverageTicketWithDiscount(rentId);
        }

        [HttpGet]
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFees([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetRentAverageTicketWithoutFees(rentId);
        }

        [HttpGet]
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFeesWithDiscount([FromQuery] Guid rentId)
        {
            return await _rentManipulationController.GetRentAverageTicketWithoutFeesWithDiscount(rentId);
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRents()
        {
            return await _rentManipulationController.GetClosedRents();
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedLateRents()
        {
            return await _rentManipulationController.GetClosedLateRents();
        }
        
        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedNotLateRents()
        {
            return await _rentManipulationController.GetClosedNotLateRents();
        }
        
        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetLateRents()
        {
            return await _rentManipulationController.GetLateRents();
        }

        [HttpGet]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetNotLateRents()
        {
            return await _rentManipulationController.GetNotLateRents();
        }

        [HttpPost]
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRentsFromPeriod([FromBody] PeriodInput input)
        {
            return await _rentManipulationController.GetClosedRentsFromPeriod(input);
        }

        [HttpPost]
        public async Task<ApiResponse<GetOpenRentsPaymentForecastOutput>> GetOpenRentsPaymentForecast([FromBody] GetOpenRentsPaymentForecastInput input)
        {
            return await _rentManipulationController.GetOpenRentsPaymentForecast(input);
        }
    }
}