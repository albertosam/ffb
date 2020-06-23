namespace AspiradorConsole
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        internal static void Main(string[] args)
        {
            Console.WriteLine("Aspirador ligado!");

            var serviceProvider = ContainerConfiguration.Configure();
            serviceProvider.GetService<ContinuousRunningProcessor>().Process(args[0]);
        }
    }
}
