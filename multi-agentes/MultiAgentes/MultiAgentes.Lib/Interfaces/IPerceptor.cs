using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib
{

    public interface IPerceptor
    {
        int X { get; set; }
        int Y { get; set; }
        bool Limpo { get; set; }
    }
}
