namespace MultiAgentes.Lib.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Simulador" />.
    /// </summary>
    public class Simulador
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private Logger _logger = new Logger();

        /// <summary>
        /// Gets or sets the Ambiente.
        /// </summary>
        public Ambiente Ambiente { get; set; }

        /// <summary>
        /// Gets or sets the Agente.
        /// </summary>
        public Agente Agente { get; set; }

        /// <summary>
        /// Gets or sets the Perceptor.
        /// </summary>
        public Perceptor Perceptor { get; set; }

        /// <summary>
        /// Gets or sets the Benchmarks.
        /// </summary>
        public List<AgenteStats> Benchmarks { get; set; } = new List<AgenteStats>();

        /// <summary>
        /// The Inicializar.
        /// </summary>
        /// <param name="dimensao">The dimensao<see cref="int"/>.</param>
        public void Inicializar(int dimensao)
        {
            _logger.Novo("Simulação");
            _logger.Log($"Inicializado simulação. Dimensão: {dimensao}");

            Ambiente = Ambiente.Criar(dimensao);
            Ambiente.SujarAletorio(Ambiente, 1);
            Perceptor = Ambiente.GetAgentePerceptor();
        }

        /// <summary>
        /// The InicializarAmbienteControlado.
        /// </summary>
        public void InicializarAmbienteControlado()
        {
            var dimensao = 5;

            _logger.Novo("Simulação Com Ambiente Controlado");
            _logger.Log($"Dimensão: {dimensao}");

            Ambiente = Ambiente.Criar(dimensao);
            Ambiente.Sujar(3, 2);
            Ambiente.Sujar(1, 4);
            Ambiente.Sujar(4, 4);

            Perceptor = Ambiente.GetAgentePerceptor();
        }

        /// <summary>
        /// The RegistrarAgente.
        /// </summary>
        /// <param name="codigo">The codigo<see cref="string"/>.</param>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao RegistrarAgente(string codigo)
        {
            switch (codigo)
            {
                case "A":
                    Agente = AgenteAleatorio();
                    break;
                case "B":
                    Agente = AgenteComSensor();
                    break;
                case "C":
                    Agente = AgenteDirecionado();
                    break;

                default:
                    Agente = AgenteAleatorio();
                    break;
            }

            //var x = Util.GetNumero(Ambiente.Dimensao);
            //var y = Util.GetNumero(Ambiente.Dimensao);
            //var posicao = Ambiente.SetPosicaoAgente(x, y);

            var posicao = Ambiente.SetPosicaoAgente(1, 2);
            this.Agente.Atual = posicao;

            _logger.Log($"{Agente.Nome} posicionado em [{posicao.X}, {posicao.Y}]");
            return posicao;
        }

        /// <summary>
        /// The Mover.
        /// </summary>
        /// <param name="direcao">The direcao<see cref="Direcao"/>.</param>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao Mover(Direcao direcao)
        {
            var posicao = Ambiente.Movimentar(direcao);
            _logger.Log($"Posição atual [{posicao.X},{posicao.Y}]");
            return posicao;
        }

        /// <summary>
        /// The ProximaPosicao.
        /// </summary>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao ProximaPosicao()
        {
            if (Perceptor.TudoLimpo())
            {
                Backup();
                return null;
            }

            var posicao = Agente.Mover();

            _logger.Log($"Posição atual [{posicao.X},{posicao.Y}]");
            return posicao;
        }

        /// <summary>
        /// The Backup.
        /// </summary>
        public void Backup()
        {
            Benchmarks.Add(new AgenteStats
            {
                Nome = Agente.Nome,
                Limpezas = Agente.Limpezas,
                Movimentos = Agente.Movimentacoes,
                Historico = Agente.Movimentos.Select(a => $" {a.Direcao} -> [{a.X}, {a.Y}] {(a.Limpo ? "[]" : "[Sujo]")} ").ToList()
            });
        }

        /// <summary>
        /// The PosicaoAtuador.
        /// </summary>
        /// <returns>The <see cref="Posicao"/>.</returns>
        public Posicao PosicaoAtuador()
        {
            return Ambiente.Atuador;
        }

        /// <summary>
        /// The Limpar.
        /// </summary>
        /// <param name="x">The x<see cref="int"/>.</param>
        /// <param name="y">The y<see cref="int"/>.</param>
        public void Limpar(int x, int y)
        {
            //var posicao = Ambiente.GetPosicao(x, y);
            //Perceptor.RemoveSujo(posicao);

            Agente.Limpar();
            Perceptor = Ambiente.GetAgentePerceptor();

            _logger.Log($"Posição [{x},{y}] limpa");
        }

        /// <summary>
        /// The GetLogs.
        /// </summary>
        /// <returns>The <see cref="List{string}"/>.</returns>
        public List<string> GetLogs()
        {
            var mensgens = _logger.Logs.SelectMany(a => a.Mensagens).ToList();
            return mensgens;
        }

        /// <summary>
        /// The AgenteAleatorio.
        /// </summary>
        /// <returns>The <see cref="Agente"/>.</returns>
        public Agente AgenteAleatorio()
        {
            return new AgenteAleatorio(this.Ambiente) { Nome = "Agente Aleatório" };
        }

        /// <summary>
        /// The AgenteComSensor.
        /// </summary>
        /// <returns>The <see cref="Agente"/>.</returns>
        public Agente AgenteComSensor()
        {
            return new AgenteComSensor(this.Ambiente) { Nome = "Agente Com Sensor" };
        }

        /// <summary>
        /// The AgenteDirecionado.
        /// </summary>
        /// <returns>The <see cref="Agente"/>.</returns>
        public Agente AgenteDirecionado()
        {
            return new AgenteDirecionado(this.Ambiente) { Nome = "Agente Direcionado" };
        }
    }
}
