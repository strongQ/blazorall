using Masa.Blazor;
using Masa.Blazor.Presets;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Extensions;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using BlazorComponent;
using XT.Common.Dtos.Admin;
using BlazorXT.Data.Base;
using Mapster;
using System.Reflection;
using MiniExcelLibs;
using Microsoft.JSInterop;
using BlazorXT.Helper;
using Microsoft.AspNetCore.Components.Forms;
using System.Data;
using System.Runtime.CompilerServices;

namespace BlazorXT.Components.DataTable
{
    public partial class AppDataTable<TItem, SearchItem, AddItem, EditItem> : IAppDataTable
        where TItem : BaseIdInput, new()
        where SearchItem : BasePageInput, new()
        where AddItem : BaseRender, new()
        where EditItem : BaseRender, new()
    {
        private MDataTable<TItem> _table;

        private List<PropertyInfo> Props = typeof(EditItem).GetAllProps().ToList();

        private Dictionary<string, string> DetailModelPairs = new Dictionary<string, string>();

        [Parameter]
        public Func<string, TItem, bool> SearchItemFilter { get; set; }
       

        private void SearchChanged(string value)
        {
            SearchModel.SearchKey= value;
            if(_items==null || _items.Count == 0)
            {
                if(Items!=null && Items.Count > 0)
                {
                    _items = new List<TItem>(Items);
                }
                else
                {

                    return;
                }
             
            }

            if (value.IsNullOrEmpty())
            {
                Items = new List<TItem>(_items);
                return;
            }
            List<TItem> list = new List<TItem>();
            foreach(var item in _items)
            {
                if (SearchItemFilter == null)
                {
                    list.Add(item);
                }
                else
                {
                    if (SearchItemFilter.Invoke(value, item))
                    {
                        list.Add(item);
                    }
                }
            }
            Items = list;
        }

        [Parameter]
        public Func<AddItem, Task> AddCall { get; set; }


        /// <summary>
        /// 获得/设置 添加模板
        /// </summary>
        [Parameter]
        public RenderFragment<AddItem> AddTemplate { get; set; }

        [Parameter]
        public string ClassString { get; set; }
        /// <summary>
        /// 导入模板位置
        /// </summary>
        [Parameter]
        public string ImportUrl { get; set; }

        [Parameter]
        public Func<IEnumerable<TItem>, Task> DeleteCall { get; set; }

        [Parameter]
        public bool Dense { get; set; }
        /// <summary>
        /// 在服务端排序
        /// </summary>
        [Parameter]
        public bool SortServer { get; set; }
        /// <summary>
        /// 获得/设置 明细行模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem> DetailRowTemplate { get; set; }

        /// <summary>
        /// 编辑后调用
        /// </summary>
        [Parameter]
        public Func<EditItem, Task> EditCall { get; set; }
        /// <summary>
        /// 导出前调用
        /// </summary>
        [Parameter]
        public Func<Task<IEnumerable<TItem>>> ExportCall { get; set; }
        /// <summary>
        /// 导入前调用
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, Task> ImportCall { get; set; }
        /// <summary>
        /// 编辑显示时调用
        /// </summary>
        [Parameter]
        public Func<EditItem,Task<EditItem>> EditShowCall { get; set; }


        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        [Parameter]
        public RenderFragment<EditItem> EditTemplate { get; set; }

        /// <summary>
        /// 表头过滤，返回DataTableHeader列表，传输参数已包含全部初始表头与表头标题
        /// </summary>
        [Parameter]
        public Action<List<DataTableHeader<TItem>>> FilterHeaders { get; set; }

        
        /// <summary>
        /// 获得/设置 Table Header 模板
        /// </summary>
        [Parameter]
        public RenderFragment<DataTableHeader> HeaderTemplate { get; set; }

        [Parameter]
        public bool IsMenuOperTemplate { get; set; } = true;

        [Parameter]
        public bool IsPage { get; set; } = true;

        [Parameter]
        public bool IsShowOperCol { get; set; } = true;

        [Parameter]
        public bool IsShowSearchKey { get; set; } = false;
        [Parameter]
        public bool IsShowClearSearch { get; set; } = true;
        [Parameter]
        public bool IsShowToolbar { get; set; } = true;

        /// <summary>
        /// 获得/设置 Table Oper 模板
        /// </summary>
        [Parameter]
        public RenderFragment<ItemColProps<TItem>> ItemColOperTemplate { get; set; }

        /// <summary>
        /// 获得/设置 Table Cols 模板
        /// </summary>
        [Parameter]
        public RenderFragment<ItemColProps<TItem>> ItemColTemplate { get; set; }

        /// <summary>
        /// 新增模板列设置
        /// </summary>
        [Parameter]
        public RenderFragment<(AddItem, string)> AddPropertyTemplate { get; set; }
        /// <summary>
        /// 编辑模板列设置
        /// </summary>
        [Parameter]
        public RenderFragment<(EditItem, string)> EditPropertyTemplate { get; set; }

        [Parameter]
        public List<TItem> Items { get; set; } = new List<TItem>();

        private List<TItem> _items = new List<TItem>();

        /// <summary>
        /// 获得/设置 其他操作栏模板
        /// </summary>
        [Parameter]
        public RenderFragment<IEnumerable<TItem>> OtherToolbarTemplate { get; set; }




        [Parameter]
        public List<PageSize> PageSizeItems { get; set; } = new List<PageSize>()
        {
            new PageSize(){Key="10",Value=10},
            new PageSize(){Key="20",Value=20},
            new PageSize(){Key="50",Value=50},
            new PageSize(){Key="100",Value=100}
         };

        [Parameter]
        public Func<SearchItem, Task<SqlSugarPagedList<TItem>>> QueryCall { get; set; }

        [Parameter]
        public Func<SearchItem, Task<List<TItem>>> QueryListCall { get; set; }

        /// <summary>
        /// 获得/设置 SearchModel 实例
        /// </summary>
        [Parameter]
        public SearchItem SearchModel
        {
            get;
            set;
        }

        /// <summary>
        /// 获得/设置 查询与操作栏模板
        /// </summary>
        [Parameter]
        public RenderFragment<SearchItem> SearchTemplate { get; set; }


      

        [Parameter]
        public bool ShowAddButton { get; set; }
        [Parameter]
        public bool ShowImportButton { get; set; }
        [Parameter]
        public bool ShowExportButton { get; set; }

        [Parameter]
        public bool ShowDeleteButton { get; set; }

        [Parameter]
        public bool ShowDetailButton { get; set; }

        [Parameter]
        public bool ShowEditButton { get; set; }

        [Parameter]
        public bool ShowFilter { get; set; } = true;

        [Parameter]
        public bool ShowQueryButton { get; set; }

        [Parameter]
        public bool ShowSelect { get; set; } = true;

        private AddItem AddModel { get; set; }
        public bool AddShow { get; set; }
        private bool DeleteLoading { get; set; }
        private TItem DetailModel { get; set; }
        private bool DetailShow { get; set; }
        private EditItem EditModel { get; set; }

        private bool EditShow { get; set; }

        /// <summary>
        /// 过滤实体列，
        /// </summary>
        private List<Filters> FilterHeaderString { get; set; } = new();

        /// <summary>
        /// 显示的所有属性
        /// </summary>
        private Dictionary<string, string> ShowProps = new Dictionary<string, string>();
        /// <summary>
        /// 所有属性
        /// </summary>
        private Dictionary<string,PropertyInfo> ShowProperties= new Dictionary<string,PropertyInfo>();

        private int FirstRender { get; set; }
        private List<DataTableHeader<TItem>> headers { get; set; } = new();

        private List<DataTableHeader<TItem>> Headers { get; set; } = new();

        private int Page { get; set; }

        private SqlSugarPagedList<TItem> PageItems { get; set; } = new();
        private bool QueryLoading { get; set; }

        private MForm SearchForm { get; set; }

        private IEnumerable<TItem> selectedItem { get; set; } = new List<TItem>();



        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        public async Task QueryClickAsync()
        {
            try
            {
                if(QueryCall==null && QueryListCall == null)
                {
                    return;
                }
                QueryLoading = true;
                StateHasChanged();

                if (QueryListCall != null)
                {
                    var totalItems = await QueryListCall.Invoke(SearchModel);
                    PageItems = new SqlSugarPagedList<TItem>
                    {
                        Items = totalItems.GetPage(SearchModel.Page, SearchModel.PageSize),
                        Total = totalItems.Count,
                        TotalPages = (totalItems.Count+SearchModel.PageSize-1)/SearchModel.PageSize
                    };
                }
                else
                {
                  
                    PageItems = await QueryCall.Invoke(SearchModel);
                }
                Items = PageItems.Items.ToList();


                _items = new List<TItem>(Items);
               
                if (!IsPage)
                {
                    SearchModel.PageSize = PageItems.Total;
                }
            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync(ex, false);
            }
            finally
            {
                QueryLoading = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                if (Page != SearchModel.Page)
                {
                    Page = SearchModel.Page;
                    await QueryClickAsync();
                }
            }
            else
            {
                Page = SearchModel.Page;
                
                if (Items.Count() <= 0)
                    await QueryClickAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ShowProps = new Dictionary<string, string>();
            if (SearchModel == null)
                SearchModel = new();

            var props = typeof(TItem).GetProperties();
            var data = typeof(TItem).GetAllPropsName();
            Headers.Add(new DataTableHeader<TItem>
            {
                Text = "序号",
                Value="Index",
                
            });
            foreach (var item in data)
            {
                var des = typeof(TItem).GetDescription(item,true);
                if (des.IsNullOrEmpty())
                {
                    des = typeof(TItem).GetDescription(item);
                    if (des.IsNotNullOrEmpty())
                    {
                        ShowProperties.Add(item, props.FirstOrDefault(x => x.Name == item));
                        ShowProps.Add(item,des);
                    }
                   
                    continue;
                }

                ShowProperties.Add(item, props.FirstOrDefault(x => x.Name == item));
                ShowProps.Add(item, des);
                Headers.Add(new()
                { Text =des , Value = item });
            }
            if (IsShowOperCol)
            {
                Headers.Add(new()
                { Text = "操作", Value = GlobalVariables.TB_Actions, Width = 200 });
            }

            if (FilterHeaders != null)
            {
                FilterHeaders.Invoke(Headers);
            }

            foreach (var item in Headers)
            {
                   
                if(item.Value == GlobalVariables.TB_Actions)
                {
                    item.Sortable = false;
                }
                var filter = new Filters()
                { Title = item.Text, Key = item.Value, Value = true, };
                FilterHeaderString.Add(filter);
            }
          

            var action = Headers.FirstOrDefault(a => a.Value == GlobalVariables.TB_Actions);
            if (action != null)
                action.Width = 70 * ((ShowEditButton ? 1 : 0) + (ShowDetailButton ? 1 : 0) + (ShowDeleteButton ? 1 : 0) + (ItemColOperTemplate != null ? 1 : 0));

            FilterChanged();
        }

        private void AddClick()
        {
            AddModel = new();
            AddShow = true;
        }

        private void AddOnCancel()
        {
            AddShow = false;
        }

        private async Task AddOnSave(ModalActionEventArgs args)
        {
            try
            {
                await AddCall(AddModel);
                await QueryClickAsync();
                AddShow = false;
            }
            catch (Exception ex)
            {
                args.Cancel();
                await PopupService.EnqueueSnackbarAsync(ex, false);
            }
        }

        private void ClearClick()
        {
            SearchForm.Reset();
        }

        private async Task DeleteClick(params TItem[] _selectedItem)
        {
            DeleteLoading = true;
            StateHasChanged();
            try
            {
                if (_selectedItem.Count() <= 0)
                {
                    await PopupService.EnqueueSnackbarAsync("选择一行后才能进行操作");
                }
                else
                {
                    if (DeleteCall != null)
                    {
                        var confirm = await OpenConfirmDialog("删除", "确定 ?");
                        if (confirm)
                        {
                            await DeleteCall(_selectedItem);
                            await QueryClickAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync(ex, false);
            }
            finally
            {
                DeleteLoading = false;
                StateHasChanged();
            }
        }

        private async Task DetailClick(params TItem[] _selectedItem)
        {
            
            if (_selectedItem.Count() > 1)
            {
                await PopupService.EnqueueSnackbarAsync("只能选择一行");
            }
            else if (_selectedItem.Count() == 1)
            {
                DetailModel = _selectedItem.FirstOrDefault();
                var strs = typeof(TItem).GetAllPropsName();
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                foreach (var item in strs)
                {
                    if (item != GlobalVariables.TB_Actions)
                    {
                        string result = string.Empty;
                        object value = typeof(TItem).GetPropValue(DetailModel, item);
                        if (value is System.DateTime)
                        {
                            result = ((System.DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                       else if(value is Enum)
                        {
                            result = ((Enum)value).ToDescription();
                        }
                        else
                        {
                            result = value?.ToString();
                        }
                        keyValuePairs.Add(item, result);
                    }
                }
                DetailModelPairs = keyValuePairs;
                DetailShow = true;
            }
            else
            {
                await PopupService.EnqueueSnackbarAsync("选择一行后才能进行操作");
            }
        }
        /// <summary>
        /// 点击导出
        /// </summary>
        /// <param name="_selectedItem"></param>
        /// <returns></returns>
        private async Task ExportClick(params TItem[] _selectedItem)
        {
            
            List<Dictionary<string, object>> dics = new List<Dictionary<string, object>>();
            List<TItem> datas = new List<TItem>();

            using MemoryStream stream = new MemoryStream();


            if (ExportCall != null)
            {
                datas = (await ExportCall.Invoke()).ToList();
            }
            else
            {
                if (_selectedItem.Length == 0 && Items.Count > 0)
                {
                    datas = Items;

                }
                else if (_selectedItem.Length > 0)
                {
                    datas = _selectedItem.ToList();
                }
                else
                {
                    //新增一条提供模板
                    datas.Add(new TItem());
                }
            }

            foreach(var data in datas)
            {
                Dictionary<string, object> single = new Dictionary<string, object>();
               foreach(var prop in ShowProps.Keys)
                {

                    object value = ShowProperties[prop].GetValue(data);
                    single.Add(ShowProps[prop], value);
                }
               dics.Add(single);
            }

            MiniExcel.SaveAs(stream, dics);
            var base64= Convert.ToBase64String(stream.ToArray());

            var type = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,";

            await JSRuntime.InvokeVoidAsync("downloadFileByBase64", type+base64, "data.xlsx");

        }
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="browserFile"></param>
        /// <returns></returns>
        public async Task UploadFile(IBrowserFile browserFile)
        {
           
            if (browserFile == null)
            {
                return;
            }

            QueryLoading = true;

            try
            {

                MemoryStream ms = new MemoryStream();
                await browserFile.OpenReadStream(1024 * 1024 * 10).CopyToAsync(ms);


                var datas = await MiniExcel.QueryAsync<TItem>(ms, null, ExcelType.XLSX);

                if(ImportCall != null)
                {
                    await ImportCall.Invoke(datas);
                }
                else
                Items = datas.ToList();

            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync(ex);
            }

            QueryLoading = false;
        }
        /// <summary>
        /// 下载模板
        /// </summary>
        /// <returns></returns>
        public async Task DownloadTemplate()
        {
            if (ImportUrl.IsNotNullOrEmpty())
            {            
               var name=  ImportUrl.Split('/').LastOrDefault();

                if (name.IsNotNullOrEmpty())
                {
                    await JSRuntime.InvokeVoidAsync("triggerFileDownload", name, ImportUrl);
                }
              
            }
            else
            {
                List<TItem> datas = new List<TItem>();

                using MemoryStream stream = new MemoryStream();
                datas.Add(new TItem() { Id=1});
                MiniExcel.SaveAs(stream, datas);
                var base64 = Convert.ToBase64String(stream.ToArray());

                var type = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,";

                await JSRuntime.InvokeVoidAsync("downloadFileByBase64", type + base64, "data.xlsx");
            }
        }


        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private async Task EditClick(params TItem[] _selectedItem)
        {
            if (_selectedItem.Count() > 1)
            {
                await PopupService.EnqueueSnackbarAsync("只能选择一行");
            }
            else if (_selectedItem.Count() == 1)
            {
                var item = _selectedItem.FirstOrDefault().Adapt<EditItem>();

                if (EditShowCall == null)
                {
                    EditModel = item;
                }
                else
                {
                    EditModel = await EditShowCall?.Invoke(item);
                }
                           
                EditShow = true;

              
            }
            else
            {
                await PopupService.EnqueueSnackbarAsync("选择一行后才能进行操作");
            }
        }

        private void EditOnCancel()
        {
            
            EditShow = false;
        }

        private async Task EditOnSave(ModalActionEventArgs args)
        {
            try
            {
                await EditCall?.Invoke(EditModel);
                await QueryClickAsync();
                EditShow = false;
            }
            catch (Exception ex)
            {
                args.Cancel();
                await PopupService.EnqueueSnackbarAsync(ex, false);
            }
        }
        private async Task InputEnter(int a)
        {
            if (ShowQueryButton)
            {
                SearchModel.Page = a;
                await QueryClickAsync();
            }
        }
        private async Task Enter(KeyboardEventArgs e)
        {
            if (ShowQueryButton)
                if (e.Code == "Enter" || e.Code == "NumpadEnter")
                {
                    await QueryClickAsync();
                }
        }

        private void FilterChanged()
        {
            headers = Headers.Where(it => FilterHeaderString.Any(a => a.Key == it.Value && a.Value == true)).ToList();
        }

        private async Task HandleOnOptionsUpdate(DataOptions dataOptions)
        {
            if (SortServer)
            {
                SearchModel.Field = dataOptions.SortBy.FirstOrDefault();
                SearchModel.Order = dataOptions.SortDesc.FirstOrDefault() ? "desc" : "asc";
                await QueryClickAsync();
            }
           
        }

        private async Task<bool> OpenConfirmDialog(string title, string content)
        {
            return await PopupService.ConfirmAsync(title, content, AlertTypes.Error);
        }

        private async Task<bool> OpenConfirmDialog(string title, string content, AlertTypes type)
        {
            return await PopupService.ConfirmAsync(title, content, type);
        }
        private int size { get; set; }
        private async Task PageChanged(int val)
        {
            if (((float)PageItems.Total / SearchModel.PageSize) < SearchModel.Page)
            {
                SearchModel.Page = (int)(PageItems.Total / SearchModel.PageSize) + 1;
            }
            if (SearchModel.Page <= 0)
            {
                SearchModel.Page = 1;
            }
            if (FirstRender >= 1 && size != SearchModel.PageSize)
            {
                size = SearchModel.PageSize;
                await QueryClickAsync();
            }

            FirstRender += 1;
            size = SearchModel.PageSize;
        }
    }
}