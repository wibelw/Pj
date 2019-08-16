using System;
using System.Collections.Generic;
using System.Text;

namespace Inversion.Entidades
{
    public class FactoryCartera
    {
        public static Cartera GetCartera()
        {
            Compra comp;
            // Configuración cartera
            Cartera cartera = new Cartera();
            cartera.Fondos = 10067;

            // Configuración compra wall street
            CarteraValor wall = new CarteraValor(cartera) { CodValor = "Wall" };
            CarteraValor iag = new CarteraValor(cartera) { CodValor = "IAG" };
            iag.PrecioActual = 4.6160;
            wall.PrecioActual = 25911.9;


            // Compras Wall
            wall.Margen = 5;
            comp = new Compra(wall, true) { PrecioCompra = 26774.5, NumCompra = 0.4 };
            comp = new Compra(wall, true) { PrecioCompra = 26617.3, NumCompra = 0.2 };
            comp = new Compra(wall, true) { PrecioCompra = 26221.9, NumCompra = 0.2 };
            comp = new Compra(wall, true) { PrecioCompra = 25826.5, NumCompra = 0.2 };
     
            // Configuración IAG     
            iag.Margen = 20;
            comp = new Compra(iag, true) { PrecioCompra = 6.0920, NumCompra = 2000 };

      
            return cartera;
        }

    }
}
