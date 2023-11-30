using GeneralCommon.Dtos.Admin.Org;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Org
{
    public class OrgManager : BaseApiManager, IOrgManager
    {
        private readonly string resourceName = "api/sysOrg";

        public OrgManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddOrgInput>>> GetOrgList(OrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";

            return await client.GetAdminData<List<AddOrgInput>>(url, UserConfig, input);
        }
        /// <summary>
        /// 增加机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<bool>> AddOrg(AddOrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";

            return await client.PostAdminData<bool>(url, UserConfig, input);
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateOrg(UpdateOrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";

            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteOrg(DeleteOrgInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";

            return await client.PostAdminData<string>(url, UserConfig, input);
        }
    }
}
