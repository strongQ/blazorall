using GeneralCommon.Extensions;
using GeneralCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.API.Managers
{
    public abstract class BaseApiManager
    {
        private readonly IHttpClientFactory _clientFactory;
        public IApiConfig UserConfig { get; set; }
        public BaseApiManager(IHttpClientFactory httpClientFactory, IApiConfig userConfig) 
        { 
            _clientFactory = httpClientFactory;
            UserConfig = userConfig;   
        }
        public HttpClient CreateHttpClient()
        {
           
           var client= _clientFactory.CreateClient();
            if(!string.IsNullOrEmpty(UserConfig.Token))
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ UserConfig.Token);
            client.BaseAddress = new Uri(UserConfig.RemoteApiUrl);
            return client;
        }
    }

    public abstract class BaseHttpManager
    {
        private readonly IHttpClientFactory _clientFactory;

        private string _url = string.Empty;
        public BaseHttpManager(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
            
        }
        public HttpClient CreateHttpClient()
        {
            var client = _clientFactory.CreateClient();
           
            client.BaseAddress = new Uri(GetBaseUrl());
            return client;
        }

        public abstract string GetBaseUrl();
    }

}
