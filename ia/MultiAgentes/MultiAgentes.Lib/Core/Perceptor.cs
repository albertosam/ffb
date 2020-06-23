namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Perceptor" />.
    /// </summary>
    public class Perceptor
    {
        /// <summary>
        /// Defines the posicoesSujas.
        /// </summary>
        private List<Posicao> posicoesSujas = new List<Posicao>();

        /// <summary>
        /// The RemoveSujo.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicao"/>.</param>
        public void RemoveSujo(Posicao posicao)
        {
            var pos = posicoesSujas.SingleOrDefault(a => a.Chave == posicao.Chave);
            posicoesSujas.Remove(pos);
        }

        /// <summary>
        /// The AddSujo.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicao"/>.</param>
        public void AddSujo(Posicao posicao)
        {
            if (!posicoesSujas.Any(a => a.Chave == posicao.Chave))
                posicoesSujas.Add(posicao);
        }

        /// <summary>
        /// The TudoLimpo.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool TudoLimpo()
        {
            return posicoesSujas.Count == 0;
        }

        /// <summary>
        /// The Proxima.
        /// </summary>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao Proxima()
        {
            return posicoesSujas.FirstOrDefault();
        }
    }
}
