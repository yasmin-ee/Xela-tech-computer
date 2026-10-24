using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class GestorRepuestos
    {
        private List<Repuesto> repuestos = new List<Repuesto>();
        public List<Repuesto> Repuestos
        {
            get { return repuestos; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de repuestos no puede ser nula.");
                }
                repuestos = value;
            }
        }

        // Último número usado para generar el código (R001, R002, ...)
        private int contadorCodigo;
        public int ContadorCodigo
        {
            get { return contadorCodigo; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El contador de código no puede ser negativo.");
                }
                contadorCodigo = value;
            }
        }

        // +Registrar(Repuesto)
        // Le asigna al repuesto su código único y lo guarda en la lista
        public void Registrar(Repuesto repuesto)
        {
            if (repuesto == null)
            {
                throw new ArgumentException("El repuesto no puede ser nulo.");
            }

            contadorCodigo = contadorCodigo + 1;
            repuesto.Codigo = "R" + contadorCodigo.ToString("000");
            repuestos.Add(repuesto);
        }

        // +ObtenerBajoMinimo() : List<Repuesto>   (existencia mínima o agotados)
        public List<Repuesto> ObtenerBajoMinimo()
        {
            List<Repuesto> resultado = new List<Repuesto>();

            for (int i = 0; i < repuestos.Count; i++)
            {
                if (repuestos[i].EstaBajoMinimo())
                {
                    resultado.Add(repuestos[i]);
                }
            }
            return resultado;
        }
    }
}