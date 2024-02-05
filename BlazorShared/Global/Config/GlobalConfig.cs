using BlazorComponent;
using BlazorComponent.I18n;
using Blazored.LocalStorage;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Global.Config
{
    public class GlobalConfig
    {
        #region Field

        private string? _pageMode;
        private bool _expandOnHover;
        private bool _navigationMini;
        private string? _favorite;
        private ILocalStorageService? _cookieStorage;
        private string? _navigationStyle;

        #endregion

        #region Property

        public static string PageModeKey { get; set; } = "GlobalConfig_PageMode";

        public static string NavigationStyleKey { get; set; } = "GlobalConfig_NavigationStyle";

        public static string ExpandOnHoverCookieKey { get; set; } = "GlobalConfig_ExpandOnHover";

        public static string FavoriteCookieKey { get; set; } = "GlobalConfig_Favorite";

        public string PageMode
        {
            get => _pageMode ?? PageModes.PageTab;
            set
            {
                _pageMode = value;
                _cookieStorage?.SetItemAsync(PageModeKey, value);
            }
        }

        public string NavigationStyle
        {
            get => _navigationStyle ?? NavigationStyles.Flat;
            set
            {
                _navigationStyle = value;
                _cookieStorage?.SetItemAsync(NavigationStyleKey, value);
            }
        }

        public bool ExpandOnHover
        {
            get => _expandOnHover;
            set
            {
                _expandOnHover = value;
                _cookieStorage?.SetItemAsync(ExpandOnHoverCookieKey, value.ToString());
            }
        }

        public string? Favorite
        {
            get => _favorite;
            set
            {
                _favorite = value;
                _cookieStorage?.SetItemAsync(FavoriteCookieKey, value.ToString()) ;
            }
        }

        #endregion

        public GlobalConfig(ILocalStorageService cookieStorage)
        {
            _cookieStorage = cookieStorage;
           
        }

        #region event

        public delegate void GlobalConfigChanged();

        #endregion

        #region Method

        public async Task Initialization()
        {
            _pageMode =await _cookieStorage.GetItemAsStringAsync(PageModeKey);
            _navigationStyle = await _cookieStorage.GetItemAsStringAsync(NavigationStyleKey); 
            var data= await _cookieStorage.GetItemAsStringAsync(PageModeKey);
            if (data.IsNotNullOrEmpty())
            {
                _expandOnHover = Convert.ToBoolean(data);
            }
            
            _favorite = await _cookieStorage.GetItemAsStringAsync(FavoriteCookieKey); 
        }
        #endregion
    }
}
