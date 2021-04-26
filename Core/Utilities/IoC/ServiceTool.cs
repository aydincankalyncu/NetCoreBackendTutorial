using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        //Injection islemlerini tanitmak icin kullanildi.
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
