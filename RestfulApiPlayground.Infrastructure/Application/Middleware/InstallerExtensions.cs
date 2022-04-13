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
    public static class InstallerExtensions
    {
        public static void AddInstallers(this IServiceCollection services, IConfiguration config)
        {
            services.AddInstallersFromAssemblies(config, Assembly.GetExecutingAssembly());
        }

        public static void AddInstallersFromAssemblyContaining<TMarker>
            (this IServiceCollection services, IConfiguration config)
        {
            services.AddInstallersFromAssembliesContaining(config, typeof(TMarker));
        }

        public static void AddInstallersFromAssembliesContaining
            (this IServiceCollection services, IConfiguration config, params Type[] assemblyMarkers)
        {
            Assembly[] assemblies = assemblyMarkers.Select(marker => marker.Assembly).ToArray();

            services.AddInstallersFromAssemblies(config, assemblies);
        }

        public static void AddInstallersFromAssemblies
            (this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var concreteTypes = assembly.DefinedTypes
                                             .Where(type =>
                                                !type.IsInterface && !type.IsAbstract);


                var installers = concreteTypes.Where(typeof(IInstaller).IsAssignableFrom)
                                    .Select(Activator.CreateInstance)
                                    .Cast<IInstaller>();

                installers.ToList().ForEach(installer => installer.AddServices(services, config));

            }
        }

    }
}
