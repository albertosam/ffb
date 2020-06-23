using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public abstract class Agente
    {
        private readonly Ambiente ambiente;
        public Posicao Atual { get; set; } = new Posicao();
        public List<Movimento> Movimentos { get; set; } = new List<Movimento>();
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
                    {
                        this.Subir();
                        this.Atual.Visitas++;
                    }
                    break;
                case Direcao.DESCER:
                    if (!this.Atual.BordaBaixo)
                    {
                        this.Descer();
                        this.Atual.Visitas++;
                    }
                    break;
                case Direcao.ESQUERDA:
                    if (!this.Atual.BordaEsquerda)
                    {
                        this.Esquerda();
                        this.Atual.Visitas++;
                    }
                    break;
                case Direcao.DIREITA:
                    if (!this.Atual.BordaDireita)
                    {
                        this.Direita();
                        this.Atual.Visitas++;
                    }
                    break;
                case Direcao.PARADO:                    
                    break;
                default:
                    break;
            }

            this.Movimentos.Add(new Movimento { Direcao = mov, X = this.Atual.X, Y = this.Atual.Y });

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

        public class Movimento
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Direcao Direcao { get; set; }
        }
    }
}
