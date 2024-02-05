
using BlazorXT.Services;

using Shared.DependencyServices;
//using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using XT.Common.Services;

using XT.Common;

using BlazorXT.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorXT.Config;


using XT.Common.Interfaces;

using Client.API.Managers;
using BlazorXT.Global.Config;
using BlazorXT.Global.Nav.Model;
using System.Net.Http.Json;
using System.Reflection;
using BlazorXT.Global.Nav;
using System.Text.Json;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Extensions;
using Masa.Blazor.Presets;
using Masa.Blazor;
using MudBlazor.Services;
using MudBlazor;
using Blazored.LocalStorage;
using BlazorXT.Data.Base;

namespace BlazorXT
{
    /// <summary>
    /// 服务扩展类
    /// </summary>
    public static class SharedServiceCollectionExtensions
    {

        //public static IFreeSql fsql;

        /// <summary>
        /// 服务扩展类,<para></para>
        /// 包含各平台差异实现
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSharedExtensions(this IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("zh-CN");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // web assamble
            //services.AddAuthorizationCore();
            //services.AddOptions();
            // 添加本行代码
            services.AddMasaBlazor(options =>
            {
                options.Defaults = new Dictionary<string, IDictionary<string, object>>()
                {
                    {
                        PopupComponents.SNACKBAR, new Dictionary<string, object>()
                        {
                            { nameof(PEnqueuedSnackbars.Closeable), true },
                            { nameof(PEnqueuedSnackbars.Position), SnackPosition.TopCenter }
                        }
                    }
                };
                options.ConfigureTheme(theme =>
                {
                    theme.Themes.Dark.Accent = "#FF4081";
                    theme.Themes.Dark.Error = "#FF5252";
                    theme.Themes.Dark.Info = "#2196F3";
                    theme.Themes.Dark.Primary = "#2196F3";
                    theme.Themes.Dark.Secondary = "#424242";
                    theme.Themes.Dark.Success = "#4CAF50";
                    theme.Themes.Dark.Warning = "#FB8C00";
                    theme.Themes.Dark.UserDefined.Add("barColor", "#1e1e1e");

                    theme.Themes.Light.Accent = "#82B1FF";
                    theme.Themes.Light.Error = "#FF5252";
                    theme.Themes.Light.Info = "#2196F3";
                    theme.Themes.Light.Primary = "#1976D2";
                    theme.Themes.Light.Secondary = "#424242";
                    theme.Themes.Light.Success = "#4CAF50";
                    theme.Themes.Light.Warning = "#FB8C00";
                    theme.Themes.Light.UserDefined.Add("barColor", "#fff");


                });
               
            })

                .AddI18nForServer("wwwroot/i18n");

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 1;
            });

           

            //services.AddScoped<IIPAddressManager, IPAddressManager>();
          
            
            // localstorage
            services.AddScoped<IUserConfig,UserConfig>();
            services.AddScoped<IApiConfig, ApiConfig>();
            services.AddScoped<ILogService, LogService>();
          
            services.AddOriginHttpClient();        
           
            // 注入接口访问
            services.AddServiceInjects<IApiManager>();

           

            services.AddBlazoredLocalStorage();

            services.AddSingleton<GlobalVariables>();

            services.AddScoped<SecurityServiceClient>();
           
            services.AddScoped<AuthenticationStateProvider, HostAuthenticationStateProvider>();

            services.AddScoped<NavHelper>();
            services.AddScoped<GlobalConfig>();
            
             
            return services;
        }

      

     

     

    }

}




