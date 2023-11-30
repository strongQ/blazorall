﻿using BlazorComponent;
using Blazored.LocalStorage;
using BlazorShared.Core;
using BlazorShared.Data.Base;
using BlazorShared.Extensions;
using BlazorShared.Global.Nav;
using BlazorShared.Global.Nav.Model;
using BlazorShared.Helper;
using BlazorShared.Models;
using Client.API.Managers.LoginManager;
using Client.API.Managers.UserManager;
using GeneralCommon.Dtos;
using GeneralCommon.Dtos.Admin.Auth;
using GeneralCommon.Dtos.Admin.Menu;
using GeneralCommon.Dtos.Login;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Auth;
using GeneralCommon.Models.Nav;
using GeneralCommon.Themes;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Config
{
    public class UserConfig:IUserConfig
    {

        #region 新增代码
       public event EventHandler<bool> ChangeThemeEvent;

        public UserConfig(ILocalStorageService cookieStorage,IApiConfig apiConfig, MasaBlazor masaBlazor,ILoginManager loginManager)
        {
            _masaBlazor = masaBlazor;
            _cookieStorage = cookieStorage;
            _apiConfig = apiConfig;
            _loginManager = loginManager;
           
        }
       private ILoginManager _loginManager;
        private MasaBlazor _masaBlazor { get; set; }
        private ILocalStorageService _cookieStorage;
        public List<NavItem> WorkbenchOutputs { get; set; }
        public List<NavItem> SameLevelMenus { get; private set; } = new();
        public List<NavItem> Navs { get;  set; } = new();

        public List<PageTabItem> PageTabItems = new();

        public AppTheme Themes { get; set; } = new AppTheme();


        private IApiConfig _apiConfig;
        public async Task InitAllAsync()
        {
            if (GlobalVariables.IsSingleApp)
            {
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
            if (GlobalVariables.IsSingleApp)
            {
                List<NavItem> items = new List<NavItem>();
                foreach(var page in GlobalVariables.Pages)
                {
                    if (page.Show)
                    {
                        items.Add(new NavItem
                        {
                            Href = page.Path,
                            Title = page.Name,
                            Icon=page.Icon
                        });
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
                Type=GeneralCommon.Enums.MenuTypeEnum.Menu,
                Path="/"
            });
            Navs.Clear();

            
           Menus.Parse(Navs);

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
           
            _cookieStorage?.SetItemAsync(GlobalVariables.ThemeCookieKey,JsonConvert.SerializeObject(Themes));
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
