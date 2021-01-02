using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        // Ekledigimiz bu servicelere erismek icin Core/Utilities/IoC/ServiceTool.cs yazdik. Examle: _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        public void Load(IServiceCollection services)
        {
            // Startup.cs de services.Add dedigimiz seyleri artik buraya ekleyebiliriz!
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            // User.ClaimRoles(); a controller dan ulasabiliyoruz ama business katmaninda ulasamiyoruz; Onun Icin;
            // Microsoft.AspNetCore.Http paketini hem Core hem Business katmanlarina kurduk.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Stopwatch i PerformanceAspect te kullandik;
            services.AddSingleton<Stopwatch>();
        }
    }
}
