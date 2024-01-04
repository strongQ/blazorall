
using BlazorShared.Core;
using BlazorShared.Global.Nav;
using BlazorShared.Models;
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
using BlazorShared.Data.Base;
using BlazorShared.Shared;
using Microsoft.AspNetCore.Components.Web;
using BlazorShared.Config;
using BlazorComponent;
using Blazored.LocalStorage;

namespace BlazorShared.Pages
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

     

        [Inject]
        public NavHelper NavService { get; set; } 

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

        [Inject]
        public NavHelper NavHelper { get; set; }

        [Inject]
        ILocalStorageService localStorage { get; set; }

        /// <summary>
        /// 登录服务
        /// </summary>
        [Inject]
        public ILoginManager  LoginManager { get; set; }
      

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IApiConfig ApiConfig { get; set; }
        
       

        public LoginShared ShareLogin { get; set; }
        [Inject]
        BlazorShared.Data.Base.GlobalVariables Global { get; set; }

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
            
                NavHelper.NavigateTo(GlobalVariables.DefaultRoute);

            }

        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <returns></returns>
        private async Task OnSetting()
        {

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
