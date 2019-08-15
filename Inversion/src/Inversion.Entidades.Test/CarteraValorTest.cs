using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inversion.Entidades.Test
{
    [TestClass]
    public class CarteraValorTest
    {
        [TestMethod]
        public void Test1()
        {

            CarteraValor cartValor = new CarteraValor() { CodValor = "Wall" };
            cartValor.Compras.Add(new Compra() { PrecioCompra = 25000, NumCompra = 0.2});



            double valor = cartValor.CalcularBeneficio(25200);

            Assert.AreEqual(valor, 40);

        }
    }
}
