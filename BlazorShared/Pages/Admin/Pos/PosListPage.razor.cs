using BlazorShared.Components.DataTable;
using BlazorShared.Helper;
using Client.API.Managers.Pos;
using GeneralCommon.Dtos.Admin.Org;
using GeneralCommon.Dtos.Admin.Pos;
using GeneralCommon.Interfaces;
using GeneralCommon.Extensions;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorShared.Pages.Admin.Pos
{
    public partial class PosListPage
    {
        public PosListPage()
        {

        }
        #region 通用Table代码
        private AppDataTable<AddPosInput, PosInput, AddPosInput, AddPosInput> _table;

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IPosManager PosManager { get; set; }



        /// <summary>
        /// 查询
        /// </summary>
        public PosInput SearchInput { get; set; } = new PosInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<AddPosInput>> QueryListCall(PosInput input)
        {
            var result = await PosManager.GetList(SearchInput);
            if (result.Code == 200)
            {
                return result.Result;
            }
            else
            {
                return new List<AddPosInput>();
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(AddPosInput input)
        {
            
            var result = await PosManager.AddPos(input);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<AddPosInput> orgs)
        {
            var result = await PosManager.DeletePos(new DeletePosInput
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
        private async Task EditCall(AddPosInput posInput)
        {
            var data = posInput.Adapt<UpdatePosInput>();
            var result = await PosManager.UpdatePos(data);
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddPosInput item)
        {
            if (search.IsNotNullOrEmpty())
            {
                return item.Name.Contains(search);
            }

            return false;

        }


        #endregion
    }
}
