using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core.Interfaces
{
    public interface ITabuleiro
    {
        int Dimensao { get; set; }
        IPosicao[,] Posicoes { get; set; }
        bool Sujo(int x, int y);
        void Sujar(int x, int y);
        void Limpar(int x, int y);
        bool Limpo();
        IPosicao GetPosicao(int x, int y);
    }
}
