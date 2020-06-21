using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiAgentes.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Central.Api.Services
{
    public class WorkService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public WorkService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope
            using (var scope = serviceProvider.CreateScope())
            {
                var simulador = scope.ServiceProvider.GetService<Simulacao>();

                try
                {
                    simulador.Iniciar();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
