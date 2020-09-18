using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRM.Infrastructure.Authentication.Infrastructure.Persistence;
using PRM.Infrastructure.Authentication.Users.Controllers;
using PRM.Infrastructure.Authentication.Users.UseCases;
using PRM.Infrastructure.Persistence.MySQL;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;

namespace PRM.Infrastructure.Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseMySql<AuthenticationDbContext>(databaseName: "authentication");

            services
                .AddTransient<IUserReadOnlyController, UserReadOnlyController>()
                .AddTransient<IUserUseCasesReadOnlyInteractor, UserUseCasesReadOnlyInteractor>()
                .AddTransient<IUserUseCasesManipulationInteractor, UserUseCasesManipulationInteractor>()
                .AddTransient(typeof(IReadOnlyPersistenceGateway<>), typeof(ReadOnlyRepository<>))
                .AddTransient(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>))
                .AddTransient(typeof(IManipulationPersistenceGateway<>), typeof(Repository<>));
            services.AddControllers();
            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}