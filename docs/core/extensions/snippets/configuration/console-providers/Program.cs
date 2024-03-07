using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<ExampleSettings>(
    builder.Configuration.GetSection("ExampleSettings"));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var settings = app.Services.GetRequiredService<IOptions<ExampleSettings>>().Value;

logger.LogInformation("{Settings}", settings);

var config = (IConfigurationRoot)app.Services.GetRequiredService<IConfiguration>();

IConfigurationProvider[] providers = [.. config!.Providers];

for (var i = 0; i < providers.Length; ++i)
{
    var provider = providers[i];

    logger.LogInformation("{Provider}", provider);
}

await app.RunAsync();
