using Microsoft.Extensions.Logging;
using MultiAgentes.Lib;
using MultiAgentes.Lib.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Reflection.Metadata.Ecma335;

namespace AspiradorConsole
{
    internal class ContinuousRunningProcessor
    {
        private static readonly System.Threading.AutoResetEvent _closingEvent = new System.Threading.AutoResetEvent(false);
        private readonly IConsolePrinter consolePrinter;
        private readonly Microsoft.Extensions.Logging.ILogger<ContinuousRunningProcessor> logger;
        private readonly Controladora controladora;

        public ContinuousRunningProcessor(IConsolePrinter consolePrinter,
                Microsoft.Extensions.Logging.ILogger<ContinuousRunningProcessor> logger,
                Controladora controladora)
        {
            this.consolePrinter = consolePrinter;
            this.logger = logger;
            this.controladora = controladora;
        }

        public void Process(string modelo)
        {
            var count = 0;

            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                logger.LogInformation("Process started!");

                controladora.Registrar(modelo);

                while (!controladora.AmbienteLimpo)
                {
                    controladora.MovimentarELimpar();

                    consolePrinter.Print(++count);
                }
            });

            Console.WriteLine("Press Ctrl + C to cancel!");
            Console.CancelKeyPress += ((s, a) =>
            {
                Console.WriteLine("Bye!");
                _closingEvent.Set();
            });

            _closingEvent.WaitOne();
        }

        
    }
}
