using System;

namespace XelaTech
{
    public class Escritorio : Equipo
    {
        private string tipoTorre;
        public string TipoTorre
        {
            get { return tipoTorre; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El tipo de torre no puede quedar vacío.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("El tipo de torre no puede exceder los 30 caracteres.");
                }
                tipoTorre = value.Trim();
            }
        }

        public Escritorio(string numeroSerie, string marca, string modelo,
                          Cliente propietario, string estadoFisico, string accesorios, string tipoTorre)
            : base(numeroSerie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            TipoTorre = tipoTorre;
        }

        public override decimal CalcularCostoRevision()
        {
            decimal costo = 100m;

            if (TipoTorre.ToLower() == "gamer")
            {
                costo = costo + 50m;
            }
            return costo;
        }

        public override Especialidad ObtenerEspecialidad()
        {
            return Especialidad.Escritorio;
        }
    }
}
