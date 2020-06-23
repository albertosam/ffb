namespace MultiAgentes.Lib.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="Ambiente" />.
    /// </summary>
    public class Ambiente
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ambiente"/> class.
        /// </summary>
        /// <param name="dimensao">The dimensao<see cref="int"/>.</param>
        public Ambiente(int dimensao)
        {
            Dimensao = dimensao;
            Posicoes = new Posicao[dimensao, dimensao];
        }

        /// <summary>
        /// Gets or sets the Posicoes.
        /// </summary>
        public Posicao[,] Posicoes { get; set; }

        /// <summary>
        /// Gets or sets the Atuador.
        /// </summary>
        public Posicao Atuador { get; set; }

        /// <summary>
        /// Gets or sets the Dimensao.
        /// </summary>
        public int Dimensao { get; set; }

        /// <summary>
        /// The Criar.
        /// </summary>
        /// <param name="dimensao">The dimensao<see cref="int"/>.</param>
        /// <returns>The <see cref="Ambiente"/>.</returns>
        public static Ambiente Criar(int dimensao)
        {
            bool bordaCima, bordaEsquerda, bordaDireita, bordaBaixo = false;

            var ambiente = new Ambiente(dimensao);
            for (int i = 0; i < dimensao; i++)
            {
                for (int j = 0; j < dimensao; j++)
                {
                    bordaCima = i == 0;
                    bordaEsquerda = j == 0;
                    bordaDireita = j == dimensao - 1;
                    bordaBaixo = i == dimensao - 1;

                    ambiente.Posicoes[i, j] = new Posicao(ambiente, i, j, bordaCima, bordaEsquerda, bordaDireita, bordaBaixo);
                }
            }

            return ambiente;
        }

        /// <summary>
        /// The SetPosicaoAgente.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao SetPosicaoAgente(int x, int y)
        {
            var posicao = this.Posicoes[x, y];
            this.Atuador = posicao;            
            return posicao;
        }

        /// <summary>
        /// The SujarAletorio.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        /// <param name="qtdePosicoes">The qtdePosicoes<see cref="int"/>.</param>
        public static void SujarAletorio(Ambiente ambiente, int qtdePosicoes)
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

        /// <summary>
        /// The GetPosicao.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao GetPosicao(int x, int y)
        {
            if (x >= 0 && y >= 0)
                if (x < Dimensao && y < Dimensao)
                    return Posicoes[x, y];

            return null;
        }

        /// <summary>
        /// The Sujar.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        public void Sujar(int x, int y)
        {
            this.Posicoes[x, y].Limpo = false;
        }

        /// <summary>
        /// The Limpar.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        public void Limpar(int x, int y)
        {
            this.Posicoes[x, y].Limpo = true;
        }

        /// <summary>
        /// The GetAgentePerceptor.
        /// </summary>
        /// <returns>The <see cref="Perceptor"/>.</returns>
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

        /// <summary>
        /// The Movimentar.
        /// </summary>
        /// <param name="direcao">The direcao<see cref="Direcao"/>.</param>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao Movimentar(Direcao direcao)
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
