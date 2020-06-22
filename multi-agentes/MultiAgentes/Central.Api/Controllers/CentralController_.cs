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

namespace Central.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentralController : ControllerBase
    {
       
        private readonly ILogger<CentralController> _logger;

        public Simulador Simulador { get; }
        public ICentralMemoria CentralMemoria { get; }

        public CentralController(ILogger<CentralController> logger, Simulador simulador,
            ICentralMemoria centralMemoria)
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
        public IActionResult PostIniciar([FromQuery] int d, [FromQuery] int s)
        {
            Simulador.Inicializar(10);
            return Ok();
        }

        /// <summary>
        /// Registra o aspirador a central de comando, posicionando-o aleatoraiamente no ambiente
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpPost("registrar")]
        public Task<Posicao_> PostRegistrar([FromBody] string nome)
        {
            var posicao = Simulador.RegistrarAgente(nome);
            return Task.FromResult(posicao);
        }

        [HttpGet("posicao")]
        public Task<Posicao_> Posicao()
        {
            var posicao = Simulador.PosicaoAtuador();
            return Task.FromResult(posicao);
        }


        [HttpGet("proximaPosicao")]
        public Task<Posicao_> ProximaPosicao()
        {
            var posicao = Simulador.ProximaPosicao();
            return Task.FromResult(posicao);
        }

        [HttpPost("movimentar")]
        public Task<Posicao_> Movimentar([FromBody]int direcao)
        {
            var posicao = Simulador.Mover((Direcao)direcao);
            return Task.FromResult(posicao);
        }

        [HttpPost("limpar")]
        public void Limpar([FromBody] Posicao_ posicao)
        {
            Simulador.Limpar(posicao);
        }

        [HttpGet("log")]
        public List<string> Log()
        {
            return Simulador.GetLogs();
        }
    }
}
