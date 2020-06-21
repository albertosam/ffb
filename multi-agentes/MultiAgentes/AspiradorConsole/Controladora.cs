using MultiAgentes.Lib;
using MultiAgentes.Lib.Enumeradores;
using MultiAgentes.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspiradorConsole
{
    internal class Controladora
    {
        private readonly ICentralClient centralClient;
        private readonly IConsolePrinter consolePrinter;
        Posicao posicao;
        public bool AmbienteLimpo { get; set; } = false;

        public Controladora(ICentralClient centralClient, IConsolePrinter consolePrinter)
        {
            this.centralClient = centralClient;
            this.consolePrinter = consolePrinter;
        }

        public async Task Registrar()
        {
            var requestRegistrar = this.centralClient.Registrar("aspirador-5000");
            requestRegistrar.Wait();

            this.posicao = await requestRegistrar;
        }

        public async Task MovimentarELimpar()
        {
            var proximaPosicaoRequest = this.centralClient.ProximaPosicao();
            proximaPosicaoRequest.Wait();

            var proximaPosicao = await proximaPosicaoRequest;
            if (proximaPosicao == null)
            {
                this.AmbienteLimpo = true;
                this.consolePrinter.AmbienteLimpo();
                return;
            }
            
            while (proximaPosicao.Chave != posicao.Chave)
            {
                var direcao = Movimentar(proximaPosicao, posicao);
                var resquestMovimento = this.centralClient.Movimentar((int)direcao);
                resquestMovimento.Wait();

                posicao = await resquestMovimento;
                this.consolePrinter.PosicaoAtual(posicao.X, posicao.Y);
            }

            this.centralClient.LimpezaRealizada(posicao).Wait();
            this.consolePrinter.Aspirado(posicao.X, posicao.Y);
        }

        private Direcao Movimentar(Posicao desejada, Posicao atual)
        {
            var difX = atual.X - desejada.X;
            var difY = atual.Y - desejada.Y;

            Direcao direcao = Direcao.PARADO;
            if (difX < 0)
            {
                direcao = Direcao.DESCER;
            }
            else if (difX > 0)
            {
                direcao = Direcao.SUBIR;
            }
            else
            {
                if (difY < 0)
                {
                    direcao = Direcao.DIREITA;
                }
                else if (difY > 0)
                {
                    direcao = Direcao.ESQUERDA;
                }
            }

            return direcao;
        }
    }
}
