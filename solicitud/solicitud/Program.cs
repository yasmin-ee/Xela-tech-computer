using System;
using System.Collections.Generic;
//solicitud 

namespace XelaTech
{
    public class Solicitud
    {
        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El código de la solicitud debe ser mayor que 0.");
                }
                codigo = value;
            }
        }

        private string descripcionProblema;
        public string DescripcionProblema
        {
            get { return descripcionProblema; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("La descripción del problema no puede quedar vacía.");
                }
                if (value.Length > 200)
                {
                    throw new ArgumentException("La descripción del problema no puede exceder los 200 caracteres.");
                }
                descripcionProblema = value.Trim();
            }
        }

        private DateTime fechaRecepcion;
        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de recepción no puede ser futura.");
                }
                fechaRecepcion = value;
            }
        }

        private Prioridad prioridad;
        public Prioridad Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        // Solo tiene get: el estado únicamente cambia con CambiarEstado, DeshacerEstado y Cancelar
        private EstadoSolicitud estado;
        public EstadoSolicitud Estado
        {
            get { return estado; }
        }

        private string diagnostico;
        public string Diagnostico
        {
            get { return diagnostico; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El diagnóstico no puede quedar vacío.");
                }
                if (value.Length > 200)
                {
                    throw new ArgumentException("El diagnóstico no puede exceder los 200 caracteres.");
                }
                diagnostico = value.Trim();
            }
        }

        // Tiempo estimado de atención, en horas
        private int tiempoEstimado;
        public int TiempoEstimado
        {
            get { return tiempoEstimado; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El tiempo estimado no puede ser negativo.");
                }
                tiempoEstimado = value;
            }
        }

        private decimal costoManoObra;
        public decimal CostoManoObra
        {
            get { return costoManoObra; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El costo de mano de obra no puede ser negativo.");
                }
                costoManoObra = value;
            }
        }

        private decimal costoRepuestos;
        public decimal CostoRepuestos
        {
            get { return costoRepuestos; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El costo de repuestos no puede ser negativo.");
                }
                costoRepuestos = value;
            }
        }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El total no puede ser negativo.");
                }
                total = value;
            }
        }

        // Pila con los estados anteriores (el último en entrar es el primero en salir)
        private Stack<EstadoSolicitud> historialEstados;
        public Stack<EstadoSolicitud> HistorialEstados
        {
            get { return historialEstados; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("El historial de estados no puede ser nulo.");
                }
                historialEstados = value;
            }
        }

        private List<DetalleRepuesto> repuestosUsados;
        public List<DetalleRepuesto> RepuestosUsados
        {
            get { return repuestosUsados; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de repuestos no puede ser nula.");
                }
                repuestosUsados = value;
            }
        }

        // Relaciones del diagrama (las flechas hacia Cliente, Equipo y Tecnico)
        private Cliente cliente;
        public Cliente Cliente
        {
            get { return cliente; }
        }

        private Equipo equipo;
        public Equipo Equipo
        {
            get { return equipo; }
        }

        private Tecnico tecnico;
        public Tecnico Tecnico
        {
            get { return tecnico; }
        }

        // Constructores

        // +Solicitud(cliente, equipo, descripcion): prioridad Normal por defecto
        public Solicitud(Cliente cliente, Equipo equipo, string descripcion)
            : this(cliente, equipo, descripcion, Prioridad.Normal)
        {
        }

        // +Solicitud(cliente, equipo, descripcion, prioridad)
        public Solicitud(Cliente cliente, Equipo equipo, string descripcion, Prioridad prioridad)
        {
            // Primero se valida todo, después se modifican los objetos relacionados
            if (cliente == null)
            {
                throw new ArgumentException("No se puede crear una solicitud para un cliente inexistente.");
            }
            if (equipo == null)
            {
                throw new ArgumentException("No se puede crear una solicitud para un equipo inexistente.");
            }
            if (equipo.TieneSolicitudActiva)
            {
                throw new InvalidOperationException("El equipo ya tiene una solicitud activa.");
            }
            if (equipo.Propietario.Codigo != cliente.Codigo)
            {
                throw new ArgumentException("El equipo no pertenece al cliente indicado.");
            }
            DescripcionProblema = descripcion;

            this.cliente = cliente;
            this.equipo = equipo;
            this.prioridad = prioridad;
            this.tecnico = null;
            fechaRecepcion = DateTime.Now;
            estado = EstadoSolicitud.Recibida;
            diagnostico = "";
            tiempoEstimado = 0;
            costoManoObra = 0m;
            costoRepuestos = 0m;
            total = 0m;
            historialEstados = new Stack<EstadoSolicitud>();
            repuestosUsados = new List<DetalleRepuesto>();

            // Efectos sobre los objetos relacionados
            equipo.TieneSolicitudActiva = true;
            cliente.RegistrarNuevaSolicitud();
        }

        // +CambiarEstado(EstadoSolicitud): solo permite avanzar al siguiente estado del proceso
        public void CambiarEstado(EstadoSolicitud nuevoEstado)
        {
            if (estado == EstadoSolicitud.Entregada || estado == EstadoSolicitud.Cancelada)
            {
                throw new InvalidOperationException("No se puede modificar una solicitud que ya fue entregada o cancelada.");
            }
            if (nuevoEstado == EstadoSolicitud.Cancelada)
            {
                throw new InvalidOperationException("Para cancelar la solicitud use el método Cancelar().");
            }
            if ((int)nuevoEstado != (int)estado + 1)
            {
                throw new InvalidOperationException("Cambio de estado inválido: de " + estado + " no se puede pasar a " + nuevoEstado + ".");
            }
            if (nuevoEstado == EstadoSolicitud.Asignada && tecnico == null)
            {
                throw new InvalidOperationException("Debe asignar un técnico con AsignarTecnico() antes de pasar a Asignada.");
            }
            if (nuevoEstado == EstadoSolicitud.EnReparacion && diagnostico == "")
            {
                throw new InvalidOperationException("Debe registrar el diagnóstico antes de pasar a reparación.");
            }

            historialEstados.Push(estado);
            estado = nuevoEstado;

            if (estado == EstadoSolicitud.Entregada)
            {
                CalcularTotal();
                tecnico.FinalizarTrabajo();
                equipo.TieneSolicitudActiva = false;
                cliente.CerrarSolicitud();
            }
        }

        // +DeshacerEstado(): regresa al estado anterior usando la pila
        public void DeshacerEstado()
        {
            if (estado == EstadoSolicitud.Entregada || estado == EstadoSolicitud.Cancelada)
            {
                throw new InvalidOperationException("No se puede deshacer una solicitud entregada o cancelada.");
            }
            // Se valida que la pila tenga información antes de desapilar
            if (historialEstados.Count == 0)
            {
                throw new InvalidOperationException("El historial está vacío, no hay cambios que deshacer.");
            }

            // Si se deshace la asignación, el técnico queda libre de este trabajo
            if (estado == EstadoSolicitud.Asignada)
            {
                tecnico.TrabajosActivos = tecnico.TrabajosActivos - 1;
                tecnico = null;
            }
            estado = historialEstados.Pop();
        }

        // +AsignarTecnico(Tecnico)
        public void AsignarTecnico(Tecnico nuevoTecnico)
        {
            if (nuevoTecnico == null)
            {
                throw new ArgumentException("El técnico no puede ser nulo.");
            }
            if (estado != EstadoSolicitud.EnEspera)
            {
                throw new InvalidOperationException("La solicitud debe estar en espera para asignarle un técnico.");
            }
            if (!nuevoTecnico.PuedeAtender(equipo.ObtenerEspecialidad()))
            {
                throw new InvalidOperationException("El técnico no tiene la especialidad requerida.");
            }

            nuevoTecnico.AsignarTrabajo();   // lanza error si no está disponible o llegó al límite
            tecnico = nuevoTecnico;
            CambiarEstado(EstadoSolicitud.Asignada);
        }

        // +AgregarRepuesto(Repuesto, int)
        public void AgregarRepuesto(Repuesto repuesto, int cantidad)
        {
            if (repuesto == null)
            {
                throw new ArgumentException("El repuesto no puede ser nulo.");
            }
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que 0.");
            }
            if (estado != EstadoSolicitud.EnDiagnostico && estado != EstadoSolicitud.EnReparacion)
            {
                throw new InvalidOperationException("Solo se pueden agregar repuestos durante el diagnóstico o la reparación.");
            }
            if (cantidad > repuesto.Existencia)
            {
                throw new ArgumentException("No hay existencia suficiente del repuesto.");
            }

            repuesto.Existencia = repuesto.Existencia - cantidad;
            DetalleRepuesto detalle = new DetalleRepuesto(repuesto, cantidad);
            repuestosUsados.Add(detalle);
            costoRepuestos = costoRepuestos + detalle.Subtotal;
        }

        // +CalcularTotal() : decimal = mano de obra + repuestos + costo de revisión del equipo
        public decimal CalcularTotal()
        {
            decimal sumaRepuestos = 0m;

            for (int i = 0; i < repuestosUsados.Count; i++)
            {
                sumaRepuestos = sumaRepuestos + repuestosUsados[i].Subtotal;
            }

            costoRepuestos = sumaRepuestos;
            total = costoManoObra + costoRepuestos + equipo.CalcularCostoRevision();
            return total;
        }

        // +Cancelar()
        public void Cancelar()
        {
            if (estado == EstadoSolicitud.Entregada)
            {
                throw new InvalidOperationException("No se puede cancelar una solicitud que ya fue entregada.");
            }
            if (estado == EstadoSolicitud.Cancelada)
            {
                throw new InvalidOperationException("La solicitud ya está cancelada.");
            }

            // Se libera al técnico (sin contarlo como servicio finalizado), al equipo y al cliente
            if (tecnico != null)
            {
                tecnico.TrabajosActivos = tecnico.TrabajosActivos - 1;
            }
            equipo.TieneSolicitudActiva = false;
            cliente.CerrarSolicitud();

            historialEstados.Push(estado);
            estado = EstadoSolicitud.Cancelada;
        }

        public override string ToString()
        {
            string nombreTecnico = "Sin asignar";
            if (tecnico != null)
            {
                nombreTecnico = tecnico.NombreCompleto;
            }

            return "Solicitud " + Codigo + " | " + Cliente.NombreCompleto + " | Equipo: " + Equipo.CodigoInterno +
                   " | " + Prioridad + " | " + Estado + " | Técnico: " + nombreTecnico +
                   " | Recibida: " + FechaRecepcion.ToString("dd/MM/yyyy HH:mm") +
                   " | Total: Q" + Total.ToString("F2");
        }
    }
}
