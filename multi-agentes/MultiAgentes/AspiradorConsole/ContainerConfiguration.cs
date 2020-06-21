using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiAgentes.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspiradorConsole
{
    internal static class ContainerConfiguration
    {
        public static Microsoft.Extensions.DependencyInjection.ServiceProvider Configure()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection()
                                 .AddLogging()
                                 .AddSingleton<IConsolePrinter, ConsolePrinter>()
                                 .AddSingleton<ContinuousRunningProcessor>()
                                 .AddSingleton<Controladora>();

            services.AddHttpClient<ICentralClient, CentralClient>();
            services.Configure<Microsoft.Extensions.Logging.LoggerFilterOptions>(c => c.MinLevel = LogLevel.Trace);

            return services.BuildServiceProvider();
        }
    }
}
