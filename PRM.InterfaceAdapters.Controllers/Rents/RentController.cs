using System;
using System.Threading.Tasks;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Rents;
using PRM.InterfaceAdapters.Controllers.BaseCore;
using PRM.InterfaceAdapters.Controllers.BaseCore.Dtos;
using PRM.InterfaceAdapters.Controllers.BaseCore.Extensions;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.FinishRents;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetOpenRentsPaymentForecasts;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.GetRentForecastPrices;
using PRM.InterfaceAdapters.Controllers.Rents.Dtos.RentProducts;
using PRM.UseCases.Rents;
using PRM.UseCases.Rents.FinishRents;
using PRM.UseCases.Rents.GetOpenRentsPaymentForecasts;
using PRM.UseCases.Rents.GetRentForecastPrices;
using PRM.UseCases.Rents.RentProducts;

namespace PRM.InterfaceAdapters.Controllers.Rents
{
    public interface IRentReadOnlyController : IBaseReadOnlyController<Rent, RentOutput>
    {
        Task<ApiResponse<GetRentForecastPriceOutput>> GetRentForecastPrice(GetRentForecastPriceInput input);
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRentsFromPeriod(PeriodInput input);
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetLateRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetNotLateRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenLateRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenNotLateRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedLateRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedNotLateRents();
        Task<ApiResponse<int>> GetLateDays(Guid rentId);
        Task<ApiResponse<DateRange>> GetRentPeriod(Guid rentId);
        Task<ApiResponse<decimal>> GetRentAverageTicket(Guid rentId);
        Task<ApiResponse<decimal>> GetRentAverageTicketWithDiscount(Guid rentId);
        Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFees(Guid rentId);
        Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFeesWithDiscount(Guid rentId);
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRents();
        Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRentsFromPeriod(PeriodInput input);
        Task<ApiResponse<GetOpenRentsPaymentForecastOutput>> GetOpenRentsPaymentForecast(GetOpenRentsPaymentForecastInput input);
    }
     
    public class RentReadOnlyController : BaseReadOnlyController<Rent, RentOutput, IRentUseCasesReadOnlyInteractor>, IRentReadOnlyController
    {
        private readonly IRentUseCasesReadOnlyInteractor _useCasesReadOnlyInteractor;

        public RentReadOnlyController(IRentUseCasesReadOnlyInteractor useCasesReadOnlyInteractor) : base(useCasesReadOnlyInteractor)
        {
            _useCasesReadOnlyInteractor = useCasesReadOnlyInteractor;
        }

        public async Task<ApiResponse<GetRentForecastPriceOutput>> GetRentForecastPrice(GetRentForecastPriceInput input)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<GetRentForecastPriceRequirement, GetRentForecastPriceResult, GetRentForecastPriceInput, GetRentForecastPriceOutput>(_useCasesReadOnlyInteractor.GetRentForecastPrice, input);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRents()
        {
            return await GetAll(r => r.IsOpen);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenRentsFromPeriod(PeriodInput input)
        {
            var period = DateRangeProvider.GetDateRange(input.StartDate, input.EndDate);
            if (!period.Success) return ApiResponses.Failure<GetAllResponse<Rent, RentOutput>>(period.Message);
            
            return await GetAll(r => r.IsOpen && period.Result.IsOnRange(r.RentPeriod));
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetLateRents()
        {
            return await GetAll(r => r.IsLate);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetNotLateRents()
        {
            return await GetAll(r => !r.IsLate);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenLateRents()
        {
            return await GetAll(r => r.IsLate && !r.IsOpen);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetOpenNotLateRents()
        {
            return await GetAll(r => !r.IsLate && r.IsOpen);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<int>> GetLateDays(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(0, rent.Message)
                : ApiResponses.Success(rent.Response.LateDays);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<DateRange>> GetRentPeriod(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(DateRangeProvider.GetDateRange(DateTime.MinValue, DateTime.MinValue.AddTicks(1)).Result, rent.Message)
                : ApiResponses.Success(rent.Response.RentPeriod);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFeesWithDiscount(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(0M, rent.Message)
                : ApiResponses.Success(rent.Response.AverageTicketWithoutFeesWithDiscount);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRents()
        {
            return await GetAll(r => r.IsClosed);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithoutFees(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(0M, rent.Message)
                : ApiResponses.Success(rent.Response.AverageTicketWithoutFees);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<decimal>> GetRentAverageTicket(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(0M, rent.Message)
                : ApiResponses.Success(rent.Response.AverageTicket);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<decimal>> GetRentAverageTicketWithDiscount(Guid rentId)
        {
            var rent = await GetById(rentId);
            return !rent.Success 
                ? ApiResponses.Failure(0M, rent.Message)
                : ApiResponses.Success(rent.Response.AverageTicketWithDiscount);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedLateRents()
        {
            return await GetAll(r => r.IsLate && r.IsClosed);
        }
        
        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedNotLateRents()
        {
            return await GetAll(r => !r.IsLate && r.IsClosed);
        }

        // TODO Migrate to UseCases
        public async Task<ApiResponse<GetAllResponse<Rent, RentOutput>>> GetClosedRentsFromPeriod(PeriodInput input)
        {
            var period = DateRangeProvider.GetDateRange(input.StartDate, input.EndDate);
            if (!period.Success) return ApiResponses.Failure<GetAllResponse<Rent, RentOutput>>(period.Message);
            
            return await GetAll(r => r.IsClosed && period.Result.IsOnRange(r.RentPeriod));
        }

        public async Task<ApiResponse<GetOpenRentsPaymentForecastOutput>> GetOpenRentsPaymentForecast(GetOpenRentsPaymentForecastInput input)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<GetOpenRentsPaymentForecastRequirement, GetOpenRentsPaymentForecastResult, GetOpenRentsPaymentForecastInput, GetOpenRentsPaymentForecastOutput>(_useCasesReadOnlyInteractor.GetOpenRentsPaymentForecast, input);
        }
    }

    public interface IRentManipulationController : IRentReadOnlyController
    {
        Task<ApiResponse<RentProductsOutput>> RentProducts(RentProductsInput input);
        Task<ApiResponse<FinishRentOutput>> FinishRent(FinishRentInput input);
    }

    public class RentManipulationController : RentReadOnlyController, IRentManipulationController
    {
        private readonly IRentUseCasesManipulationInteractor _useCasesInteractor;

        public RentManipulationController(IRentUseCasesManipulationInteractor useCasesInteractor) : base(useCasesInteractor)
        {
            _useCasesInteractor = useCasesInteractor;
        }

        public async Task<ApiResponse<RentProductsOutput>> RentProducts(RentProductsInput input)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<RentProductsRequirement, RentProductsResult, RentProductsInput, RentProductsOutput>(_useCasesInteractor.RentProducts, input);
        }

        public async Task<ApiResponse<FinishRentOutput>> FinishRent(FinishRentInput input)
        {
            return await ApiResponses.GetUseCaseInteractorResponse<FinishRentRequirement, FinishRentResult, FinishRentInput, FinishRentOutput>(_useCasesInteractor.FinishRent, input);
        }
    }
}