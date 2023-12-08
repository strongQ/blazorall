using BlazorShared.Config;
using BlazorShared.Models;
using BlazorShared.Services;
using Client.API.Managers.LoginManager;
using GeneralCommon.Dtos.Admin.Auth;
using GeneralCommon.Dtos.Login;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GeneralCommon.Extensions;
using System.Text;
using System.Threading.Tasks;
using BlazorShared.Data.Base;
using BlazorComponent.I18n;
using BlazorShared.Pages;
using BlazorShared.Shared;
using Newtonsoft.Json;
using BlazorComponent;
using Blazored.LocalStorage;
using System.Security.Principal;


namespace BlazorShared.Core
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILoginManager _loginService;
        private IApiConfig _apiConfig;

        private readonly ILocalStorageService _localStorageService;

        private GlobalVariables _global;



        public HostAuthenticationStateProvider(ILoginManager loginService, IApiConfig apiConfig, ILocalStorageService localStorageService,GlobalVariables globalVariables)
        {
            _loginService = loginService;
            _apiConfig = apiConfig;
            _localStorageService = localStorageService;
            _global=globalVariables;

        }



        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_global.IsSingleApp)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "Admin") };
                var identity = new ClaimsIdentity(claims, nameof(HostAuthenticationStateProvider));
                return new AuthenticationState(new ClaimsPrincipal (identity));
            }
            string token = await _localStorageService.GetItemAsync<string>("token");
            var config = await _localStorageService.GetItemAsync<ApiConfig>("HiSetting");
            if (config != null)
            {
                _apiConfig.RemoteApiUrl = config.RemoteApiUrl;
                _apiConfig.OtherUrl = config.OtherUrl;
                _apiConfig.Token = config.Token;
            }
            if (string.IsNullOrEmpty(token) || TokenHelper.IsTokenExpired(token))
            {
                var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                return anonymous;
            }
            var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TokenHelper.ParseClaimsFromJwt(token), "jwt"));

            var loginUser = new AuthenticationState(userClaimPrincipal);
            return loginUser;
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }



    }
}
