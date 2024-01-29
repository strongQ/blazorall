using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using BlazorShared;
using BlazorShared.Data.Base;
using ECS.Pages;
using XT.Common.Config;
using BlazorSSR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddSingleton(new AppSettings(builder.Environment.IsDevelopment()));

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddSharedExtensions();
// ���ҳ�浥��ʵ��
builder.Services.AddEcsPageServices();



var api = AppSettings.GetValue("RemoteApiUrl");

var grpc = AppSettings.GetValue("GrpcUrl");
var singleApp = AppSettings.GetValue<bool>("SingleApp");




//builder.Services.AddServerService();

var app = builder.Build();

var global=app.Services.GetService<GlobalVariables>();
global.IniPages(new List<string>
{
    "ECS.Pages"
});

global.RemoteApiUrl = api;
global.IsSingleApp = singleApp;
global.GrpcUrl = grpc;






app.UseStaticFiles();
//app.UseDefaultFiles();



app.UseAntiforgery();

app.MapRazorComponents<BlazorSSR.App>()
    .AddInteractiveServerRenderMode();



app.Run();



