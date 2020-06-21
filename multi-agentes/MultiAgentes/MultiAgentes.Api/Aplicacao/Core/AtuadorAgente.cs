using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public class AtuadorAgente : Agente
    {
        private readonly IComunicador comunicador;

        public AtuadorAgente(IComunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public override void Comunicar(IPerceptor perceptor)
        {
            this.comunicador.LimpezaRealizada(perceptor);
        }

        public override void Executar(IPerceptor perceptor)
        {
            throw new NotImplementedException();
        }
    }
}
