using BlazorXT.Layout;

using XT.Common.Models.Nav;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Attributes;

namespace BlazorXT.Data.Base
{
    public  class GlobalVariables
    {
        public static string Url = string.Empty;
      
        public const string DefaultRoute = "/";
        /// <summary>
        /// 存储配置
        /// </summary>
        public const string StoreKey = "HiSetting";

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
        public  string RemoteApiUrl { get; set; }
        /// <summary>
        /// 远程Grpc地址
        /// </summary>
        public  string GrpcUrl { get; set; }
        /// <summary>
        /// 是否单程序
        /// </summary>
        public  bool IsSingleApp { get; set; }
       
        /// <summary>
        /// 所有界面
        /// </summary>
        public  List<RazorPageModel> Pages { get; set; }

        public List<Assembly> Assemblies { get; set; }

        /// <summary>
        /// 是否过期,默认过期
        /// </summary>
        public  bool IsExpired { get; set; } = true;

        /// <summary>
        /// 初始化界面，界面所在的程序集
        /// </summary>
        /// <param name="assemblies"></param>
        public void IniPages(List<string> assemblies)
        {
            Pages = GetPages();
            if(assemblies!=null && assemblies.Any())
            AddAssemblyPages(assemblies);
        }


        private  List<RazorPageModel> GetPages()
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
        private  void AddAssemblyPages(List<string> assemblies)
        {
            List<RazorPageModel> datas = new List<RazorPageModel>();
            // Get all the components whose base class is ComponentBase


            List<Assembly> others = new List<Assembly>();

           
            foreach (var assembly in assemblies)
            {
                try
                {
                    var assemble = Assembly.Load(assembly);
                    others.Add(assemble);
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Assemblies = others;

            Pages.AddRange(datas);
        }
    }
}
