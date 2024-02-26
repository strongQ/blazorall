
using BlazorXT.Core;
using BlazorXT.Global.Nav;
using BlazorXT.Models;
using Client.API.Managers.LoginManager;

using XT.Common.Interfaces;
using XT.Common.Models;
using XT.Common.Models.SignalR;
using XT.Common.Services;

using Masa.Blazor;
using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

using XT.Common.Extensions;

using Timer = System.Timers.Timer;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using XT.Common.Dtos.Admin.Menu;
using BlazorXT.Data.Base;
using BlazorXT.Shared;
using Microsoft.AspNetCore.Components.Web;
using BlazorXT.Config;
using BlazorComponent;
using Blazored.LocalStorage;
using Masa.Blazor.Presets.Cron;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorXT.Pages
{
    public partial class Login
    {

      
        public Login()
        {
           
            User = new LoginUser
            {
                UserName = "superadmin",
                Password = "123456"
            };
        }

        public LoginUser User 
        {
            get;set;
        }
        /// <summary>
        /// 是否显示验证码
        /// </summary>
        private bool? _showCaptcha;

     


        private void SignalRService_MessageEvent(object? sender, InformModel e)
        {
            return;
        }

   
        public bool IsLoading { get; set; }

     

       

        private SecurityServiceClient _provider;
        [Inject]
        public SecurityServiceClient Provider
        {
            get { return _provider; }
            set 
            {
                if (_provider == null)
                {
                    _provider = value;
                  
                }
               
            }
        }


        [Inject]
        [NotNull]
        public IPopupService? PopupService { get; set; }

        [Inject]
        public ILogService LogService { get; set; }

      

        

        /// <summary>
        /// 登录服务
        /// </summary>
        [Inject]
        public ILoginManager  LoginManager { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IApiConfig ApiConfig { get; set; }
        [Inject]
        public AuthenticationStateProvider Authentication { get; set; }

        public LoginShared ShareLogin { get; set; }
        [Inject]
        BlazorXT.Data.Base.GlobalVariables Global { get; set; }

        private async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await OnLogin();
            }
        }

        


        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>

        private async Task OnLogin()
        {
            IsLoading = true;
            if (ApiConfig.RemoteApiUrl.IsNullOrEmpty())
            {
                ApiConfig.RemoteApiUrl = Global.RemoteApiUrl;
            }


            var result = await Provider.Login(new XT.Common.Dtos.Admin.Auth.LoginInput
          {
              Account=User.UserName,
              Password=User.Password,
              Code=User.Code,
              CodeId=User.CodeID
          });
            IsLoading = false;
            if (result.Code!=200)
            {
             
              await PopupService.EnqueueSnackbarAsync($"登录失败，{result.Message}",BlazorComponent.AlertTypes.Error);
              await ShareLogin.ClickImg();
            }
            else
            {

                //if (Authentication is HostAuthenticationStateProvider provider)
                //{
                //    await provider.Notify();                 
                //}
                NavigationManager.NavigateTo(GlobalVariables.DefaultRoute);

            }

        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <returns></returns>
        private async Task OnSetting()
        {

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Authentication is HostAuthenticationStateProvider provider)
            {
                var result = await provider.Notify(true);

                if (result) return;
            }
        }

        protected override async Task OnInitializedAsync()
        {
          

            if (ApiConfig.RemoteApiUrl.IsNullOrEmpty())
            {
                ApiConfig.RemoteApiUrl = Global.RemoteApiUrl;
            }

            // 获取登录配置
            var configAdmin = await LoginManager.GetLoginConfig();
            if (configAdmin.Code == 200)
            {
                _showCaptcha = configAdmin.Result.CaptchaEnabled;
                if (_showCaptcha==true)
                {
                   await ShareLogin.ClickImg();
                }
               
            }
        }
    }
}
