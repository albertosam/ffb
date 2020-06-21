using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public abstract class Agente : IAgente
    {
        private IAmbiente ambiente;
        public IComunicador Comunicador { get; set; }

        public IAmbiente Ambiente { get => ambiente; set => ambiente = value; }


        public abstract void Comunicar(IPosicao posicao);

        public void Configurar(IAmbiente ambiente, IComunicador comunicador)
        {
            this.Ambiente = ambiente;
            this.Comunicador = comunicador;
        }

        public abstract void Executar(IPosicao posicao);
    }
}
