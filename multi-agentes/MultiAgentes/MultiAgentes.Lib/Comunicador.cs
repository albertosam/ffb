using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public class Comunicador : IComunicador
    {
        private List<IPosicao> _perceptores = new List<IPosicao>();
        public void Limpar(IPosicao posicao)
        {
            if (!_perceptores.Contains(posicao))
                _perceptores.Add(posicao);
        }

        public void LimpezaRealizada(IPosicao posicao)
        {
            var aRemover = _perceptores.Where(x => x.X == posicao.X && x.Y == posicao.Y).FirstOrDefault();
            _perceptores.Remove(aRemover);
        }

        public IPosicao Proximo()
        {
           return  _perceptores.FirstOrDefault();
        }
    }
}
