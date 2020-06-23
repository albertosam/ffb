namespace AspiradorConsole
{
    using MultiAgentes.Lib.Services;
    using Direcao = MultiAgentes.Lib.Core.Direcao;

    /// <summary>
    /// Defines the <see cref="Controlador" />.
    /// </summary>
    internal class Controlador
    {
        /// <summary>
        /// Defines the centralClient.
        /// </summary>
        private readonly ICentralClient centralClient;

        /// <summary>
        /// Defines the consolePrinter.
        /// </summary>
        private readonly IConsolePrinter consolePrinter;

        /// <summary>
        /// Defines the posicao.
        /// </summary>
        internal Posicionamento posicao;

        /// <summary>
        /// Gets or sets a value indicating whether AmbienteLimpo.
        /// </summary>
        public bool AmbienteLimpo { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controlador"/> class.
        /// </summary>
        /// <param name="centralClient">The centralClient<see cref="ICentralClient"/>.</param>
        /// <param name="consolePrinter">The consolePrinter<see cref="IConsolePrinter"/>.</param>
        public Controlador(ICentralClient centralClient, IConsolePrinter consolePrinter)
        {
            this.centralClient = centralClient;
            this.consolePrinter = consolePrinter;
        }

        /// <summary>
        /// The Registrar.
        /// </summary>
        /// <param name="modelo">The modelo<see cref="string"/>.</param>
        public void Registrar(string modelo)
        {
            this.posicao = this.centralClient.Registrar(modelo);
        }

        /// <summary>
        /// The MovimentarELimpar.
        /// </summary>
        public void MovimentarELimpar()
        {
            // seleciona posição
            var proximaPosicao = this.centralClient.ProximaPosicao();
            if (proximaPosicao == null)
            {
                this.AmbienteLimpo = true;
                this.consolePrinter.AmbienteLimpo();
                return;
            }

            // movimenta para posição selecionada
            while (proximaPosicao.Chave != posicao.Chave)
            {
                var direcao = Movimentar(proximaPosicao, posicao);
                this.posicao = this.centralClient.Movimentar((int)direcao);

                this.consolePrinter.PosicaoAtual(posicao.X, posicao.Y);
            }

            // realiza limpeza
            if (posicao.Limpo == false)
            {
                posicao.Limpo = true;
                this.centralClient.LimpezaRealizada(posicao);
                this.consolePrinter.Aspirado(posicao.X, posicao.Y);
            }
        }

        /// <summary>
        /// The Movimentar.
        /// </summary>
        /// <param name="desejada">The desejada<see cref="Posicionamento"/>.</param>
        /// <param name="atual">The atual<see cref="Posicionamento"/>.</param>
        /// <returns>The <see cref="Direcao"/>.</returns>
        private Direcao Movimentar(Posicionamento desejada, Posicionamento atual)
        {
            var difX = atual.X - desejada.X;
            var difY = atual.Y - desejada.Y;

            Direcao direcao = Direcao.PARADO;
            if (difX < 0)
            {
                direcao = Direcao.DESCER;
            }
            else if (difX > 0)
            {
                direcao = Direcao.SUBIR;
            }
            else
            {
                if (difY < 0)
                {
                    direcao = Direcao.DIREITA;
                }
                else if (difY > 0)
                {
                    direcao = Direcao.ESQUERDA;
                }
            }

            return direcao;
        }
    }
}
