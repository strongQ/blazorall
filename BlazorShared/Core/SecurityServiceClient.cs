using BlazorComponent;
using Blazored.LocalStorage;
using BlazorXT.Config;
using BlazorXT.Data.Base;
using BlazorXT.Pages;
using Client.API.Managers.LoginManager;
using Client.API.Models;
using XT.Common.Dtos.Admin.Auth;
using XT.Common.Extensions;
using XT.Common.Interfaces;
using XT.Common.Models.Auth;
using XT.Common.Models.Server;
using Microsoft.AspNetCore.Components.Authorization;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Core
{
    public class SecurityServiceClient
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ILoginManager _http;
        private readonly IUserConfig _userConfig;
        private readonly IApiConfig _apiConfig;
       
        private readonly GlobalVariables _global;

        public SecurityServiceClient(ILocalStorageService localStorageService, IUserConfig userConfig, IApiConfig apiConfig,
            ILoginManager loginService
           ,GlobalVariables globalVariables)
        {
            _localStorageService = localStorageService;
            _http = loginService;
            _apiConfig = apiConfig;
            _userConfig = userConfig;
           
            _global = globalVariables;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<LoginOutput>> Login(LoginInput model)
        {
            if (_global.IsSingleApp)
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

                    _apiConfig.Token = result.Result.AccessToken;

                    await _localStorageService.SetItemAsync(GlobalVariables.StoreKey, _apiConfig);
                   
                    await _localStorageService.SetItemAsync("refreshToken", result.Result.RefreshToken);
                  



                }

            }

            return result;


        }
        public async Task Logout()
        {
            _apiConfig.Token = "";
            await _localStorageService.SetItemAsync(GlobalVariables.StoreKey,_apiConfig);
            await _localStorageService.RemoveItemAsync("refreshToken");
         
        }
        public async Task<string> PrepareBearerToken()
        {
          
            var token = await GetTokenAsync();
           
            _apiConfig.Token = token;

            return token;
            
        }
        public async Task<string> GetTokenAsync()
        {
          

            var config = await _localStorageService.GetItemAsync<ApiConfig>(GlobalVariables.StoreKey);
            if (config != null)
            {
                _apiConfig.RemoteApiUrl = config.RemoteApiUrl;
                _apiConfig.OtherUrl = config.OtherUrl;
                _apiConfig.Token = config.Token;
                
            }


            if (string.IsNullOrEmpty(_apiConfig.Token))
                return string.Empty;

            if (TokenHelper.IsTokenExpired(_apiConfig.Token))
                _apiConfig.Token = await TryRefreshToken(_apiConfig.Token);

            return _apiConfig.Token;
        }
        private async Task<string> TryRefreshToken(string token)
        {
            
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

            await _localStorageService.SetItemAsync(GlobalVariables.StoreKey, _apiConfig);

            return response.Result;

        }
    }
}
