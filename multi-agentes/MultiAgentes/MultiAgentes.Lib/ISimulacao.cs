using MultiAgentes.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib
{
    public class Simulacao
    {
        private AtuadorAgente _atuador;
        private PerceptorAgente _perceptor;
        public Simulacao()
        {
        }

        public IAmbiente Ambiente { get; protected set; }

        public void Iniciar()
        {
            //ThreadStart aspiradorThreadDelegate = new ThreadStart(agenteLimpeza.Rodar);
            //Thread aspirador = new Thread(aspiradorThreadDelegate);
            //aspirador.Start();
        }

        public void ConfigurarAmbiente(int dimensao, int sujeira)
        {
            Ambiente = MultiAgentes.Lib.Ambiente.Criar(dimensao);
            _perceptor = new PerceptorAgente();

            Ambiente.AddAgente(_perceptor);

            MultiAgentes.Lib.Ambiente.Sujar(Ambiente, sujeira);
        }

        public void AddAspirador(string nome)
        {
            var atuador = new AtuadorAgente(nome);
            Ambiente.AddAgente(atuador);

            _atuador = atuador;
        }
    }
}
