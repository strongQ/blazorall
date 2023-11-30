using BlazorComponent;
using Client.API.Managers.RoleManager;
using GeneralCommon.Dtos.Admin.Menu;
using GeneralCommon.Dtos.Admin.Role;
using GeneralCommon.Enums;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Pages.Admin
{
    public abstract class BaseListPageVM<T> where T : class
    {
        public BaseListPageVM(IPopupService service) 
        {
            PopupService= service;
        }
        public event EventHandler RefreshEvent;
        /// <summary>
        /// 每页数目
        /// </summary>
        public List<int> PageSizes { get; set; } = new() { 10, 20, 50, 100 };
        /// <summary>
        /// 列头
        /// </summary>
        public  List<DataTableHeader<T>> Headers { get; set; }

        /// <summary>
        /// 弹窗服务
        /// </summary>
        public IPopupService PopupService { get; set; }

        /// <summary>
        /// 按钮权限
        /// </summary>
        public List<string> Buttons { get; set; }

        /// <summary>
        /// 动态查询
        /// </summary>
        public string? Search { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 加载
        /// </summary>
        public bool Loading { get; set; } = false;
        /// <summary>
        /// 是否是详情
        /// </summary>
        public bool IsDetail { get; set; } = false;

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Datas { get; set; } = new List<T>();

        /// <summary>
        /// 选中的数据
        /// </summary>
        public T SelectData { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool ShowEdit { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string EditTitle { get; set; } = "新增";

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="value"></param>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool FilterOnlyCapsText(object value, string search, T item);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public abstract  Task EditSave();

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual void EditClose()
        {
            ShowEdit = false;
        }

        /// <summary>
        /// 显示编辑框
        /// </summary>
        /// <param name="role"></param>
        public abstract Task EditShow(T role, CrudEnum type);

        /// <summary>
        /// 复位
        /// </summary>
        public abstract void SearchReset();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task Delete(long id);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public abstract  Task GetPageDatas(bool init=false);

      
       

        #region 验证
        private Func<string, StringBoolean> RequiredRule = value => !string.IsNullOrEmpty(value) ? true : "Required.";
        /// <summary>
        /// 验证器
        /// </summary>
        public IEnumerable<Func<string, StringBoolean>> RequireRules => new List<Func<string, StringBoolean>>
        {
            RequiredRule
        };

        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public abstract Task InitData();




    }
}
