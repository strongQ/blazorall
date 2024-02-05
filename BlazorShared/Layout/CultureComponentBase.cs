using BlazorComponent.I18n;
using BlazorXT.Core;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Layout
{
    public class CultureComponentBase : BaseComponentBase
    {
        [CascadingParameter]
        public CultureInfo Culture { get; set; }

       

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        public I18n LanguageService { get; set; }

        public string ScopeT(string scope, string key, params object[] args)
        {
            return string.Format(LanguageService.T(scope, key, true), args);
        }
      

        public string T(string key, params object[] args)
        {
            if (key.IsNullOrEmpty())
            {
                return "";
            }
            return string.Format(LanguageService.T(key, false, key), args);
        }
        /// <summary>
        /// 获取当前Url
        /// </summary>
        /// <returns></returns>
        public string GetPageUrl()
        {
            return NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/");
        }
    }
}
