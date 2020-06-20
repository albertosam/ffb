using Microsoft.Extensions.Logging;
using MultiAgentes.Api.Application.Core;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application
{
    public static class AgenteContruir
    {
        public static LimpezaAgente AgenteLimpeza(string nome, ITabuleiro tabuleiro, ILogger logger)
        {
            var agente = new LimpezaAgente(nome, tabuleiro, logger);
            return agente;
        }

        public static SujeiraAgente AgenteSujeira(string nome, ITabuleiro tabuleiro, ILogger logger)
        {
            var agente = new SujeiraAgente(nome, tabuleiro, logger);
            return agente;
        }
    }
}
