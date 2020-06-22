using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Perceptor
    {
        private List<Posicao_> posicoesSujas = new List<Posicao_>();

        public void RemoveSujo(Posicao_ posicao)
        {
            var pos = posicoesSujas.SingleOrDefault(a => a.Chave == posicao.Chave);
            posicoesSujas.Remove(pos);
        }

        public void AddSujo(Posicao_ posicao)
        {
            if (!posicoesSujas.Any(a => a.Chave == posicao.Chave))
                posicoesSujas.Add(posicao);
        }

        public bool TudoLimpo()
        {
            return posicoesSujas.Count == 0;
        }

        public Posicao_ Proxima()
        {
            return posicoesSujas.FirstOrDefault();
        }
    }
}
