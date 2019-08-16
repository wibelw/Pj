using Inversion.Entidades.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Inversion.Entidades
{
    public class CarteraValor
    {
        private Cartera cartera;

        [EpplusCol(Order = 1)]
        public string CodValor { get; set; }
        [EpplusCol(Order = 2)]
        public double PrecioActual { get; set; }

        [EpplusCol(Order = 3)]
        public double PrecioCompraMedio
        {
            get
            {
                return Compras.Sum(c => c.PrecioCompra * c.NumCompra) / TotalNumCompra;
            }
        }
        [EpplusCol(Order = 4)]
        public double Margen { get; set; }

        [EpplusCol(Order = 5)]
        public double MargenTotal
        {
            get
            {
                return Compras.Sum(c => c.Margen);
            }
        }

        [EpplusCol(Order = 6)]
        public double BeneficioTotal
        {
            get {
                return Compras.Sum(c => c.Beneficio);
            }
        }

 

        [EpplusCol(Order = 7)]
        public double TotalNumCompra
        {
            get
            {
                return Compras.Sum(c => c.NumCompra);
            }
        }



        public  List<Compra> Compras { get; }

        public CarteraValor(Cartera cart)
        {
            cartera = cart;
            cartera.Valores.Add(this);
            Compras = new List<Compra>();
        }

     
        public void ComprasToExcel(ExcelPackage pack)
        {
            UtilExcel.ListToExcel(pack,$"{CodValor} Compras", Compras);
        }

        public void TotalToExcel(ExcelPackage pack)
        {
            List<CarteraValor> lista = new List<CarteraValor>{this};
            UtilExcel.ListToExcel(pack, $"{CodValor} Total", lista);
        }

    }
}
