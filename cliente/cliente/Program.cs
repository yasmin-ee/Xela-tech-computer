// Clientes
//RECORDATORIO BORRAR COMNETARIOS ¡¡¡¡¡
namespace XelaTech
{
    public class Cliente
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                //el codigo no puede quedar vacio y no puede exceder los 10 caracteres
                if (value != null && value.Length <= 10)
                {
                    codigo = value;
                }
                else
                {
                    Console.WriteLine("El código no puede quedar vacío y no puede exceder los 10 caracteres.");
                }
            }
        }

        private string nombreCompleto;
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set
            {
                // el nombre no puede quedad vacio y no puede exceder los 50 caracteres(por motivos de seguridad)
                if (value != null && value.Length <= 50)
                {
                    nombreCompleto = value;
                }
                else
                {
                    Console.WriteLine("El nombre no puede quedar vacío y no puede exceder los 50 caracteres.");
                }

            }
        }

        private int telefono;
        public int Telefono
        {
            get { return telefono; }
            set
            {
                //el telefono no puede quedar vacio y no puede exceder los 10 caracteres
                if (value.ToString().Length <= 10)
                {
                    telefono = value;
                }
                else
                {
                    Console.WriteLine("El teléfono no puede quedar vacío y no puede exceder los 10 caracteres.");
                }
            }
        }

        private string correoElectronico;
        public string CorreoElectronico
        {
            get { return correoElectronico; }
            set
            {
                //el correo no puede quedar vacio y no puede exceder los 50 caracteres
                if (value != null && value.Length <= 50)
                {
                    correoElectronico = value;
                }
                else
                {
                    Console.WriteLine("El correo electrónico no puede quedar vacío y no puede exceder los 50 caracteres.");
                }
            }
        }
        private string tipoCliente;
        public string TipoCliente
        {
            get { return tipoCliente; }
            set {                 //el tipo de cliente no puede quedar vacio y no puede exceder los 20 caracteres
                if (value != null && value.Length <= 20)
                {
                    tipoCliente = value;
                }
                else
                {
                    Console.WriteLine("El tipo de cliente no puede quedar vacío y no puede exceder los 20 caracteres.");
                } 
            }
        }
        private int cantidadSolicitudes;
        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set
            {
                if (value != null && value > 0)
                {
                    cantidadSolicitudes = value;
                }
                else 
                {
                    Console.WriteLine("La cantidad de solicitudes debe ser un número positivo");
                }
            }
        }

        private int solicitidesActivas;
        public int SolicitidesActivas
        {
            get { return solicitidesActivas; }
            set
            {
                if (value != null && value > 0)
                {
                    solicitidesActivas = value;
                }
                else
                {
                    Console.WriteLine("La cantidad de solicitudes activas debe ser un número positivo");
                }
            }
        }
        
        //constructores 

        public virtual void ActualizarDatosCliente(string codigo, string nombreCompleto, int telefono, string correoElectronico, string tipoCliente, int cantidadSolicitudes, int solicitidesActivas)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            TipoCliente = tipoCliente;
            CantidadSolicitudes = cantidadSolicitudes;
            SolicitidesActivas = solicitidesActivas;
        }

        public void RegistrarNuevaSolicitud(string codigo, string nombreCompleto, int telefono, string correoElectronico, string tipoCliente, int cantidadSolicitudes, int solicitidesActivas)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            TipoCliente = tipoCliente;
            CantidadSolicitudes = cantidadSolicitudes;
            SolicitidesActivas = solicitidesActivas;
        }

        public void CerrarSolicitud(string codigo, string nombreCompleto, int telefono, string correoElectronico, string tipoCliente, int cantidadSolicitudes, int solicitidesActivas)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            TipoCliente = tipoCliente;
            CantidadSolicitudes = cantidadSolicitudes;
            SolicitidesActivas = solicitidesActivas;
        }

    }
}