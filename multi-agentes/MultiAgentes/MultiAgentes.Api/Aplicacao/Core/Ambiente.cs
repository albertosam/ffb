using MultiAgentes.Api.Aplicacao.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Aplicacao.Core
{
    public class Ambiente : IAmbiente
    {
        private IComunicador comunicador;
        private Posicao[,] posicoes;
        private int dimensao;
        public Ambiente(int dimensao)
        {
            this.dimensao = dimensao;
            comunicador = new Comunicador();
            posicoes = new Posicao[dimensao, dimensao];
        }

        public int Dimensao => dimensao;

        public Posicao[,] Posicoes => posicoes;

        public void AddAgente(IAgente agente)
        {
            agente.Configurar(this, comunicador);
        }
    }
}
