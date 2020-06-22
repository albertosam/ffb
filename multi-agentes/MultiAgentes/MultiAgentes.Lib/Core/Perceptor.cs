using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Perceptor
    {
        private List<Posicao> posicoesSujas = new List<Posicao>();

        public void RemoveSujo(Posicao posicao)
        {
            var pos = posicoesSujas.SingleOrDefault(a => a.Chave == posicao.Chave);
            posicoesSujas.Remove(pos);
        }

        public void AddSujo(Posicao posicao)
        {
            if (!posicoesSujas.Any(a => a.Chave == posicao.Chave))
                posicoesSujas.Add(posicao);
        }

        public bool TudoLimpo()
        {
            return posicoesSujas.Count == 0;
        }

        public Posicao Proxima()
        {
            return posicoesSujas.FirstOrDefault();
        }
    }
}
