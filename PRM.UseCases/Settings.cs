using Microsoft.Extensions.DependencyInjection;
using PRM.UseCases.Products;
using PRM.UseCases.Products.CheckAvailabilities;
using PRM.UseCases.Products.CheckProductAvailabilities;
using PRM.UseCases.Products.GetAvailables;
using PRM.UseCases.Products.GetLastProductRents;
using PRM.UseCases.Products.GetRentalHistories;
using PRM.UseCases.Renters;
using PRM.UseCases.Renters.GetLastRenterRents;
using PRM.UseCases.Renters.GetProductsPerRentAverages;
using PRM.UseCases.Renters.GetRentalHistories;
using PRM.UseCases.Rents;
using PRM.UseCases.Rents.FinishRents;
using PRM.UseCases.Rents.GetOpenRentsPaymentForecasts;
using PRM.UseCases.Rents.GetRentForecastPrices;
using PRM.UseCases.Rents.RentProducts;
using PRM.UseCases.Rents.Validations.Requirements.Rents;

namespace PRM.UseCases
{
    public static class UseCasesSettings
    {
     
        public static IServiceCollection AddUseCasesTransients(this IServiceCollection services)
        {
            services
                //Products
                .AddTransient<IProductUseCasesReadOnlyInteractor, ProductUseCasesReadOnlyInteractor>()
                .AddTransient<IProductUseCasesManipulationInteractor, ProductUseCasesManipulationInteractor>()
                .AddTransient<ICheckProductAvailability, CheckProductAvailability>()
                .AddTransient<IGetAvailablesProducts, GetAvailablesProducts>()
                .AddTransient<IGetUnavailablesProducts, GetUnavailablesProducts>()
                
                // ProductRentalHistory
                .AddTransient<IProductRentalHistoryUseCasesReadOnlyInteractor, ProductRentalHistoryUseCasesReadOnlyInteractor>()
                .AddTransient<IGetProductRentalHistory, GetProductRentalHistory>()
                .AddTransient<IGetLastProductRent, GetLastProductRent>()
                
                //Rents
                .AddTransient<IRentUseCasesReadOnlyInteractor, RentUseCasesReadOnlyInteractor>()
                .AddTransient<IRentUseCasesManipulationInteractor, RentUseCasesManipulationInteractor>()
                .AddTransient<IGetRentForecastPrice, GetRentForecastPrice>()
                .AddTransient<IRentProducts, RentProducts>()
                .AddTransient<IFinishRent, FinishRent>()
                .AddTransient<IValidateRentRequirement, ValidateRentRequirement>()
                .AddTransient<IGetOpenRentsPaymentForecast, GetOpenRentsPaymentForecast>()
                
                // RenterRentalHistory
                .AddTransient<IRenterRentalHistoryUseCasesReadOnlyInteractor, RenterRentalHistoryUseCasesReadOnlyInteractor>()
                .AddTransient<IGetRenterRentalHistory, GetRenterRentalHistory>()
                .AddTransient<IGetLastRenterRent, GetLastRenterRent>()
                .AddTransient<IGetProductsPerRentAverage, GetProductsPerRentAverage>()
                
                // Renters
                .AddTransient<IRenterUseCasesReadOnlyInteractor, RenterUseCasesReadOnlyInteractor>()
                .AddTransient<IRenterUseCasesManipulationInteractor, RenterUseCasesManipulationInteractor>()
                ;

            return services;
        }
    }
}