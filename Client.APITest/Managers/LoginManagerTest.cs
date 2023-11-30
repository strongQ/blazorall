using BlazorShared.Config;
using Client.API.Managers;
using Client.API.Managers.LoginManager;
using GeneralCommon.Interfaces;
using GeneralCommon.Models;
using GeneralCommon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.APITest.Managers
{
    [TestClass]
    public class LoginManagerTest
    {
        private IServiceProvider _serviceProvider;
       public LoginManagerTest()
        {
            _serviceProvider = ConfigServices();
           var user= _serviceProvider.GetService<IApiConfig>();
            user.RemoteApiUrl = "http://localhost:8002";

        }
        public  IServiceProvider ConfigServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IUserConfig, UserConfig>();

            //services.AddSingleton<IApiConfig,apiser>

            //services.AddInjects<IApiManager>();


          

            return services.BuildServiceProvider();
        }
        [TestMethod]
        public async Task TestLogin()
        {
            var logManager= _serviceProvider.GetService<ILoginManager>();
           
           // Assert.IsTrue(result.IsSuccess);
        }
    }
}
