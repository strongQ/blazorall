using BlazorXT.Components.DataTable;
using Client.API.Managers.File;
using XT.Common.Dtos.Admin.File;
using XT.Common.Dtos.Admin;
using XT.Common.Interfaces;
using Microsoft.AspNetCore.Components;
using XT.Common.Extensions;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorXT.Helper;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.Metadata;
using System.Net.Http.Headers;
using Client.API.Managers.MenuManager;
using XT.Common.Dtos.Admin.Menu;

namespace Admin.Pages.Pages.Admin.File
{
    public partial class FileListPage
    {
        public FileListPage()
        {

        }
        #region 通用Table代码
        private AppDataTable<AddFileInput, PageFileInput, AddFileInput, AddFileInput> _table;

        [Inject]
        public IUserConfig UserConfig { get; set; }
        [Inject]
        public IFileManager FileManager { get; set; }



        /// <summary>
        /// 查询
        /// </summary>
        public PageFileInput SearchInput { get; set; } = new PageFileInput();


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private async Task DeleteCall(IEnumerable<AddFileInput> files)
        {
            var result = await FileManager.DeleteFile(new DeleteFileInput
            {
                Id = files.FirstOrDefault().Id
            });
            result.ShowMessage(PopupService);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<SqlSugarPagedList<AddFileInput>> QueryCall(PageFileInput input)
        {
            var result = await FileManager.GetPage(input);
            return result.GetResult();
        }



        /// <summary>
        /// 快速搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SearchFilter(string search, AddFileInput item)
        {
            if (search.IsNotNullOrEmpty() && item.FileName != null)
            {
                return item.FileName.Contains(search);
            }

            return false;

        }


        #endregion
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private async Task Download(AddFileInput file)
        {
            await JSRuntime.InvokeVoidAsync("downloadFile", file.Url,file.FileName);
        }

      

        private bool loading = false;
        private async Task UploadFiles(List<IBrowserFile> files)
        {
            if(files.Count == 0)
            {
                return;
            }
            loading = true;
            try
            {
                using var content = new MultipartFormDataContent();
                
                foreach (var file in files)
                {
                    var fileContent =
                           new StreamContent(file.OpenReadStream(1024 * 1024*50));

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(file.ContentType);

                    content.Add(
                        content: fileContent,
                        name: "\"files\"",
                        fileName: file.Name);
                }
               var result= await FileManager.UploadFiles(content);
                result.ShowMessage(PopupService);
               await _table.QueryClickAsync();
            }
            catch(Exception ex)
            {
               await PopupService.EnqueueSnackbarAsync(new Masa.Blazor.Presets.SnackbarOptions
               {
                   Title = ex.Message,
                   Type=BlazorComponent.AlertTypes.Error
               });
            }

           

            loading = false;
        }
    }
}
