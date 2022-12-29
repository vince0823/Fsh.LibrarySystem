using FluentValidation.AspNetCore;
using FSH.Learn.Application;
using FSH.Learn.Application.Common.Interfaces;
using FSH.Learn.Host.Configurations;
using FSH.Learn.Host.Controllers;
using FSH.Learn.Infrastructure;
using FSH.Learn.Infrastructure.BackgroundJobs;
using FSH.Learn.Infrastructure.Common;
using FSH.Learn.Infrastructure.Filter;
using Hangfire;
using Serilog;

[assembly: ApiConventionType(typeof(FSHApiConventions))]

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddConfigurations();
    builder.Host.UseSerilog((_, config) =>
    {
        config.WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration);
    });

    builder.Services.AddControllers().AddFluentValidation();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddMvc(options =>
    {
        // 加入返回结果处理
        options.Filters.Add<ApiResultFilter>();
    });

    var app = builder.Build();

    await app.Services.InitializeDatabasesAsync();

    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();
    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}