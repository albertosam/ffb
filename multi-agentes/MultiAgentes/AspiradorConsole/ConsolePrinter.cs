using System;
using System.Collections.Generic;
using System.Text;

namespace AspiradorConsole
{
    internal interface IConsolePrinter
    {
        void Print(int count);
        void Aspirado(int x, int y);
        void AmbienteLimpo();
        void PosicaoAtual(int x, int y);
    }

    internal class ConsolePrinter : IConsolePrinter
    {
        public void PosicaoAtual(int x, int y)
        {
            Console.WriteLine($"# Posição atual {x}:{y}");
        }

        public void Aspirado(int x, int y)
        {
            Console.WriteLine($"### Ambiente {x}:{y} aspirado");
        }

        public void AmbienteLimpo()
        {
            Console.WriteLine($"############# Ambiente limpo");
        }

        public void Print(int count)
        {
            Console.WriteLine($"Current Count {count}");
        }
    }
}
