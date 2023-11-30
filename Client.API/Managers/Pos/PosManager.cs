using GeneralCommon.Dtos.Admin.Org;
using GeneralCommon.Dtos.Admin.Pos;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Pos
{
    public class PosManager : BaseApiManager, IPosManager
    {
        private readonly string resourceName = "api/sysPos";
        public PosManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddPosInput>>> GetList(PosInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";

            return await client.GetAdminData<List<AddPosInput>>(url, UserConfig, input);
        }
        /// <summary>
        /// 添加职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddPos(AddPosInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";

            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 更新职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       public async Task<AdminCodeResult<string>> UpdatePos(UpdatePosInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";

            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeletePos(DeletePosInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";

            return await client.PostAdminData<string>(url, UserConfig, input);
        }

    }
}
