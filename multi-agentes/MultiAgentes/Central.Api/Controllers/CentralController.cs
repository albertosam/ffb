namespace Central.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MultiAgentes.Lib;
    using MultiAgentes.Lib.Core;
    using MultiAgentes.Lib.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CentralController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CentralController : ControllerBase
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<CentralController> _logger;

        /// <summary>
        /// Gets the Simulador.
        /// </summary>
        public Simulador Simulador { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CentralController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{CentralController}"/>.</param>
        /// <param name="simulador">The simulador<see cref="Simulador"/>.</param>
        public CentralController(ILogger<CentralController> logger, Simulador simulador)
        {
            _logger = logger;
            Simulador = simulador;
        }

        /// <summary>
        /// Registra o aspirador a central de comando.
        /// Informações de ambiente são carregadas e realizada marcações sobe posições sujas.
        /// </summary>
        /// <param name="nome">.</param>
        /// <returns>.</returns>
        [HttpPost("registrar")]
        public Task<Posicionamento> PostRegistrar([FromBody] string nome)
        {
            // inicializa simulador
            // carregando informações do ambiente e do agente
            Simulador.InicializarAmbienteControlado();

            var posicao = Simulador.RegistrarAgente(nome);
            return Task.FromResult(posicao.Parse());
        }

        /// <summary>
        /// Posição atual do aspirador.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("posicao")]
        public Task<Posicionamento> Posicao()
        {
            var posicao = Simulador.PosicaoAtuador();
            return Task.FromResult(posicao.Parse());
        }

        /// <summary>
        /// Obtém proxima posição a ser assumida.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("proximaPosicao")]
        public Task<Posicionamento> ProximaPosicao()
        {
            var posicao = Simulador.ProximaPosicao();
            return Task.FromResult(posicao?.Parse());
        }

        /// <summary>
        /// Realiza movimento.
        /// </summary>
        /// <param name="direcao">.</param>
        /// <returns>.</returns>
        [HttpPost("movimentar")]
        public Task<Posicionamento> Movimentar([FromBody] int direcao)
        {
            var posicao = Simulador.Mover((Direcao)direcao);
            return Task.FromResult(posicao.Parse());
        }

        /// <summary>
        /// Informa limpeza de posição.
        /// </summary>
        /// <param name="posicao">.</param>
        [HttpPost("limpar")]
        public void Limpar([FromBody] Posicionamento posicao)
        {
            Simulador.Limpar(posicao.X, posicao.Y);
        }

        /// <summary>
        /// Logs registrados.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("log")]
        public List<string> Log()
        {
            return Simulador.GetLogs();
        }

        /// <summary>
        /// Informações sobre perfomance registradas nas simulações.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("stats")]
        public List<AgenteStats> Stats()
        {
            return Simulador.Benchmarks;
        }
    }
}
