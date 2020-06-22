using Microsoft.Extensions.DependencyInjection;
using MultiAgentes.Lib.Core;
using MultiAgentes.Lib.Enumeradores;
using MultiAgentes.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Direcao = MultiAgentes.Lib.Core.Direcao;

namespace AspiradorConsole
{
    internal class Controladora
    {
        private readonly ICentralClient centralClient;
        private readonly IConsolePrinter consolePrinter;
        Posicionamento posicao;
        public bool AmbienteLimpo { get; set; } = false;

        public Controladora(ICentralClient centralClient, IConsolePrinter consolePrinter)
        {
            this.centralClient = centralClient;
            this.consolePrinter = consolePrinter;
        }

        public void Registrar(string modelo)
        {
            this.posicao = this.centralClient.Registrar(modelo);
        }

        public void MovimentarELimpar()
        {
            // realiza limpeza
            if (posicao.Limpo == false)
            {
                this.centralClient.LimpezaRealizada(posicao);
                this.consolePrinter.Aspirado(posicao.X, posicao.Y);
            }

            // seleciona posição
            var proximaPosicao = this.centralClient.ProximaPosicao();
            if (proximaPosicao == null)
            {
                this.AmbienteLimpo = true;
                this.consolePrinter.AmbienteLimpo();
                return;
            }

            // movimenta para posição selecionada
            while (proximaPosicao.Chave != posicao.Chave)
            {
                var direcao = Movimentar(proximaPosicao, posicao);
                this.posicao = this.centralClient.Movimentar((int)direcao);

                this.consolePrinter.PosicaoAtual(posicao.X, posicao.Y);
            }
        }

        private Direcao Movimentar(Posicionamento desejada, Posicionamento atual)
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
