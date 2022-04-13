using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulApiPlayground.Infrastructure.Application.Middleware.Interceptors;
using System.Reflection;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware
{
    public static class InterceptorExtensions
    {

        public static void AddInterceptorsFromAssemblyContaining<TMarker>
            (this WebApplication app)
        {
            AddInterceptorsFromAssembliesContaining(app, typeof(TMarker));
        }

        private static void AddInterceptorsFromAssembliesContaining
            (this WebApplication app, params Type[] assemblyMarkers)
        {
            Assembly[] assemblies = assemblyMarkers.Select(marker => marker.Assembly).ToArray();

            AddInterceptorsFromAssemblies(app, assemblies);
        }

        private static void AddInterceptorsFromAssemblies
            (this WebApplication app, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var interceptorTypes = assembly.DefinedTypes
                                             .Where(type => 
                                                !type.IsInterface && !type.IsAbstract && 
                                                typeof(IInterceptor).IsAssignableFrom(type));

                var interceptors = interceptorTypes.Select(Activator.CreateInstance)
                                                 .Cast<IInterceptor>();

                interceptors.OrderBy(mid => mid.Order)
                           .ToList().ForEach(mid => mid.ConfigureInterceptor(app));
            }
        }
    }
}
