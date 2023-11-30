using GeneralCommon.Dtos.Admin.User;
using GeneralCommon.Dtos.Admin;
using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GeneralCommon.Dtos.Admin.DataBase;

namespace Client.API.Managers.DevelopManager
{
    public class DatabaseManager : BaseApiManager, IDatabaseManager
    {
        private readonly string resourceName = "api/sysDatabase";
        private readonly IApiConfig _userConfig;
        public DatabaseManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }
        /// <summary>
        /// 获取库列表
        /// </summary>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<string>>> GetList()
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";


            return await client.GetAdminData<List<string>>(url, _userConfig, null);
        }
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<DbTableInfo>>> GetTableList(string configID)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/tableList/{configID}";


            return await client.GetAdminData<List<DbTableInfo>>(url, _userConfig, null);
        }
        /// <summary>
        /// 获取数据列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<DbColumnOutput>>> GetColumnList(DbSearchInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/columnList/{input.TableName}/{input.ConfigId}";


            return await client.GetAdminData<List<DbColumnOutput>>(url, _userConfig, null);
        }
        /// <summary>
        /// 更新列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateColumn(UpdateDbColumnInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/updateColumn";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="input"></param>
        public async Task<AdminCodeResult<string>> DeleteColumn(DeleteDbColumnInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/deleteColumn";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddColumn(DbColumnInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/addColumn";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 编辑表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> UpdateTable(UpdateDbTableInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/updateTable";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteTable(DeleteDbTableInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/deleteTable";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> AddTable(DbTableInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/addTable";
            return await client.PostAdminData<string>(url, _userConfig, input);
        }
        /// <summary>
        /// 创建模板数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<string>>> CreateRazor(CreateEntityInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/createRazor";
            return await client.PostAdminData<List<string>>(url, _userConfig, input);
        }
    }
}
