using XT.Common.Dtos.Admin.Dict;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Dtos.Admin.Logging;
using XT.Common.Extensions;
using XT.Common.Models.Server;
using System.ComponentModel;
using System.Xml.Linq;

namespace Client.API.Managers.Dict
{
    public class DictManager : BaseApiManager, IDictManager
    {
        private readonly string resourceName = "/api/sysDictData";
        private readonly string resourceType = "/api/sysDictType";
        public DictManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 查询页面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<AddDictDataInput>>> GetPageData( PageDictDataInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/page";


            return await client.GetAdminData<SqlSugarPagedList<AddDictDataInput>>(url, UserConfig, input);

        }
        /// <summary>
        /// 获取字典值列表
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取字典值列表")]
        public async Task<AdminCodeResult<List<AddDictDataInput>>> GetList(GetDataDictDataInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";


            return await client.GetAdminData<List<AddDictDataInput>>(url, UserConfig, input);
        }
        /// <summary>
        /// 增加字典值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        [DisplayName("增加字典值")]
        public async Task<AdminCodeResult<string>> AddDictData(AddDictDataInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateDictData(UpdateDictDataInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteDictData(DeleteDictDataInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddDictDataInput>>> GetDataList(string code)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/dataList/{code}";


            return await client.GetAdminData<List<AddDictDataInput>>(url, UserConfig, null);
        }

        /// <summary>
        /// 查询页面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<AddDictTypeInput>>> GetTypePageData(PageDictTypeInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceType}/page";


            return await client.GetAdminData<SqlSugarPagedList<AddDictTypeInput>>(url, UserConfig, input);

        }
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddDictTypeInput>>> GetTypeList(GetDataDictTypeInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceType}/list";


            return await client.GetAdminData<List<AddDictTypeInput>>(url, UserConfig, input);
        }
        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddDictType(AddDictTypeInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceType}/add";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateDictType(UpdateDictTypeInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceType}/update";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }
        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteDictType(DeleteDictTypeInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceType}/delete";


            return await client.PostAdminData<string>(url, UserConfig, input);
        }


    }
}
