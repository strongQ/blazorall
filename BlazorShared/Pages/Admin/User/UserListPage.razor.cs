using BlazorComponent;
using BlazorShared.Components.DataTable;
using Client.API.Managers.RoleManager;
using Client.API.Managers.UserManager;
using XT.Common.Dtos.Admin;
using XT.Common.Dtos.Admin.User;
using XT.Common.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Components;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Data.Base;
using BlazorShared.Helper;
using Masa.Blazor;
using Microsoft.JSInterop;

namespace BlazorShared.Pages.Admin.User
{
    public partial class UserListPage
    {
        #region 通用Table代码
        private AppDataTable<AddUserInput, PageUserInput, AddUserInput, AddUserInput> _table;
        public UserListPage()
        {

        }
        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IUserManager UserManager { get; set; }

        [Inject]
        public IRoleManager RoleManager { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        public PageUserInput SearchInput { get; set; } = new PageUserInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<AddUserInput>> QueryCall(PageUserInput input)
        {
            input.Account = input.SearchKey;
            var result = await UserManager.GetUserPage(SearchInput);

            if (result.Code == 200)
            {
                return result.Result;
            }
            else
            {
                return new SqlSugarPagedList<AddUserInput> { };
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(AddUserInput input)
        {
            var result = await UserManager.AddUser(input);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<AddUserInput> users)
        {
            var result = await UserManager.DeleteUser(new DeleteUserInput
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
        private async Task EditCall(AddUserInput user)
        {
            var data = user.Adapt<UpdateUserInput>();
            var result = await UserManager.UpdateUser(data);
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddUserInput item)
        {
            if (search.IsNotNullOrEmpty())
            {
                return item.Account.Contains(search);
            }

            return false;

        }

        /// <summary>
        /// 表头设置
        /// </summary>
        /// <param name="datas"></param>
        private void FilterHeaders(List<DataTableHeader<AddUserInput>> datas)
        {
            datas.RemoveWhere(it => it.Value == nameof(AddUserInput.Password));
            datas.RemoveWhere(it => it.Value == nameof(AddUserInput.RoleIdList));



            foreach (var item in datas)
            {
                item.Sortable = false;
                item.Filterable = false;
                item.Divider = false;
                item.Align = DataTableHeaderAlign.Start;
                item.CellClass = " table-minwidth ";
                switch (item.Value)
                {
                    case nameof(AddUserInput.Account):
                        item.Sortable = true;
                        break;

                    case nameof(AddUserInput.CreateTime):
                        item.Sortable = true;
                        break;

                    case nameof(AddUserInput.Birthday):
                        item.Sortable = true;
                        break;

                    case nameof(AddUserInput.Status):
                        item.Sortable = true;
                        break;

                    case GlobalVariables.TB_Actions:
                       
                        item.CellClass = "";
                        break;
                }
            }
        } 
        #endregion

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public async Task ResetPwd(AddUserInput addUser)
        {
            var result = await PopupService.ConfirmAsync("提示", $"是否重置{addUser.Account}的密码");

            if (result)
            {
                var admin = await UserManager.ResetPwd(new ResetPwdUserInput
                {
                    Id = addUser.Id
                });

                admin.ShowMessage(PopupService);
            }
        }

      


     

    


        /// <summary>
        /// 供选择的部门
        /// </summary>
        private List<XT.Common.Dtos.Admin.Org.AddOrgInput> Orgs { get; set; } = new List<XT.Common.Dtos.Admin.Org.AddOrgInput>();

        /// <summary>
        /// 角色集合
        /// </summary>

        private List<XT.Common.Dtos.Admin.Role.RoleOutput> Roles { get; set; } = new List<XT.Common.Dtos.Admin.Role.RoleOutput>();
        
       
        /// <summary>
        /// 职位
        /// </summary>
        private List<XT.Common.Dtos.Admin.Pos.AddPosInput> Poss { get; set; } = new List<XT.Common.Dtos.Admin.Pos.AddPosInput>();

        /// <summary>
        /// 选中部门
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public async Task CallSeletDept((List<long>, List<XT.Common.Dtos.Admin.Org.AddOrgInput>) datas)
        {
            if (datas.Item1.Count > 0)
            {
                SearchInput.OrgId = datas.Item1[0];
            }
            await _table.QueryClickAsync();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected async override Task OnInitializedAsync()
        {
            var role = await RoleManager.GetList();
            if (role.Code == 200)
            {
                Roles = role.Result;
            }

            var result = await RoleManager.GetOrgPage(new XT.Common.Dtos.Admin.Org.OrgInput
            {
                Id = 0
            });
            if (result.Code == 200)
            {
                Orgs = result.Result;
            }

            Poss = (await UserManager.GetPosList(new XT.Common.Dtos.Admin.Pos.PosInput())).GetResult();
            await base.OnInitializedAsync();
        }


        /// <summary>
        /// 编辑前赋予角色权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<AddUserInput> EditShowCall(AddUserInput user)
        {
            user.RoleIdList = (await UserManager.GetOwnRoleList(user.Id)).GetResult();
            user.ExtOrgIdList = (await UserManager.GetOwnExtOrgList(user.Id)).GetResult();
            return user;
        }
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task StatusChanged(XT.Common.Enums.StatusEnum status,AddUserInput user)
        {
           var result= await UserManager.SetStatus(new UserInput
            {
                Id = user.Id,
                Status = status
            });
            result.ShowMessage(PopupService);
        }

    }
}
