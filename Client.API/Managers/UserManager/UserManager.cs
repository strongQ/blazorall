using XT.Common.Dtos.Admin.Role;
using XT.Common.Dtos.Admin;
using XT.Common.Dtos.Admin.User;
using XT.Common.Extensions;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using XT.Common.Dtos.Admin.Pos;
using XT.Common.Models.SignalR;
using Microsoft.Extensions.Options;

namespace Client.API.Managers.UserManager
{
    public class UserManager : BaseApiManager, IUserManager
    {
        private readonly string resourceName = "/api/sysUser";
        private readonly string posName = "api/sysPos";
        private readonly IApiConfig _userConfig;
        public UserManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }

        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<AddUserInput>>> GetUserPage(PageUserInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/page";


            return await client.GetAdminData<SqlSugarPagedList<AddUserInput>>(url, _userConfig, input);
        }

        /// <summary>
        /// 获取用户拥有角色集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [DisplayName("获取用户拥有角色集合")]
        public async Task<AdminCodeResult<List<long>>> GetOwnRoleList(long userId)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/ownRoleList/{userId}";


            return await client.GetAdminData<List<long>>(url, _userConfig, null);
        }
        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<int>> SetStatus(UserInput userInput)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/setStatus";


            return await client.PostAdminData<int>(url, _userConfig, userInput);
        }



        /// <summary>
        /// 获取用户扩展机构集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [DisplayName("获取用户扩展机构集合")]
        public async Task<AdminCodeResult<List<SysUserExtOrg>>> GetOwnExtOrgList(long userId)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/ownExtOrgList/{userId}";


            return await client.GetAdminData<List<SysUserExtOrg>>(url, _userConfig, null);
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("获取职位列表")]
        public async Task<AdminCodeResult<List<AddPosInput>>> GetPosList(PosInput input)
        {
            var client = CreateHttpClient();
            var url = $"{posName}/list";


            return await client.GetAdminData<List<AddPosInput>>(url, _userConfig, input);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddUser(AddUserInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";


            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateUser(UpdateUserInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";


            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteUser(DeleteUserInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("重置用户密码")]
        public async Task<AdminCodeResult<string>> ResetPwd(ResetPwdUserInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/resetPwd";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 查看用户基本信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("查看用户基本信息")]
       public async Task<AdminCodeResult<AddUserInput>> GetBaseInfo()
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/baseInfo";

            return await client.GetAdminData<AddUserInput>(url, _userConfig, null);
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("更新用户基本信息")]
        public async Task<AdminCodeResult<string>> UpdateBaseInfo(AddUserInput user)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/baseInfo";

            return await client.PostAdminData<string>(url, _userConfig, user);
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("修改用户密码")]
       public async Task<AdminCodeResult<string>> ChangePwd(ChangePwdInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/changePwd";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }
    }
}
