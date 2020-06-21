using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public class Posicao : IPerceptor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Limpo { get; set; }
    }
}
