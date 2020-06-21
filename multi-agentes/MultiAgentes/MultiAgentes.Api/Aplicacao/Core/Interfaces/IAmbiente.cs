using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core.Interfaces
{
    public interface IAmbiente
    {
        Posicao[,] Posicoes { get; }
        int Dimensao { get; }
        void AddAgente(IAgente agente);
    }
}
