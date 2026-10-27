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

        public Portatil(string numeroSerie, string marca, string modelo,
                        Cliente propietario, string estadoFisico, string accesorios, bool incluyeCargador)
            : base(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            IncluyeCargador = incluyeCargador;
        }

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