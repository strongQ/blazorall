using XT.Common.Dtos.Admin.Menu;
using XT.Common.Dtos.Admin.User;
using XT.Common.Extensions;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.API.Managers.MenuManager
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuManager : BaseApiManager, IMenuManager
    {
        private readonly IApiConfig _userConfig;
        private readonly string resourceName = "/api/sysMenu";

        public MenuManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) : base(httpClientFactory, userConfig)
        {
            _userConfig = userConfig;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="deleteMenuInput"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<string>> Delete(DeleteMenuInput deleteMenuInput)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/delete";

            return await client.PostAdminData<string>(url, _userConfig, deleteMenuInput);
        }
        /// <summary>
        /// 获取菜单列表数据
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public async Task<AdminCodeResult<List<AddMenuInput>>> GetPage(MenuInput searchInput)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/list";

            return await client.GetAdminData<List<AddMenuInput>>(url, _userConfig, searchInput);
        }

        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DisplayName("增加菜单")]
        public async Task<AdminCodeResult<string>> AddMenu(AddMenuInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/add";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       
        [DisplayName("更新菜单")]
        public async Task<AdminCodeResult<string>> UpdateMenu(UpdateMenuInput input)
        {
            var client = CreateHttpClient();
            var url = $"{resourceName}/update";

            return await client.PostAdminData<string>(url, _userConfig, input);
        }

    }
}
