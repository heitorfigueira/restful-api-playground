using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RestfulApiPlayground.Infrastructure.Presentation.Errors;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware.Interceptors;

public class DepthLoggingInterceptor : IInterceptor
{
    private readonly RequestDelegate _next;

    public DepthLoggingInterceptor()
    {

    }

    public DepthLoggingInterceptor(RequestDelegate next)
    {
        _next = next;
    }

    public IApplicationBuilder ConfigureInterceptor(IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DepthLoggingInterceptor>();
    }

    public async Task Invoke(HttpContext context)
    {

        try
        {
            HttpRequest request = context.Request;

            Log.Logger.Debug("Método {Method}, Query {Query} e Path {Path}",
                request.Method,
                request.QueryString,
                request.Path.ToString());


            await _next(context);

            HttpResponse response = context.Response;

            Log.Logger.Debug("Status Code {StatusCode} e Body {Body}",
                response.StatusCode,
                response.Body);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex.Message);
        }

    }
}
