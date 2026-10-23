using System;

namespace XelaTech
{
    public class Portatil : Equipo
    {
        private bool incluyeCargador;
        public bool IncluyeCargador
        {
            get { return incluyeCargador; }
            set { incluyeCargador = value; }
        }

        // Constructor: llama al constructor de Equipo con base
        public Portatil(string codigoInterno, string numeroSerie, string marca, string modelo,
                        Cliente propietario, string estadoFisico, string accesorios, bool incluyeCargador)
            : base(codigoInterno, numeroSerie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            IncluyeCargador = incluyeCargador;
        }

        // Costo base Q150. Si no trae cargador, se suman Q25 para usar uno del taller en las pruebas
        public override decimal CalcularCostoRevision()
        {
            decimal costo = 150m;

            if (!IncluyeCargador)
            {
                costo = costo + 25m;
            }
            return costo;
        }

        public override Especialidad ObtenerEspecialidad()
        {
            return Especialidad.Portatiles;
        }
    }
}