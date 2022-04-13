using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware.Installers;

public class SwaggerInstaller : IInstaller, IMiddlewareInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", 
                new OpenApiInfo { 
                    Title = "ResftulApiPlayground", 
                    Version = "v1" 
                });
        });
    }

    public void ConfigureServices(WebApplication app)
    {
        if (app.Environment.IsDevelopment() ||
            app.Environment.IsStaging())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(options => options.SerializeAsV2 = true);
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResftulApiPlayground v1"));
        }
    }
}
