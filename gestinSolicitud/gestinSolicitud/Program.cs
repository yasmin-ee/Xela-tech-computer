using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class GestorSolicitudes
    {
        private List<Solicitud> solicitudes = new List<Solicitud>();
        public List<Solicitud> Solicitudes
        {
            get { return solicitudes; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de solicitudes no puede ser nula.");
                }
                solicitudes = value;
            }
        }

        // Cola FIFO de solicitudes urgentes (se atiende primero)
        private Queue<Solicitud> colaUrgente = new Queue<Solicitud>();
        public Queue<Solicitud> ColaUrgente
        {
            get { return colaUrgente; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La cola urgente no puede ser nula.");
                }
                colaUrgente = value;
            }
        }

        // Cola FIFO de solicitudes normales
        private Queue<Solicitud> colaNormal = new Queue<Solicitud>();
        public Queue<Solicitud> ColaNormal
        {
            get { return colaNormal; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La cola normal no puede ser nula.");
                }
                colaNormal = value;
            }
        }

        // Último código asignado (la primera solicitud recibe el 1)
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

        // +Crear(cliente, equipo, descripcion, prioridad)
        // Crea la solicitud con código único y la guarda en la lista (todavía no está en cola)
        public void Crear(Cliente cliente, Equipo equipo, string descripcion, Prioridad prioridad)
        {
            Solicitud nueva = new Solicitud(cliente, equipo, descripcion, prioridad);

            contadorCodigo = contadorCodigo + 1;
            nueva.Codigo = contadorCodigo;
            solicitudes.Add(nueva);
        }

        // +Encolar(Solicitud): la coloca en la cola de su prioridad y pasa a EnEspera
        public void Encolar(Solicitud solicitud)
        {
            if (solicitud == null)
            {
                throw new ArgumentException("La solicitud no puede ser nula.");
            }
            if (solicitud.Estado != EstadoSolicitud.Recibida)
            {
                throw new InvalidOperationException("Solo se pueden encolar solicitudes en estado Recibida.");
            }

            if (solicitud.Prioridad == Prioridad.Urgente)
            {
                colaUrgente.Enqueue(solicitud);
            }
            else
            {
                colaNormal.Enqueue(solicitud);
            }
            solicitud.CambiarEstado(EstadoSolicitud.EnEspera);
        }

        // +AtenderSiguiente() : Solicitud
        // Primero la cola urgente y luego la normal, respetando el orden de llegada (FIFO)
        public Solicitud AtenderSiguiente()
        {
            if (colaUrgente.Count > 0)
            {
                return colaUrgente.Dequeue();
            }
            if (colaNormal.Count > 0)
            {
                return colaNormal.Dequeue();
            }
            throw new InvalidOperationException("No hay solicitudes en espera en ninguna cola.");
        }

        // +CambiarEstado(codigo, estado)
        public void CambiarEstado(int codigo, EstadoSolicitud estado)
        {
            Solicitud solicitud = ObtenerPorCodigo(codigo);

            if (estado == EstadoSolicitud.EnEspera)
            {
                throw new InvalidOperationException("Para pasar a En espera use Encolar().");
            }
            solicitud.CambiarEstado(estado);
        }

        // +DeshacerCambio(codigo)
        public void DeshacerCambio(int codigo)
        {
            Solicitud solicitud = ObtenerPorCodigo(codigo);

            // Si todavía está formada en una cola, no se puede regresar a Recibida
            if (solicitud.Estado == EstadoSolicitud.EnEspera &&
                (colaUrgente.Contains(solicitud) || colaNormal.Contains(solicitud)))
            {
                throw new InvalidOperationException("La solicitud sigue en cola; no se puede deshacer hasta que sea atendida.");
            }
            solicitud.DeshacerEstado();
        }

        // +BuscarPorEstado(estado) : List<Solicitud>  (búsqueda secuencial, todas las coincidencias)
        public List<Solicitud> BuscarPorEstado(EstadoSolicitud estado)
        {
            List<Solicitud> resultado = new List<Solicitud>();

            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes[i].Estado == estado)
                {
                    resultado.Add(solicitudes[i]);
                }
            }
            return resultado;
        }

        // Auxiliar privado (no está en el diagrama): busca una solicitud por código o lanza error
        private Solicitud ObtenerPorCodigo(int codigo)
        {
            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes[i].Codigo == codigo)
                {
                    return solicitudes[i];
                }
            }
            throw new ArgumentException("No existe una solicitud con el código " + codigo + ".");
        }
    }
}
