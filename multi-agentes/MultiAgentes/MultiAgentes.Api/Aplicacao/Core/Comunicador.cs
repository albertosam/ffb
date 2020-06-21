using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public class Comunicador : IComunicador
    {
        private List<IPerceptor> _perceptores = new List<IPerceptor>();
        public void Limpar(IPerceptor perceptor)
        {
            if (!_perceptores.Contains(perceptor))
                _perceptores.Add(perceptor);
        }

        public void LimpezaRealizada(IPerceptor perceptor)
        {
            _perceptores.Remove(perceptor);
        }
    }
}
