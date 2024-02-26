using BlazorComponent;
using Blazored.LocalStorage;
using BlazorXT.Core;
using BlazorXT.Data.Base;
using BlazorXT.Extensions;
using BlazorXT.Global.Nav;
using BlazorXT.Global.Nav.Model;
using BlazorXT.Helper;
using BlazorXT.Models;
using Client.API.Managers.LoginManager;
using Client.API.Managers.UserManager;

using XT.Common.Dtos.Admin.Auth;
using XT.Common.Dtos.Admin.Menu;

using XT.Common.Extensions;
using XT.Common.Interfaces;

using XT.Common.Models.Nav;
using XT.Common.Themes;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.I18n;

namespace BlazorXT.Config
{
    public class UserConfig:IUserConfig
    {
        I18n _i18N;
        #region 新增代码
        public event EventHandler<bool> ChangeThemeEvent;

        public UserConfig(ILocalStorageService cookieStorage,IApiConfig apiConfig, MasaBlazor masaBlazor,ILoginManager loginManager,GlobalVariables globalVariables, I18n i18)
        {
            _masaBlazor = masaBlazor;
            _cookieStorage = cookieStorage;
            _apiConfig = apiConfig;
            _loginManager = loginManager;
            _globalVariables = globalVariables;
            _i18N = i18;
            _i18N.CultureChanged -= _i18N_CultureChanged;
            _i18N.CultureChanged += _i18N_CultureChanged;
        }
        private void _i18N_CultureChanged(object? sender, EventArgs e)
        {
            Navs.ForEach(x =>
            {
                x.Title = _i18N.T(x.State);
            });
        }
        private ILoginManager _loginManager;
        private GlobalVariables _globalVariables;
        private MasaBlazor _masaBlazor { get; set; }
        private ILocalStorageService _cookieStorage;
        public List<NavItem> WorkbenchOutputs { get; set; }
        public List<NavItem> SameLevelMenus { get; private set; } = new();
        public List<NavItem> Navs { get;  set; } = new();

       

        public AppTheme Themes { get; set; } = new AppTheme();


        private IApiConfig _apiConfig;
        public async Task InitAllAsync()
        {
            if (_globalVariables.IsSingleApp)
            {
                _apiConfig.RemoteApiUrl = GlobalVariables.Url;
                LoginUser = new LoginUserOutput
                {
                    Account = "SuperAdmin"
                };
            }
            else
            {
              

                if (_apiConfig.RemoteApiUrl.IsNullOrEmpty())
                {
                    return;
                }
                await InitUserAsync();
            }
            
            await InitMenuAsync();
        }
        public async Task InitUserAsync()
        {
          
            LoginUser = (await _loginManager.LoginInfo()).GetResult();

           

           
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public async Task InitMenuAsync()
        {
            if (_globalVariables.IsSingleApp)
            {
                List<NavItem> items = new List<NavItem>();
                foreach(var page in _globalVariables.Pages)
                {
                    if (page.Show)
                    {
                       var item= new NavItem
                        {
                            Href = page.Path,
                            Title = _i18N.T(page.Name, true),
                            State=page.Name,
                           Icon =page.Icon
                        };
                      
                        if (item.Icon.Contains("fa-")) item.Icon = "fa:" + item.Icon;
                        items.Add(item);
                    }
                }
                Navs = items;
                return;
            }
         
            Menus = (await _loginManager.GetMenus()).GetResult();
            Menus.Insert(0,new MenuOutput
            {
                Meta=new SysMenuMeta
                {
                    Icon="mdi-home",
                    Title="首页",                   
                },             
                Id = 0,
                Type=XT.Common.Enums.MenuTypeEnum.Menu,
                Path="/"
            });
            Navs.Clear();

            
           Menus.Parse(Navs,_globalVariables.Pages);

        }
        /// <summary>
        /// 是否含有权限点
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsHasButtonWithRole(string code)
        {
            if (LoginUser.Account=="superAdmin") return true;
            if(LoginUser.Buttons==null) return false;
            return LoginUser.Buttons.Contains(code);
        }
       
        //设置深浅主题统一由这个方法为入口
        public async Task SetDarkOrLightTheme()
        {
            Themes.IsDark = !Themes.IsDark;

            ChangeThemeEvent?.Invoke(this, Themes.IsDark);
            SetMasaTheme();
        }

       
        private void SetMasaTheme()
        {
           
            
            if (_masaBlazor.Theme.Dark != Themes.IsDark)
                _masaBlazor.ToggleTheme();
           
            _cookieStorage?.SetItemAsync(GlobalVariables.ThemeCookieKey,Themes.ToJson());
        }

        public void Exit()
        {
            return;
        }

        #endregion




        /// <summary>
        /// 登录的用户
        /// </summary>
        public LoginUserOutput LoginUser { get; set; } = new LoginUserOutput();

        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuOutput> Menus { get; set; } = new List<MenuOutput>();
      





       
       
    }
}
