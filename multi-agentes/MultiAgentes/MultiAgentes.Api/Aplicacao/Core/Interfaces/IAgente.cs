using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core.Interfaces
{
    public interface IAgente
    {
        void Executar(IPerceptor perceptor);
        void Comunicar(IPerceptor perceptor);
        void Configurar(IAmbiente ambiente, IComunicador comunicador);
    }
}
