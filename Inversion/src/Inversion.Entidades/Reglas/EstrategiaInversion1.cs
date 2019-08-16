using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inversion.Entidades
{
    public class EstrategiaInversion1
    {
        public Cartera cartera { get; }
        public CarteraValor carteraValor { get; }
        public EstrategiaInversion1(Cartera cart,string codValor)
        {
            cartera = cart;
            carteraValor = (from CarteraValor valor in cart.Valores where valor.CodValor.Equals(codValor) select valor).First();
        }

        public Compra GetCompraSuperior()
        {
            Compra comp = (from Compra c in carteraValor.Compras
                           where c.PrecioCompra > carteraValor.PrecioActual
                           orderby c.PrecioCompra ascending
                           select c
                           ).FirstOrDefault();
            return comp;
        }

        public Compra GetCompraInferior()
        {
            Compra comp = (from Compra c in carteraValor.Compras
                           where c.PrecioCompra < carteraValor.PrecioActual
                           orderby c.PrecioCompra descending
                           select c
                           ).FirstOrDefault();
            return comp;
        }



        public double CalcularDistancia(Compra compra)
        {
            if (compra == null)
            {
                return 0;
            }
            else
            {
                return compra.NumCompra * (compra.PrecioCompra - carteraValor.PrecioActual);
            }            
        }
        public double MinDistanciaConSuperior()
        {
            return 20;
        }

        public double MinDistanciaConInferior()
        {
            return 20;
        }

        public double NumContratosComprar()
        {
            return 0.2;
        }


        public Compra CalcularProximaCompra()
        {
            
            Compra proximaCompra = new Compra(carteraValor, false);

            Compra compraSuperior = GetCompraSuperior();
            Compra compraInferior = GetCompraInferior();

         //   double distanciaSuperior = CalcularDistancia(compraSuperior);
          //  double distanciaInferior = CalcularDistancia(compraInferior);
            double precioSuperiorCalculado=double.MaxValue;
            double precioInferiorCalculado = double.MaxValue;
            if (compraSuperior != null)
            {
                precioSuperiorCalculado = compraSuperior.PrecioCompra - MinDistanciaConSuperior();
                proximaCompra.PrecioCompra = Math.Min(precioSuperiorCalculado, carteraValor.PrecioActual);
            }
            if (compraInferior != null)
            {
                precioInferiorCalculado = compraInferior.PrecioCompra - MinDistanciaConInferior();
                proximaCompra.PrecioCompra = Math.Min(precioInferiorCalculado, carteraValor.PrecioActual);
            }
            return proximaCompra;
        }
    }
}
