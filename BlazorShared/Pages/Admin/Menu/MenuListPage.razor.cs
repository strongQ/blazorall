using BlazorShared.Components.DataTable;
using Client.API.Managers.RoleManager;
using Client.API.Managers.UserManager;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Role;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XT.Common.Extensions;
using Client.API.Managers.MenuManager;
using BlazorShared.Helper;
using Mapster;

namespace BlazorShared.Pages.Admin.Menu
{
    public partial class MenuListPage
    {
        #region 通用Table代码
        private AppDataTable<AddMenuInput, MenuInput, AddMenuInput, AddMenuInput> _table;
        public MenuListPage()
        {

        }
        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IMenuManager MenuManager { get; set; }
        [Inject]
        public IRoleManager RoleManager { get; set; }




        /// <summary>
        /// 查询
        /// </summary>
        public MenuInput SearchInput { get; set; } = new MenuInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<AddMenuInput>> QueryListCall(MenuInput input)
        {
            input.Title = input.SearchKey;
          var result=( await MenuManager.GetPage(input)).GetResult();
            return result;
           
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(AddMenuInput input)
        {
           
            var result = await MenuManager.AddMenu(input);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<AddMenuInput> menus)
        {
            var result = await MenuManager.Delete(new DeleteMenuInput
            {
                Id = menus.FirstOrDefault().Id
            });
            result.ShowMessage(PopupService);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task EditCall(AddMenuInput data)
        {
            var update = data.Adapt<UpdateMenuInput>();
            var result = await MenuManager.UpdateMenu(update);
            result.ShowMessage(PopupService);
        }


        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddMenuInput item)
        {
            if (search.IsNotNullOrEmpty() && item.Title.IsNotNullOrEmpty())
            {
                return item.Title.Contains(search);
            }

            return false;

        }


        #endregion


        private List<AddMenuInput> Menus = new List<AddMenuInput>();


        protected async override Task OnInitializedAsync()
        {
            Menus = (await RoleManager.GetMenus(new XT.Common.Dtos.Admin.Menu.MenuInput())).GetResult();
            await base.OnInitializedAsync();
        }




    }
}
