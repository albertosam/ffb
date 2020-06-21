using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public class Posicao : IPosicao
    {
        public Posicao() { }
        public Posicao(int x, int y, bool limpo)
        {
            X = x;
            Y = y;
            Limpo = limpo;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Limpo { get; set; }

        public string Chave => $"{X}{Y}";
    }
}
