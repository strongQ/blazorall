using BlazorComponent;
using BlazorXT.Global.Nav.Model;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Extensions;
using XT.Common.Interfaces;
using Masa.Blazor.Components.Editor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Global.Nav
{
    public class NavHelper
    {
        private List<NavModel> _navList;
        private NavigationManager _navigationManager;

        public List<NavModel> Navs { get; } = new();

        public List<NavModel> SameLevelNavs { get; } = new();

        public List<PageTabItem> PageTabItems { get; } = new();

        public string CurrentUri => _navigationManager.Uri;

        

        public NavHelper(NavigationManager navigationManager,IUserConfig userConfig)
        {
          
            _navigationManager = navigationManager;
            ReloadMenus(userConfig.Menus);

         
        }
       
        /// <summary>
        /// 重新加载导航菜单
        /// </summary>
        /// <param name="menus"></param>
        public void ReloadMenus(List<MenuOutput> menus)
        {
            if(menus.Count==0) return;
            Navs.Clear();
            List<NavModel> navs = new List<NavModel>();
            ConvertMenusToNavs(menus,navs);
            _navList = navs;
            Initialization();
        }

       
        /// <summary>
        /// 递归添加菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="navs"></param>
        private void ConvertMenusToNavs(List<MenuOutput> menus,List<NavModel> navs) 
        { 
            foreach (var menu in menus) 
            { 
                NavModel nav = new NavModel { Title = menu.Meta.Title, Id = menu.Id, Icon = menu.Meta.Icon, Href = menu.Path }; 
               
                if (menu.Children != null && menu.Children.Count > 0) 
                {
                    List<NavModel> childNavs = new List<NavModel>();
                    ConvertMenusToNavs(menu.Children,childNavs);
                    nav.Children = childNavs;
                }
                if(nav.Icon.Contains("fa-"))
                navs.Add(nav);
            }
        }

        private void Initialization()
        {
            Navs.Add(new NavModel
            {
                Href = "/",
                Title = "首页",
                FullTitle="首页",
                Id = 0,
                Icon = "mdi-home"
            });
            SameLevelNavs.Add(new NavModel
            {
                Href = "/",
                Title = "首页",
                FullTitle = "首页",
                Id = 0,
                Icon = "mdi-home"
            });
            _navList.ForEach(nav =>
            {
                

                if (nav.Children is not null)
                {
                    nav.Children = nav.Children.Where(c => c.Hide is false).ToList();

                    nav.Children.ForEach(child =>
                    {
                        child.ParentId = nav.Id;
                        child.FullTitle = $"{nav.Title} {child.Title}";
                        child.ParentIcon = nav.Icon;
                        if(child.Children is not null)
                        {
                            child.Children.ForEach(x =>
                            {
                                x.ParentId = child.Id;
                                x.FullTitle = $"{nav.Title} {child.Title} {x.Title}";
                                x.ParentIcon = child.Icon;
                                SameLevelNavs.Add(x);
                            });
                        }
                        SameLevelNavs.Add(child);
                    });
                }
                if (nav.Hide is false)
                {
                    Navs.Add(nav);
                    SameLevelNavs.Add(nav);
                }
            });

           

            SameLevelNavs.Where(nav => nav.Href is not null).ForEachAsync(nav =>
            {
                // The following path will not open a new tab
                if (nav.Href is "app/user/view" or "app/user/edit" or "app/ecommerce/details")
                {
                    nav.Target = "Self";
                }

                PageTabItems.Add(new(nav.Title, nav.Href, nav.Icon));
                return Task.CompletedTask;
            });
        }

        public void NavigateTo(NavModel nav)
        {
            _navigationManager.NavigateTo(nav.Href ?? "");
        }

        public void NavigateTo(string href)
        {
            _navigationManager.NavigateTo(href);
        }
    }

    public record PageTabItem(string Title, string Href, string Icon);
}
