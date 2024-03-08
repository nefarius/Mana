using Mana.Models;

using Microsoft.AspNetCore.Hosting.StaticWebAssets;

using MudBlazor.Services;

using Nefarius.Utilities.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args).Setup();

builder.Configuration.AddEnvironmentVariables();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddOptions<ManaConfiguration>().Bind(builder.Configuration.GetSection("Mana"));

builder.Services.AddMemoryCache();

WebApplication app = builder.Build().Setup();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
    //app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();