using System;

namespace XelaTech
{
    public abstract class Equipo
    {
        private string codigoInterno;
        public string CodigoInterno
        {
            get { return codigoInterno; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El código interno no puede quedar vacío.");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("El código interno no puede exceder los 10 caracteres.");
                }
                codigoInterno = value.Trim();
            }
        }

        private string numeroSerie;
        public string NumeroSerie
        {
            get { return numeroSerie; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El número de serie no puede quedar vacío.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("El número de serie no puede exceder los 30 caracteres.");
                }
                numeroSerie = value.Trim();
            }
        }

        private string marca;
        public string Marca
        {
            get { return marca; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("La marca no puede quedar vacía.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("La marca no puede exceder los 30 caracteres.");
                }
                marca = value.Trim();
            }
        }

        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El modelo no puede quedar vacío.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("El modelo no puede exceder los 30 caracteres.");
                }
                modelo = value.Trim();
            }
        }

        private Cliente propietario;
        public Cliente Propietario
        {
            get { return propietario; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("El equipo debe tener un propietario existente.");
                }
                propietario = value;
            }
        }

        private string estadoFisico;
        public string EstadoFisico
        {
            get { return estadoFisico; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El estado físico no puede quedar vacío.");
                }
                if (value.Length > 100)
                {
                    throw new ArgumentException("El estado físico no puede exceder los 100 caracteres.");
                }
                estadoFisico = value.Trim();
            }
        }

        private string accesorios;
        public string Accesorios
        {
            get { return accesorios; }
            set
            {
                // Si no se entregaron accesorios se guarda "Ninguno"
                if (value == null || value.Trim() == "")
                {
                    accesorios = "Ninguno";
                }
                else
                {
                    if (value.Length > 100)
                    {
                        throw new ArgumentException("Los accesorios no pueden exceder los 100 caracteres.");
                    }
                    accesorios = value.Trim();
                }
            }
        }

        private bool tieneSolicitudActiva;
        public bool TieneSolicitudActiva
        {
            get { return tieneSolicitudActiva; }
            set { tieneSolicitudActiva = value; }
        }

        // Constructor (las clases hijas lo llaman con base). El código interno lo asigna GestorEquipos al registrar
        protected Equipo(string numeroSerie, string marca, string modelo,
                         Cliente propietario, string estadoFisico, string accesorios)
        {
            NumeroSerie = numeroSerie;
            Marca = marca;
            Modelo = modelo;
            Propietario = propietario;
            EstadoFisico = estadoFisico;
            Accesorios = accesorios;
            tieneSolicitudActiva = false;
        }

        // Cada tipo de equipo calcula su costo de revisión (polimorfismo)
        public abstract decimal CalcularCostoRevision();

        // Cada tipo de equipo indica qué especialidad de técnico necesita
        public abstract Especialidad ObtenerEspecialidad();

        public string MostrarFicha()
        {
            return "Código: " + CodigoInterno + Environment.NewLine +
                   "Serie: " + NumeroSerie + Environment.NewLine +
                   "Marca / Modelo: " + Marca + " " + Modelo + Environment.NewLine +
                   "Propietario: " + Propietario.NombreCompleto + Environment.NewLine +
                   "Estado físico: " + EstadoFisico + Environment.NewLine +
                   "Accesorios: " + Accesorios + Environment.NewLine +
                   "Especialidad requerida: " + ObtenerEspecialidad() + Environment.NewLine +
                   "Costo de revisión: Q" + CalcularCostoRevision().ToString("F2") + Environment.NewLine +
                   "Solicitud activa: " + (TieneSolicitudActiva ? "Sí" : "No");
        }
    }
}
