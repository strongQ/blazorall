using BlazorComponent;
using BlazorShared.Models;
using Client.API.Managers.LoginManager;
using XT.Common.Interfaces;
using XT.Common.Models.SignalR;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using XT.Common.Extensions;

namespace BlazorShared.Shared
{
    public partial class LoginShared
    {
        public LoginShared()
        {

        }
        private bool _show;

        private string? _imgUrl;

        [Parameter]
        public LoginUser User { get; set; }


        [Parameter]
        public bool Loading { get; set; }

        [Inject]
        public ILoginManager  LoginManager { get; set; }

        [Inject]
        public IApiConfig  Config { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Parameter]
        public bool HideLogo { get; set; }
        /// <summary>
        /// 是否显示验证码
        /// </summary>
        [Parameter]
        public bool? ShowCaptcha { get; set; }

        [Parameter]
        public double Width { get; set; } = 410;

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public string CreateAccountRoute { get; set; } = $"pages/authentication/register-v1";

        [Parameter]
        public string ForgotPasswordRoute { get; set; } = $"pages/authentication/forgot-password-v1";

        [Parameter]
        public EventCallback<MouseEventArgs> OnLogin { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnSetting { get; set; }

        /// <summary>
        /// 点击图片
        /// </summary>
        /// <returns></returns>
        public async Task ClickImg()
        {
            if (Config.RemoteApiUrl.IsNullOrEmpty())
            {
                return;
            }
            var data = await LoginManager.GetCaptcha();

            if (data.Code == 200)
            {
                _imgUrl = "data:text/html;base64," + data.Result.Img;
                User.CodeID = data.Result.Id;
            }

        }

        protected override async Task OnInitializedAsync()
        {
           
           
            await base.OnInitializedAsync();
        }


    }
}
