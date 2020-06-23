namespace MultiAgentes.Lib.Services
{
    /// <summary>
    /// Defines the <see cref="Posicionamento" />.
    /// </summary>
    public class Posicionamento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Posicionamento"/> class.
        /// </summary>
        public Posicionamento()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Posicionamento"/> class.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        /// <param name="limpo">The limpo<see cref="bool"/>.</param>
        public Posicionamento(int x, int y, bool limpo)
        {
            X = x;
            Y = y;
            Limpo = limpo;
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Limpo.
        /// </summary>
        public bool Limpo { get; set; }

        /// <summary>
        /// Gets the Chave.
        /// </summary>
        public string Chave => $"{X}{Y}";
    }
}
