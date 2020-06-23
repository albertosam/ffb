namespace MultiAgentes.Lib.Core
{
    /// <summary>
    /// Defines the <see cref="AgenteAleatorio" />.
    /// </summary>
    public class AgenteAleatorio : Agente
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgenteAleatorio"/> class.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        public AgenteAleatorio(Ambiente ambiente) : base(ambiente)
        {
        }

        /// <summary>
        /// The GetDirecao.
        /// </summary>
        /// <returns>The <see cref="Direcao"/>.</returns>
        public override Direcao GetDirecao()
        {
            var direcao = Util.MovimentoAleatorio();
            return direcao;
        }
    }
}
