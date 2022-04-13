using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware.Installers
{
    public interface IInstaller
    {
        void AddServices(IServiceCollection services, IConfiguration configuration);
        public int Order => -1;
    }
}
