using Microsoft.Extensions.Logging;
using MultiAgentes.Api.Application.Core.Enums;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core
{
    public class LimpezaAgente : AgenteComSensor
    {
        private readonly ITabuleiro tabuleiro;
        private readonly ILogger logger;

        public LimpezaAgente(string nome, ITabuleiro tabuleiro, ILogger logger) : base(nome, tabuleiro, logger)
        {
            this.tabuleiro = tabuleiro;
            this.logger = logger;
        }

        public override void Executar()
        {
            if (this.Atual.Sujo)
            {
                this.Atuacoes++;
                this.tabuleiro.Limpar(this.Atual.X, this.Atual.Y);
            }
        }
    }
}
