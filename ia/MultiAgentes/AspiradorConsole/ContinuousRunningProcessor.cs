namespace AspiradorConsole
{
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// Defines the <see cref="ContinuousRunningProcessor" />.
    /// </summary>
    internal class ContinuousRunningProcessor
    {
        /// <summary>
        /// Defines the _closingEvent.
        /// </summary>
        private static readonly System.Threading.AutoResetEvent _closingEvent = new System.Threading.AutoResetEvent(false);

        /// <summary>
        /// Defines the consolePrinter.
        /// </summary>
        private readonly IConsolePrinter consolePrinter;

        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly Microsoft.Extensions.Logging.ILogger<ContinuousRunningProcessor> logger;

        /// <summary>
        /// Defines the controladora.
        /// </summary>
        private readonly Controlador controladora;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinuousRunningProcessor"/> class.
        /// </summary>
        /// <param name="consolePrinter">The consolePrinter<see cref="IConsolePrinter"/>.</param>
        /// <param name="logger">The logger<see cref="Microsoft.Extensions.Logging.ILogger{ContinuousRunningProcessor}"/>.</param>
        /// <param name="controladora">The controladora<see cref="Controlador"/>.</param>
        public ContinuousRunningProcessor(IConsolePrinter consolePrinter,
                Microsoft.Extensions.Logging.ILogger<ContinuousRunningProcessor> logger,
                Controlador controladora)
        {
            this.consolePrinter = consolePrinter;
            this.logger = logger;
            this.controladora = controladora;
        }

        /// <summary>
        /// The Process.
        /// </summary>
        /// <param name="modelo">The modelo<see cref="string"/>.</param>
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
