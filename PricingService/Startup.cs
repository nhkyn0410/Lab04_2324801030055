using PricingService.DataAccess;
using PricingService.Domain;
using PricingService.Init;

namespace PricingService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddSingleton<IDataStore>(new InMemoryDataStore(DemoTariffs.All()));
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
