using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace MultiAgentes.Lib.Core
{
    public class AgenteBenchmark
    {
        public string Nome { get; set; }
        public int Movimentos { get; set; }
        public int Limpezas { get; set; }
        public decimal Razao => decimal.Divide(Movimentos, Limpezas);

        //considerando como mais eficiente o inverso multiplicativo (1/x) onde x é o número de
        //movimentos realizados dividido pelo número de limpezas
        public decimal Coeficiente => decimal.Round(decimal.Divide(1M, Razao), 5);
    }
}
