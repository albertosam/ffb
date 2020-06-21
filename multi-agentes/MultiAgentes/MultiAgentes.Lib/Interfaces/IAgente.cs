using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib.Interfaces
{
    public interface IAgente
    {
        void Executar(IPosicao posicao);
        void Comunicar(IPosicao posicao);
        void Configurar(IAmbiente ambiente, IComunicador comunicador);
    }
}
