using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib
{
    public static class Util
    {
        public static int GetNumero(int max)
        {
            var random = new Random();
            var numero = random.Next(0, max-1);

            return numero;
        }
    }
}
