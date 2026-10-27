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

        public void Crear(Cliente cliente, Equipo equipo, string descripcion, Prioridad prioridad)
        {
            Solicitud nueva = new Solicitud(cliente, equipo, descripcion, prioridad);

            contadorCodigo = contadorCodigo + 1;
            nueva.Codigo = contadorCodigo;
            solicitudes.Add(nueva);
        }

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

        public void CambiarEstado(int codigo, EstadoSolicitud estado)
        {
            Solicitud solicitud = ObtenerPorCodigo(codigo);

            if (estado == EstadoSolicitud.EnEspera)
            {
                throw new InvalidOperationException("Para pasar a En espera use Encolar().");
            }
            solicitud.CambiarEstado(estado);
        }

        public void DeshacerCambio(int codigo)
        {
            Solicitud solicitud = ObtenerPorCodigo(codigo);

            if (solicitud.Estado == EstadoSolicitud.EnEspera &&
                (colaUrgente.Contains(solicitud) || colaNormal.Contains(solicitud)))
            {
                throw new InvalidOperationException("La solicitud sigue en cola; no se puede deshacer hasta que sea atendida.");
            }
            solicitud.DeshacerEstado();
        }

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
