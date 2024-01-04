using BlazorShared.Components.DataTable;
using BlazorShared.Helper;
using Client.API.Managers.Log;
using XT.Common.Dtos.Admin;
using XT.Common.Dtos.Admin.Logging;
using XT.Common.Interfaces;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorShared.Pages.Admin.Log
{
    public partial class OpLogListPage
    {
        public OpLogListPage()
        {

        }
        #region 通用Table代码
        private AppDataTable<AddLogInput, PageLogInput, AddLogInput, AddLogInput> _table;

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IOpLogManager LogManager { get; set; }



        /// <summary>
        /// 查询
        /// </summary>
        public PageLogInput SearchInput { get; set; } = new PageLogInput();




        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<AddLogInput>> QueryCall(PageLogInput input)
        {
            var result = await LogManager.GetPage(input);
            return result.GetResult();
        }
       
     

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddLogInput item)
        {
            if (search.IsNotNullOrEmpty() && item.ActionName!=null)
            {
                return item.ActionName.Contains(search);
            }

            return false;

        }


        #endregion
    }
}
