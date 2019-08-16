using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using System;
using System.Diagnostics;

namespace Inversion.Entidades.Test
{
    [TestClass]
    public class CarteraValorTest
    {

  
        [TestMethod]
        public void GenerarExcel()
        {
            Cartera cartera = FactoryCartera.GetCartera();
            string pathFile= @"c:\tmp\cartera.xlsx";
            ExcelPackage pck = UtilExcel.CrearExcelPackage(pathFile);
            cartera.ToExcel(pck);
            UtilExcel.OpenMicrosoftExcel(pathFile);     
        }

        [TestMethod]
        public void Estrategia1()
        {
            Cartera cartera = FactoryCartera.GetCartera();
            EstrategiaInversion1 estrategia = new EstrategiaInversion1(cartera, "Wall");

            string pathFile = @"c:\tmp\cartera.xlsx";
            ExcelPackage pck = UtilExcel.CrearExcelPackage(pathFile);
            cartera.ToExcel(pck);
            UtilExcel.OpenMicrosoftExcel(pathFile);
            //Compra compraAValidar = new Compra(cartValor) { PrecioCompra = 25594.5, NumCompra = 0.2 };
            //BooelanMensaje resultado= EstrategiaInversion1.ValidarCompra(cartValor, compraAValidar);
            //            Assert.AreEqual(resultado.Valor, false);
        }


    }
}
