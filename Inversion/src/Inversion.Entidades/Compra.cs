using System;

namespace Inversion.Entidades
{
    public class Compra: IComparable<Compra>
    {
        public double PrecioCompra { get; set; }

        public double NumCompra { get; set; }

        public int CompareTo(Compra other) { return PrecioCompra.CompareTo(other.PrecioCompra); }


        public double CalcularBeneficio(double precioActual)
        {
             return  NumCompra * (precioActual - PrecioCompra);
        }
    }
}
