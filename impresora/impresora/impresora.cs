using System;

namespace XelaTech
{
    public class Impresora : Equipo
    {
        private string tipoImpresion;
        public string TipoImpresion
        {
            get { return tipoImpresion; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El tipo de impresión no puede quedar vacío.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("El tipo de impresión no puede exceder los 30 caracteres.");
                }
                tipoImpresion = value.Trim();
            }
        }

        // Constructor: llama al constructor de Equipo con base
        public Impresora(string numeroSerie, string marca, string modelo,
                         Cliente propietario, string estadoFisico, string accesorios, string tipoImpresion)
            : base(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            TipoImpresion = tipoImpresion;
        }

        // Láser Q120, Tinta Q80 y cualquier otro tipo Q100
        public override decimal CalcularCostoRevision()
        {
            string tipo = TipoImpresion.ToLower();

            if (tipo == "laser" || tipo == "láser")
            {
                return 120m;
            }
            if (tipo == "tinta")
            {
                return 80m;
            }
            return 100m;
        }

        public override Especialidad ObtenerEspecialidad()
        {
            return Especialidad.Impresoras;
        }
    }
}
