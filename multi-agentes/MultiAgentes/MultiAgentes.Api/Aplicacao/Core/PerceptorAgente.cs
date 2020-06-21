using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public class PerceptorAgente : Agente
    {
        private readonly IComunicador comunicador;
        private Posicao PosicaoAtual { get; set; }

        public PerceptorAgente(IComunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public override void Comunicar(IPerceptor perceptor)
        {
            this.comunicador.Limpar(perceptor);
        }

        public override void Executar(IPerceptor perceptor)
        {
            for (int i = 0; i < Ambiente.Dimensao; i++)
            {
                for (int j = 0; j < Ambiente.Dimensao; j++)
                {
                    if (!Ambiente.Posicoes[i, j].Limpo)
                        this.Comunicar(Ambiente.Posicoes[i, j]);
                }
            }
        }
    }
}
