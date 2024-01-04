using XT.Common.Dtos.Admin.File;
using XT.Common.Dtos.Admin;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Client.API.Managers.File
{
    public interface IFileManager:IApiManager
    {
        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<SqlSugarPagedList<AddFileInput>>> GetPage(PageFileInput input);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<FileOutput>>> UploadFiles(MultipartFormDataContent files);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteFile(DeleteFileInput input);
    }
}
