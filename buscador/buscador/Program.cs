using System;
using System.Collections.Generic;

namespace XelaTech
{
    public static class Buscador
    {
        // +BusquedaSecuencial(lista, texto) : List
        // Sobrecarga 1: clientes por nombre parcial. Devuelve TODAS las coincidencias.
        public static List<Cliente> BusquedaSecuencial(List<Cliente> lista, string texto)
        {
            ValidarBusqueda(lista, texto);
            List<Cliente> resultado = new List<Cliente>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].NombreCompleto.ToLower().Contains(texto.Trim().ToLower()))
                {
                    resultado.Add(lista[i]);
                }
            }
            return resultado;
        }

        // Sobrecarga 2: equipos por marca. Devuelve TODAS las coincidencias.
        public static List<Equipo> BusquedaSecuencial(List<Equipo> lista, string texto)
        {
            ValidarBusqueda(lista, texto);
            List<Equipo> resultado = new List<Equipo>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Marca.ToLower().Contains(texto.Trim().ToLower()))
                {
                    resultado.Add(lista[i]);
                }
            }
            return resultado;
        }

        // Sobrecarga 3: solicitudes por estado (el texto es el nombre del estado). Devuelve TODAS las coincidencias.
        public static List<Solicitud> BusquedaSecuencial(List<Solicitud> lista, string texto)
        {
            ValidarBusqueda(lista, texto);
            List<Solicitud> resultado = new List<Solicitud>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Estado.ToString().ToLower().Contains(texto.Trim().ToLower()))
                {
                    resultado.Add(lista[i]);
                }
            }
            return resultado;
        }

        // +BusquedaBinaria(Solicitud[], codigo) : int
        // Devuelve la posición de la solicitud o -1 si no existe. Exige datos ordenados por código.
        public static int BusquedaBinaria(Solicitud[] arreglo, int codigo)
        {
            if (arreglo == null || arreglo.Length == 0)
            {
                throw new InvalidOperationException("No hay solicitudes para buscar.");
            }
            if (!EstaOrdenada(arreglo))
            {
                throw new InvalidOperationException("Los datos no están ordenados por código; no se puede hacer la búsqueda binaria.");
            }

            int inicio = 0;
            int fin = arreglo.Length - 1;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;

                if (arreglo[medio].Codigo == codigo)
                {
                    return medio;
                }
                if (arreglo[medio].Codigo < codigo)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }
            return -1;
        }

        // +EstaOrdenada(Solicitud[]) : bool  (ascendente por código)
        public static bool EstaOrdenada(Solicitud[] arreglo)
        {
            if (arreglo == null)
            {
                throw new ArgumentException("El arreglo de solicitudes no puede ser nulo.");
            }

            for (int i = 0; i < arreglo.Length - 1; i++)
            {
                if (arreglo[i].Codigo > arreglo[i + 1].Codigo)
                {
                    return false;
                }
            }
            return true;
        }

        // Auxiliar privado (no está en el diagrama): validaciones comunes de la búsqueda secuencial
        private static void ValidarBusqueda(object lista, string texto)
        {
            if (lista == null)
            {
                throw new ArgumentException("La lista a buscar no puede ser nula.");
            }
            if (texto == null || texto.Trim() == "")
            {
                throw new ArgumentException("Debe ingresar un texto para buscar.");
            }
        }
    }
}