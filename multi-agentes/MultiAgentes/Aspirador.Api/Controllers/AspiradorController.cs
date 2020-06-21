using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aspirador.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AspiradorController : ControllerBase
    {
        private readonly ILogger<AspiradorController> logger;

        public AspiradorController(ILogger<AspiradorController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return null;
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> Limpar()
        {
            return null;
        }
    }
}
