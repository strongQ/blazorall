﻿using GeneralCommon.Dtos.Admin.File;
using GeneralCommon.Dtos.Admin.Role;
using GeneralCommon.Dtos.Admin;
using GeneralCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GeneralCommon.Dtos.Admin.Logging;
using GeneralCommon.Extensions;
using GeneralCommon.Models.Server;

namespace Client.API.Managers.File
{
    public class FileManager : BaseApiManager, IFileManager
    {
        private readonly string resourceName = "/api/sysFile";
        public FileManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
        }
        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<SqlSugarPagedList<AddFileInput>>> GetPage( PageFileInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/page";


            return await client.GetAdminData<SqlSugarPagedList<AddFileInput>>(url, UserConfig, input);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<FileOutput>>> UploadFiles(MultipartFormDataContent files)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/uploadFiles";
            return await client.PostAdmin<List<FileOutput>>(url, UserConfig, files);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> DeleteFile(DeleteFileInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";
            return await client.PostAdminData<string>(url, UserConfig, input);
        }

    }
}
