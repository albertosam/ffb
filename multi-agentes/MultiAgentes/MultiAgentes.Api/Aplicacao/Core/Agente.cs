using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public abstract class Agente : IAgente
    {
        private IAmbiente ambiente;
        private IComunicador comunicador;

        public IAmbiente Ambiente { get => ambiente; set => ambiente = value; }

        public abstract void Comunicar(IPerceptor perceptor);

        public void Configurar(IAmbiente ambiente, IComunicador comunicador)
        {
            this.Ambiente = ambiente;
            this.comunicador = comunicador;
        }

        public abstract void Executar(IPerceptor perceptor);
    }
}
