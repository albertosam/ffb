using Microsoft.Extensions.DependencyInjection;
using MultiAgentes.Lib.Services;
using System;

namespace AspiradorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aspirador ligado!");

            var serviceProvider = ContainerConfiguration.Configure();
            serviceProvider.GetService<ContinuousRunningProcessor>().Process(args[0]);
        }

    }
}
