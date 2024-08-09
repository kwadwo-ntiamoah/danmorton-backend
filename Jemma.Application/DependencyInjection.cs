using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Jemma.Application
{
    public static class DependencyInjection
    {   
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            
            return services;
        }
    }
}