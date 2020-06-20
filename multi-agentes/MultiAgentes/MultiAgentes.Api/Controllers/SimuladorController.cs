using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiAgentes.Api.Application;
using MultiAgentes.Api.Application.Core;

namespace MultiAgentes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimuladorController : ControllerBase
    {


        private readonly ILogger<SimuladorController> _logger;

        public SimuladorController(ILogger<SimuladorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            var tabuleiro = TabuleiroConstruir.Construir(10);
            var agenteLimpeza = AgenteContruir.AgenteLimpeza("aspirador", tabuleiro, _logger);
            var agenteSujeira = AgenteContruir.AgenteSujeira("criança", tabuleiro, _logger);

            //tabuleiro.Posicoes[2, 5].Sujo = true;
            agenteLimpeza.Atual = tabuleiro.Posicoes[2, 0];
            agenteSujeira.Atual = tabuleiro.Posicoes[2, 5];


            ThreadStart criancaThreadDelegate = new ThreadStart(agenteSujeira.Rodar);
            Thread crianca = new Thread(criancaThreadDelegate);
            crianca.Start();
            crianca.Interrupt();

            ThreadStart aspiradorThreadDelegate = new ThreadStart(agenteLimpeza.Rodar);
            Thread aspirador = new Thread(aspiradorThreadDelegate);
            aspirador.Start();

      

        }
    }
}
