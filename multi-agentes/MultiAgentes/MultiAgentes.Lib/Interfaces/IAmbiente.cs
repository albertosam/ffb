using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib.Interfaces
{
    public interface IAmbiente
    {
        IPosicao[,] Posicoes { get; }
        int Dimensao { get; }
        void AddAgente(IAgente agente);
        IPosicao Proximo();
    }
}
