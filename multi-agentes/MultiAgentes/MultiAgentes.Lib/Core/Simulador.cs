using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Simulador
    {
        private Logger _logger = new Logger();

        public Ambiente Ambiente { get; set; }
        public Agente Agente { get; set; }
        public Perceptor Perceptor { get; set; }
        public List<AgenteStats> Benchmarks { get; set; } = new List<AgenteStats>();

        public void Inicializar(int dimensao)
        {
            _logger.Novo("Simulação");
            _logger.Log($"Inicializado simulação. Dimensão: {dimensao}");

            Ambiente = Ambiente.Criar(dimensao);
            Ambiente.SujarAletorio(Ambiente, 1);
            Perceptor = Ambiente.GetAgentePerceptor();
        }

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

        public Posicao Mover(Direcao direcao)
        {
            var posicao = Ambiente.Movimentar(direcao);
            _logger.Log($"Posição atual [{posicao.X},{posicao.Y}]");
            return posicao;
        }

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

        public void Backup()
        {
            Benchmarks.Add(new AgenteStats
            {
                Nome = Agente.Nome,
                Limpezas = Agente.Limpezas,
                Movimentos = Agente.Movimentacoes,
                Historico = Agente.Movimentos.Select(a => $" {a.Direcao} -> [{a.X}, {a.Y}] ").ToList()
            });
        }

        public Posicao PosicaoAtuador()
        {
            return Ambiente.Atuador;
        }

        public void Limpar(int x, int y)
        {
            //var posicao = Ambiente.GetPosicao(x, y);
            //Perceptor.RemoveSujo(posicao);

            Agente.Limpar();
            Perceptor = Ambiente.GetAgentePerceptor();

            _logger.Log($"Posição [{x},{y}] limpa");
        }

        public List<string> GetLogs()
        {
            var mensgens = _logger.Logs.SelectMany(a => a.Mensagens).ToList();
            return mensgens;
        }

        public Agente AgenteAleatorio()
        {
            return new AgenteAleatorio(this.Ambiente) { Nome = "Agente Aleatório" };
        }

        public Agente AgenteComSensor()
        {
            return new AgenteComSensor(this.Ambiente) { Nome = "Agente Com Sensor" };
        }

        public Agente AgenteDirecionado()
        {
            return new AgenteDirecionado(this.Ambiente) { Nome = "Agente Direcionado" };
        }
    }
}
