
using GeneralCommon.Dtos.Login;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Net.Http;
using GeneralCommon.Extensions;
using GeneralCommon.Dtos.Admin.Auth;
using GeneralCommon.Dtos.Admin.Menu;
using Client.API.Models;

namespace Client.API.Managers.LoginManager
{
    public class LoginManager :BaseApiManager, ILoginManager
    {
        private  readonly string resourceName = "/api/sysAuth";
        private readonly string menuResource = "/api/sysMenu";
        private IApiConfig _userConfig;
       
        public LoginManager(IHttpClientFactory httpClientFactory,IApiConfig userConfig):base(httpClientFactory,userConfig) 
        {
          _userConfig = userConfig;
        }
        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AdminCodeResult<CaptchaOutput>> GetCaptcha()
        {
            try
            {
                var client = CreateHttpClient();
                var res = await client.GetAsync($"{resourceName}/captcha");
                AdminCodeResult<CaptchaOutput> tokenResult = await res.AdminResult<CaptchaOutput>(_userConfig);
                return tokenResult;

            }
            catch (Exception ex)
            {
                return new AdminCodeResult<CaptchaOutput>()
                {
                    Message = ex.Message
                };
            }
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> TokenByRefresh(TokenInput input)
        {

            var client = CreateHttpClient();
            var url = $"{resourceName}/tokenByRefresh";

            var msg = await client.PostAdminData<string>(url, _userConfig, input);


            return msg;
        }


        /// <summary>
        /// 获取登录配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AdminCodeResult<LoginConfigOutput>> GetLoginConfig()
        {
            try
            {
                var client = CreateHttpClient();
                var res = await client.GetAsync($"{resourceName}/loginConfig");

                AdminCodeResult<LoginConfigOutput> tokenResult = await res.AdminResult<LoginConfigOutput>(_userConfig);
                return tokenResult;

            }
            catch (Exception ex)
            {
                return new AdminCodeResult<LoginConfigOutput>()
                {
                    Message=ex.Message
                };
            }
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AdminCodeResult<List<MenuOutput>>> GetMenus()
        {
            var url = $"{menuResource}/loginMenuTree";
            return await CreateHttpClient().GetAdminData<List<MenuOutput>>(url, _userConfig);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<LoginOutput>> Login(LoginInput data)
        {
           
            
                var client = CreateHttpClient();
                var url=$"{resourceName}/login";

               var msg= await client.PostAdminData<LoginOutput>(url, _userConfig, data);


            return msg;
           


          

        }
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public async Task<AdminCodeResult<LoginUserOutput>> LoginInfo()
        {
            try
            {

                var client = CreateHttpClient();
                // 获取按钮权限
                var res1 = await client.GetAsync($"{resourceName}/userInfo");

                var datas = await res1.AdminResult<LoginUserOutput>(_userConfig);

                return datas;
            }
            catch (Exception ex)
            {
                return new AdminCodeResult<LoginUserOutput>
                {
                    Message = ex.Message
                };
            }
        }
    }
}
