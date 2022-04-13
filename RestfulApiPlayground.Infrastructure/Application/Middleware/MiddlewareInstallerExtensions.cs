using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulApiPlayground.Infrastructure.Application.Middleware.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware
{
    public static class MiddlewareInstallerExtensions
    {

        public static void AddMiddlewareInstallersFromAssemblyContaining<TMarker>
            (this WebApplication app)
        {
            app.AddMiddlewareInstallersFromAssembliesContaining(typeof(TMarker));
        }

        public static void AddMiddlewareInstallersFromAssembliesContaining
            (this WebApplication app, params Type[] assemblyMarkers)
        {
            Assembly[] assemblies = assemblyMarkers.Select(marker => marker.Assembly).ToArray();

            app.AddMiddlewareInstallersFromAssemblies(assemblies);
        }

        public static void AddMiddlewareInstallersFromAssemblies
            (this WebApplication app, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var concreteTypes = assembly.DefinedTypes
                                             .Where(type =>
                                                !type.IsInterface && !type.IsAbstract);

                var middlewareInstallers = concreteTypes.Where(typeof(IMiddlewareInstaller).IsAssignableFrom)
                                    .Select(Activator.CreateInstance)
                                    .Cast<IMiddlewareInstaller>();

                middlewareInstallers.ToList().ForEach(mid => mid.ConfigureServices(app));
            }
        }

    }
}
