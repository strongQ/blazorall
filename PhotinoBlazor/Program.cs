﻿using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using XT.Common.Config;
using ECS.Pages;
using BlazorXT;
using BlazorXT.Services;
using BlazorXT.Data.Base;

internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

     

        // register root component and selector
        appBuilder.RootComponents.Add<BlazorXT.App>("app");

        appBuilder.Services.AddSingleton(new AppSettings(false));

        appBuilder.Services.AddAuthorizationCore();
        appBuilder.Services.AddOptions();

        appBuilder.Services.AddSharedExtensions();
        // 添加页面单独实现
        appBuilder.Services.AddEcsPageServices();

        var app = appBuilder.Build();

        // customize window
        app.MainWindow
            //需要有favicon.ico
            .SetIconFile("Server.ico")
            .SetTitle("ECS大屏");

       
        var api = AppSettings.GetValue("RemoteApiUrl");

        var grpc = AppSettings.GetValue("GrpcUrl");
        var singleApp = AppSettings.GetValue<bool>("SingleApp");
        var global = app.Services.GetService<GlobalVariables>();
        global.IniPages(new List<string>
{
    "ECS.Pages",
    "Admin.Pages"
});
        GlobalVariables.Url = "http://localhost";

        global.RemoteApiUrl = api;
        global.IsSingleApp = singleApp;
        global.GrpcUrl = grpc;

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();

    }
}
