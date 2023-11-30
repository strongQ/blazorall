using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using BlazorShared;
using GeneralCommon.Config;
using BlazorShared.Data.Base;
using ECS.Pages.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml" });
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = (CompressionLevel)4;
});
builder.Services.AddControllers();

builder.Services.AddSingleton(new AppSettings(builder.Environment.IsDevelopment()));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSharedExtensions();
// 添加页面单独实现
builder.Services.AddEcsPageServices();



var api = AppSettings.GetValue("RemoteApiUrl");

var grpc = AppSettings.GetValue("GrpcUrl");
var singleApp = AppSettings.GetValue<bool>("SingleApp");

GlobalVariables.PageAssemblies = new List<string>
{
    "ECS.Pages"
};

GlobalVariables.RemoteApiUrl = api;
GlobalVariables.IsSingleApp = singleApp;
GlobalVariables.GrpcUrl = grpc;


//builder.Services.AddServerService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseStaticFiles();
//app.UseDefaultFiles();
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    RequestPath = new PathString("/pic")
});


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});




app.Run();



