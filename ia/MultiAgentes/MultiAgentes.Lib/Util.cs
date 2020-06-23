namespace MultiAgentes.Lib
{
    using MultiAgentes.Lib.Core;
    using MultiAgentes.Lib.Services;
    using System;

    /// <summary>
    /// Defines the <see cref="Util" />.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicao"/>.</param>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        public static Posicionamento Parse(this Posicao posicao) => new Posicionamento(posicao.X, posicao.Y, posicao.Limpo);

        /// <summary>
        /// The GetNumero.
        /// </summary>
        /// <param name="max">The max<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetNumero(int max)
        {
            var random = new Random();
            var numero = random.Next(0, max - 1);

            return numero;
        }

        /// <summary>
        /// The MovimentoAleatorio.
        /// </summary>
        /// <returns>The <see cref="Direcao"/>.</returns>
        public static Direcao MovimentoAleatorio()
        {
            var prox = new Random().Next(1, 5);
            switch (prox)
            {
                case 1:
                    return Direcao.SUBIR;

                case 2:
                    return Direcao.DESCER;

                case 3:
                    return Direcao.ESQUERDA;

                case 4:
                    return Direcao.DIREITA;

                case 5:
                    return Direcao.PARADO;

                default:
                    return Direcao.DIREITA;
            }
        }

        /// <summary>
        /// The MovimentoDirecionado.
        /// </summary>
        /// <param name="desejadaX">The desejadaX<see cref="int"/>.</param>
        /// <param name="desejadaY">The desejadaY<see cref="int"/>.</param>
        /// <param name="atualX">The atualX<see cref="int"/>.</param>
        /// <param name="atualY">The atualY<see cref="int"/>.</param>
        /// <returns>The <see cref="Direcao"/>.</returns>
        public static Direcao MovimentoDirecionado(int desejadaX, int desejadaY, int atualX, int atualY)
        {
            var difX = atualX - desejadaX;
            var difY = atualY - desejadaY;

            Direcao direcao = Direcao.PARADO;
            if (difX < 0)
            {
                direcao = Direcao.DESCER;
            }
            else if (difX > 0)
            {
                direcao = Direcao.SUBIR;
            }
            else
            {
                if (difY < 0)
                {
                    direcao = Direcao.DIREITA;
                }
                else if (difY > 0)
                {
                    direcao = Direcao.ESQUERDA;
                }
            }

            return direcao;
        }
    }
}
