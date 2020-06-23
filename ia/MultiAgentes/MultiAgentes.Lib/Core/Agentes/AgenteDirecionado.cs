namespace MultiAgentes.Lib.Core
{
    /// <summary>
    /// Defines the <see cref="AgenteDirecionado" />.
    /// </summary>
    public class AgenteDirecionado : Agente
    {
        /// <summary>
        /// Defines the ambiente.
        /// </summary>
        private readonly Ambiente ambiente;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgenteDirecionado"/> class.
        /// </summary>
        /// <param name="ambiente">The ambiente<see cref="Ambiente"/>.</param>
        public AgenteDirecionado(Ambiente ambiente) : base(ambiente)
        {
            this.ambiente = ambiente;
        }

        /// <summary>
        /// The GetDirecao.
        /// </summary>
        /// <returns>The <see cref="Direcao"/>.</returns>
        public override Direcao GetDirecao()
        {
            Perceptor p = ambiente.GetAgentePerceptor();
            if (p.TudoLimpo())
                return Direcao.PARADO;

            var proxima = p.Proxima();
            var direcao = Util.MovimentoDirecionado(proxima.X, proxima.Y, Atual.X, Atual.Y);
            return direcao;
        }
    }
}
