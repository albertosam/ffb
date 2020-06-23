namespace MultiAgentes.Lib.Core
{
    /// <summary>
    /// Defines the <see cref="Posicao" />.
    /// </summary>
    public class Posicao
    {
        /// <summary>
        /// Defines the ambiente.
        /// </summary>
        private Ambiente ambiente;

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Ocupado.
        /// </summary>
        public bool Ocupado { get; set; }

        /// <summary>
        /// Gets or sets the Visitas.
        /// </summary>
        public int Visitas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Limpo.
        /// </summary>
        public bool Limpo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Posicao"/> class.
        /// </summary>
        public Posicao()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Posicao"/> class.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        /// <param name="bordaCima">The bordaCima<see cref="bool"/>.</param>
        /// <param name="bordaEsquerda">The bordaEsquerda<see cref="bool"/>.</param>
        /// <param name="bordaDireita">The bordaDireita<see cref="bool"/>.</param>
        /// <param name="bordaBaixo">The bordaBaixo<see cref="bool"/>.</param>
        public Posicao(Ambiente ambiente, int x, int y, bool bordaCima, bool bordaEsquerda, bool bordaDireita, bool bordaBaixo)
        {
            this.ambiente = ambiente;
            Limpo = true;
            X = x;
            Y = y;
            BordaCima = bordaCima;
            BordaEsquerda = bordaEsquerda;
            BordaDireita = bordaDireita;
            BordaBaixo = bordaBaixo;
            Visitas = 0;
            Limpezas = 0;
        }

        /// <summary>
        /// Gets the Chave.
        /// </summary>
        public string Chave => $"{X}{Y}";

        /// <summary>
        /// Gets or sets a value indicating whether BordaCima.
        /// </summary>
        public bool BordaCima { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether BordaEsquerda.
        /// </summary>
        public bool BordaEsquerda { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether BordaDireita.
        /// </summary>
        public bool BordaDireita { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether BordaBaixo.
        /// </summary>
        public bool BordaBaixo { get; set; }

        /// <summary>
        /// Gets or sets the Limpezas.
        /// </summary>
        public int Limpezas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Ocupada.
        /// </summary>
        public bool Ocupada { get; set; }

        /// <summary>
        /// Gets the VizinhoEsquerda.
        /// </summary>
        public Posicao VizinhoEsquerda { get => this.ambiente.GetPosicao(X, Y - 1); }

        /// <summary>
        /// Gets the VizinhoDireita.
        /// </summary>
        public Posicao VizinhoDireita { get => this.ambiente.GetPosicao(X, Y + 1); }

        /// <summary>
        /// Gets the VizinhoAcima.
        /// </summary>
        public Posicao VizinhoAcima { get => this.ambiente.GetPosicao(X - 1, Y); }

        /// <summary>
        /// Gets the VizinhoAbaixo.
        /// </summary>
        public Posicao VizinhoAbaixo { get => this.ambiente.GetPosicao(X + 1, Y); }
    }
}
