using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class GestorTecnicos
    {
        private List<Tecnico> tecnicos = new List<Tecnico>();
        public List<Tecnico> Tecnicos
        {
            get { return tecnicos; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de técnicos no puede ser nula.");
                }
                tecnicos = value;
            }
        }

        // Último número usado para generar el código (T001, T002, ...)
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

        // +Registrar(nombre, especialidad)
        public void Registrar(string nombre, Especialidad especialidad)
        {
            string nuevoCodigo = "T" + (contadorCodigo + 1).ToString("000");

            Tecnico nuevo = new Tecnico(nuevoCodigo, nombre, especialidad);

            contadorCodigo = contadorCodigo + 1;
            tecnicos.Add(nuevo);
        }

        // +BuscarDisponible(Especialidad) : Tecnico
        // Elige al técnico disponible, con cupo y con la especialidad requerida que tenga menos trabajos activos.
        // Devuelve null si no hay ninguno.
        public Tecnico BuscarDisponible(Especialidad especialidad)
        {
            Tecnico elegido = null;

            for (int i = 0; i < tecnicos.Count; i++)
            {
                Tecnico t = tecnicos[i];

                if (t.Disponible && t.TieneCupo() && t.PuedeAtender(especialidad))
                {
                    if (elegido == null || t.TrabajosActivos < elegido.TrabajosActivos)
                    {
                        elegido = t;
                    }
                }
            }
            return elegido;
        }

        // +Calificar(codigo, int)
        public void Calificar(string codigo, int calificacion)
        {
            Tecnico tecnico = ObtenerPorCodigo(codigo);
            tecnico.RegistrarCalificacion(calificacion);
        }

        // Auxiliar privado (no está en el diagrama): busca un técnico por código o lanza error
        private Tecnico ObtenerPorCodigo(string codigo)
        {
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (tecnicos[i].Codigo == codigo)
                {
                    return tecnicos[i];
                }
            }
            throw new ArgumentException("No existe un técnico con el código " + codigo + ".");
        }
    }
}