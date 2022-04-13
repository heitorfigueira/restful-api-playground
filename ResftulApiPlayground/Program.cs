using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using HashidsNet;
using ResftulApiPlayground.Service;
using Microsoft.OpenApi.Models;
using RestfulApiPlayground.src.Application.Contracts;
using RestfulApiPlayground.Infrastructure.Application.Middleware;
using Serilog;
using RestfulApiPlayground.Infrastructure.Application.Middleware.Interceptors;
using RestfulApiPlayground.Infrastructure.Application.Middleware.Installers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


IServiceCollection services = builder.Services;

Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("log.txt")
                    .MinimumLevel.Debug()
                    .CreateLogger();

services.AddInstallersFromAssemblyContaining<IInstaller>(builder.Configuration);

services.AddSingleton<IHashids>(_ => new Hashids("configurar"));
services.AddSingleton<IRecipeRepository, RecipeDictionaryRepository>();

var app = builder.Build();

app.AddMiddlewareInstallersFromAssemblyContaining<IMiddlewareInstaller>();
app.AddInterceptorsFromAssemblyContaining<IInterceptor>();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();