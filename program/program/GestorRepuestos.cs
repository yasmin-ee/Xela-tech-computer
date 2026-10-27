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
