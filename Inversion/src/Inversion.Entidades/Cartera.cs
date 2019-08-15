using System;
using System.Collections.Generic;
using System.Text;

namespace Inversion.Entidades
{
    public class Cartera
    {
        public readonly List<CarteraValor> Valores;

        public Cartera()
        {
            Valores = new List<CarteraValor>();
        }


    }
}
