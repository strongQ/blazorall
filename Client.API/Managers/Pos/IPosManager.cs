using XT.Common.Dtos.Admin.Pos;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.Pos
{
    /// <summary>
    /// 职位
    /// </summary>
    public interface IPosManager:IApiManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddPosInput>>> GetList(PosInput input);

        /// <summary>
        /// 添加职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> AddPos(AddPosInput input);

        /// <summary>
        /// 更新职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> UpdatePos(UpdatePosInput input);

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> DeletePos(DeletePosInput input);
        
    }
}
