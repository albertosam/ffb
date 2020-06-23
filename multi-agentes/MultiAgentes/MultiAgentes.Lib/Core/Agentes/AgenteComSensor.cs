using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class AgenteComSensor : Agente
    {
        public AgenteComSensor(Ambiente ambiente) : base(ambiente)
        {
        }

        public override Direcao GetDirecao()
        {
            List<Direcao> direcoes = new List<Direcao>
            {
                VerificarSituacao(this.Atual.VizinhoAcima, Direcao.SUBIR),
                VerificarSituacao(this.Atual.VizinhoAbaixo, Direcao.DESCER),
                VerificarSituacao(this.Atual.VizinhoEsquerda, Direcao.ESQUERDA),
                VerificarSituacao(this.Atual.VizinhoDireita, Direcao.DIREITA)
            };

            var naoParado = direcoes.Where(a => a != Direcao.PARADO).ToList();
            if (naoParado.Count >= 1)
            {
                var index = Util.GetNumero(naoParado.Count);
                return naoParado[index];
            }

            return Util.MovimentoAleatorio();
        }

        private Direcao VerificarSituacao(Posicao posicao, Direcao movimento) => posicao != null && !posicao.Limpo ? movimento : Direcao.PARADO;
    }
}
