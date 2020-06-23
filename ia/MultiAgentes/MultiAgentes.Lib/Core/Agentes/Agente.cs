namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Agente" />.
    /// </summary>
    public abstract class Agente
    {
        /// <summary>
        /// Defines the ambiente.
        /// </summary>
        private readonly Ambiente ambiente;

        /// <summary>
        /// Gets or sets the Atual.
        /// </summary>
        public Posicao Atual { get; set; } = new Posicao();

        /// <summary>
        /// Gets or sets the Movimentos.
        /// </summary>
        public List<Movimento> Movimentos { get; set; } = new List<Movimento>();

        /// <summary>
        /// Gets or sets the Movimentacoes.
        /// </summary>
        public int Movimentacoes { get; set; }

        /// <summary>
        /// Gets or sets the Limpezas.
        /// </summary>
        public int Limpezas { get; set; }

        /// <summary>
        /// Gets or sets the Nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// The GetDirecao.
        /// </summary>
        /// <returns>The <see cref="Direcao"/>.</returns>
        public abstract Direcao GetDirecao();

        /// <summary>
        /// Initializes a new instance of the <see cref="Agente"/> class.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        public Agente(Ambiente ambiente)
        {
            this.ambiente = ambiente;
        }

        /// <summary>
        /// The Mover.
        /// </summary>
        /// <returns>The <see cref="Posicao"/>.</returns>
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

        /// <summary>
        /// The Limpar.
        /// </summary>
        public void Limpar()
        {
            this.Atual.Limpo = true;
            this.Atual.Limpezas++;
            this.Limpezas++;
        }

        /// <summary>
        /// The Subir.
        /// </summary>
        private void Subir()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X - 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        /// <summary>
        /// The Descer.
        /// </summary>
        private void Descer()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X + 1, this.Atual.Y);
            this.Movimentacoes++;
        }

        /// <summary>
        /// The Esquerda.
        /// </summary>
        private void Esquerda()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X, this.Atual.Y - 1);
            this.Movimentacoes++;
        }

        /// <summary>
        /// The Direita.
        /// </summary>
        private void Direita()
        {
            this.Atual = this.ambiente.GetPosicao(this.Atual.X, this.Atual.Y + 1);
            this.Movimentacoes++;
        }

        /// <summary>
        /// Defines the <see cref="Movimento" />.
        /// </summary>
        public class Movimento
        {
            /// <summary>
            /// Gets or sets the X.
            /// </summary>
            public int X { get; set; }

            /// <summary>
            /// Gets or sets the Y.
            /// </summary>
            public int Y { get; set; }

            /// <summary>
            /// Gets or sets the Direcao.
            /// </summary>
            public Direcao Direcao { get; set; }
        }
    }
}
