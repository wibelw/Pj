using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inversion.Entidades
{
    public class EstrategiaInversion1
    {
        public static BooelanMensaje ValidarCompra(CarteraValor cartV,Compra comp)
        {
            BooelanMensaje result =new BooelanMensaje();
            result.Valor = true;


            Double MaxContratos = 0.2;
            Double distanciaMinima = -40;
            Double precioActual = comp.PrecioCompra;
      
            //  No se permite comprar mas de 0,2 contratos
            if (comp.NumCompra > MaxContratos)
            {
                result.Mensaje = $"El número de contratos de la operación ({comp.NumCompra}) supera el máximo permitdo ({MaxContratos})";
                result.Valor = false;
            }

            // Busco la operación con precio inferior realizada. 
            // Si en esta operación se pierde menos del valor permitido no se permite realizar otra compra
            Compra ultCompra = cartV.Compras.Min();
            if(ultCompra !=null)
            {
                Double beneficio = ultCompra.CalcularBeneficio(precioActual);
                if (beneficio > distanciaMinima)
                {
                    result.Mensaje = $"El beneficio actual de la última compra es {beneficio}), debe ser inferior a ({distanciaMinima})";
                    result.Valor = false;
                }

            }

            return result;
        }

    }
}
