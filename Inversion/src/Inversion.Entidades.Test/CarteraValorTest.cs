using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Inversion.Entidades.Test
{
    [TestClass]
    public class CarteraValorTest
    {
        [TestMethod]
        public void Test1()
        {

            double precioActual = 25631.6;
            CarteraValor cartValor = new CarteraValor() { CodValor = "Wall" };
            cartValor.Compras.Add(new Compra() { PrecioCompra = 26774.5, NumCompra = 0.4});
            cartValor.Compras.Add(new Compra() { PrecioCompra = 26617.3, NumCompra = 0.2 });
            cartValor.Compras.Add(new Compra() { PrecioCompra = 26221.9, NumCompra = 0.2 });
            cartValor.Compras.Add(new Compra() { PrecioCompra = 25940.9, NumCompra = 0.4 });
            cartValor.Compras.Add(new Compra() { PrecioCompra = 25789.6, NumCompra = 0.2 });
            cartValor.Compras.Add(new Compra() { PrecioCompra = 25594.5, NumCompra = 0.2 });


            double valor = cartValor.CalcularBeneficio(precioActual);
            Debug.WriteLine($"Valor Cartera:{valor}");
            Assert.AreEqual(valor, 40);

            Compra comp2 = new Compra() { PrecioCompra = 25100, NumCompra = 0.2 };
            BooelanMensaje resultado= EstrategiaInversion1.ValidarCompra(cartValor, comp2);
            Assert.AreEqual(resultado.Valor, false);

        }
    }
}
