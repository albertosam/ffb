using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class AgenteAleatorio : Agente
    {
        public AgenteAleatorio(Ambiente ambiente) : base(ambiente)
        {
        }

        public override Direcao GetDirecao()
        {
            var direcao = Util.MovimentoAleatorio();
            return direcao;
        }
    }
}
