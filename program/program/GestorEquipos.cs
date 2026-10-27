using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class GestorEquipos
    {
        private List<Equipo> equipos = new List<Equipo>();
        public List<Equipo> Equipos
        {
            get { return equipos; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de equipos no puede ser nula.");
                }
                equipos = value;
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


        public void Registrar(Equipo equipo)
        {
            if (equipo == null)
            {
                throw new ArgumentException("El equipo no puede ser nulo.");
            }
            if (SerieExiste(equipo.NumeroSerie))
            {
                throw new ArgumentException("Ya existe un equipo con el número de serie " + equipo.NumeroSerie + ".");
            }

            contadorCodigo = contadorCodigo + 1;
            equipo.CodigoInterno = "E" + contadorCodigo.ToString("000");
            equipos.Add(equipo);
        }

        public bool SerieExiste(string numeroSerie)
        {
            if (numeroSerie == null)
            {
                return false;
            }

            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].NumeroSerie.ToLower() == numeroSerie.Trim().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public List<Equipo> BuscarPorMarca(string marca)
        {
            return Buscador.BusquedaSecuencial(equipos, marca);
        }
    }
}
