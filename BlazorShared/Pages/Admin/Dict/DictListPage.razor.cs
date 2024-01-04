using BlazorShared.Components.DataTable;
using Client.API.Managers.Dict;
using XT.Common.Dtos.Admin.Dict;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Helper;
using Client.API.Managers.MenuManager;
using XT.Common.Dtos.Admin.Menu;
using Mapster;

namespace BlazorShared.Pages.Admin.Dict
{
    public partial class DictListPage
    {
        public DictListPage()
        {

        }

        #region 通用Table代码
        private AppDataTable<AddDictTypeInput, PageDictTypeInput, AddDictTypeInput, AddDictTypeInput> _table;

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IDictManager DictManager { get; set; }



        /// <summary>
        /// 查询
        /// </summary>
        public PageDictTypeInput SearchInput { get; set; } = new PageDictTypeInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<AddDictTypeInput>> QueryCall(PageDictTypeInput input)
        {
            var result = await DictManager.GetTypePageData(input);
            return result.GetResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteTypeCall(IEnumerable<AddDictTypeInput> menus)
        {
            var result = await DictManager.DeleteDictType(new DeleteDictTypeInput
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
        private async Task EditTypeCall(AddDictTypeInput data)
        {
            var update = data.Adapt<UpdateDictTypeInput>();
            var result = await DictManager.UpdateDictType(update);
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddDictTypeInput item)
        {
            if (search.IsNotNullOrEmpty() && item.Name != null)
            {
                return item.Name.Contains(search);
            }

            return false;

        }




        #endregion
        private bool DictShow = false;
        private AppDataTable<AddDictDataInput, PageDictDataInput, AddDictDataInput, AddDictDataInput> _tableDict;
        private async void ShowDicts(AddDictTypeInput input)
        {
            DictShow = true;
            SearchDictInput.DictTypeId = input.Id;
            //await _tableDict.QueryClickAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        public PageDictDataInput SearchDictInput { get; set; } = new PageDictDataInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<AddDictDataInput>> QueryDictCall(PageDictDataInput input)
        {
            var result = await DictManager.GetPageData(input);
            return result.GetResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteDataCall(IEnumerable<AddDictDataInput> menus)
        {
            var result = await DictManager.DeleteDictData(new DeleteDictDataInput
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
        private async Task EditDataCall(AddDictDataInput data)
        {
            var update = data.Adapt<UpdateDictTypeInput>();
            var result = await DictManager.UpdateDictType(update);
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchDictFilter(string search, AddDictDataInput item)
        {
            if (search.IsNotNullOrEmpty() && item.Value != null)
            {
                return item.Value.Contains(search);
            }

            return false;

        }
    }
}
