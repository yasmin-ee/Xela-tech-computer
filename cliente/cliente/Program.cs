using System;
using System.Collections.Generic;
// equipo
namespace XelaTech
{
    public class GestorEquipos
    {
        private List<Equipo> equipos = new List<Equipo>();

        public int Cantidad
        {
            get { return equipos.Count; }
        }

        public List<Equipo> ObtenerTodos()
        {
            return equipos;
        }

        // Registrar: impide códigos internos y números de serie duplicados
        public void Registrar(Equipo nuevo)
        {
            if (nuevo == null)
            {
                throw new ArgumentException("El equipo no puede ser nulo.");
            }
            if (BuscarPorCodigo(nuevo.CodigoInterno) != null)
            {
                throw new ArgumentException("Ya existe un equipo con el código " + nuevo.CodigoInterno + ".");
            }
            if (ExisteNumeroSerie(nuevo.NumeroSerie))
            {
                throw new ArgumentException("Ya existe un equipo con el número de serie " + nuevo.NumeroSerie + ".");
            }
            equipos.Add(nuevo);
        }

        public bool ExisteNumeroSerie(string numeroSerie)
        {
            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].NumeroSerie == numeroSerie)
                {
                    return true;
                }
            }
            return false;
        }

        // Búsqueda secuencial por código (devuelve null si no existe)
        public Equipo BuscarPorCodigo(string codigo)
        {
            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].CodigoInterno == codigo)
                {
                    return equipos[i];
                }
            }
            return null;
        }

        // Búsqueda secuencial por marca: devuelve TODAS las coincidencias
        public List<Equipo> BuscarPorMarca(string marca)
        {
            List<Equipo> resultado = new List<Equipo>();

            if (marca == null || marca.Trim() == "")
            {
                throw new ArgumentException("Debe ingresar una marca para buscar.");
            }

            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].Marca.ToLower().Contains(marca.Trim().ToLower()))
                {
                    resultado.Add(equipos[i]);
                }
            }
            return resultado;
        }
    }
}