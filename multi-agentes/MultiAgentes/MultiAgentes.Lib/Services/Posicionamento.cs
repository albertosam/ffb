using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Services
{
    public class Posicionamento
    {
        public Posicionamento()
        {
        }

        public Posicionamento(int x, int y, bool limpo)
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
