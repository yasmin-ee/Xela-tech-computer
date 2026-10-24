using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class GestorClientes
    {
        private List<Cliente> clientes = new List<Cliente>();
        public List<Cliente> Clientes
        {
            get { return clientes; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La lista de clientes no puede ser nula.");
                }
                clientes = value;
            }
        }

        // Último número usado para generar el código (C001, C002, ...)
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

        // +Registrar(nombre, telefono, correo, tipo)
        // El código se genera solo y es único. Si algún dato es inválido, lanza error y no registra nada.
        public void Registrar(string nombre, string telefono, string correo, TipoCliente tipo)
        {
            string nuevoCodigo = "C" + (contadorCodigo + 1).ToString("000");

            if (BuscarPorCodigo(nuevoCodigo) != null)
            {
                throw new InvalidOperationException("Ya existe un cliente con el código " + nuevoCodigo + ".");
            }

            Cliente nuevo = new Cliente(nuevoCodigo, nombre, telefono, correo, tipo);

            contadorCodigo = contadorCodigo + 1;
            clientes.Add(nuevo);
        }

        // +Actualizar(codigo)
        // Pide por consola el nuevo teléfono y correo; con Enter se deja el dato actual
        public void Actualizar(string codigo)
        {
            Cliente cliente = BuscarPorCodigo(codigo);

            if (cliente == null)
            {
                throw new ArgumentException("No existe un cliente con el código " + codigo + ".");
            }

            Console.WriteLine("Cliente: " + cliente);
            Console.Write("Nuevo teléfono (Enter para dejar el actual): ");
            string telefono = Console.ReadLine();
            Console.Write("Nuevo correo (Enter para dejar el actual): ");
            string correo = Console.ReadLine();

            if (telefono == null || telefono.Trim() == "")
            {
                telefono = cliente.Telefono;
            }
            if (correo == null || correo.Trim() == "")
            {
                correo = cliente.Correo;
            }

            cliente.ActualizarDatos(telefono, correo);
        }

        // +BuscarPorCodigo(string) : Cliente   (devuelve null si no existe)
        public Cliente BuscarPorCodigo(string codigo)
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].Codigo == codigo)
                {
                    return clientes[i];
                }
            }
            return null;
        }

        // +BuscarPorNombreParcial(string) : List<Cliente>   (búsqueda secuencial, todas las coincidencias)
        public List<Cliente> BuscarPorNombreParcial(string texto)
        {
            return Buscador.BusquedaSecuencial(clientes, texto);
        }
    }
}