using BlazorShared.Components.DataTable;
using Client.API.Managers.DevelopManager;
using Client.API.Managers.MenuManager;
using Client.API.Managers.RoleManager;
using XT.Common.Dtos.Admin.DataBase;
using XT.Common.Dtos.Admin.Menu;
using XT.Common.Interfaces;
using XT.Common.Extensions;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Helper;
using Mapster;
using Util.Reflection.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using BlazorComponent;

namespace BlazorShared.Pages.Admin.Develop
{
    public partial class DatabaseListPage
    {
        #region 通用Table代码
        private AppDataTable<DbColumnOutput, DbSearchInput, DbColumnOutput, DbColumnOutput> _table;
        public DatabaseListPage()
        {

        }
        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IDatabaseManager DatabaseManager { get; set; }
       




        /// <summary>
        /// 查询
        /// </summary>
        public DbSearchInput SearchInput { get; set; } = new DbSearchInput();

        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, DbColumnOutput item)
        {
            if (search.IsNotNullOrEmpty() && item.DbColumnName.IsNotNullOrEmpty())
            {
                return item.DbColumnName.Contains(search);
            }

            return false;

        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<DbColumnOutput>> QueryListCall(DbSearchInput input)
        {
            
            var result = (await DatabaseManager.GetColumnList(input)).GetResult();
            return result;

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddCall(DbColumnOutput input)
        {
            var data = input.Adapt<DbColumnInput>();
            data.ConfigId = SearchInput.ConfigId;
            data.TableName = SearchInput.TableName;
            var result = await DatabaseManager.AddColumn(data);
            result.ShowMessage(PopupService);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<DbColumnOutput> menus)
        {
            var result = await DatabaseManager.DeleteColumn(new DeleteDbColumnInput
            {
                DbColumnName=menus.FirstOrDefault().DbColumnName,
                ConfigId=SearchInput.ConfigId,
                TableName=SearchInput.TableName             
                
            });
            result.ShowMessage(PopupService);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task EditCall(DbColumnOutput data)
        {
            var update = data.Adapt<UpdateDbColumnInput>();

            update.TableName = SearchInput.TableName;
            update.OldColumnName=data.OldColumnName;
            update.ColumnName = data.DbColumnName;
            update.ConfigId=SearchInput.ConfigId;
            var result = await DatabaseManager.UpdateColumn(update);
            result.ShowMessage(PopupService);
        }
        /// <summary>
        /// 展示前逻辑
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<DbColumnOutput> EditShowCall(DbColumnOutput data)
        {
            data.OldColumnName = data.DbColumnName;
            return data;
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

        /// <summary>
        /// 数据表
        /// </summary>
        public List<DbTableInfo> Tables { get; set; } = new List<DbTableInfo>();

        private List<DbTableInfo> OriginTables = new List<DbTableInfo>();

        public List<string> Dbs { get; set; }
        /// <summary>
        /// 选中的数据库发生变化
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public async Task DbChanged(string configID)
        {
            Tables = (await DatabaseManager.GetTableList(configID)).GetResult();
            OriginTables = Tables;
            SearchInput.ConfigId = configID;
        }

        /// <summary>
        /// 选中表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task TableChanged(string tableName)
        {
            
            SearchInput.TableName = tableName;
            var table = Tables.FirstOrDefault(x => x.Name == tableName);

            _selectTable = table.Adapt<DbTableInfo>();
            await _table.QueryClickAsync();
        }

        /// <summary>
        /// 搜索表名
        /// </summary>
        /// <param name="search"></param>
        public void SearchTableChanged(string search)
        {
            var tables = OriginTables.Where(x => x.Name.Contains(search??""));
            Tables = tables.ToList();

        }
        /// <summary>
        /// 数据类型
        /// </summary>
        private List<string> DataTypes = new List<string>
        {
            "varchar2",
            "number",
            "text",
            "varchar",
            "nvarchar",
            "char",
            "nchar",
            "timestamp",
            "datetime",
            "int",
            "smallint",
            "tinyint",
            "bigint",
            "bit",
            "decimal",
            "date",
            "blob",
            "clob",
            "int8",
            "bool"
        };
        private DbTableInfo _selectTable = new DbTableInfo();

        public bool TableAddDialog { get; set; }
        public bool TableEditDialog { get; set; }

        public bool ShowCode { get; set; }

        private bool _autoGrow = false;

        private StringNumber _tab = 1;

        private List<string> _entityResult;
        protected async override Task OnInitializedAsync()
        {
            Dbs = (await DatabaseManager.GetList()).GetResult();
            await base.OnInitializedAsync();
        }

       
        /// <summary>
        /// 保存表
        /// </summary>
        private async Task TableEditSave()
        {
            var table = new UpdateDbTableInput
            {
                Description = _selectTable.Description,
                TableName = _selectTable.Name,
                ConfigId = SearchInput.ConfigId,
                OldTableName = SearchInput.TableName
               
            };
           var result= await DatabaseManager.UpdateTable(table);

            result.ShowMessage(PopupService);
        }

        private async Task DeleteTable()
        {
           var result=await  PopupService.ConfirmAsync("提示", "是否删除表" + SearchInput.TableName);
            if (result)
            {
                var data = new DeleteDbTableInput
                {
                    ConfigId = SearchInput.ConfigId,
                    TableName = SearchInput.TableName
                };
                (await DatabaseManager.DeleteTable(data)).ShowMessage(PopupService);
            }
        }
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        private async Task CreateEntity()
        {
            _autoGrow = false;
            var result = await DatabaseManager.CreateRazor(new CreateEntityInput
            {
                BaseClassName = "EntityBase",
                ConfigId = SearchInput.ConfigId,
                EntityName = SearchInput.TableName,
                TableName = SearchInput.TableName,
                Position="Client.Entity"
                
            });

            if (result.Code == 200)
            {
                if (result.Result.IsNotNullOrEmpty())
                {
                    _entityResult = result.Result;

                    _autoGrow = true;
                    ShowCode = true;
                }
            }

           
            
        }

        private void CloseAction(bool close)
        {
         
                TableAddDialog = false;
            
        }





    }
}
