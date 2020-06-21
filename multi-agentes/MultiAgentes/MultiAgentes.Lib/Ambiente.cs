using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public class Ambiente : IAmbiente
    {
        private IComunicador comunicador;
        private IPosicao[,] posicoes;
        private int dimensao;
        public Ambiente(int dimensao)
        {
            this.dimensao = dimensao;
            this.comunicador = new Comunicador();

            posicoes = new IPosicao[dimensao, dimensao];
        }

        public int Dimensao => dimensao;
      

        public IPosicao[,] Posicoes => posicoes;

        public void AddAgente(IAgente agente)
        {
            agente.Configurar(this, comunicador);
        }

        public static IAmbiente Criar(int dimensao)
        {
            var ambiente = new Ambiente(dimensao);
            for (int i = 0; i < dimensao; i++)
            {
                for (int j = 0; j < dimensao; j++)
                {
                    ambiente.Posicoes[i, j] = new Posicao(i, j, true);
                }
            }

            return ambiente;
        }

        public static void Sujar(IAmbiente ambiente , int qtde)
        {
            var random = new Random();
            for (int i = 0; i < qtde;)
            {
                var x = random.Next(0, ambiente.Dimensao - 1);
                var y = random.Next(0, ambiente.Dimensao - 1);

                if (ambiente.Posicoes[x, y].Limpo)
                {
                    ambiente.Posicoes[x, y].Limpo = false;
                    i++;
                }
            }
        }

        public IPosicao Proximo()
        {
            return comunicador.Proximo();
        }
    }
}
