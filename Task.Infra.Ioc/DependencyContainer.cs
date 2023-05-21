using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Task.Application.Interfaces.Services;
using Task.Application.Services;
using Task.Domain.Helpers;
using Task.Domain.Interfaces.Repos;
using Task.Domain.Repos;

namespace Task.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IDriverRepository, DriverRepository>();


            services.AddScoped<DBSetting>(sp =>
            sp.GetRequiredService<IOptions<DBSetting>>().Value);
        }
           
    }
}
