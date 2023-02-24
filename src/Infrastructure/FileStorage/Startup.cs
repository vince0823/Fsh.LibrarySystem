using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace FSH.Learn.Infrastructure.FileStorage;

internal static class Startup
{
    internal static IApplicationBuilder UseFileStorage(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            RequestPath = new PathString("/Files")
        });
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files/Templates")),
            RequestPath = new PathString("/Templates")
        });

        return app;
    }

}