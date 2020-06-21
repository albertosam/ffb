using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAgentes.Lib
{
    public class AtuadorAgente : Agente
    {
        private string nome;
        public IPosicao Posicao { get; set; }

        public AtuadorAgente(string nome)
        {
            this.nome = nome;
        }

        public override void Comunicar(IPosicao posicao)
        {
            this.Comunicador.LimpezaRealizada(posicao);
        }

        public override void Executar(IPosicao posicao)
        {
            this.Posicao.Limpo = true;
            this.Comunicar(posicao);
        }
    }
}
