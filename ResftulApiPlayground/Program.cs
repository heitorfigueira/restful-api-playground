using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using HashidsNet;
using ResftulApiPlayground.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddSingleton<IHashids>(_ => new Hashids("configurar"));

services.AddSingleton<IRecipeRepository, RecipeDictionaryRepository>();

services.AddRouting(options => options.LowercaseUrls = true);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResftulApiPlayground", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(options => options.SerializeAsV2 = true);
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResftulApiPlayground v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();