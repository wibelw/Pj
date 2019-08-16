using Inversion.Entidades.Excel;
using System;
using System.Runtime.Serialization;

namespace Inversion.Entidades
{
    public class Compra: IComparable<Compra>
    {
        private CarteraValor carteraValor;

        public Compra(CarteraValor cart,Boolean addToCartera)
        {           
            carteraValor = cart;
            if (addToCartera)
            {
                carteraValor.Compras.Add(this);
            }            
        }

        [EpplusCol(Order = 1)]
        public double PrecioCompra { get; set; }

        [EpplusCol(Order = 2)]
        public double NumCompra { get; set; }

        [EpplusCol(Order = 3)]
        public double Margen {
            get { return NumCompra * carteraValor.PrecioActual * carteraValor.Margen/100; }
        }

        [EpplusCol(Order = 4)]
        public double Beneficio
        {
            get { return NumCompra * (carteraValor.PrecioActual - PrecioCompra); }
        }

        public int CompareTo(Compra other) { return PrecioCompra.CompareTo(other.PrecioCompra); }

        public override string ToString()
        {
            return $"PrecioCompra:{PrecioCompra},NumCompra:{NumCompra}";
        }
       
    }
}
