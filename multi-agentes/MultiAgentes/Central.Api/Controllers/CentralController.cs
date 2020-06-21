using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiAgentes.Lib;

namespace Central.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentralController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CentralController> _logger;

        public Simulacao Simulacao { get; }
        public ICentralMemoria CentralMemoria { get; }

        public CentralController(ILogger<CentralController> logger, Simulacao simulacao,
            ICentralMemoria centralMemoria)
        {
            _logger = logger;
            Simulacao = simulacao;
            CentralMemoria = centralMemoria;
        }

        [HttpGet("iniciar")]
        public IActionResult PostIniciar([FromQuery] int d, [FromQuery] int s)
        {
            CentralMemoria.Inicializar();
            return Ok();
        }

        [HttpPost("registrar")]
        public Task<IPosicao> PostRegistrar([FromBody] string nome)
        {
            var posicao = CentralMemoria.RegistrarAspirador(nome);
            return Task.FromResult(posicao);
        }

        [HttpGet("proximaPosicao")]
        public Task<IPosicao> ProximaPosicao()
        {
            var posicao = CentralMemoria.GetProximaPosicao();
            return Task.FromResult(posicao);
        }

        [HttpPost("movimentar")]
        public Task<IPosicao> Movimentar([FromBody]int direcao)
        {
            var posicao = CentralMemoria.Movimentar((MultiAgentes.Lib.Enumeradores.Direcao)direcao);
            return Task.FromResult(posicao);
        }

        [HttpPost("limpar")]
        public void Movimentar([FromBody] Posicao posicao)
        {
            CentralMemoria.Limpar(posicao);
        }

        [HttpGet("localizacao")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
