using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Application.Middleware.Interceptors;

public interface IInterceptor
{
    IApplicationBuilder ConfigureInterceptor(IApplicationBuilder builder);
    public int Order => -1;
}
