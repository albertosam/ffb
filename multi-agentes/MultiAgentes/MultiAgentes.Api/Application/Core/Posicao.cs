using MultiAgentes.Api.Application.Core.Enums;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core
{
    public class Posicao : IPosicao
    {
        public Posicao(ITabuleiro tabuleiro, int x, int y, bool bordaCima, bool bordaEsquerda, bool bordaDireita, bool bordaBaixo)
        {
            Tabuleiro = tabuleiro;
            X = x;
            Y = y;
            BordaCima = bordaCima;
            BordaEsquerda = bordaEsquerda;
            BordaDireita = bordaDireita;
            BordaBaixo = bordaBaixo;
            Sujo = false;
            Visitas = 0;
            Limpezas = 0;
        }

        public ITabuleiro Tabuleiro { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool BordaCima { get; set; }
        public bool BordaEsquerda { get; set; }
        public bool BordaDireita { get; set; }
        public bool BordaBaixo { get; set; }
        public bool Sujo { get; set; }
        public int Visitas { get; set; }
        public int Limpezas { get; set; }
        public bool Ocupada { get; set; }
        public IPosicao VizinhoEsquerda { get => this.Tabuleiro.GetPosicao(X, Y - 1); }
        public IPosicao VizinhoDireita { get => this.Tabuleiro.GetPosicao(X, Y + 1); }
        public IPosicao VizinhoAcima { get => this.Tabuleiro.GetPosicao(X - 1, Y); }
        public IPosicao VizinhoAbaixo { get => this.Tabuleiro.GetPosicao(X + 1, Y); }
    }
}
