using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class Logger
    {
        public List<Log> Logs { get; set; } = new List<Log>();

        private Log atual;

        public void Novo(string identificacao)
        {
            atual = new Log(identificacao);
            Logs.Add(atual);
        }

        public void Log(string mensagem)
        {
            atual.Mensagens.Add(mensagem);
        }
    }

    public class Log
    {
        public Log(string identificacao)
        {
            Identificacao = identificacao;
        }

        public string Identificacao { get; set; }
        public List<string> Mensagens { get; set; } = new List<string>();
    }
}
