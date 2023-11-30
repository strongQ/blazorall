using GeneralCommon.Dtos.Admin.Menu;
using GeneralCommon.Models.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.API.Managers.MenuManager
{
    public interface IMenuManager : IApiManager
    {
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="deleteMenuInput"></param>
        /// <returns></returns>
        Task<AdminCodeResult<string>> Delete(DeleteMenuInput deleteMenuInput);
        /// <summary>
        /// 获取菜单列表数据
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        Task<AdminCodeResult<List<AddMenuInput>>> GetPage(MenuInput searchInput);

        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("增加菜单")]
        Task<AdminCodeResult<string>> AddMenu(AddMenuInput input);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [DisplayName("更新菜单")]
        Task<AdminCodeResult<string>> UpdateMenu(UpdateMenuInput input);

    }
}
