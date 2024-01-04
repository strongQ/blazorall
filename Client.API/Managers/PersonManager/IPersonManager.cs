using Client.API.Models;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.PersonManager
{
    public interface IPersonManager: IApiManager
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<ReturnModel<ActionResultVm<OwnPersonDto>>> Query(KeyWordQuery keyword, Pagination pagination); 
    }
}
