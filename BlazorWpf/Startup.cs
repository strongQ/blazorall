using BlazorShared;
using BlazorShared.Data.Base;
using GeneralCommon.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using ECS.Pages.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWpf
{
    public static class Startup
    {
        public static IServiceProvider? Services { get; private set; }

        public static void Init()
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices(WireupServices)
                           .Build();
            Services = host.Services;
        }

        private static void WireupServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddWpfBlazorWebView();

            services.AddSingleton(new AppSettings(false));

            // 添加页面单独实现
            services.AddEcsPageServices();

        

            var api = AppSettings.GetValue("RemoteApiUrl");

            var grpc = AppSettings.GetValue("GrpcUrl");
            var singleApp = AppSettings.GetValue<bool>("SingleApp");

            GlobalVariables.PageAssemblies = new List<string>
{
    "ECS.Pages"
};

            GlobalVariables.RemoteApiUrl = api;
            GlobalVariables.IsSingleApp = singleApp;
            GlobalVariables.GrpcUrl = grpc;
            GlobalVariables.IsExpired= false; 
            services.AddSharedExtensions();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif
        }
    }
}
