using System;
using System.Collections.Generic;
using System.Text;

namespace Inversion.Entidades.App
{
     class Program
    {
        public static void Main(string[] args)
        {
            bool salir = false;
            Cartera cartera = FactoryCartera.GetCartera();
            EstrategiaInversion1 estrategia = new EstrategiaInversion1(cartera, "Wall");
            string comando;
            Console.WriteLine("Inserta comando");
            while (!salir) { 
                comando = Console.ReadLine();
                if(comando.Equals("q")) { salir = true; }
                else {
                   estrategia.carteraValor.PrecioActual = Double.Parse(comando);
                  Compra comp= estrategia.CalcularProximaCompra();
                    Console.WriteLine(String.Format("Próxima compra:{0}",comp.ToString()));
                }
            }   
        }
    }
}
