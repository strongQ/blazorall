using GeneralCommon.Dtos.Admin.DataBase;
using GeneralCommon.Interfaces;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.DevelopManager
{
    /// <summary>
    /// 数据库
    /// </summary>
    public interface IDatabaseManager:IApiManager
    {
        /// <summary>
        /// 获取库列表
        /// </summary>
        /// <returns></returns>
        Task<AdminCodeResult<List<string>>> GetList();

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<DbTableInfo>>> GetTableList(string configID);

        /// <summary>
        /// 获取数据列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<DbColumnOutput>>> GetColumnList(DbSearchInput input);

        /// <summary>
        /// 更新列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateColumn(UpdateDbColumnInput input);

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="input"></param>
        Task<AdminCodeResult<string>> DeleteColumn(DeleteDbColumnInput input);

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> AddColumn(DbColumnInput input);

        /// <summary>
        /// 编辑表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateTable(UpdateDbTableInput input);

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteTable(DeleteDbTableInput input);

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> AddTable(DbTableInput input);

        /// <summary>
        /// 创建模板数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<string>>> CreateRazor(CreateEntityInput input);


    }
}
