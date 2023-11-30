using BlazorComponent;
using BlazorShared.Components.DataTable;
using BlazorShared.Data.Base;
using Client.API.Managers.RoleManager;
using Client.API.Managers.UserManager;
using GeneralCommon.Dtos.Admin.Org;
using GeneralCommon.Dtos.Admin;
using GeneralCommon.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Helper;
using Client.API.Managers.MenuManager;
using GeneralCommon.Dtos.Admin.Menu;
using Client.API.Managers.Org;
using Mapster;
using GeneralCommon.Extensions;

namespace BlazorShared.Pages.Admin.Dept
{
    public partial class DeptListPage
    {
        public DeptListPage()
        {

        }
        #region 通用Table代码
        private AppDataTable<AddOrgInput, OrgInput, AddOrgInput, AddOrgInput> _table;
      
        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IOrgManager OrgManager { get; set; }

      

        /// <summary>
        /// 查询
        /// </summary>
        public OrgInput SearchInput { get; set; } = new OrgInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<AddOrgInput>> QueryListCall(OrgInput input)
        {
            var result = await OrgManager.GetOrgList(SearchInput);
            if (result.Code == 200)
            {
                return result.Result;
            }
            else
            {
                return new List<AddOrgInput>();
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(AddOrgInput input)
        {
            input.Pid = SearchInput.Id;
            var result = await OrgManager.AddOrg(input);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<AddOrgInput> orgs)
        {
            var result = await OrgManager.DeleteOrg(new DeleteOrgInput
            {
                Id = orgs.FirstOrDefault().Id
            });
            result.ShowMessage(PopupService);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task EditCall(AddOrgInput orgInput)
        {
            var data = orgInput.Adapt<UpdateOrgInput>();
            var result = await OrgManager.UpdateOrg(data);
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddOrgInput item)
        {
            if (search.IsNotNullOrEmpty())
            {
                return item.Name.Contains(search);
            }

            return false;

        }

       
        #endregion

     









        /// <summary>
        /// 供选择的部门
        /// </summary>
        private List<GeneralCommon.Dtos.Admin.Org.AddOrgInput> Orgs { get; set; } = new List<GeneralCommon.Dtos.Admin.Org.AddOrgInput>();

    


       

        /// <summary>
        /// 选中部门
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public async Task CallSeletDept((List<long>, List<GeneralCommon.Dtos.Admin.Org.AddOrgInput>) datas)
        {
            if (datas.Item1.Count > 0)
            {
                SearchInput.Id = datas.Item1[0];
            }
            await _table.QueryClickAsync();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected async override Task OnInitializedAsync()
        {
           

            var result = await OrgManager.GetOrgList(new GeneralCommon.Dtos.Admin.Org.OrgInput
            {
                Id = -1
            });
            if (result.Code == 200)
            {
                Orgs = result.Result;
            }

         
            await base.OnInitializedAsync();
        }


      
      
    }
}
