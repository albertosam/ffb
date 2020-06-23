using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class AgenteDirecionado : Agente
    {
        private readonly Ambiente ambiente;

        public AgenteDirecionado(Ambiente ambiente) : base(ambiente)
        {
            this.ambiente = ambiente;
        }

        public override Direcao GetDirecao()
        {
            Perceptor p = ambiente.GetAgentePerceptor();
            if (p.TudoLimpo())
                return Direcao.PARADO;

            var proxima = p.Proxima();
            var direcao = Util.MovimentoDirecionado(proxima.X, proxima.Y, Atual.X, Atual.Y);
            return direcao;
        }

        private Direcao VerificarSituacao(Posicao posicao, Direcao movimento) => posicao != null && !posicao.Limpo ? movimento : Direcao.PARADO;
    }
}
