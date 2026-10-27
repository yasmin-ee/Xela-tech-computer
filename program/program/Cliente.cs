using System;

namespace XelaTech
{
    public enum TipoCliente
    {
        Estudiante,
        Profesional,
        Negocio,
        Institucion
    }

    public class Cliente
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El código no puede quedar vacío.");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("El código no puede exceder los 10 caracteres.");
                }
                codigo = value.Trim();
            }
        }

        private string nombreCompleto;
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El nombre no puede quedar vacío.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("El nombre no puede exceder los 50 caracteres.");
                }
                nombreCompleto = value.Trim();
            }
        }

        private string telefono;
        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El teléfono no puede quedar vacío.");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("El teléfono no puede exceder los 10 caracteres.");
                }
               
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsDigit(value[i]))
                    {
                        throw new ArgumentException("El teléfono solo puede contener números.");
                    }
                }
                telefono = value;
            }
        }

        private string correo;
        public string Correo
        {
            get { return correo; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El correo no puede quedar vacío.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("El correo no puede exceder los 50 caracteres.");
                }
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("El correo debe contener el símbolo @.");
                }
                correo = value.Trim();
            }
        }

        private TipoCliente tipoCliente;
        public TipoCliente TipoCliente
        {
            get { return tipoCliente; }
            set { tipoCliente = value; }
        }

        private int cantidadSolicitudes;
        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad de solicitudes no puede ser negativa.");
                }
                cantidadSolicitudes = value;
            }
        }

        private int solicitudesActivas;
        public int SolicitudesActivas
        {
            get { return solicitudesActivas; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Las solicitudes activas no pueden ser negativas.");
                }
                if (value > cantidadSolicitudes)
                {
                    throw new ArgumentException("Las solicitudes activas no pueden superar el total de solicitudes.");
                }
                solicitudesActivas = value;
            }
        }

        public Cliente()
        {
            cantidadSolicitudes = 0;
            solicitudesActivas = 0;
        }

        public Cliente(string codigo, string nombreCompleto, string telefono, string correo, TipoCliente tipoCliente)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            Correo = correo;
            TipoCliente = tipoCliente;
            cantidadSolicitudes = 0;
            solicitudesActivas = 0;
        }

        public void ActualizarDatos(string telefono, string correo)
        {
            Telefono = telefono;
            Correo = correo;
        }

        public void RegistrarNuevaSolicitud()
        {
            CantidadSolicitudes = CantidadSolicitudes + 1;
            SolicitudesActivas = SolicitudesActivas + 1;
        }

        public void CerrarSolicitud()
        {
            if (SolicitudesActivas == 0)
            {
                throw new InvalidOperationException("El cliente no tiene solicitudes activas para cerrar.");
            }
            SolicitudesActivas = SolicitudesActivas - 1;
        }

        public override string ToString()
        {
            return Codigo + " | " + NombreCompleto + " | Tel: " + Telefono + " | " + Correo +
                   " | " + TipoCliente + " | Solicitudes: " + CantidadSolicitudes +
                   " | Activas: " + SolicitudesActivas;
        }
    }
}
