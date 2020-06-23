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

            if (args.Length == 0)
            {
                Console.Error.WriteLine("Informe o parâmetro MODELO: [A ou B ou C]!");
                //A - Aleatorio
                //B - Com Sensor
                //C - Direcionado
                return;
            }

            Console.WriteLine("Aspirador ligado!");
            var serviceProvider = ContainerConfiguration.Configure();
            serviceProvider.GetService<ContinuousRunningProcessor>().Process(args[0]);
        }
    }
}
