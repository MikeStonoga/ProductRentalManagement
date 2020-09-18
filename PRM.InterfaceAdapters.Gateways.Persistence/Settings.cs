using System;
using Microsoft.Extensions.DependencyInjection;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;

namespace PRM.InterfaceAdapters.Gateways.Persistence
{
    public static class PersistenceSettings
    {
        public static IServiceCollection AddPersistenceScopeds(this IServiceCollection services, Type readOnlyImplementation, Type readOnlyInterface,Type manipulationImplementation)
        {
            services
                .AddScoped(typeof(IReadOnlyPersistenceGateway<>), readOnlyImplementation)
                .AddScoped(readOnlyInterface, readOnlyImplementation)
                .AddScoped(typeof(IManipulationPersistenceGateway<>), manipulationImplementation);
            
            return services;
        }
        
    }
}