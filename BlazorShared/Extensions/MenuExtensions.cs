using BlazorXT.Data.Base;
using BlazorXT.Global.Config;
using BlazorXT.Global.Nav;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Models.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Extensions
{
    public static class MenuExtensions
    {
        public static List<NavItem> Parse(this List<MenuOutput> menus, List<NavItem> allNavs, List<RazorPageModel> pages)
        {


            List<NavItem> items = new List<NavItem>();
            foreach (var menu in menus)
            {
                var item = menu.Parse();
                if (menu.Children != null && menu.Children.Count > 0)
                {
                    item.Children = menu.Children.Parse(allNavs, pages);
                }
                if (menu.Type == XT.Common.Enums.MenuTypeEnum.Menu || menu.Type == XT.Common.Enums.MenuTypeEnum.Dir)
                {
                    if (menu.Meta.Icon.Contains("fa-") || menu.Path == "/")
                    {


                        if (menu.Meta.Icon.Contains("fa-"))
                        {
                            item.Icon = "fa:" + item.Icon;
                        }
                        if (menu.Type == XT.Common.Enums.MenuTypeEnum.Menu)
                        {

                            var data = pages?.FirstOrDefault(x => x.Path == menu.Path);
                            if (data != null || pages == null)
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
            Target = "_self",
            ID = menu.Id,
            ParentID = menu.Pid
        };


    }
}
