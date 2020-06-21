using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public class PerceptorAgente : Agente, IAgente
    {
        private Posicao PosicaoAtual { get; set; }

        public override void Comunicar(IPosicao posicao)
        {
            this.Comunicador.Limpar(posicao);
        }

        public override void Executar(IPosicao posicao)
        {
            for (int i = 0; i < Ambiente.Dimensao; i++)
            {
                for (int j = 0; j < Ambiente.Dimensao; j++)
                {
                    if (!Ambiente.Posicoes[i, j].Limpo)
                    {
                        this.Comunicar(Ambiente.Posicoes[i, j]);
                    }
                }
            }
        }

    }
}
