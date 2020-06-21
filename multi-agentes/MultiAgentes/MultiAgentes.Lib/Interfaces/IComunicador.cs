using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib.Interfaces
{
    public interface IComunicador
    {
        void Limpar(IPosicao posicao);
        void LimpezaRealizada(IPosicao posicao);
        IPosicao Proximo();
    }
}
