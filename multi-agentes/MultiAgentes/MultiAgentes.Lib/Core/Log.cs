namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Logger" />.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Gets or sets the Logs.
        /// </summary>
        public List<Log> Logs { get; set; } = new List<Log>();

        /// <summary>
        /// Defines the atual.
        /// </summary>
        private Log atual;

        /// <summary>
        /// The Novo.
        /// </summary>
        /// <param name="identificacao">The identificacao<see cref="string"/>.</param>
        public void Novo(string identificacao)
        {
            atual = new Log(identificacao);
            Logs.Add(atual);
        }

        /// <summary>
        /// The Log.
        /// </summary>
        /// <param name="mensagem">The mensagem<see cref="string"/>.</param>
        public void Log(string mensagem)
        {
            atual.Mensagens.Add(mensagem);
        }
    }

    /// <summary>
    /// Defines the <see cref="Log" />.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="identificacao">The identificacao<see cref="string"/>.</param>
        public Log(string identificacao)
        {
            Identificacao = identificacao;
        }

        /// <summary>
        /// Gets or sets the Identificacao.
        /// </summary>
        public string Identificacao { get; set; }

        /// <summary>
        /// Gets or sets the Mensagens.
        /// </summary>
        public List<string> Mensagens { get; set; } = new List<string>();
    }
}
