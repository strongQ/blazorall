using Client.API.Models;
using GeneralCommon.Dtos.Admin.Auth;
using GeneralCommon.Dtos.Admin.Menu;
using GeneralCommon.Dtos.Login;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.LoginManager
{
    public interface ILoginManager:IApiManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<AdminCodeResult<LoginOutput>> Login(LoginInput data);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> TokenByRefresh(TokenInput input);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<LoginUserOutput>> LoginInfo();
        /// <summary>
        /// 获取登录配置
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<LoginConfigOutput>> GetLoginConfig();
        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<CaptchaOutput>> GetCaptcha();
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<List<MenuOutput>>> GetMenus();
    }
}
