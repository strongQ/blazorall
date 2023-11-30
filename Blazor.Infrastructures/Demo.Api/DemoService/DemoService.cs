using Demo.Main.Dto;
using Demo.Main.IService;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Api.DemoService
{
    public class DemoService : BaseApiService, IDemoService
    {
        private readonly IUserConfig _userConfig;
        private readonly string resourceName = "/api/demo";
        public DemoService(System.Net.Http.IHttpClientFactory httpClientFactory, IUserConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<PatCellOutput>> GetAll()
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/all";

            var result= await client.GetAdminData<List<PatCellOutput>>(url, _userConfig, null);

            return result.Result;
        }
    }
}
