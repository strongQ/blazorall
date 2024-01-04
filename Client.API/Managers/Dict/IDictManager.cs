using XT.Common.Dtos.Admin.Dict;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Dict
{
    public interface IDictManager:IApiManager
    {
        /// <summary>
        /// 查询页面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<SqlSugarPagedList<AddDictDataInput>>> GetPageData(PageDictDataInput input);

        /// <summary>
        /// 获取字典值列表
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取字典值列表")]
        Task<AdminCodeResult<List<AddDictDataInput>>> GetList(GetDataDictDataInput input);

        /// <summary>
        /// 增加字典值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [DisplayName("增加字典值")]
        Task<AdminCodeResult<string>> AddDictData(AddDictDataInput input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateDictData(UpdateDictDataInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteDictData(DeleteDictDataInput input);

        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddDictDataInput>>> GetDataList(string code);


        /// <summary>
        /// 查询页面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<SqlSugarPagedList<AddDictTypeInput>>> GetTypePageData(PageDictTypeInput input);

        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddDictTypeInput>>> GetTypeList(GetDataDictTypeInput input);

        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       Task<AdminCodeResult<string>> AddDictType(AddDictTypeInput input);

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdateDictType(UpdateDictTypeInput input);

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeleteDictType(DeleteDictTypeInput input);
       
    }
}
