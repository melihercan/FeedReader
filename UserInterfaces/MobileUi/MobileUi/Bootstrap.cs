using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileUi
{
    public class Bootstrap
    {
        public static App Init()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<App>();
                })
                .Build();

            return host.Services.GetService<App>();
        }
    }
}
