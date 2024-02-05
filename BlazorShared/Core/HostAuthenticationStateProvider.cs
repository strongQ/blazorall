using BlazorXT.Config;
using BlazorXT.Models;
using BlazorXT.Services;
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
using BlazorXT.Data.Base;
using BlazorComponent.I18n;
using BlazorXT.Pages;
using BlazorXT.Shared;
using BlazorComponent;
using Blazored.LocalStorage;
using System.Security.Principal;


namespace BlazorXT.Core
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILoginManager _loginService;
        private IApiConfig _apiConfig;
        
       

        private GlobalVariables _global;


        private SecurityServiceClient _client;

        public HostAuthenticationStateProvider(ILoginManager loginService, IApiConfig apiConfig,GlobalVariables globalVariables,SecurityServiceClient securityService)
        {
            _loginService = loginService;
            _apiConfig = apiConfig;
           
            _global=globalVariables;
            _client = securityService;

        }



        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           
                if (_global.IsSingleApp)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "Admin") };
                    var identity = new ClaimsIdentity(claims, nameof(HostAuthenticationStateProvider));
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }

               

                   
                    if ( string.IsNullOrEmpty(_apiConfig.Token) || TokenHelper.IsTokenExpired(_apiConfig.Token))
                    {
                        var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                        return anonymous;
                    }
                    var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TokenHelper.ParseClaimsFromJwt(_apiConfig.Token), "jwt"));

                    var loginUser = new AuthenticationState(userClaimPrincipal);
                    return loginUser;
               
            
          
           
          
        }

        public async Task<bool> Notify(bool isLogin=false)
        {

            var token = await _client.PrepareBearerToken();
            if (token.IsNotNullOrEmpty())
            {             
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }
            else
            {
                if (!isLogin)
                {
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
                return false;
            }

           
        }

      



    }
}
