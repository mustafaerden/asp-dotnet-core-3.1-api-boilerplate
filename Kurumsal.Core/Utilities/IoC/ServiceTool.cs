using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Utilities.IoC
{
    // Bu yapi vasitasiyla DotNetCore un kendi servislerini erisebiliyor olucaz.(Startup.cs deki services yani)
    // Yapiyi kurduktan sonra Startup.cs e eklemeyi unutma!
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
