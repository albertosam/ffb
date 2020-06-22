using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Posicao
    {
        private Ambiente ambiente;
        public int X { get; set; }
        public int Y { get; set; }
        public bool Ocupado { get; set; }
        public int Visitas { get; set; }
        public bool Limpo { get; set; }

        public Posicao() { }
        public Posicao(Ambiente ambiente, int x, int y,  bool bordaCima, bool bordaEsquerda, bool bordaDireita, bool bordaBaixo)
        {
            this.ambiente = ambiente;
            Limpo = true;
            X = x;
            Y = y;
            BordaCima = bordaCima;
            BordaEsquerda = bordaEsquerda;
            BordaDireita = bordaDireita;
            BordaBaixo = bordaBaixo;
            Visitas = 0;
            Limpezas = 0;
        }

        public string Chave => $"{X}{Y}";
        public bool BordaCima { get; set; }
        public bool BordaEsquerda { get; set; }
        public bool BordaDireita { get; set; }
        public bool BordaBaixo { get; set; }
        public int Limpezas { get; set; }
        public bool Ocupada { get; set; }
        public Posicao VizinhoEsquerda { get => this.ambiente.GetPosicao(X, Y - 1); }
        public Posicao VizinhoDireita { get => this.ambiente.GetPosicao(X, Y + 1); }
        public Posicao VizinhoAcima { get => this.ambiente.GetPosicao(X - 1, Y); }
        public Posicao VizinhoAbaixo { get => this.ambiente.GetPosicao(X + 1, Y); }
    }
}
