using MultiAgentes.Api.Application.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application
{
    public static class Util
    {
        public static Movimento MovimentoAleatorio()
        {
            var prox = new Random().Next(1, 5);
            switch (prox)
            {
                case 1:
                    return Movimento.ACIMA;

                case 2:
                    return Movimento.DESCE;

                case 3:
                    return Movimento.ESQUERDA;

                case 4:
                    return Movimento.DIREITA;

                case 5:
                    return Movimento.PARADO;

                default:
                    return Movimento.DIREITA;
            }
        }
    }
}