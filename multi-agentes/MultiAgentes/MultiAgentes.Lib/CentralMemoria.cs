using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;
using MultiAgentes.Lib.Interfaces;
using MultiAgentes.Lib.Enumeradores;

namespace MultiAgentes.Lib
{
    public interface ICentralMemoria
    {
        void Inicializar();
        IPosicao RegistrarAspirador(string nome);
        IPosicao GetProximaPosicao();
        IPosicao Movimentar(Direcao direcao);
        void Limpar(IPosicao posicao);
    }

    public class CentralMemoria : ICentralMemoria
    {
        public IAmbiente Ambiente { get; set; }
        public AtuadorAgente Atuador { get; set; }
        public PerceptorAgente Perceptor { get; set; }

        public IPosicao GetProximaPosicao()
        {
            this.Perceptor.Executar(null);
            return this.Ambiente.Proximo();
        }

        public IPosicao Movimentar(Direcao direcao)
        {
            var x = this.Atuador.Posicao.X;
            var y = this.Atuador.Posicao.Y;

            switch (direcao)
            {
                case Direcao.SUBIR:
                    x--;
                    break;
                case Direcao.DESCER:
                    x++;
                    break;
                case Direcao.ESQUERDA:
                    y--;
                    break;
                case Direcao.DIREITA:
                    y++;
                    break;
                case Direcao.PARADO:
                    break;
                default:
                    break;
            }

            this.Atuador.Posicao = this.Ambiente.Posicoes[x, y];
            return this.Atuador.Posicao;
        }

        public void Limpar(IPosicao posicao)
        {
            this.Atuador.Executar(posicao);
        }

        public void Inicializar()
        {
            this.Ambiente = MultiAgentes.Lib.Ambiente.Criar(10);
            MultiAgentes.Lib.Ambiente.Sujar(this.Ambiente, 2);
            this.Perceptor = new PerceptorAgente();
            this.Ambiente.AddAgente(this.Perceptor);
            this.Perceptor.Executar(null);

        }

        public IPosicao RegistrarAspirador(string nome)
        {
            this.Atuador = new AtuadorAgente(nome);
            this.Atuador.Posicao = this.Ambiente.Posicoes[2, 3];
            this.Ambiente.AddAgente(this.Atuador);

            return this.Atuador.Posicao;
        }
    }
}
