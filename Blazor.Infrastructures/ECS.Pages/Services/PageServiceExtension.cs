
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common;
using XT.Sql.Extensions;

namespace ECS.Pages
{
    public static class PageServiceExtension
    {
        /// <summary>
        /// 添加ECS服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEcsPageServices(this IServiceCollection services)
        {
           
            services.AddSignalRService();
          
            services.AddOriginHttpClient();
            // 添加Sql
            services.AddXTDbSetup();
           
            return services;
        }
    }
}
