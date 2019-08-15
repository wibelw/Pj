using System;
using System.Collections.Generic;

namespace Inversion.Entidades
{
    public class CarteraValor
    {
        public string CodValor { get; set; }

        public  List<Compra> Compras { get; }

        public CarteraValor()
        {
            Compras = new List<Compra>();
        }

        public double CalcularBeneficio(double PrecioActual)
        {
            double beneficio = 0;
            foreach (var c in Compras)
            {
                beneficio += c.NumCompra * (PrecioActual - c.PrecioCompra);
            }
            return beneficio;
        }
        
    }
}
