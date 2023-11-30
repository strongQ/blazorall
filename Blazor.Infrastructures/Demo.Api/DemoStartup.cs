using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.Design;

namespace Demo.Api
{
    public static class DemoStartup
    {
        public static IServiceCollection AddDemoServices(this IServiceCollection services)
        {
            // 注入接口访问
            services.AddServiceInjects<ICallService>(typeof(DemoStartup));

            return services;
        }
    }
}
