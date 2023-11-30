﻿using BlazorShared.Data.Base;
using BlazorShared.Global.Nav;
using GeneralCommon.Dtos.Admin.Menu;
using GeneralCommon.Models.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Extensions
{
    public static class MenuExtensions
    {
        public static List<NavItem> Parse(this List<MenuOutput> menus,List<NavItem> allNavs)
        {
            List<NavItem> items = new List<NavItem>();
            foreach (var menu in menus)
            {
                var item = menu.Parse();
                if (menu.Children!=null && menu.Children.Count > 0)
                {
                    item.Children = menu.Children.Parse(allNavs);
                }
                if (menu.Type == GeneralCommon.Enums.MenuTypeEnum.Menu || menu.Type == GeneralCommon.Enums.MenuTypeEnum.Dir)
                {
                    if (menu.Meta.Icon.Contains("fa-") || menu.Path == "/")
                    {
                        var data = GlobalVariables.Pages.FirstOrDefault(x => x.Path == menu.Path);

                        if (menu.Meta.Icon.Contains("fa-"))
                        {
                            item.Icon = "fa:" + item.Icon;
                        }
                        if (menu.Type == GeneralCommon.Enums.MenuTypeEnum.Menu)
                        {

                           
                            if (data != null)
                            {
                                allNavs.Add(item);

                                items.Add(item);
                            }

                        }
                        else
                        {
                            items.Add(item);
                        }



                    }

                }

            }
           
            return items;
        }

        public static NavItem Parse(this MenuOutput menu) => new()
        {
            Title = menu.Meta.Title,
            Icon = menu.Meta.Icon,
            Href = menu.Path,
            Target ="_self",
            ID=menu.Id,
            ParentID=menu.Pid
        };

        public static List<PageTabItem> SameLevelMenuPasePageTab(this List<MenuOutput> nav)
        {
            List<PageTabItem> pageTabItems = new List<PageTabItem>();
            if (nav == null) return pageTabItems;
            foreach (var item in nav)
            {
                if (item.Type == GeneralCommon.Enums.MenuTypeEnum.Menu)
                {
                    if (item.Meta.Icon == null)
                        pageTabItems.Add(new PageTabItem(item.Meta.Title, item.Component, ""));
                    else
                        pageTabItems.Add(new PageTabItem(item.Meta.Title, item.Component, item.Meta.Icon));
                }
            }
            return pageTabItems;
        }
    }
}