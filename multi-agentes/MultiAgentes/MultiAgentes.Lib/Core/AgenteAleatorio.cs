using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class AgenteAleatorio : Agente_
    {
        public AgenteAleatorio(Ambiente_ ambiente) : base(ambiente)
        {
        }

        public override Direcao GetDirecao()
        {
            var direcao = Util.MovimentoAleatorio();
            return direcao;
        }
    }
}
