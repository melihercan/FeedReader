using Blazored.Modal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUi
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWebUiServices(this IServiceCollection services)
        {
            services.AddBlazoredModal();
            return services;
        }

    }
}
