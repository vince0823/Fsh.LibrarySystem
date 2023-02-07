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
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

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

    builder.Services.AddControllers().AddFluentValidation().ConfigureApiBehaviorOptions(options =>
    {
        // ʹ���Զ���ģ����֤
        options.InvalidModelStateResponseFactory = (context) =>
        {
            var result = new ApiResult()
            {
                Code = (int)HttpStatusCode.Conflict,
                Message = string.Join(",", context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)))
            };
            return new JsonResult(result);
        };
    }).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
    });

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddMvc(options =>
    {
        // ���뷵�ؽ������
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