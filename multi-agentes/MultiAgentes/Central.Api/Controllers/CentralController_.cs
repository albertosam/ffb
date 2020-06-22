using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiAgentes.Lib;
using MultiAgentes.Lib.Core;
using MultiAgentes.Lib.Services;

namespace Central.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentralController : ControllerBase
    {
       
        private readonly ILogger<CentralController> _logger;

        public Simulador Simulador { get; }

        public CentralController(ILogger<CentralController> logger, Simulador simulador)
        {
            _logger = logger;
            Simulador = simulador;
        }

        /// <summary>
        /// 1. Inicializa objeto Simulador criando ambiente de acordo com as informações de dimensão;
        /// 2. Realiza marcações de sujeira em posições aleatórias do ambiente criado
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet("iniciar")]
        public IActionResult PostIniciar()
        {
            //Simulador.Inicializar(5);
            Simulador.InicializarAmbienteControlado();
            return Ok();
        }

        /// <summary>
        /// Registra o aspirador a central de comando, posicionando-o aleatoraiamente no ambiente
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpPost("registrar")]
        public Task<Posicionamento> PostRegistrar([FromBody] string nome)
        {
            Simulador.InicializarAmbienteControlado();

            var posicao = Simulador.RegistrarAgente(nome);
            return Task.FromResult(posicao.Parse());
        }

        [HttpGet("posicao")]
        public Task<Posicionamento> Posicao()
        {
            var posicao = Simulador.PosicaoAtuador();
            return Task.FromResult(posicao.Parse());
        }


        [HttpGet("proximaPosicao")]
        public Task<Posicionamento> ProximaPosicao()
        {
            var posicao = Simulador.ProximaPosicao();
            return Task.FromResult(posicao?.Parse());
        }

        [HttpPost("movimentar")]
        public Task<Posicionamento> Movimentar([FromBody]int direcao)
        {
            var posicao = Simulador.Mover((Direcao)direcao);
            return Task.FromResult(posicao.Parse());
        }

        [HttpPost("limpar")]
        public void Limpar([FromBody] Posicionamento posicao)
        {
            Simulador.Limpar(posicao.X, posicao.Y);
        }

        [HttpGet("log")]
        public List<string> Log()
        {
            return Simulador.GetLogs();
        }

        [HttpGet("stats")]
        public List<AgenteBenchmark> Stats()
        {
            return Simulador.Benchmarks;
        }

    }

}
