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

        //considerando como mais eficiente o inverso multiplicativo (1/x) onde x é o número de
        //movimentos realizados dividido pelo número de limpezas
        public decimal Coeficiente => decimal.Round(1 / (Movimentos / Limpezas), 10);
    }
}
