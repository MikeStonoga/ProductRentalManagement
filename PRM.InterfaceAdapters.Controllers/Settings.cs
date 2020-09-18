using Microsoft.Extensions.DependencyInjection;
using PRM.InterfaceAdapters.Controllers.Products;
using PRM.InterfaceAdapters.Controllers.Renters;
using PRM.InterfaceAdapters.Controllers.Rents;

namespace PRM.InterfaceAdapters.Controllers
{
    public static class ControllersSettings
    {
        public static IServiceCollection AddControllersTransients(this IServiceCollection services)
        {
            services
                // Products
                .AddTransient<IProductReadOnlyController, ProductReadOnlyController>()
                .AddTransient<IProductManipulationController, ProductManipulationController>()
                
                // Rents
                .AddTransient<IRentReadOnlyController, RentReadOnlyController>()
                .AddTransient<IRentManipulationController, RentManipulationController>()
                
                // Renters
                .AddTransient<IRenterReadOnlyController, RenterReadOnlyController>()
                .AddTransient<IRenterManipulationController, RenterManipulationController>()
                ;
            return services;
        }
    }
}