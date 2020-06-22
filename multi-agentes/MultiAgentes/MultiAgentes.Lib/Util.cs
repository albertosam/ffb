using MultiAgentes.Lib.Core;
using MultiAgentes.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib
{
    public static class Util
    {
        public static Posicionamento Parse(this Posicao posicao) => new Posicionamento(posicao.X, posicao.Y, posicao.Limpo);
        public static int GetNumero(int max)
        {
            var random = new Random();
            var numero = random.Next(0, max-1);

            return numero;
        }

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
    }
}
