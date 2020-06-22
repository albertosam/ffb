using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Simulador
    {
        private Logger _logger = new Logger();

        public Ambiente_ Ambiente { get; set; }
        public Agente_ Agente { get; set; }
        public Perceptor Perceptor { get; set; }

        public void Inicializar(int dimensao)
        {
            _logger.Novo("Simulação");
            _logger.Log($"Inicializado simulação. Dimensão: {dimensao}");

            Ambiente = Ambiente_.Criar(dimensao);
            Ambiente_.SujarAletorio(Ambiente, 3);

            Perceptor = Ambiente.GetAgentePerceptor();
        }

        public Posicao_ RegistrarAgente(string nome)
        {
            Agente = new Agente_(nome);
            var x = Util.GetNumero(Ambiente.Dimensao);
            var y = Util.GetNumero(Ambiente.Dimensao);

            var posicao = Ambiente.SetPosicaoAgente(x, y);

            _logger.Log($"{nome} posicionado em [{posicao.X}, {posicao.Y}]");
            return posicao;
        }

        public Posicao_ Mover(Direcao direcao)
        {
            var posicao = Ambiente.Movimentar(direcao);
            _logger.Log($"Posição atual [{posicao.X},{posicao.Y}]");
            return posicao;
        }

        public Posicao_ ProximaPosicao()
        {
            return Perceptor.Proxima();
        }

        public Posicao_ PosicaoAtuador()
        {
            return Ambiente.Atuador;
        }

        public void Limpar(Posicao_ posicao)
        {
            Ambiente.Limpar(posicao.X, posicao.Y);
            Perceptor.RemoveSujo(posicao);
            _logger.Log($"Posição [{posicao.X},{posicao.Y}] limpa");
        }


        public List<string> GetLogs()
        {
            var mensgens = _logger.Logs.SelectMany(a => a.Mensagens).ToList();
            return mensgens;
        }
    }
}
