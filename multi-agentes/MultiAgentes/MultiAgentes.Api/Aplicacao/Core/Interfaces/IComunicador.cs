using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core.Interfaces
{
    public interface IComunicador
    {
        void Limpar(IPerceptor perceptor);
        void LimpezaRealizada(IPerceptor perceptor);
    }
}
