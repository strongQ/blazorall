using XT.Common.Dtos.Admin.Role;
using XT.Common.Dtos.Admin;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Dtos.Admin.Auth;
using XT.Common.Interfaces;
using System.Net.Http;
using XT.Common.Extensions;
using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Menu;
using System.ComponentModel;

namespace Client.API.Managers.RoleManager
{
    public class RoleManager: BaseApiManager,IRoleManager
    {
        private readonly string resourceName = "/api/sysRole";
        private readonly string orgName = "/api/sysOrg";
        private readonly string menuName = "/api/sysMenu";
        private IApiConfig _userConfig;
        public RoleManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<RoleOutput>>> GetList()
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";


            return await client.GetAdminData<List<RoleOutput>>(url, _userConfig, null);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<SysRole>>> GetPage(PageRoleInput input)
        {

            var client = CreateHttpClient();
            var url = $"{resourceName}/page";
          

           return  await client.GetAdminData<SqlSugarPagedList<SysRole>>(url, _userConfig,input);

        }
        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddOrgInput>>> GetOrgPage(OrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{orgName}/list";

            return await client.GetAdminData<List<AddOrgInput>>(url, _userConfig, input);
        }

        /// <summary>
        /// 根据角色Id获取机构Id集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<long>>> GetRoleOrgIdList(RoleInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/ownOrgList";

            return await client.GetAdminData<List<long>>(url, _userConfig, input);
        }

        /// <summary>
        /// 根据角色Id获取菜单Id集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<long>>> GetMenuIdList(RoleInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/ownMenuList";

            return await client.GetAdminData<List<long>>(url, _userConfig, input);
        }
        /// <summary>
        /// 授权数据范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> GrantDataScope(RoleOrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/grantDataScope";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddMenuInput>>> GetMenus(MenuInput input)
        {
            var client = CreateHttpClient();
            var url = $"{menuName}/list";

            return await client.GetAdminData<List<AddMenuInput>>(url, _userConfig, input);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddRole(AddRoleInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       public async Task<AdminCodeResult<string>> UpdateRole(UpdateRoleInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteRole(DeleteRoleInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

    }
}
