using XT.Common.Models.Server;
using XT.Common.Services;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Extensions;

namespace BlazorShared.Services
{
    public partial class HttpService
    {
       
        ILogService _log;
        IJSRuntime _jSRuntime;
        public HttpService(HttpClient http,

            ILogService log,
            IJSRuntime jsRuntime)
        {
            HttpClient = http;
            _log = log;
            
           
            _jSRuntime = jsRuntime;
        }

        public HttpClient HttpClient { get; }
        public async Task<ReturnModel<T>> GetJson<T>(string address)
        {
            var result = await HttpRequest<T, T>(address, "get", default);
            return result;
        }

        public async Task<ReturnModel<object>> GetJson(string address)
        {
            var result = await HttpRequest<object, object>(address, "get", default);
            return result;
        }

        public async Task<ReturnModel<T>> PostJson<K, T>(string address, K data)
        {
            var result = await HttpRequest<K, T>(address, "post", data);
            return result;
        }

        public async Task<ReturnModel<object>> PostJson<K>(string address, K data)
        {
            var result = await HttpRequest<K, object>(address, "post", data);
            return result;
        }

        async Task<ReturnModel<T>> HttpRequest<K, T>(string address, string model, K data)
        {
            ReturnModel<T> result;
            try
            {
                HttpResponseMessage responseMessage;
                if (model.ToLower() == "get")
                {
                    responseMessage = await HttpClient.GetAsync(address);
                }
                else if (model.ToLower() == "post")
                {
                    responseMessage = await HttpClient.PostAsJsonAsync(address, data);
                }
                else
                {
                    throw new Exception("暂不支持的请求方法");
                }
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    //当这里解析失败，又找不到具体原因，试试使用Newtonsoft.json进行解析
                    var resultMsg = await responseMessage.Content.ReadAsStringAsync();
                    result = resultMsg.ToObject<ReturnModel<T>>();
                }
                else
                {
                    result = new ReturnModel<T>()
                    {
                        Msg = "请求失败:" + responseMessage.ReasonPhrase,
                       // Status = responseMessage.StatusCode,
                    };
                }
            }
            catch (Exception e)
            {
                result = new ReturnModel<T>()
                {
                    Msg = "请求失败，发生请求错误",
                    //Status = HttpStatusCode.BadRequest,
                    //Detail = new System.Collections.Generic.Dictionary<string, string>()
                    //{
                    //    { "请求错误信息", e.Message}
                    //}
                };
            }
          //  Response(result);
            return result;
        }

        //public void Response<T>(ReturnModel<T> result)
        //{
        //    switch (result.Status)
        //    {
        //        case HttpStatusCode.OK://正确响应
        //            break;
        //        case HttpStatusCode.Redirect://重定向
        //            _log.Log(result.Title);
        //          //  _navigationManager.NavigateTo(result.Url);
        //            break;
        //        case HttpStatusCode.RedirectMethod://强制重定向
        //             _log.Log(result.Title);
        //           // _navigationManager.NavigateTo(_jSRuntime, result.Url, true);
        //            break;
        //        case HttpStatusCode.Unauthorized://权限不足
        //        case HttpStatusCode.InternalServerError://发生严重错误
        //        default:
        //            string msg = "";
        //            if (result.Detail != null && result.Detail.Count > 0)
        //            {
        //                foreach (var item in result.Detail)
        //                {
        //                    msg += $"{item.Key}：{item.Value}<br>";
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(result.Url))
        //            {
        //                msg += $"<a target='_Blank' href='{result.Url}'>点击查看解决办法</a><br>";
        //            }
        //            Console.WriteLine(msg);
        //            _ = _notificationService.Open(new NotificationConfig()
        //            {
        //                Message = result.Title,
        //                Description = msg,
        //                NotificationType = NotificationType.Error
        //            });
        //            break;
        //    }
        //}


    }
}
