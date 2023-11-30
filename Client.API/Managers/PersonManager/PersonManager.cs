using Client.API.Models;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.PersonManager
{
    public class PersonManager : BaseApiManager, IPersonManager
    {
        private readonly string resourceName = "api/ownperson";
        public PersonManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ReturnModel<ActionResultVm<OwnPersonDto>>> Query(KeyWordQuery keyword, Pagination pagination)
        {
            try
            {
                ActionResultVm<OwnPersonDto> person = new ActionResultVm<OwnPersonDto>();
                var client = CreateHttpClient();
                var param=$"pageIndex={pagination.PageIndex}&pageSize={pagination.PageSize}&keyWords={keyword.KeyWords}";
                if (keyword.CreateTime != null && keyword.CreateTime.Count>0) 
                {
                    foreach(var time in keyword.CreateTime)
                    {
                        param += $"&createTime={time.ToString("yyyy-MM-dd")}";
                    }
                }
                var response = await client.GetAsync($"{resourceName}/query?{param}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    result= result.Base64Decode();
                    person = JsonConvert.DeserializeObject<ActionResultVm<OwnPersonDto>>(result);
                   
                }
                 

                return new ReturnModel<ActionResultVm<OwnPersonDto>>
                {
                    Data = person,
                    Flag = response.IsSuccessStatusCode
                };

            }
            catch (Exception ex)
            {
                return new ReturnModel<ActionResultVm<OwnPersonDto>>
                {
                    Msg = ex.Message
                };
            }
        }
    }
}
