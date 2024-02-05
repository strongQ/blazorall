using BlazorComponent;
using BlazorXT.Components.DataTable;
using BlazorXT.Data.Base;
using Client.API.Managers.RoleManager;
using Client.API.Managers.UserManager;
using XT.Common.Dtos.Admin.User;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XT.Common.Dtos.Admin.Role;
using XT.Common.Extensions;
using BlazorXT.Helper;

using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Menu;
using Mapster;

namespace Admin.Pages.Pages.Admin.Role
{
    public partial class RoleListPage
    {
        #region 通用Table代码
        private AppDataTable<SysRole, PageRoleInput, SysRole, SysRole> _table;
        public RoleListPage()
        {

        }
        [Inject]
        public IUserConfig UserConfig { get; set; }
     

        [Inject]
        public IRoleManager RoleManager { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        public PageRoleInput SearchInput { get; set; } = new PageRoleInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<SysRole>> QueryCall(PageRoleInput input)
        {
            input.Name = input.SearchKey;
            var result = await RoleManager.GetPage(input);

            if (result.Code == 200)
            {
                return result.Result;
            }
            else
            {
                return new SqlSugarPagedList<SysRole> { };
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(SysRole input)
        {
            var add = input.Adapt<AddRoleInput>();
            add.MenuIdList = MenuIdList;
            var result = await RoleManager.AddRole(add);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<SysRole> users)
        {
            var result = await RoleManager.DeleteRole(new DeleteRoleInput
            {
                Id = users.FirstOrDefault().Id
            });
            result.ShowMessage(PopupService);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task EditCall(SysRole data)
        {
            var add = data.Adapt<UpdateRoleInput>();
            add.MenuIdList = MenuIdList;
            var result = await RoleManager.UpdateRole(add);
            result.ShowMessage(PopupService);
        }
      

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, SysRole item)
        {
            if (search.IsNotNullOrEmpty())
            {
                return item.Name.Contains(search);
            }

            return false;

        }

       
        #endregion

    

        private List<AddOrgInput> Orgs=new List<AddOrgInput>();
        private List<AddMenuInput> Menus = new List<AddMenuInput>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected async override Task OnInitializedAsync()
        {

           Orgs= (await RoleManager.GetOrgPage(new XT.Common.Dtos.Admin.Org.OrgInput
            {
                Id = 0
            })).GetResult();

           Menus= (await RoleManager.GetMenus(new XT.Common.Dtos.Admin.Menu.MenuInput())).GetResult();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 是否显示授权
        /// </summary>
        public bool ShowGrant { get; set; }

        private SysRole GrantRole { get; set; }

       

        private List<long> MenuIdList;
        /// <summary>
        /// 菜单选择
        /// </summary>
        /// <param name="menus"></param>
        private void MenuChanged(List<long> menus)
        {
            MenuIdList = menus;
        }
        /// <summary>
        /// 显示授权范围
        /// </summary>
        /// <param name="role"></param>
        public async void GrantShow(SysRole role)
        {

         
            GrantRole = role;
            ShowGrant = true;
        }

        public async Task GrantClose(bool ok)
        {
            if(ok)
           await _table.QueryClickAsync();

            ShowGrant= false;
        }



    }
}

