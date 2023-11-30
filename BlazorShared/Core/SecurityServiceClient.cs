using BlazorComponent;
using Blazored.LocalStorage;
using BlazorShared.Config;
using BlazorShared.Data.Base;
using BlazorShared.Pages;
using Client.API.Managers.LoginManager;
using Client.API.Models;
using GeneralCommon.Dtos.Admin.Auth;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Auth;
using GeneralCommon.Models.Server;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Core
{
    public class SecurityServiceClient
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ILoginManager _http;
        private readonly IUserConfig _userConfig;
        private readonly IApiConfig _apiConfig;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public SecurityServiceClient(ILocalStorageService localStorageService, IUserConfig userConfig, IApiConfig apiConfig,
            ILoginManager loginService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorageService = localStorageService;
            _http = loginService;
            _apiConfig = apiConfig;
            _userConfig = userConfig;
            _authenticationStateProvider = authenticationStateProvider;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<LoginOutput>> Login(LoginInput model)
        {
            if (GlobalVariables.IsSingleApp)
            {
                _userConfig.LoginUser = new LoginUserOutput
                {
                    Account = model.Account,

                };

                return new AdminCodeResult<LoginOutput>
                {
                    Code = 200
                };
            }
            var result = await _http.Login(model);
            if (result.Code == 200)
            {
                _apiConfig.Token = result.Result.AccessToken;
                var info = await _http.LoginInfo();
                if (info.Code == 200)
                {
                    _userConfig.LoginUser = info.Result;
                    await _userConfig.InitAllAsync();

                    await _localStorageService.SetItemAsync("HiSetting", _apiConfig);
                    await _localStorageService.SetItemAsync("token", result.Result.AccessToken);
                    await _localStorageService.SetItemAsync("refreshToken", result.Result.RefreshToken);
                    (_authenticationStateProvider as HostAuthenticationStateProvider).Notify();



                }

            }

            return result;


        }
        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");
            await _localStorageService.RemoveItemAsync("refreshToken");
            (_authenticationStateProvider as HostAuthenticationStateProvider).Notify();
        }
        public async Task PrepareBearerToken()
        {
          
            var token = await GetTokenAsync();
           
            _apiConfig.Token = token;
            
        }
        public async Task<string> GetTokenAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
                return string.Empty;

            if (TokenHelper.IsTokenExpired(token))
                token = await TryRefreshToken();

            return token;
        }
        private async Task<string> TryRefreshToken()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");
            string refreshToken = await _localStorageService.GetItemAsync<string>("refreshToken");
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
            {
                return string.Empty;
            }

            TokenInput tokenModel = new TokenInput(token, refreshToken);

            var response = await _http.TokenByRefresh(tokenModel);
            if (response.Code != 200)
            {
                return string.Empty;
            }


            await _localStorageService.SetItemAsync("token", response.Result);
            return response.Result;

        }
    }
}
