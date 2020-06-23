namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="AgenteComSensor" />.
    /// </summary>
    public class AgenteComSensor : Agente
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgenteComSensor"/> class.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        public AgenteComSensor(Ambiente ambiente) : base(ambiente)
        {
        }

        /// <summary>
        /// The GetDirecao.
        /// </summary>
        /// <returns>The <see cref="Direcao"/>.</returns>
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
            if (naoParado.Count == 1)
                return naoParado.First();
            else if (naoParado.Count >= 1)
            {
                var index = Util.GetNumero(naoParado.Count);
                return naoParado[index];
            }

            return Util.MovimentoAleatorio();
        }

        /// <summary>
        /// The VerificarSituacao.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicao"/>.</param>
        /// <param name="movimento">The movimento<see cref="Direcao"/>.</param>
        /// <returns>The <see cref="Direcao"/>.</returns>
        private Direcao VerificarSituacao(Posicao posicao, Direcao movimento) => posicao != null && !posicao.Limpo ? movimento : Direcao.PARADO;
    }
}
