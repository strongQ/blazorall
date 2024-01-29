using BlazorShared.Config;
using BlazorShared.Models;
using BlazorShared.Services;
using Client.API.Managers.LoginManager;
using XT.Common.Dtos.Admin.Auth;
using XT.Common.Dtos.Login;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using XT.Common.Extensions;
using System.Text;
using System.Threading.Tasks;
using BlazorShared.Data.Base;
using BlazorComponent.I18n;
using BlazorShared.Pages;
using BlazorShared.Shared;
using BlazorComponent;
using Blazored.LocalStorage;
using System.Security.Principal;


namespace BlazorShared.Core
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILoginManager _loginService;
        private IApiConfig _apiConfig;
        private string _token = string.Empty;
        private readonly ILocalStorageService _localStorageService;

        private GlobalVariables _global;


        private static bool _isFirst = true;

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
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }

               

                   
                    if (string.IsNullOrEmpty(_token) || TokenHelper.IsTokenExpired(_token))
                    {
                        var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                        return anonymous;
                    }
                    var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TokenHelper.ParseClaimsFromJwt(_token), "jwt"));

                    var loginUser = new AuthenticationState(userClaimPrincipal);
                    return loginUser;
               
            
          
           
          
        }

        public async Task Notify()
        {
            _token = await _localStorageService.GetItemAsync<string>("token");
            var config = await _localStorageService.GetItemAsync<ApiConfig>("HiSetting");
            if (config != null)
            {
                _apiConfig.RemoteApiUrl = config.RemoteApiUrl;
                _apiConfig.OtherUrl = config.OtherUrl;
                _apiConfig.Token = config.Token;
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }



    }
}
