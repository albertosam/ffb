namespace AspiradorConsole
{
    using System;

    /// <summary>
    /// Defines the <see cref="IConsolePrinter" />.
    /// </summary>
    internal interface IConsolePrinter
    {
        /// <summary>
        /// The Print.
        /// </summary>
        /// <param name="count">The count<see cref="int"/>.</param>
        void Print(int count);

        /// <summary>
        /// The Aspirado.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        void Aspirado(int x, int y);

        /// <summary>
        /// The AmbienteLimpo.
        /// </summary>
        void AmbienteLimpo();

        /// <summary>
        /// The PosicaoAtual.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        void PosicaoAtual(int x, int y);
    }

    /// <summary>
    /// Defines the <see cref="ConsolePrinter" />.
    /// </summary>
    internal class ConsolePrinter : IConsolePrinter
    {
        /// <summary>
        /// The PosicaoAtual.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        public void PosicaoAtual(int x, int y)
        {
            Console.WriteLine($"# Posição atual {x}:{y}");
        }

        /// <summary>
        /// The Aspirado.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        public void Aspirado(int x, int y)
        {
            Console.WriteLine($"### Ambiente {x}:{y} aspirado");
        }

        /// <summary>
        /// The AmbienteLimpo.
        /// </summary>
        public void AmbienteLimpo()
        {
            Console.WriteLine($"############# Ambiente limpo");
        }

        /// <summary>
        /// The Print.
        /// </summary>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void Print(int count)
        {
            Console.WriteLine($"Current Count {count}");
        }
    }
}
