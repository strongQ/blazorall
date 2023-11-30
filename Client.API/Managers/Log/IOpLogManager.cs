using GeneralCommon.Dtos.Admin.Logging;
using GeneralCommon.Dtos.Admin;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Log
{
    public interface IOpLogManager: IApiManager
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<SqlSugarPagedList<AddLogInput>>> GetPage(PageLogInput input);
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<bool>> Clear();
    }
}
