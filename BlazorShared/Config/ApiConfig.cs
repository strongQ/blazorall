using XT.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Config
{
    public class ApiConfig : IApiConfig
    {
        public string RemoteApiUrl { get; set; }
        public string OtherUrl { get; set; }
        public string Token { get; set; }

        public event EventHandler AuthorizationFailedEvent;

        public void AuthorizationFailedInvoke()
        {
            AuthorizationFailedEvent?.Invoke(null, null);
        }

        public HttpClient CreateHttpClient()
        {
            throw new NotImplementedException();
        }
    }
}
