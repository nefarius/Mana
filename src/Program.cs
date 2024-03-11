/*
using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
*/

using System.Net.Http.Headers;
using System.Text;

using Mana.Models;
using Mana.Models.Refit;

using Microsoft.AspNetCore.Hosting.StaticWebAssets;

using MudBlazor.Services;

using Nefarius.Utilities.AspNetCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Refit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args).Setup( /*options =>
{
    options.Configuration.AddEnvironmentVariables();

    IConfigurationSection section = options.Configuration.GetSection("Mana");
    ManaConfiguration cfg = section.Get<ManaConfiguration>();

    if (cfg.Logging.LogToZinc)
    {
        options.Serilog.Configuration.WriteTo.Elasticsearch(new[] { cfg.Logging.NodeUrl }, opts =>
        {
            opts.DataStream = new DataStreamName("logs", "Mana", "Mana");
            opts.BootstrapMethod = BootstrapMethod.Failure;
            opts.ConfigureChannel = channelOpts =>
            {
                channelOpts.BufferOptions = new BufferOptions { ExportMaxConcurrency = 10 };
            };
        });
    }
}*/);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddOptions<ManaConfiguration>().Bind(builder.Configuration.GetSection("Mana"));

builder.Services.AddRefitClient<IZincSearchApi>(new RefitSettings(
        new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        })))
    .ConfigureHttpClient(client =>
    {
        ManaConfiguration appConfig = builder.Configuration.GetSection("Mana").Get<ManaConfiguration>();

        client.BaseAddress = new Uri(appConfig.Elastic.ServerUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{appConfig.Elastic.Username}:{appConfig.Elastic.Password}")));
    });

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