using BlazorComponent.I18n;
using BlazorXT.Global.Nav.Model;
using BlazorXT.Services;
using XT.Common.Interfaces;
using XT.Common.Models.Nav;
using Microsoft.AspNetCore.Components;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent;
using Blazored.LocalStorage;


namespace BlazorXT.Shared
{
    public partial class Favorite
    {
        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        private ILocalStorageService Cookie { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }



        List<string> FavoriteMenus { get; set; } = new List<string>();

        protected async override Task OnInitializedAsync()
        {
            var ids = await Cookie.GetItemAsStringAsync("HiFavorite");

            if (ids.IsNotNullOrEmpty())
            {
                FavoriteMenus = ids.Split('|').Select(v => v).ToList();
            }

            await base.OnInitializedAsync();
        }



        bool _open;
        string? _search;

        void OnOpen(bool open)
        {
            _open = open;
            if (open is true)
            {
                _search = null;
            }
        }

        List<NavItem> GetNavs(string? search)
        {
            List<NavItem>? output = new List<NavItem>();

            if (search is null || search == "") output.AddRange(UserConfig.Navs.Where(n => FavoriteMenus.Contains(n.Title)));
            else
            {
                output.AddRange(UserConfig.Navs.Where(n => n.Href is not null && n.Title.Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            return output;
        }

        List<NavItem> GetFavoriteMenus() => GetNavs(null);

        void AddOrRemoveFavoriteMenu(string title)
        {
            if (FavoriteMenus.Contains(title)) FavoriteMenus.Remove(title);
            else FavoriteMenus.Add(title);
            Cookie.SetItemAsync("HiFavorite", string.Join("|", FavoriteMenus));
        }

        void NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }


    }
}
