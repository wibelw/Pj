using Inversion.Entidades.Excel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;

namespace Inversion.Entidades
{
    public class Cartera
    {
        public readonly List<CarteraValor> Valores;

        [EpplusCol(Order = 1)]
        public double Fondos { get; set; }

        [EpplusCol(Order = 2)]
        public double SaldoDisponible => Fondos - MargenTotal + BeneficioTotal;

        [EpplusCol(Order = 3)]
        public double MargenTotal => Valores.Sum(c => c.MargenTotal);

        [EpplusCol(Order = 4)]
        public double BeneficioTotal => Valores.Sum(c => c.BeneficioTotal);


        public Cartera()
        {
            Valores = new List<CarteraValor>();
        }
        public void ToExcel(ExcelPackage pck)
        {
            foreach (var valor in Valores)
            {
                valor.ComprasToExcel(pck);
                valor.TotalToExcel(pck);
            }
            List<Cartera> lista = new List<Cartera> { this };
            UtilExcel.ListToExcel(pck, "Cartera", lista);
                    
        }


    }
}

