using MultiAgentes.Api.Application.Core.Enums;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core
{
    public class Tabuleiro : ITabuleiro
    {
        public int Dimensao { get; set; }
        public IPosicao[,] Posicoes { get; set; }

        public IPosicao GetPosicao(int x, int y)
        {
            if (x >= 0 && y >= 0)
                if (x < Dimensao && y < Dimensao)
                    return Posicoes[x, y];

            return null;
        }

        public void Limpar(int x, int y)
        {
            this.Posicoes[x, y].Sujo = false;
        }

        public void Sujar(int x, int y)
        {
            this.Posicoes[x, y].Sujo = true;
        }

        public bool Sujo(int x, int y)
        {
            return this.Posicoes[x, y].Sujo;
        }

        public bool Limpo()
        {
            var limpo = true;
            for (int i = 0; i < Dimensao; i++)
            {
                for (int j = 0; j < Dimensao; j++)
                {
                    if (Posicoes[i, j].Sujo)
                        limpo = false;
                }
            }

            return limpo;
        }
    }
}
