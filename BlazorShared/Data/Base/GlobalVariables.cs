using BlazorShared.Layout;
using GeneralCommon.Attributes;
using GeneralCommon.Models.Nav;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Data.Base
{
    public static class GlobalVariables
    {
        static GlobalVariables()
        {
            // 获取所有路由数据
            Pages=GetPages();
        }
        public const string DefaultRoute = "/";
        /// <summary>
        /// 是否登录
        /// </summary>
        public static bool IsLogin { get; set; }

        /// <summary>
        /// 主题cookie
        /// </summary>
        public const string ThemeCookieKey = "TB_ThemeCookieKey";

        /// <summary>
        /// 表格操作列标识
        /// </summary>
        public const string TB_Actions = "TB_Actions";
        /// <summary>
        /// 远程API地址
        /// </summary>
        public static string RemoteApiUrl { get; set; }
        /// <summary>
        /// 远程Grpc地址
        /// </summary>
        public static string GrpcUrl { get; set; }
        /// <summary>
        /// 是否单程序
        /// </summary>
        public static bool IsSingleApp { get; set; }
       
        /// <summary>
        /// 所有界面
        /// </summary>
        public static List<RazorPageModel> Pages { get; set; }

        /// <summary>
        /// 是否过期,默认过期
        /// </summary>
        public static bool IsExpired { get; set; } = true;

        private static List<string> _pageAssemblies=new List<string>();
        /// <summary>
        /// page所在的程序集
        /// </summary>
        public static List<string> PageAssemblies
        {
            get { return _pageAssemblies; }
            set 
            { 
                _pageAssemblies = value;
                if(value != null && value.Count>0) 
                AddAssemblyPages(); 
            }
        }


        private static List<RazorPageModel> GetPages()
        {
            List<RazorPageModel> datas = new List<RazorPageModel>();
            // Get all the components whose base class is ComponentBase
            var components = Assembly.GetExecutingAssembly()
                                   .ExportedTypes
                                   .Where(t =>
                                  t.IsSubclassOf(typeof(CultureComponentBase)));

            foreach (var component in components)
            {


                // Now check if this component contains the Authorize attribute
                var allAttributes = component.GetCustomAttributes(inherit: true);

                var authorizeDataAttributes =
                                allAttributes.OfType<PageAttribute>().ToArray();

                // If it does, show this to us... 
                foreach (var authorizeData in authorizeDataAttributes)
                {

                    datas.Add(new RazorPageModel
                    {
                        Path = authorizeData.Path,
                        Name = authorizeData.Name,
                        Show = authorizeData.Show,
                        Icon = authorizeData.Icon
                    });
                }
            }


           

            return datas;
        }

        /// <summary>
        /// 添加额外的页面
        /// </summary>
        private static void AddAssemblyPages()
        {
            List<RazorPageModel> datas = new List<RazorPageModel>();
            // Get all the components whose base class is ComponentBase
           


            foreach (var assembly in PageAssemblies)
            {
                var assemble = Assembly.Load(assembly);
                var pages = assemble.ExportedTypes.Where(t =>
                                   t.IsSubclassOf(typeof(CultureComponentBase)));
                foreach (var page in pages)
                {


                    // Now check if this component contains the Authorize attribute
                    var allAttributes = page.GetCustomAttributes(inherit: true);

                    var authorizeDataAttributes =
                                    allAttributes.OfType<PageAttribute>().ToArray();

                    // If it does, show this to us... 
                    foreach (var authorizeData in authorizeDataAttributes)
                    {

                        datas.Add(new RazorPageModel
                        {
                            Path = authorizeData.Path,
                            Name = authorizeData.Name,
                            Show = authorizeData.Show,
                            Icon = authorizeData.Icon
                        });
                    }
                }
            }

            Pages.AddRange(datas);
        }
    }
}
