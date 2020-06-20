using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using MultiAgentes.Api.Application.Core.Enums;
using MultiAgentes.Api.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application.Core
{
    public abstract class Agente : IAgente
    {
        private readonly ITabuleiro tabuleiro;
        private readonly ILogger logger;

        protected Agente(string nome, ITabuleiro tabuleiro, ILogger logger)
        {
            this.Nome = nome;
            this.tabuleiro = tabuleiro;
            this.logger = logger;
        }

        public IPosicao Atual { get; set; }
        public List<IPosicao> Historico { get; set; } = new List<IPosicao>();
        public Modo Modo { get; set; }
        private bool Ativo = true;
        public int Movimentacoes { get; set; }
        public int Atuacoes { get; set; }
        public string Nome { get; set; }

        public void Movimentar()
        {
            var posicaoAnterior = this.Atual;
            this.Historico.Add(this.Atual);
            var mov = ProximoMovimento();

            switch (mov)
            {
                case Movimento.ACIMA:
                    if (!this.Atual.BordaCima)
                        this.Subir();
                    break;
                case Movimento.DESCE:
                    if (!this.Atual.BordaBaixo)
                        this.Descer();
                    break;
                case Movimento.ESQUERDA:
                    if (!this.Atual.BordaEsquerda)
                        this.Esquerda();
                    break;
                case Movimento.DIREITA:
                    if (!this.Atual.BordaDireita)
                        this.Direita();
                    break;
                default:
                    break;
            }

            posicaoAnterior.Ocupada = false;
            posicaoAnterior.Visitas++;
            this.Atual.Ocupada = true;
        }

        private void Subir()
        {
            this.Atual = this.tabuleiro.GetPosicao(this.Atual.X - 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        private void Descer()
        {
            this.Atual = this.tabuleiro.GetPosicao(this.Atual.X + 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        private void Esquerda()
        {
            this.Atual = this.tabuleiro.GetPosicao(this.Atual.X, this.Atual.Y - 1);
            this.Movimentacoes++;
        }

        private void Direita()
        {
            this.Atual = this.tabuleiro.GetPosicao(this.Atual.X, this.Atual.Y + 1);
            this.Movimentacoes++;
        }

        public void Rodar()
        {
            int cont = 0;
            while (Ativo && cont < 1000 && !tabuleiro.Limpo())
            {
                cont++;
                this.Movimentar();
                this.Executar();

                this.logger.LogInformation($"[{this.Atual.X}, {this.Atual.Y}]");

                Thread.Sleep(1000);
            }

            this.logger.LogInformation($"{Nome}: {cont} movimentações |  {1 / (Movimentacoes / Atuacoes)} coeficiente");
        }

        public abstract void Executar();
        public abstract Movimento ProximoMovimento();

    }
}
