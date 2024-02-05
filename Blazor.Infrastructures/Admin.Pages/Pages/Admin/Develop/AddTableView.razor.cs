using BlazorComponent;
using BlazorXT.Helper;
using Client.API.Managers.DevelopManager;
using XT.Common.Dtos.Admin.DataBase;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Admin.Pages.Pages.Admin.Develop
{
    public partial class AddTableView
    {
        [Parameter]
        public bool ShowAddTable { get; set; }
        [Parameter]
        public List<string> DbTypes { get; set; }

        [Parameter]
        public string ConfigID { get; set; }
        [Parameter]
        public Action<bool> CloseAction { get; set; }


        [Inject]
        public IDatabaseManager DatabaseManager { get; set; }

        private DbTableInfo _selectTable = new DbTableInfo();

        private List<DbColumnOutput> TableDatas = new List<DbColumnOutput>();
        public AddTableView()
        {

        }


        private async Task Save()
        {
           var result=await DatabaseManager.AddTable(new DbTableInput
            {
                DbColumnInfoList = TableDatas.Adapt<List<DbColumnInput>>(),
                Description = _selectTable.Description,
                TableName = _selectTable.Name,
                ConfigId = ConfigID
            });

           var ok= result.ShowMessage(PopupService);

            if (ok)
            {
                CloseAction?.Invoke(true);
                ShowAddTable = false;
            }
        }

        private async Task Close()
        {
            CloseAction?.Invoke(true);
            ShowAddTable = false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        private async Task DeleteData(IEnumerable<DbColumnOutput> datas) 
        {
            TableDatas.Remove(datas.FirstOrDefault());
        }

        private void AddPrimary()
        {
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "ID",
                DataType = "bigint",
                ColumnDescription = "主键",
                IsPrimarykey = true
            });
        }

        private void AddCommon()
        {
            TableDatas.Add(new DbColumnOutput
            {
                IsNullable = true
            });
        }

        private void AddTenant()
        {
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "TenantId",
                DataType = "bigint",
                ColumnDescription = "租户Id",
                IsNullable = true
            });
        }

        private void AddBaseCommon()
        {
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "CreateTime",
                DataType = "datetime",
                ColumnDescription = "创建时间",
                IsNullable = true
            });
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "UpdateTime",
                DataType = "datetime",
                ColumnDescription = "更新时间",
                IsNullable = true
            });
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "CreateUserId",
                DataType = "bigint",
                ColumnDescription = "创建者Id",
                IsNullable = true
            });
            TableDatas.Add(new DbColumnOutput
            {
                DbColumnName = "IsDelete",
                DataType = "bit",
                ColumnDescription = "软删除",
                IsNullable = true
            });
        }
    }
}
