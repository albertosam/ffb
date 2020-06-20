using MultiAgentes.Api.Application.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core.Interfaces
{
    public interface IPosicao
    {
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
        public IPosicao VizinhoEsquerda { get; }
        public IPosicao VizinhoDireita { get; }
        public IPosicao VizinhoAcima { get; }
        public IPosicao VizinhoAbaixo { get; }
    }
}
