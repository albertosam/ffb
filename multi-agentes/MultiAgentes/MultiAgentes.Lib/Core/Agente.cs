using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public abstract class Agente
    {
        private readonly Ambiente ambiente;
        public Posicao Atual { get; set; } = new Posicao();
        public int Movimentacoes { get; set; }
        public int Limpezas { get; set; }
        public string Nome { get; set; }
        public abstract Direcao GetDirecao();
        public Agente(Ambiente ambiente)
        {
            this.ambiente = ambiente;
        }

        public Posicao Mover()
        {
            var mov = GetDirecao();

            switch (mov)
            {
                case Direcao.SUBIR:
                    if (!this.Atual.BordaCima)
                        this.Subir();
                    break;
                case Direcao.DESCER:
                    if (!this.Atual.BordaBaixo)
                        this.Descer();
                    break;
                case Direcao.ESQUERDA:
                    if (!this.Atual.BordaEsquerda)
                        this.Esquerda();
                    break;
                case Direcao.DIREITA:
                    if (!this.Atual.BordaDireita)
                        this.Direita();
                    break;
                default:
                    break;
            }

            // incrementa contador de visitas a posição
            this.Atual.Visitas++;

            return this.Atual;
        }

        public void Limpar()
        {
            this.Atual.Limpo = true;
            this.Atual.Limpezas++;
            this.Limpezas++;
        }

        private void Subir()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X - 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        private void Descer()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X + 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        private void Esquerda()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X, this.Atual.Y - 1);
            this.Movimentacoes++;
        }

        private void Direita()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X, this.Atual.Y + 1);
            this.Movimentacoes++;
        }
    }
}
