namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AgenteStats" />.
    /// </summary>
    public class AgenteStats
    {
        /// <summary>
        /// Gets or sets the Nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Gets or sets the Movimentos.
        /// </summary>
        public int Movimentos { get; set; }

        /// <summary>
        /// Gets or sets the Limpezas.
        /// </summary>
        public int Limpezas { get; set; }

        /// <summary>
        /// Gets the Razao.
        /// </summary>
        public decimal Razao => decimal.Divide(Movimentos, Limpezas);

        //considerando como mais eficiente o inverso multiplicativo (1/x) onde x é o número de
        //movimentos realizados dividido pelo número de limpezas
        /// <summary>
        /// Gets the Coeficiente.
        /// </summary>
        public decimal Coeficiente => decimal.Round(decimal.Divide(1M, Razao), 5);

        /// <summary>
        /// Gets or sets the Historico.
        /// </summary>
        public List<string> Historico { get; set; } = new List<string>();
    }
}
