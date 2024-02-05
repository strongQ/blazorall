using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

using XT.Common.Config;
using BlazorSSR;
using BlazorXT;
using BlazorXT.Data.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddSingleton(new AppSettings(builder.Environment.IsDevelopment()));

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddSharedExtensions();
// 添加页面单独实现
//builder.Services.AddEcsPageServices();



var api = AppSettings.GetValue("RemoteApiUrl");

var grpc = AppSettings.GetValue("GrpcUrl");
var singleApp = AppSettings.GetValue<bool>("SingleApp");






var app = builder.Build();

var global=app.Services.GetService<GlobalVariables>();
global.IniPages(new List<string>
{
    "Admin.Pages",
    "ECS.Pages"
});

GlobalVariables.Url = "http://localhost";

global.RemoteApiUrl = api;
global.IsSingleApp = singleApp;
global.GrpcUrl = grpc;






app.UseStaticFiles();
//app.UseDefaultFiles();



app.UseAntiforgery();

app.MapRazorComponents<BlazorSSR.App>()
    .AddInteractiveServerRenderMode();



app.Run();



