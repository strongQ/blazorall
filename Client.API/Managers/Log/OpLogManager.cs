using GeneralCommon.Dtos.Admin;
using GeneralCommon.Dtos.Admin.Logging;
using GeneralCommon.Dtos.Admin.Role;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Log
{
    public class OpLogManager : BaseApiManager, IOpLogManager
    {
        private readonly string resourceName = "/api/sysLogOp";
        public OpLogManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<AddLogInput>>> GetPage(PageLogInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/page";


            return await client.GetAdminData<SqlSugarPagedList<AddLogInput>>(url, UserConfig, input);
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public async Task<AdminCodeResult<bool>> Clear()
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/clear";


            return await client.PostAdminData<bool>(url, UserConfig,null);
        }

      
    }
}
