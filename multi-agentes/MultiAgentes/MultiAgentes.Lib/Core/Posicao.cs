using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Posicao_
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Ocupado { get; set; }
        public int Visitas { get; set; }
        public bool Limpo { get; set; }

        public Posicao_() { }
        public Posicao_(int x, int y, bool limpo)
        {
            X = x;
            Y = y;
            Limpo = limpo;
        }

        public string Chave => $"{X}{Y}";
    }
}
