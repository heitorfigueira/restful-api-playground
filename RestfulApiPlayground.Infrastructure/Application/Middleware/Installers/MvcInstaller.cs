using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
        }

    }
}
