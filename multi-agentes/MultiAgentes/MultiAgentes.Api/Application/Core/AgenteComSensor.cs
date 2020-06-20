using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using MultiAgentes.Api.Application.Core.Enums;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core
{
    public abstract class AgenteComSensor : Agente
    {
        private readonly ITabuleiro tabuleiro;

        public AgenteComSensor(string nome, ITabuleiro tabuleiro, ILogger logger) : base(nome, tabuleiro, logger)
        {
            this.tabuleiro = tabuleiro;
        }

        public override Movimento ProximoMovimento()
        {
            List<Movimento> movimentos = new List<Movimento>
            {
                Verificar(this.Atual.VizinhoAcima, Movimento.ACIMA),
                Verificar(this.Atual.VizinhoAbaixo, Movimento.DESCE),
                Verificar(this.Atual.VizinhoEsquerda, Movimento.ESQUERDA),
                Verificar(this.Atual.VizinhoDireita, Movimento.DIREITA)
            };

            var naoParado = movimentos.Where(a => a != Movimento.PARADO).ToList();
            if (naoParado.Count >= 1)
                return naoParado.First();

            return Util.MovimentoAleatorio();
        }

        private Movimento Verificar(IPosicao posicao, Movimento movimento) => posicao != null && posicao.Sujo ? movimento : Movimento.PARADO;
    }
}
