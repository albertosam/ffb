using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Ambiente_
    {
        public Ambiente_(int dimensao)
        {
            Dimensao = dimensao;
            Posicoes = new Posicao_[dimensao, dimensao];
        }

        public Posicao_[,] Posicoes { get; set; }
        public Posicao_ Atuador { get; set; }
        public int Dimensao { get; set; }

        public static Ambiente_ Criar(int dimensao)
        {
            bool bordaCima, bordaEsquerda, bordaDireita, bordaBaixo = false;

            var ambiente = new Ambiente_(dimensao);
            for (int i = 0; i < dimensao; i++)
            {
                for (int j = 0; j < dimensao; j++)
                {
                    bordaCima = i == 0;
                    bordaEsquerda = j == 0;
                    bordaDireita = j == dimensao - 1;
                    bordaBaixo = i == dimensao - 1;

                    ambiente.Posicoes[i, j] = new Posicao_(ambiente, i, j, bordaCima, bordaEsquerda, bordaDireita, bordaBaixo);
                }
            }

            return ambiente;
        }

        public Posicao_ SetPosicaoAgente(int x, int y)
        {
            var posicao = this.Posicoes[x, y];
            this.Atuador = posicao;
            return posicao;
        }

        public static void SujarAletorio(Ambiente_ ambiente, int qtdePosicoes)
        {
            var random = new Random();
            for (int i = 0; i < qtdePosicoes;)
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

        public Posicao_ GetPosicao(int x, int y)
        {
            if (x >= 0 && y >= 0)
                if (x < Dimensao && y < Dimensao)
                    return Posicoes[x, y];

            return null;
        }


        public void Sujar(int x, int y)
        {
            this.Posicoes[x, y].Limpo = false;
        }

        public void Limpar(int x, int y)
        {
            this.Posicoes[x, y].Limpo = true;
        }

        public Perceptor GetAgentePerceptor()
        {
            Perceptor p = new Perceptor();
            for (int i = 0; i < Dimensao; i++)
            {
                for (int j = 0; j < Dimensao; j++)
                {
                    if (!this.Posicoes[i, j].Limpo)
                        p.AddSujo(this.Posicoes[i, j]);
                }
            }

            return p;
        }

        public Posicao_ Movimentar(Direcao direcao)
        {
            var x = this.Atuador.X;
            var y = this.Atuador.Y;

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

            this.Atuador = this.Posicoes[x, y];
            return this.Atuador;
        }

    }
}
