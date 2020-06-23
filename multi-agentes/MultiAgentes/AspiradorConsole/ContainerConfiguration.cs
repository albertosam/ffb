namespace AspiradorConsole
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MultiAgentes.Lib.Services;

    /// <summary>
    /// Defines the <see cref="ContainerConfiguration" />.
    /// </summary>
    internal static class ContainerConfiguration
    {
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.ServiceProvider"/>.</returns>
        public static Microsoft.Extensions.DependencyInjection.ServiceProvider Configure()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection()
                                 .AddLogging()
                                 .AddSingleton<IConsolePrinter, ConsolePrinter>()
                                 .AddSingleton<ContinuousRunningProcessor>()
                                 .AddSingleton<Controlador>();

            services.AddHttpClient<ICentralClient, CentralClient>();
            services.Configure<Microsoft.Extensions.Logging.LoggerFilterOptions>(c => c.MinLevel = LogLevel.Trace);

            return services.BuildServiceProvider();
        }
    }
}
