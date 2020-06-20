using MultiAgentes.Api.Application.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core.Interfaces
{
    public interface IAgente
    {
        string Nome { get; set; }
        int Atuacoes { get; set; }
        int Movimentacoes { get; set; }
        Modo Modo { get; set; }
        IPosicao Atual { get; set; }
        void Movimentar();
        List<IPosicao> Historico { get; set; }
    }
}
