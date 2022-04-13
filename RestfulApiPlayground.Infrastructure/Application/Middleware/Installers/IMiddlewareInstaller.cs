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
    public interface IMiddlewareInstaller
    {
        void ConfigureServices(WebApplication app);
        public int Order => -1;
    }
}
