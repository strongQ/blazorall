using GeneralCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Config
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
    }
}
