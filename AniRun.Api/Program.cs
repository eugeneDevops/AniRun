using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AniRun.Application.Extensions;
using AniRun.Persistence.Extensions;
using AniRun.Api;
using AniRun.Application.Services;

var builder = WebApplication.CreateBuilder(args);

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
ServicePointManager.CheckCertificateRevocationList = true;

builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCaching();
builder.Services.AddDefaultCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMvcCore();
builder.Services.AddHttpClient();

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAntDesign();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseFileServer();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseResponseCaching();
app.UseStaticFiles();
app.MapControllers();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();