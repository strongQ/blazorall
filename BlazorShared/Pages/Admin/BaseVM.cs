using BlazorComponent;
using Masa.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Pages.Admin
{
    public abstract class BaseVM
    {
        public BaseVM(IPopupService service)
        {
            PopupService = service;
        }
        public event EventHandler RefreshEvent;

        /// <summary>
        /// 弹窗服务
        /// </summary>
        public IPopupService PopupService { get; set; }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public abstract Task InitData();

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
    }
}
