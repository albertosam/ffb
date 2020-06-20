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
    public abstract class AgenteAleatorio : Agente
    {
        public AgenteAleatorio(string nome, ITabuleiro tabuleiro, ILogger logger) : base(nome, tabuleiro, logger)
        {
        }

        public override Movimento ProximoMovimento()
        {
            return Util.MovimentoAleatorio();
        }
    }
}
